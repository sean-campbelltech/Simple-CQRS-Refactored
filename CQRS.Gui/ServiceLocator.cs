using CQRS.Simple.Routers;

namespace CQRS.Gui
{
    public static class ServiceLocator
    {
        public static MessageRouter MessageRouter { get; set; }
    }
}