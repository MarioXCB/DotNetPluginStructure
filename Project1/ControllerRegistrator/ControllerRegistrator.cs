using BasePlugin;

namespace Core.ControllerRegistrator
{
    public class ControllerRegistrator : IControllerRegistrator
    {
        IServiceCollection Services;
        public ControllerRegistrator(IServiceCollection services)
        {
            Services = services;
        }
        public void registerNewController(ControllerDefinition controllerDefinition)
        {

        }
    }
}
