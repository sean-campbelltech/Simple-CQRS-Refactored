using CQRS.Simple.Messages;

namespace CQRS.Simple.Events
{
    public class Event : Message
    {
        public int Version;
    }
}