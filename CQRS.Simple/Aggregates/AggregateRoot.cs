using System;
using System.Collections.Generic;
using CQRS.Simple.Events;
using CQRS.Simple.Extensions;

namespace CQRS.Simple.Aggregates
{
    public abstract class AggregateRoot
    {
        protected Guid _id;
        private readonly List<Event> _changes = new List<Event>();

        public Guid Id
        {
            get { return _id; }
        }

        public IEnumerable<Event> GetUncommittedChanges()
        {
            return _changes;
        }

        public void MarkChangesAsCommitted()
        {
            _changes.Clear();
        }

        public void ReplayEvents(IEnumerable<Event> events)
        {
            foreach (var @event in events) ApplyChange(@event, false);
        }

        protected void RaiseEvent(Event @event)
        {
            ApplyChange(@event, true);
        }

        // push atomic aggregate changes to local history for further processing (EventStore.SaveEvents)
        private void ApplyChange(Event @event, bool isNew)
        {
            this.AsDynamic().Apply(@event);
            if (isNew) _changes.Add(@event);
        }
    }
}