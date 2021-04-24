using System;
using CQRS.Simple.Events;

namespace CQRS.Simple.Aggregates
{
    public class InventoryItemAggregate : AggregateRoot
    {
        private bool _activated;
        private int _stockCount;

        public InventoryItemAggregate()
        {
            // used to create in repository ... many ways to avoid this, eg making private constructor
        }

        public InventoryItemAggregate(Guid id, string name)
        {
            RaiseEvent(new ItemCreatedEvent(id, name, -1));
        }

        public void ChangeName(string newName)
        {
            if (string.IsNullOrEmpty(newName)) throw new ArgumentException("newName");

            RaiseEvent(new ItemRenamedEvent(_id, newName));
        }

        public void CheckIn(int count)
        {
            if (count <= 0) throw new InvalidOperationException("must have a count greater than 0 to add to inventory");

            RaiseEvent(new ItemsCheckedInEvent(_id, count));
        }

        public void Remove(int count)
        {
            if (count <= 0) throw new InvalidOperationException("cant remove negative count from inventory");

            if (count > _stockCount) throw new InvalidOperationException($"{count} is greater that the stock level of {_stockCount}!");

            RaiseEvent(new ItemsRemovedEvent(_id, count));
        }


        public void Deactivate()
        {
            if (!_activated) throw new InvalidOperationException("already deactivated");

            RaiseEvent(new ItemDeactivatedEvent(_id));
        }

        private void Apply(ItemCreatedEvent @event)
        {
            _id = @event.Id;
            _activated = true;
        }


        private void Apply(ItemDeactivatedEvent @event)
        {
            _activated = false;
        }

        private void Apply(ItemsCheckedInEvent @event)
        {
            _stockCount += @event.Count;
        }

        private void Apply(ItemsRemovedEvent @event)
        {
            _stockCount -= @event.Count;
        }
    }
}