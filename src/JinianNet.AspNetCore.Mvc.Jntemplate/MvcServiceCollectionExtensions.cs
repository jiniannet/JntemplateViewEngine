using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System; 
using Microsoft.Extensions.DependencyInjection;

namespace JinianNet.AspNetCore.Mvc.Jntemplate
{
    public static class MvcServiceCollectionExtensions
    {
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
