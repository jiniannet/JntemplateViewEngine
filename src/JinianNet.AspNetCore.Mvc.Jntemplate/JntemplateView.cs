using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using System; 
using System.IO;
using System.Linq; 
using System.Threading.Tasks; 

namespace JinianNet.AspNetCore.Mvc.Jntemplate
{
    public class JntemplateView : IView
    {
        private readonly IJntemplateViewEngine _viewEngine;
        private readonly IJntemplatePage _viewStartPage;

        public JntemplateView(
            IJntemplateViewEngine viewEngine,
            IJntemplatePage viewStartPage,
            IJntemplatePage jntPage)
        {
            _viewEngine = viewEngine ??
                throw new ArgumentNullException(nameof(viewEngine));
            _viewStartPage = viewStartPage;
            Page = jntPage ??
                throw new ArgumentNullException(nameof(jntPage));
        }

        public string Path => Page.Path;

        public IJntemplatePage Page { get; }

        public async Task RenderAsync(ViewContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var bodyWriter = await RenderPageAsync(Page, context, invokeViewStart: true);
            await RenderLayoutAsync(context, bodyWriter);
        }

        private async Task<TextWriter> RenderPageAsync(
            IJntemplatePage page,
            ViewContext context,
            bool invokeViewStart)
        {
            if (invokeViewStart)
            {
                await RenderViewStartsAsync(context);
            }

            await RenderPageCoreAsync(page, context);

            return page.ViewContext.Writer;
        }

        private Task RenderPageCoreAsync(IJntemplatePage page, ViewContext context)
        {
            page.ViewContext = context;
            return page.ExecuteAsync();
        }

        private async Task RenderViewStartsAsync(ViewContext context)
        {
            if (_viewStartPage != null)
            {
                string layout = null;
                await RenderPageCoreAsync(_viewStartPage, context);
                layout = _viewEngine.GetAbsolutePath(_viewStartPage.Path, _viewStartPage.Layout);

                if (layout != null)
                {
                    Page.Layout = layout;
                }
            }
        }

        private async Task RenderLayoutAsync(ViewContext context, TextWriter writer)
        {
            var pageContent = Page.BodyContent.ToString();
            if (_viewStartPage != null)
            {
                Page.Layout = Page.Layout ?? _viewStartPage.Layout;
            }
            if (Page.Layout != null)
            {
                //layou 支持像RZ一样，使用 $RenderBody() 来呈现模板
                context.ViewData["RenderBody"] = new JinianNet.JNTemplate.FuncHandler(m =>
                {
                    return pageContent;
                });

                context.ViewData["Title"] = Page.Title;

                var layoutPage = GetLayoutPage(context, Page.Path, Page.Layout);
                writer = await RenderPageAsync(layoutPage, context, invokeViewStart: true);

                var layoutContent = layoutPage.BodyContent.ToString();

                await writer.WriteAsync(layoutContent);
            }
            else
            {
                await writer.WriteAsync(pageContent);
            }
        }

        private IJntemplatePage GetLayoutPage(ViewContext context, string executingFilePath, string layoutPath)
        {
            var layoutPageResult = _viewEngine.GetView(executingFilePath, layoutPath, isMainPage: true);
            var originalLocations = layoutPageResult.SearchedLocations;
            if (layoutPageResult.View == null)
            {
                layoutPageResult = _viewEngine.FindView(context, layoutPath, isMainPage: true);
            }

            if (layoutPageResult.View == null)
            {
                var locations = string.Empty;
                if (originalLocations.Any())
                {
                    locations = Environment.NewLine + string.Join(Environment.NewLine, originalLocations);
                }

                if (layoutPageResult.SearchedLocations.Any())
                {
                    locations +=
                        Environment.NewLine + string.Join(Environment.NewLine, layoutPageResult.SearchedLocations);
                }

                throw new InvalidOperationException($"The layout view '{layoutPath}' could not be located. The following locations were searched:{locations}");
            }

            var layoutPage = ((JntemplateView)layoutPageResult.View).Page;
            return layoutPage;
        }
    }
}