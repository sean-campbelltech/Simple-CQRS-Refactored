using System;

namespace CQRS.Simple.Events
{
    public class ItemsCheckedInToInventory : Event
    {
        public Guid Id;
        public readonly int Count;

        public ItemsCheckedInToInventory(Guid id, int count)
        {
            Id = id;
            Count = count;
        }
    }
}