using System;

namespace CQRS.Simple.Commands
{
    public class RemoveItemsCommand : Command
    {
        public Guid InventoryItemId;
        public readonly int Count;

        public RemoveItemsCommand(Guid inventoryItemId, int count)
        {
            InventoryItemId = inventoryItemId;
            Count = count;
        }
    }
}