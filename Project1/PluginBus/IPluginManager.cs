using BasePlugin;

namespace Core.PluginBus
{
    public interface IPluginManager
    {
        public List<IControllerFunctionPlugin> ControllerPluginsCommands { get; set; }
        bool LoadControllerPlugin(string pluginPath);
        bool UnloadControllerPlugin(string pluginPath);
    }
}
