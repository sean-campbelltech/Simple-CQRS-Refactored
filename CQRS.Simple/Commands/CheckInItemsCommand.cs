using System;

namespace CQRS.Simple.Commands
{
    public class CheckInItemsCommand : Command
    {
        public Guid InventoryItemId;
        public readonly int Count;

        public CheckInItemsCommand(Guid inventoryItemId, int count)
        {
            InventoryItemId = inventoryItemId;
            Count = count;
        }
    }
}