using JinianNet.JNTemplate;
using JinianNet.JNTemplate.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace JinianNet.AspNetCore.Mvc.Jntemplate.Configuration
{
    public class JntConfig : EngineConfig
    {
        [Variable("Data", VariableType.System)]
        public VariableScope Data { get; set; }
    }
}
