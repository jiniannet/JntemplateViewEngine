using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using Microsoft.Extensions.DependencyInjection;

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
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }
            services.AddOptions()
                .AddTransient<IConfigureOptions<JntemplateViewEngineOptions>, JntemplateViewEngineOptionsSetup>();

            if (setupAction != null)
            {
                services.Configure(setupAction);
            }

            services.AddTransient<IConfigureOptions<MvcViewOptions>, JntemplateMvcViewOptionsSetup>()
                .AddSingleton<IJntemplateViewEngine, JntemplateViewEngine>();

            return services;
        }
    }
}
