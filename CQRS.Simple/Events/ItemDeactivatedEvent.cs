using System;

namespace CQRS.Simple.Events
{
    public class ItemDeactivatedEvent : Event
    {
        public readonly Guid Id;

        public ItemDeactivatedEvent(Guid id)
        {
            Id = id;
        }
    }
}