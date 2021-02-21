using JinianNet.JNTemplate;
using JinianNet.JNTemplate.Configuration;

namespace JinianNet.AspNetCoreViewEngine.Jntemplate.Configuration
{
    /// <summary>
    ///  An <see cref="EngineConfig"/>
    /// </summary>
    public class JntemplateConfig : EngineConfig
    {
        /// <summary>
        /// global data
        /// </summary>
        [Variable("Data", VariableType.System)]
        public VariableScope Data { get; set; }
        /// <summary>
        /// ContentRootPath
        /// </summary>
        [Variable("ContentRootPath", VariableType.Environment)]
        public string ContentRootPath { get; set; }
        /// <summary>
        /// WebRootPath
        /// </summary>
        [Variable("WebRootPath", VariableType.Environment)]
        public string WebRootPath { get; set; }
    }
}
