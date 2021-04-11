using System;

namespace CQRS.Simple.Commands
{
    public class RemoveItemsCommand : Command
    {
        public Guid InventoryItemId;
        public readonly int Count;
        public readonly int OriginalVersion;

        public RemoveItemsCommand(Guid inventoryItemId, int count, int originalVersion)
        {
            InventoryItemId = inventoryItemId;
            Count = count;
            OriginalVersion = originalVersion;
        }
    }
}