using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JntemplateViewEngineDemo.Models
{
    /// <summary>
    /// site view model
    /// </summary>
    public class SiteViewModel
    {
        /// <summary>
        /// site name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        ///  url
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// engine version
        /// </summary>
        public string Version { get; set; }
    }
}
