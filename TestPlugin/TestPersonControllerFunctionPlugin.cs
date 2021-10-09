using BasePlugin;

namespace TestPlugin
{
    public class TestPersonControllerFunctionPlugin : IControllerFunctionPlugin
    {
        public PluginContract PluginContract {  get; set; }
        public ControllerDefinition ControllerDefinition {  get; set; } 

        public TestPersonControllerFunctionPlugin()
        {
            PluginContract = new PluginContract()
            {
                pluginId = "TestPlugin",
                pluginName = "TestPlugin",
                license = "OpenSource 1"
            };
            ControllerDefinition = new ControllerDefinition()
            {
                Id = "PersonController",
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