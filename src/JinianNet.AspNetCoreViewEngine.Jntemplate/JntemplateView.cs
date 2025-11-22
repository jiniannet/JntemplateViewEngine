using JinianNet.JNTemplate;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using System;
using System.Threading.Tasks;

namespace JinianNet.AspNetCoreViewEngine.Jntemplate
{
    /// <summary>
    /// Default implementation for <see cref="IView"/> 
    /// </summary>
    public class JntemplateView : IView
    {
        private IEngine _engine;
        /// <summary>
        /// Initializes a new instance of <see cref="JntemplateView"/>
        /// </summary>
        /// <param name="path">The view path.</param> 
        /// <param name="engine"></param>

        public JntemplateView(IEngine engine, string path)
        {
            this.Path = path;
            _engine = engine;
        }

        /// <inheritdoc />
        public string Path { get; set; }

        /// <inheritdoc />
        public Task RenderAsync(ViewContext context)
        {
            if (context == null)
            {
                Task.FromException(new ArgumentNullException(nameof(context)));
            }
            var data = context.ViewData;
            var writer = context.Writer;
            var t = _engine.LoadTemplate(Path);
            foreach (var kv in data)
            {
                if (kv.Value != null)
                {
                    if (kv.Value is VariableElement var)
                    {
                        t.Context.TempData.Set(kv.Key, var.Value, var.Type);
                    }
                    else
                    {
                        t.Context.TempData.Set(kv.Key, kv.Value, kv.Value.GetType());
                    }
                }
            }
            t.Render(writer);
            return Task.CompletedTask; 
        }
    }
}