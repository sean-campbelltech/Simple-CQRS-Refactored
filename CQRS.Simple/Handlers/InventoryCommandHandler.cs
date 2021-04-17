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
            var aggregate = new InventoryItemAggregate(command.InventoryItemId, command.Name);

            _eventSourcingHandler.Save(aggregate, -1);
        }

        public void Handle(DeactivateItemCommand command)
        {
            var aggregate = _eventSourcingHandler.GetById(command.InventoryItemId);
            aggregate.Deactivate();

            _eventSourcingHandler.Save(aggregate, command.OriginalVersion);
        }

        public void Handle(RemoveItemsCommand command)
        {
            var aggregate = _eventSourcingHandler.GetById(command.InventoryItemId);
            aggregate.Remove(command.Count);

            _eventSourcingHandler.Save(aggregate, command.OriginalVersion);
        }

        public void Handle(CheckInItemsCommand command)
        {
            var aggregate = _eventSourcingHandler.GetById(command.InventoryItemId);
            aggregate.CheckIn(command.Count);

            _eventSourcingHandler.Save(aggregate, command.OriginalVersion);
        }

        public void Handle(RenameItemCommand command)
        {
            var aggregate = _eventSourcingHandler.GetById(command.InventoryItemId);
            aggregate.ChangeName(command.NewName);

            _eventSourcingHandler.Save(aggregate, command.OriginalVersion);
        }
    }
}