using System;

namespace CQRS.Simple.Commands
{
    public class CreateItemCommand : Command
    {
        public readonly Guid InventoryItemId;
        public readonly string Name;

        public CreateItemCommand(Guid inventoryItemId, string name)
        {
            InventoryItemId = inventoryItemId;
            Name = name;
        }
    }
}