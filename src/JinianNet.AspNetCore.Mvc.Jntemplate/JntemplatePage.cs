using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.FileProviders;
using System;
using System.IO;
using System.Threading.Tasks;
using JinianNet.JNTemplate;
using System.Dynamic;

namespace JinianNet.AspNetCore.Mvc.Jntemplate
{
    public class JntemplatePage : IJntemplatePage
    {
        private readonly IFileProvider _contentRootFileProvider;

        public JntemplatePage(IFileProvider contentRootFileProvider)
        {
            _contentRootFileProvider = contentRootFileProvider;
            this.Model = new ExpandoObject();
        }

        public IHtmlContent BodyContent { get; set; }

        public string Layout { get; set; }

        public dynamic Model { get; set; }

        public string Path { get; set; }

        public string Title { get; set; }

        public ViewContext ViewContext { get; set; }

        public async Task ExecuteAsync()
        {
            var fileInfo = _contentRootFileProvider.GetFileInfo(Path);

            var html = await ExecuteAsync(fileInfo);

            BodyContent = new HtmlString(html);
        }
        private async Task<String> ExecuteAsync(IFileInfo fileInfo)
        {
            string templateText;
            using (var readStream = fileInfo.CreateReadStream())
            {
                using (var reader = new StreamReader(readStream))
                {
                    templateText = await reader.ReadToEndAsync();
                }
            }
            return await ExecuteAsync(templateText, System.IO.Path.GetDirectoryName(fileInfo.PhysicalPath));
        }
        private async Task<String> ExecuteAsync(string text, string path)
        {

            return await Task.Run<string>(() =>
            {
                var t = (Template)Engine.CreateTemplate(text);
                t.Context.CurrentPath = path;
                t.Set("Model", this.Model);
                foreach (var kv in ViewContext.ViewData)
                {
                    t.Set(kv.Key, kv.Value);
                }
                //foreach (var kv in ViewContext.TempData)
                //{
                //    t.Set(kv.Key, kv.Value);
                //}
                t.Set("ViewBag", ViewContext.ViewBag);
                return t.Render();
            });
        }
    }
}
