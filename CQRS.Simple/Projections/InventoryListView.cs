using CQRS.Simple.DTO;
using CQRS.Simple.Events;
using CQRS.Simple.Handlers;
using CQRS.Simple.Repositories;

namespace CQRS.Simple.Projections
{
    public class InventoryListView : Handles<InventoryItemCreated>, Handles<InventoryItemRenamed>, Handles<InventoryItemDeactivated>
    {
        public void Handle(InventoryItemCreated message)
        {
            FakeDatabase.list.Add(new InventoryItemListDto(message.Id, message.Name));
        }

        public void Handle(InventoryItemRenamed message)
        {
            var item = FakeDatabase.list.Find(x => x.Id == message.Id);
            item.Name = message.NewName;
        }

        public void Handle(InventoryItemDeactivated message)
        {
            FakeDatabase.list.RemoveAll(x => x.Id == message.Id);
        }
    }
}