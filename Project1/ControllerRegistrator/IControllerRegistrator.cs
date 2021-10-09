namespace Core.ControllerRegistrator
{
    public interface IControllerRegistrator
    {
        void registerNewController(BasePlugin.ControllerDefinition controllerDefinition);
    }
}
