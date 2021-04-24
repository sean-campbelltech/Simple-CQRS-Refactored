using System;

namespace CQRS.Simple.Commands
{
    public class DeactivateItemCommand : Command
    {
        public readonly Guid InventoryItemId;

        public DeactivateItemCommand(Guid inventoryItemId)
        {
            InventoryItemId = inventoryItemId;
        }
    }

}