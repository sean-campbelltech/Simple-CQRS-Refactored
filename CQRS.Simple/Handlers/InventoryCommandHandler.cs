using CQRS.Simple.Aggregates;
using CQRS.Simple.Commands;
using CQRS.Simple.Repositories;

namespace CQRS.Simple.Handlers
{
    public class InventoryCommandHandler : ICommandHandler
    {
        private readonly IEventSourcingHandler<InventoryItemAggregate> _eventSourcingHandler;

        public InventoryCommandHandler(IEventSourcingHandler<InventoryItemAggregate> eventSourcingHandler)
        {
            _eventSourcingHandler = eventSourcingHandler;
        }

        public void Handle(CreateItemCommand command)
        {
            var item = new InventoryItemAggregate(command.InventoryItemId, command.Name);
            _eventSourcingHandler.Save(item, -1);
        }

        public void Handle(DeactivateItemCommand command)
        {
            var item = _eventSourcingHandler.GetById(command.InventoryItemId);
            item.Deactivate();
            _eventSourcingHandler.Save(item, command.OriginalVersion);
        }

        public void Handle(RemoveItemsCommand command)
        {
            var item = _eventSourcingHandler.GetById(command.InventoryItemId);
            item.Remove(command.Count);
            _eventSourcingHandler.Save(item, command.OriginalVersion);
        }

        public void Handle(CheckInItemsCommand command)
        {
            var item = _eventSourcingHandler.GetById(command.InventoryItemId);
            item.CheckIn(command.Count);
            _eventSourcingHandler.Save(item, command.OriginalVersion);
        }

        public void Handle(RenameItemCommand command)
        {
            var item = _eventSourcingHandler.GetById(command.InventoryItemId);
            item.ChangeName(command.NewName);
            _eventSourcingHandler.Save(item, command.OriginalVersion);
        }
    }
}