using JinianNet.JNTemplate;
using JinianNet.JNTemplate.CodeCompilation;
using JinianNet.JNTemplate.Dynamic;
using JinianNet.JNTemplate.Hosting;
using JinianNet.JNTemplate.Parsers;
using JinianNet.JNTemplate.Resources;
using JinianNet.JNTemplate.Runtime;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace JinianNet.AspNetCoreViewEngine.Jntemplate
{
    /// <summary>
    /// Extensions methods for configuring MVC via an <see cref="IServiceCollection"/>.
    /// </summary>
    public static class MvcServiceCollectionExtensions
    {
        /// <summary>
        /// Registers Jntemplate view engine services.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/>.</param>
        /// <param name="setupAction">A setup action that configures the <see cref="JntemplateViewEngineOptions"/>.</param>
        /// <returns>The <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection AddJntemplateViewEngine(
            this IServiceCollection services,
            Action<JntemplateViewEngineOptions> setupAction = null)
        {
#if FRAMEWORK || NETSTANDARD
            if (services == null)
                throw new ArgumentNullException(nameof(services));
#else
            ArgumentNullException.ThrowIfNull(services); 
#endif
            services.AddSingleton<IEngine>(Engine.Current);

            //services.AddSingleton<IHostEnvironment,DefaultHostEnvironment>();
            //services.AddSingleton<IOptions, RuntimeOptions>();
            //services.AddSingleton<IOptions, RuntimeOptions>();

            services.AddOptions()
                .AddTransient<IConfigureOptions<JntemplateViewEngineOptions>, JntemplateViewEngineOptionsSetup>();

            if (setupAction != null)
                services.Configure(setupAction);

            services.AddTransient<IConfigureOptions<MvcViewOptions>, JntemplateMvcViewOptionsSetup>()
                .AddSingleton<IJntemplateViewEngine, JntemplateViewEngine>();

            return services;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="setupAction"></param>
        /// <returns></returns>
        public static IMvcBuilder AddJntemplateViewEngine(
            this IMvcBuilder builder,
            Action<JntemplateViewEngineOptions> setupAction = null)
        {
            AddJntemplateViewEngine(builder.Services, setupAction);
            return builder;
        }
    }
}
