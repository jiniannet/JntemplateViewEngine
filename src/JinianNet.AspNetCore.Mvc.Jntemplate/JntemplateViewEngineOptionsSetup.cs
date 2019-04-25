using Microsoft.Extensions.Options;
using JinianNet.JNTemplate;

namespace JinianNet.AspNetCore.Mvc.Jntemplate
{
    public class JntemplateViewEngineOptionsSetup : ConfigureOptions<JntemplateViewEngineOptions>
    {
        public JntemplateViewEngineOptionsSetup()
            : base(options => ConfigureMarkdown(options))
        {

        }

        private static void ConfigureMarkdown(JntemplateViewEngineOptions options)
        {
            //if(Engine.ResourceDirectories != null && Engine.ResourceDirectories.Length>0)
            //{
            //    options.ViewLocationFormats.Add("/template/{1}/{0}" + JntemplateViewEngine.ViewExtension);
            //}
            for (var i = 0; i < JntemplateViewEngine.ViewExtension.Length; i++)
            {
                options.ViewLocationFormats.Add("/Views/{1}/{0}" + JntemplateViewEngine.ViewExtension[i]);
                options.ViewLocationFormats.Add("/Views/Shared/{0}" + JntemplateViewEngine.ViewExtension[i]);
            }
        }
    }
}
