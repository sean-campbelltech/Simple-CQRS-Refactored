using System;

namespace CQRS.Simple.Events
{
    public class InventoryItemDeactivated : Event
    {
        public readonly Guid Id;

        public InventoryItemDeactivated(Guid id)
        {
            Id = id;
        }
    }
}