using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasePlugin
{
    public interface IControllerFunctionPlugin
    {
        public PluginContract PluginContract { get; set; }
        public ControllerDefinition getController();
        public object runActionForController(string id, object value);
    }
}
