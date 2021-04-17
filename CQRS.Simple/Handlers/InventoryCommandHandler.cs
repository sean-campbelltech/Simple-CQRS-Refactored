using CQRS.Simple.Aggregates;
using CQRS.Simple.Commands;
using CQRS.Simple.Repositories;

namespace CQRS.Simple.Handlers
{
    public class InventoryCommandHandler : IInventoryCommandHandler
    {
        private readonly IRepository<InventoryItemAggregate> _repository;

        public InventoryCommandHandler(IRepository<InventoryItemAggregate> repository)
        {
            _repository = repository;
        }

        public void Handle(CreateItemCommand command)
        {
            var item = new InventoryItemAggregate(command.InventoryItemId, command.Name);
            _repository.Save(item, -1);
        }

        public void Handle(DeactivateItemCommand command)
        {
            var item = _repository.GetById(command.InventoryItemId);
            item.Deactivate();
            _repository.Save(item, command.OriginalVersion);
        }

        public void Handle(RemoveItemsCommand command)
        {
            var item = _repository.GetById(command.InventoryItemId);
            item.Remove(command.Count);
            _repository.Save(item, command.OriginalVersion);
        }

        public void Handle(CheckInItemsCommand command)
        {
            var item = _repository.GetById(command.InventoryItemId);
            item.CheckIn(command.Count);
            _repository.Save(item, command.OriginalVersion);
        }

        public void Handle(RenameItemCommand command)
        {
            var item = _repository.GetById(command.InventoryItemId);
            item.ChangeName(command.NewName);
            _repository.Save(item, command.OriginalVersion);
        }
    }
}