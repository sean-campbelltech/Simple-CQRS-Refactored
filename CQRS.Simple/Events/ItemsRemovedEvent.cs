using System;

namespace CQRS.Simple.Events
{
    public class ItemsRemovedEvent : Event
    {
        public Guid Id;
        public readonly int Count;

        public ItemsRemovedEvent(Guid id, int count)
        {
            Id = id;
            Count = count;
        }
    }
}