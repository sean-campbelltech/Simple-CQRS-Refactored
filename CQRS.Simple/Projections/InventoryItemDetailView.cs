using System;
using CQRS.Simple.DTO;
using CQRS.Simple.Events;
using CQRS.Simple.Handlers;
using CQRS.Simple.Repositories;

namespace CQRS.Simple.Projections
{
    public class InventoryItemDetailView : IHandler<ItemCreatedEvent>, IHandler<ItemDeactivatedEvent>, IHandler<ItemRenamedEvent>, IHandler<ItemsRemovedEvent>, IHandler<ItemsCheckedInEvent>
    {
        public void Handle(ItemCreatedEvent message)
        {
            FakeDatabase.details.Add(message.Id, new InventoryItemDetailsDto(message.Id, message.Name, 0, 0));
        }

        public void Handle(ItemRenamedEvent message)
        {
            InventoryItemDetailsDto d = GetDetailsItem(message.Id);
            d.Name = message.NewName;
            d.Version = message.Version;
        }

        private InventoryItemDetailsDto GetDetailsItem(Guid id)
        {
            InventoryItemDetailsDto d;

            if (!FakeDatabase.details.TryGetValue(id, out d))
            {
                throw new InvalidOperationException("did not find the original inventory this shouldnt happen");
            }

            return d;
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
            FakeDatabase.details.Remove(message.Id);
        }
    }
}