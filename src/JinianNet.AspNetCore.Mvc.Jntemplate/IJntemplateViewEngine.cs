using Microsoft.AspNetCore.Mvc.ViewEngines;

namespace JinianNet.AspNetCore.Mvc.Jntemplate
{
    /// <summary>
    /// An <see cref="IViewEngine"/> used to render pages that use the Jntemplate syntax.
    /// </summary>
    public interface IJntemplateViewEngine : IViewEngine
    {
        /// <summary>
        /// Converts the given <paramref name="pagePath"/> to be absolute, relative to
        /// <paramref name="executingFilePath"/> unless <paramref name="pagePath"/> is already absolute.
        /// </summary>
        /// <param name="executingFilePath">The absolute path to the currently-executing page, if any.</param>
        /// <param name="pagePath">The path to the page.</param>
        /// <returns>
        /// The combination of <paramref name="executingFilePath"/> and <paramref name="pagePath"/> if
        /// <paramref name="pagePath"/> is a relative path. The <paramref name="pagePath"/> value (unchanged)
        /// otherwise.
        /// </returns>
        string GetAbsolutePath(string executingFilePath, string pagePath);
    }
}
