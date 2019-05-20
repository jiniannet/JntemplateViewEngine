using JinianNet.AspNetCore.Mvc.Jntemplate.Configuration;
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

        public static IApplicationBuilder UseJntemplate(this IApplicationBuilder app, Action<JntConfig> configureEngine)
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
            return app;
        }
    }
}
