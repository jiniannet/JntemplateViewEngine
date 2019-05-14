using Microsoft.Extensions.Options;
using JinianNet.JNTemplate;

namespace JinianNet.AspNetCore.Mvc.Jntemplate
{
    public class JntemplateViewEngineOptionsSetup : ConfigureOptions<JntemplateViewEngineOptions>
    {
        public JntemplateViewEngineOptionsSetup()
            : base(options => ConfigureViewEngine(options))
        {

        }

        private static void ConfigureViewEngine(JntemplateViewEngineOptions options)
        {
            //if(Engine.ResourceDirectories != null && Engine.ResourceDirectories.Length>0)
            //{
            //    options.ViewLocationFormats.Add("/template/{1}/{0}" + JntemplateViewEngine.ViewExtension);
            //}
            for (var i = 0; i < JntemplateViewEngine.ViewExtension.Count; i++)
            {
                options.ViewLocationFormats.Add("/Views/{1}/{0}" + JntemplateViewEngine.ViewExtension[i]);
                options.ViewLocationFormats.Add("/Views/Shared/{0}" + JntemplateViewEngine.ViewExtension[i]);
            }
        }
    }
}
