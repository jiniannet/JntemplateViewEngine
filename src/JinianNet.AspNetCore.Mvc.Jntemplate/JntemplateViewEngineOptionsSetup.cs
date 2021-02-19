using Microsoft.Extensions.Options;
using System;

namespace JinianNet.AspNetCore.Mvc.Jntemplate
{
    /// <summary>
    /// Default implementation for <see cref="IConfigureOptions<JntemplateViewEngineOptions>"/> 
    /// </summary>
    public class JntemplateViewEngineOptionsSetup : IConfigureOptions<JntemplateViewEngineOptions>
    {
        /// <inheritdoc />
        public void Configure(JntemplateViewEngineOptions options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            for (var i = 0; i < JntemplateViewEngine.ViewExtension.Count; i++)
            {
                options.ViewLocationFormats.Add("/Views/{1}/{0}" + JntemplateViewEngine.ViewExtension[i]);
                options.ViewLocationFormats.Add("/Views/Shared/{0}" + JntemplateViewEngine.ViewExtension[i]);

                options.AreaViewLocationFormats.Add("/Areas/{2}/Views/{1}/{0}" + JntemplateViewEngine.ViewExtension[i]);
                options.AreaViewLocationFormats.Add("/Areas/{2}/Views/Shared/{0}" + JntemplateViewEngine.ViewExtension[i]);
                options.AreaViewLocationFormats.Add("/Views/Shared/{0}" + JntemplateViewEngine.ViewExtension[i]);
            }
            //options.ViewLocationFormats.Add("/Views/{1}/{0}" + RazorViewEngine.ViewExtension);
            //options.ViewLocationFormats.Add("/Views/Shared/{0}" + RazorViewEngine.ViewExtension);

            //options.AreaViewLocationFormats.Add("/Areas/{2}/Views/{1}/{0}" + RazorViewEngine.ViewExtension);
            //options.AreaViewLocationFormats.Add("/Areas/{2}/Views/Shared/{0}" + RazorViewEngine.ViewExtension);
            //options.AreaViewLocationFormats.Add("/Views/Shared/{0}" + RazorViewEngine.ViewExtension);
        }
         
    }
}
