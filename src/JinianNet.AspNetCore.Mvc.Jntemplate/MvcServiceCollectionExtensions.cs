using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System; 
using Microsoft.Extensions.DependencyInjection;

namespace JinianNet.AspNetCore.Mvc.Jntemplate
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
        /// <returns>The <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection AddJntemplateViewEngine(this IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.AddOptions()
                .AddTransient<IConfigureOptions<JntemplateViewEngineOptions>, JntemplateViewEngineOptionsSetup>()
                .AddTransient<IConfigureOptions<MvcViewOptions>, JntemplateMvcViewOptionsSetup>()
                .AddSingleton<IJntemplateViewEngine, JntemplateViewEngine>();

            return services;
        }

        /// <summary>
        /// Registers Jntemplate view engine services.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/>.</param>
        /// <param name="setupAction">A setup action that configures the <see cref="JntemplateViewEngineOptions"/>.</param>
        /// <returns>The <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection AddJntemplateViewEngine(
            this IServiceCollection services,
            Action<JntemplateViewEngineOptions> setupAction)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (setupAction == null)
            {
                throw new ArgumentNullException(nameof(setupAction));
            }

            services.AddOptions()
                .AddTransient<IConfigureOptions<JntemplateViewEngineOptions>, JntemplateViewEngineOptionsSetup>()
                .Configure(setupAction)
                .AddTransient<IConfigureOptions<MvcViewOptions>, JntemplateMvcViewOptionsSetup>()
                .AddSingleton<IJntemplateViewEngine, JntemplateViewEngine>();

            return services;
        }
    }
}
