using System;

namespace CQRS.Simple.Events
{
    public class ItemRenamedEvent : Event
    {
        public readonly Guid Id;
        public readonly string NewName;

        public ItemRenamedEvent(Guid id, string newName)
        {
            Id = id;
            NewName = newName;
        }
    }
}