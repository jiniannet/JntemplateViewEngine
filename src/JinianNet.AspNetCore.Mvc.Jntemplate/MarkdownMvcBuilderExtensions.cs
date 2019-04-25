using JinianNet.AspNetCore.Mvc.JNTemplate;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class MarkdownMvcBuilderExtensions
    {
        public static IMvcBuilder UseJntemplate(this IMvcBuilder builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            builder.Services.AddOptions();
            builder.Services.AddTransient<IConfigureOptions<JntemplateViewEngineOptions>, JntemplateViewEngineOptionsSetup>();
            builder.Services.AddTransient<IConfigureOptions<MvcViewOptions>, JntemplateMvcViewOptionsSetup>();
            builder.Services.AddSingleton<IJntemplateViewEngine, JntemplateViewEngine>();

            return builder;
        }

        public static IMvcBuilder UseJntemplate(
            this IMvcBuilder builder,
            Action<JntemplateViewEngineOptions> setupAction)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (setupAction == null)
            {
                throw new ArgumentNullException(nameof(setupAction));
            }

            builder.Services.AddOptions();
            builder.Services.AddTransient<IConfigureOptions<JntemplateViewEngineOptions>, JntemplateViewEngineOptionsSetup>();
            builder.Services.Configure(setupAction);
            builder.Services.AddTransient<IConfigureOptions<MvcViewOptions>, JntemplateMvcViewOptionsSetup>();
            builder.Services.AddSingleton<IJntemplateViewEngine, JntemplateViewEngine>();

            return builder;
        }
    }
}
