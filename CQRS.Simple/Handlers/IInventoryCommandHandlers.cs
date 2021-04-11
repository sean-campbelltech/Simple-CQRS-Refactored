using CQRS.Simple.Commands;

namespace CQRS.Simple.Handlers
{
    public interface IInventoryCommandHandlers
    {
        void Handle(CreateInventoryItem message);
        void Handle(DeactivateInventoryItem message);
        void Handle(RemoveItemsFromInventory message);
        void Handle(CheckInItemsToInventory message);
        void Handle(RenameInventoryItem message);
    }
}