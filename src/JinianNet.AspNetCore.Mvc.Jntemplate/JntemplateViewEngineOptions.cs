using System.Collections.Generic;

namespace JinianNet.AspNetCore.Mvc.Jntemplate
{
    public class JntemplateViewEngineOptions
    {
        public IList<string> ViewLocationFormats { get; } = new List<string>();
    }
}