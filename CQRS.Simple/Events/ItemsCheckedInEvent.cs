using System;

namespace CQRS.Simple.Events
{
    public class ItemsCheckedInEvent : Event
    {
        public Guid Id;
        public readonly int Count;

        public ItemsCheckedInEvent(Guid id, int count)
        {
            Id = id;
            Count = count;
        }
    }
}