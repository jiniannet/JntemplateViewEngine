using JinianNet.AspNetCoreViewEngine.Jntemplate.Configuration;
using JinianNet.JNTemplate;
using JinianNet.JNTemplate.Configuration;
using JinianNet.JNTemplate.Runtime;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace JinianNet.AspNetCoreViewEngine.Jntemplate
{
    /// <summary>
    /// 
    /// </summary>
    public static class MvcApplicationBuilderExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="app">The <see cref="IApplicationBuilder"/>.</param> 
        /// <param name="configureEngine">A setup action that configures the <see cref="JntemplateConfig"/>.</param>
        /// <returns></returns>
        public static IApplicationBuilder UseJntemplate(this IApplicationBuilder app, Action<JntemplateConfig> configureEngine = null)
        {
            if (configureEngine == null)
                return app;
            var conf = new JntemplateConfig();
            conf.GlobalData = new Dictionary<string, object>();
            configureEngine?.Invoke(conf);
            Engine.Configure(conf);
            if (string.IsNullOrWhiteSpace(conf.ContentRootPath))
                conf.ContentRootPath = System.IO.Directory.GetCurrentDirectory();
            Engine.Current.AppendResourcePath(conf.ContentRootPath);
            return app;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="app">The <see cref="IApplicationBuilder"/>.</param> 
        /// <param name="configureEngine">A setup action that configures the <see cref="JntemplateConfig"/>.</param>
        /// <returns></returns>
        public static IApplicationBuilder UseJntemplate(this IApplicationBuilder app, Action<IOptions, IEngine> configureEngine)
        {
            if (configureEngine == null)
                return app;
            var engine = app.ApplicationServices.GetService<IEngine>();
            engine.Configure(o => configureEngine(o, engine));
            return app;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="app">The <see cref="IApplicationBuilder"/>.</param>  
        /// <returns></returns>
        public static IApplicationBuilder UseCompileJntemplate(this IApplicationBuilder app)
        {
            var engine = app.ApplicationServices.GetService<IEngine>();
            engine.UseCompileEngine();
            return app;
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="app">The <see cref="IApplicationBuilder"/>.</param>  
        /// <param name="configureEngine">The <see cref="IApplicationBuilder"/>.</param>  
        /// <returns></returns>
        public static IApplicationBuilder UseCompileJntemplate(this IApplicationBuilder app, Action<IOptions> configureEngine)
        {
            var engine = app.ApplicationServices.GetService<IEngine>();
            engine.UseCompileEngine();
            engine.Configure(configureEngine);
            return app;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="app">The <see cref="IApplicationBuilder"/>.</param>  
        /// <returns></returns>
        public static IApplicationBuilder UseInterpretationJntemplate(this IApplicationBuilder app)
        {
            var engine = app.ApplicationServices.GetService<IEngine>();
            engine.UseInterpretationEngine();
            return app;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="app">The <see cref="IApplicationBuilder"/>.</param>  
        /// <param name="configureEngine">The <see cref="IApplicationBuilder"/>.</param>  
        /// <returns></returns>
        public static IApplicationBuilder UseInterpretationJntemplate(this IApplicationBuilder app, Action<IOptions> configureEngine)
        {
            var engine = app.ApplicationServices.GetService<IEngine>();
            engine.UseInterpretationEngine();
            engine.Configure(configureEngine);
            return app;
        }
    }
}
