using JinianNet.JNTemplate;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;

namespace Microsoft.AspNetCore.Mvc
{
    /// <summary>
    /// Extensions methods for <see cref="ViewDataDictionary"/>.
    /// </summary>
    public static class ControllerExtensions
    {
        /// <summary>
        /// Set a new value for variables.
        /// </summary>
        /// <param name="key">The key of the element to get</param> 
        /// <param name="value">The element with the specified key.</param>
        /// <typeparam name="T">The type of elements in the  <see cref="VariableScope"/>.</typeparam>
        /// <param name="controller">see <see cref="Controller"/>.</param> 
        public static void Set<T>(this Controller controller, string key, T value)
        {
            if (controller != null)
            {
                controller.ViewData[key] = new VariableElement(typeof(T), value);
            }
        }

        /// <summary>
        /// Set a new value for variables.
        /// </summary>
        /// <param name="key">The key of the element to get</param> 
        /// <param name="value">The element with the specified key.</param>
        /// <param name="controller">see <see cref="Controller"/>.</param> 
        /// <param name="valueType"></param>
        public static void Set(this Controller controller, string key, object value, Type valueType)
        {
            if (controller != null)
            {
                controller.ViewData[key] = new VariableElement(valueType, value);
            }
        }
    }
}
