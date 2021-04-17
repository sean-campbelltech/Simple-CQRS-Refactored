using CQRS.Simple.DTO;
using CQRS.Simple.Events;
using CQRS.Simple.Handlers;
using CQRS.Simple.Repositories;

namespace CQRS.Simple.Projections
{
    public class InventoryListView : IHandler<ItemCreatedEvent>, IHandler<ItemRenamedEvent>, IHandler<ItemDeactivatedEvent>
    {
        public void Handle(ItemCreatedEvent message)
        {
            FakeDatabase.list.Add(new InventoryItemListDto(message.Id, message.Name));
        }

        public void Handle(ItemRenamedEvent message)
        {
            var item = FakeDatabase.list.Find(x => x.Id == message.Id);
            item.Name = message.NewName;
        }

        public void Handle(ItemDeactivatedEvent message)
        {
            FakeDatabase.list.RemoveAll(x => x.Id == message.Id);
        }
    }
}