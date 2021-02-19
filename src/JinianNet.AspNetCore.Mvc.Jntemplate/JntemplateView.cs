using JinianNet.JNTemplate;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using System; 
using System.Threading.Tasks;

namespace JinianNet.AspNetCore.Mvc.Jntemplate
{
    /// <summary>
    /// Default implementation for <see cref="IView"/> 
    /// </summary>
    public class JntemplateView : IView
    {
        /// <summary>
        /// Initializes a new instance of <see cref="JntemplateView"/>
        /// </summary>
        /// <param name="path">The view path.</param> 

        public JntemplateView(string path)
        {
            this.Path = path;
        }

        /// <inheritdoc />
        public string Path { get; set; }


        /// <inheritdoc />
        public Task RenderAsync(ViewContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }
            var viewPath = this.Path.TrimStart('/');
            var data = context.ViewData;
            var writer = context.Writer;

            return Task.Run(() =>
            {
                var t = Engine.LoadTemplate(viewPath);
                foreach (var kv in data)
                {
                    if (kv.Value != null)
                    {
                        t.Context.TempData.Set(kv.Key, kv.Value, kv.Value.GetType());
                    }
                }
                t.Render(writer);
            });
        } 
    }
}