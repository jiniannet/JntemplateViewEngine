using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace JinianNet.AspNetCoreViewEngine.Jntemplate
{
    /// <summary>
    /// Default implementation of <see cref="IJntemplateViewEngine"/>.
    /// </summary>
    /// <remarks>
    /// For <c>ViewResults</c> returned from controllers, views should be located in
    /// <see cref="JntemplateViewEngineOptions.ViewLocationFormats"/>
    /// by default. For the controllers in an area, views should exist in
    /// <see cref="JntemplateViewEngineOptions.AreaViewLocationFormats"/>.
    /// </remarks>
    public class JntemplateViewEngine : IJntemplateViewEngine
    {
        /// <summary>
        /// ViewExtension
        /// </summary>
        public static List<string> ViewExtension { get; } = new List<string>(new string[] { ".jnt", ".html" });

        private const string AreaKey = "area";
        private const string ControllerKey = "controller";
        private const string PageKey = "page";

        private readonly JntemplateViewEngineOptions _options;
        private readonly IFileProvider _contentRootFileProvider;
        private readonly JNTemplate.IEngine _engine;
        //private readonly string _contentRootPath;

        /// <summary>
        /// Initializes a new instance of the <see cref="JntemplateViewEngine" />.
        /// </summary>
        public JntemplateViewEngine(
            JNTemplate.IEngine engine,
            IOptions<JntemplateViewEngineOptions> optionsAccessor,
            IHostingEnvironment env)
        {
            _options = optionsAccessor.Value;

            if (_options.ViewLocationFormats.Count == 0)
            {
                throw new ArgumentException(nameof(optionsAccessor));
            }
            _contentRootFileProvider = env.ContentRootFileProvider;
            _engine = engine;
            //env.ContentRootPath
            //_contentRootPath = env.ContentRootPath;
        }

        /// <inheritdoc />
        public ViewEngineResult FindView(ActionContext context, string viewName, bool isMainPage)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (string.IsNullOrEmpty(viewName))
            {
                throw new ArgumentException(nameof(viewName));
            }

            if (IsApplicationRelativePath(viewName) || IsRelativePath(viewName))
            {
                return ViewEngineResult.NotFound(viewName, Enumerable.Empty<string>());
            }

            return LocatePageFromViewLocations(context, viewName, isMainPage);
        }

        /// <inheritdoc />
        public ViewEngineResult GetView(string executingFilePath, string viewPath, bool isMainPage)
        {
            if (string.IsNullOrEmpty(viewPath))
            {
                throw new ArgumentException(nameof(viewPath));
            }

            if (!(IsApplicationRelativePath(viewPath) || IsRelativePath(viewPath)))
            {
                return ViewEngineResult.NotFound(viewPath, Enumerable.Empty<string>());
            }

            return LocatePageFromPath(executingFilePath, viewPath, isMainPage);
        }

        /// <inheritdoc />
        public string GetAbsolutePath(string executingFilePath, string pagePath)
        {
            if (string.IsNullOrEmpty(pagePath))
            {
                return pagePath;
            }

            if (IsApplicationRelativePath(pagePath))
            {
                return pagePath;
            }

            if (!IsRelativePath(pagePath))
            {
                return pagePath;
            }

            if (string.IsNullOrEmpty(executingFilePath))
            {
                return Path.AltDirectorySeparatorChar + pagePath;
            }

            var index = executingFilePath.LastIndexOf(Path.AltDirectorySeparatorChar);
            return executingFilePath.Substring(0, index + 1) + pagePath;
        }

        private ViewEngineResult LocatePageFromViewLocations(
            ActionContext actionContext,
            string viewName,
            bool isMainPage)
        {
            var controllerName = GetNormalizedRouteValue(actionContext, ControllerKey);
            var areaName = GetNormalizedRouteValue(actionContext, AreaKey);
            string pageName = null;
            if (actionContext.ActionDescriptor.RouteValues.ContainsKey(PageKey))
            {
                // Only calculate the Razor Page name if "page" is registered in RouteValues.
                pageName = GetNormalizedRouteValue(actionContext, PageKey);
            }


            var searchedLocations = new List<string>();

            foreach (var location in _options.ViewLocationFormats)
            {
                var view = string.Format(location, viewName, controllerName);
                var fileInfo = _contentRootFileProvider.GetFileInfo(view);
                if (fileInfo.Exists)
                {
                    return ViewEngineResult.Found(viewName, new JntemplateView(_engine, fileInfo.PhysicalPath));
                }

                searchedLocations.Add(view);
            }
            return ViewEngineResult.NotFound(viewName, searchedLocations);
        }

        private ViewEngineResult LocatePageFromPath(string executingFilePath, string pagePath, bool isMainPage)
        {
            var applicationRelativePath = GetAbsolutePath(executingFilePath, pagePath);

            if (!(IsApplicationRelativePath(pagePath) || IsRelativePath(pagePath)))
            {
                return ViewEngineResult.NotFound(applicationRelativePath, Enumerable.Empty<string>());
            }
            var fileInfo = _contentRootFileProvider.GetFileInfo(applicationRelativePath);
            if (fileInfo.Exists)
                applicationRelativePath = fileInfo.PhysicalPath;
            return ViewEngineResult.Found(applicationRelativePath, new JntemplateView(_engine, applicationRelativePath));
        }

        private static string GetNormalizedRouteValue(ActionContext context, string key)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (!context.RouteData.Values.TryGetValue(key, out object routeValue))
            {
                return null;
            }

            var actionDescriptor = context.ActionDescriptor;
            string normalizedValue = null;
            if (actionDescriptor.RouteValues.TryGetValue(key, out string value) &&
                !string.IsNullOrEmpty(value))
            {
                normalizedValue = value;
            }

            var stringRouteValue = routeValue?.ToString();
            if (string.Equals(normalizedValue, stringRouteValue, StringComparison.OrdinalIgnoreCase))
            {
                return normalizedValue;
            }

            return stringRouteValue;
        }

        private static bool IsApplicationRelativePath(string name)
        {
            return name[0] == '~' || name[0] == '/';
        }

        private static bool IsRelativePath(string name)
        {
            for (var i = 0; i < ViewExtension.Count; i++)
            {
                if (name.EndsWith(ViewExtension[i], StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
