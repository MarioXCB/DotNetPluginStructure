using BasePlugin;
using System.Reflection;

namespace Core.PluginBus
{
    public class ControllerPluginManager : IPluginManager
    {
        public List<IControllerFunctionPlugin> ControllerPluginsCommands { get; set; } = new List<IControllerFunctionPlugin>();
        public bool LoadControllerPlugin(string pluginPath)
        {
            Assembly plugins = LoadPlugin(pluginPath);
            ControllerPluginsCommands.AddRange(CreateCommands(plugins));
            return true;
        }

        public bool UnloadControllerPlugin(string pluginPTH)
        {
            throw new NotImplementedException();
        }

        static Assembly LoadPlugin(string relativePath)
        {
            // Navigate up to the solution root
            string root = Path.GetFullPath(Path.Combine(
                Path.GetDirectoryName(
                    Path.GetDirectoryName(
                        Path.GetDirectoryName(
                            Path.GetDirectoryName(
                                Path.GetDirectoryName(typeof(Program).Assembly.Location)))))));

            string pluginLocation = Path.GetFullPath(Path.Combine(root, relativePath.Replace('\\', Path.DirectorySeparatorChar)));
            Console.WriteLine($"Loading commands from: {pluginLocation}");
            PluginLoadContext loadContext = new PluginLoadContext(pluginLocation);
            return loadContext.LoadFromAssemblyName(AssemblyName.GetAssemblyName(pluginLocation));
        }

        static IEnumerable<IControllerFunctionPlugin> CreateCommands(Assembly assembly)
        {
            int count = 0;

            foreach (Type type in assembly.GetTypes())
            {
                if (typeof(IControllerFunctionPlugin).IsAssignableFrom(type))
                {
                    IControllerFunctionPlugin result = Activator.CreateInstance(type) as IControllerFunctionPlugin;
                    if (result != null)
                    {
                        count++;
                        yield return result;
                    }
                }
            }

            if (count == 0)
            {
                string availableTypes = string.Join(",", assembly.GetTypes().Select(t => t.FullName));
                throw new ApplicationException(
                    $"Can't find any type which implements ICommand in {assembly} from {assembly.Location}.\n" +
                    $"Available types: {availableTypes}");
            }
        }
    }
}
