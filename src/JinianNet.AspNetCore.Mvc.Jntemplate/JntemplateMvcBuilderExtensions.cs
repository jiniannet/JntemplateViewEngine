//using JinianNet.AspNetCore.Mvc.Jntemplate;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.DependencyInjection;
//using Microsoft.Extensions.Options;
//using System;

//namespace JinianNet.AspNetCore.Mvc
//{
//    public static class JntemplateMvcBuilderExtensions
//    {
//        public static IMvcBuilder AddJntemplateViewEngine(this IMvcBuilder builder)
//        {
//            if (builder == null)
//            {
//                throw new ArgumentNullException(nameof(builder));
//            }

//            builder.Services.AddOptions()
//                .AddTransient<IConfigureOptions<JntemplateViewEngineOptions>, JntemplateViewEngineOptionsSetup>()
//                .AddTransient<IConfigureOptions<MvcViewOptions>, JntemplateMvcViewOptionsSetup>()
//                .AddSingleton<IJntemplateViewEngine, JntemplateViewEngine>();

//            return builder;
//        }

//        public static IMvcBuilder AddJntemplateViewEngine(
//            this IMvcBuilder builder,
//            Action<JntemplateViewEngineOptions> setupAction)
//        {
//            if (builder == null)
//            {
//                throw new ArgumentNullException(nameof(builder));
//            }

//            if (setupAction == null)
//            {
//                throw new ArgumentNullException(nameof(setupAction));
//            }

//            builder.Services.AddOptions()
//                .AddTransient<IConfigureOptions<JntemplateViewEngineOptions>, JntemplateViewEngineOptionsSetup>()
//                .Configure(setupAction)
//                .AddTransient<IConfigureOptions<MvcViewOptions>, JntemplateMvcViewOptionsSetup>()
//                .AddSingleton<IJntemplateViewEngine, JntemplateViewEngine>();

//            return builder;
//        }
//    }
//}
