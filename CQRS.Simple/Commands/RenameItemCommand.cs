using System;

namespace CQRS.Simple.Commands
{
    public class RenameItemCommand : Command
    {
        public readonly Guid InventoryItemId;
        public readonly string NewName;

        public RenameItemCommand(Guid inventoryItemId, string newName)
        {
            InventoryItemId = inventoryItemId;
            NewName = newName;
        }
    }
}