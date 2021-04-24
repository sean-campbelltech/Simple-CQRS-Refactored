using CQRS.Simple.Messages;

namespace CQRS.Simple.Events
{
    public class Event : Message
    {
        public int Version;

        public Event() { }

        public Event(int version)
        {
            Version = version;
        }
    }
}