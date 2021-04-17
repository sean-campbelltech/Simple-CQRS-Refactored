using CQRS.Simple.Commands;

namespace CQRS.Simple.Handlers
{
    public interface IInventoryCommandHandler
    {
        void Handle(CreateItemCommand message);
        void Handle(DeactivateItemCommand message);
        void Handle(RemoveItemsCommand message);
        void Handle(CheckInItemsCommand message);
        void Handle(RenameItemCommand message);
    }
}