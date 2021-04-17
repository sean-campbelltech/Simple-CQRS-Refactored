using System;
using CQRS.Simple.DTO;
using CQRS.Simple.Events;
using CQRS.Simple.Repositories;

namespace CQRS.Simple.Handlers
{
    // EventHandler that populates the Read database
    public class EventHandler : IEventHandler
    {
        public void Handle(ItemCreatedEvent message)
        {
            ReadDatabase.details.Add(message.Id, new InventoryItemDetailsDto(message.Id, message.Name, 0, 0));
            ReadDatabase.list.Add(new InventoryItemListDto(message.Id, message.Name));
        }

        private InventoryItemDetailsDto GetDetailsItem(Guid id)
        {
            InventoryItemDetailsDto d;

            if (!ReadDatabase.details.TryGetValue(id, out d))
            {
                throw new InvalidOperationException("did not find the original inventory this shouldnt happen");
            }

            return d;
        }

        public void Handle(ItemRenamedEvent message)
        {
            InventoryItemDetailsDto d = GetDetailsItem(message.Id);
            d.Name = message.NewName;
            d.Version = message.Version;

            var item = ReadDatabase.list.Find(x => x.Id == message.Id);
            item.Name = message.NewName;
        }

        public void Handle(ItemsRemovedEvent message)
        {
            InventoryItemDetailsDto d = GetDetailsItem(message.Id);
            d.CurrentCount -= message.Count;
            d.Version = message.Version;
        }

        public void Handle(ItemsCheckedInEvent message)
        {
            InventoryItemDetailsDto d = GetDetailsItem(message.Id);
            d.CurrentCount += message.Count;
            d.Version = message.Version;
        }

        public void Handle(ItemDeactivatedEvent message)
        {
            ReadDatabase.details.Remove(message.Id);
            ReadDatabase.list.RemoveAll(x => x.Id == message.Id);
        }
    }
}