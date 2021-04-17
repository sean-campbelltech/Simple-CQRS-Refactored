using System;
using CQRS.Simple.Events;

namespace CQRS.Simple.Aggregates
{
    public class InventoryItemAggregate : AggregateRoot
    {
        private bool _activated;
        private Guid _id;
        private string _name;

        public InventoryItemAggregate()
        {
            // used to create in repository ... many ways to avoid this, eg making private constructor
        }

        public InventoryItemAggregate(Guid id, string name)
        {
            ApplyChange(new ItemCreatedEvent(id, name));
        }

        public override Guid Id
        {
            get { return _id; }
        }

        private void Apply(ItemCreatedEvent e)
        {
            _id = e.Id;
            _activated = true;
            _name = e.Name;
        }

        private void Apply(ItemDeactivatedEvent e)
        {
            _activated = false;
        }

        public void ChangeName(string newName)
        {
            if (string.IsNullOrEmpty(newName)) throw new ArgumentException("newName");
            ApplyChange(new ItemRenamedEvent(_id, newName));
        }

        public void Remove(int count)
        {
            if (count <= 0) throw new InvalidOperationException("cant remove negative count from inventory");
            ApplyChange(new ItemsRemovedEvent(_id, count));
        }


        public void CheckIn(int count)
        {
            if (count <= 0) throw new InvalidOperationException("must have a count greater than 0 to add to inventory");
            ApplyChange(new ItemsCheckedInEvent(_id, count));
        }

        public void Deactivate()
        {
            if (!_activated) throw new InvalidOperationException("already deactivated");
            ApplyChange(new ItemDeactivatedEvent(_id));
        }
    }
}