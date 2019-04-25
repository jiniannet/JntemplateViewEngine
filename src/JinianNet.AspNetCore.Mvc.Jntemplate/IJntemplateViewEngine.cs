using Microsoft.AspNetCore.Mvc.ViewEngines;

namespace JinianNet.AspNetCore.Mvc.Jntemplate
{
    public interface IJntemplateViewEngine : IViewEngine
    {
        string GetAbsolutePath(string executingFilePath, string pagePath);
    }
}
