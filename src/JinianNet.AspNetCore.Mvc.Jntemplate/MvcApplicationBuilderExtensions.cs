using JinianNet.JNTemplate;
using JinianNet.JNTemplate.Configuration;
using Microsoft.AspNetCore.Builder;
using System;

namespace JinianNet.AspNetCore.Mvc.Jntemplate
{
    public static class MvcApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseJntemplate(this IApplicationBuilder app)
        {
            Engine.Configure(EngineConfig.CreateDefault());

            return app;
        }

        public static IApplicationBuilder UseJntemplate(this IApplicationBuilder app, Action<ConfigBase> configureEngine)
        {
            var conf = EngineConfig.CreateDefault();
            if (configureEngine != null)
            {
                configureEngine(conf);
            }
            Engine.Configure(conf);
            return app;
        }
    }
}
