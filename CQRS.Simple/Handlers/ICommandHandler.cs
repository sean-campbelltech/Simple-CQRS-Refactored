using CQRS.Simple.Commands;

namespace CQRS.Simple.Handlers
{
    public interface ICommandHandler
    {
        void Handle(CreateItemCommand command);
        void Handle(DeactivateItemCommand command);
        void Handle(RemoveItemsCommand command);
        void Handle(CheckInItemsCommand command);
        void Handle(RenameItemCommand command);
    }
}