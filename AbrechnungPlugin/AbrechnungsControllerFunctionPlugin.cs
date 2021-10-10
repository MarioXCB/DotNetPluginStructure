using BasePlugin;

namespace AbrechnungsPlugin
{
    public class AbrechnungsControllerFunctionPlugin : IControllerFunctionPlugin
    {
        public PluginContract PluginContract {  get; set; }
        public ControllerDefinition ControllerDefinition {  get; set; } 

        public AbrechnungsControllerFunctionPlugin()
        {
            PluginContract = new PluginContract()
            {
                pluginId = "AbrechnungPlugin",
                pluginName = "AbrechnungPlugin",
                license = "OpenSource 1"
            };
            ControllerDefinition = new ControllerDefinition()
            {
                Id = "AbrechnungController",
                Method = BasePlugin.HttpMethod.HttpGet
            };
        }

        public ControllerDefinition getController()
        {
            return ControllerDefinition;
        }

        public object runActionForController(string id, object value)
        {
            throw new NotImplementedException();
        }
    }
}