using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;

namespace JinianNet.AspNetCoreViewEngine.Jntemplate
{
    /// <summary>
    /// Options for <see cref="MvcViewOptions"/> 
    /// </summary>
    public class JntemplateMvcViewOptionsSetup : IConfigureOptions<MvcViewOptions>
    {
        private readonly IJntemplateViewEngine _viewEngine;

        /// <summary>
        /// Initializes a new instance of <see cref="JntemplateMvcViewOptionsSetup"/>
        /// </summary>
        /// <param name="viewEngine">The <see cref="IJntemplateViewEngine"/>.</param> 
        public JntemplateMvcViewOptionsSetup(IJntemplateViewEngine viewEngine)
        {
            _viewEngine = viewEngine ?? 
                throw new ArgumentNullException(nameof(viewEngine));
        }

        /// <inheritdoc />
        public void Configure(MvcViewOptions options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }
            options.ViewEngines.Insert(0,_viewEngine);
        }
    }
}
