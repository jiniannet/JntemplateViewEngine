using JinianNet.AspNetCore.Mvc.Jntemplate.Configuration;
using JinianNet.JNTemplate;
using JinianNet.JNTemplate.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using System;

namespace JinianNet.AspNetCore.Mvc.Jntemplate
{
    public static class MvcApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseJntemplate(this IApplicationBuilder app, IHostingEnvironment env)
        {
            //Engine.Configure(EngineConfig.CreateDefault());
            Runtime.AppendResourcePath(env.ContentRootPath);
            return app;
        }

        public static IApplicationBuilder UseJntemplate(this IApplicationBuilder app, IHostingEnvironment env, Action<JntConfig> configureEngine)
        {
            var conf = new JntConfig();
            configureEngine?.Invoke(conf);
            if (conf.Data != null)
            {
                Engine.Configure(conf, conf.Data);
            }
            else
            {
                Engine.Configure(conf);
            }
            Runtime.AppendResourcePath(env.ContentRootPath);
            return app;
        }
    }
}
