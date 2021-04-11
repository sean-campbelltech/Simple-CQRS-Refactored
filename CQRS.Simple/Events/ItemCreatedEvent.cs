using System;

namespace CQRS.Simple.Events
{
    public class ItemCreatedEvent : Event
    {
        public readonly Guid Id;
        public readonly string Name;
        public ItemCreatedEvent(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}