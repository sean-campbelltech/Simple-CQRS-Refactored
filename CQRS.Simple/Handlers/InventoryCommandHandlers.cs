using CQRS.Simple.Aggregates;
using CQRS.Simple.Commands;
using CQRS.Simple.Repositories;

namespace CQRS.Simple.Handlers
{
    public class InventoryCommandHandlers : IInventoryCommandHandlers
    {
        private readonly IRepository<InventoryItemAggregate> _repository;

        public InventoryCommandHandlers(IRepository<InventoryItemAggregate> repository)
        {
            _repository = repository;
        }

        public void Handle(CreateItemCommand message)
        {
            var item = new InventoryItemAggregate(message.InventoryItemId, message.Name);
            _repository.Save(item, -1);
        }

        public void Handle(DeactivateItemCommand message)
        {
            var item = _repository.GetById(message.InventoryItemId);
            item.Deactivate();
            _repository.Save(item, message.OriginalVersion);
        }

        public void Handle(RemoveItemsCommand message)
        {
            var item = _repository.GetById(message.InventoryItemId);
            item.Remove(message.Count);
            _repository.Save(item, message.OriginalVersion);
        }

        public void Handle(CheckInItemsCommand message)
        {
            var item = _repository.GetById(message.InventoryItemId);
            item.CheckIn(message.Count);
            _repository.Save(item, message.OriginalVersion);
        }

        public void Handle(RenameItemCommand message)
        {
            var item = _repository.GetById(message.InventoryItemId);
            item.ChangeName(message.NewName);
            _repository.Save(item, message.OriginalVersion);
        }
    }
}