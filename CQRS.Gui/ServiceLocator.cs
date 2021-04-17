using CQRS.Simple.Bus;

namespace CQRS.Gui
{
    public static class ServiceLocator
    {
        public static MessageRouter Bus { get; set; }
    }
}