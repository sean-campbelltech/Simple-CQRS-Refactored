using System;
using System.Linq;
using CQRS.Simple.Aggregates;
using CQRS.Simple.Repositories;

namespace CQRS.Simple.Handlers
{
    public class EventSourcingHandler<T> : IEventSourcingHandler<T> where T : AggregateRoot, new() //shortcut you can do as you see fit with new()
    {
        private readonly IEventStore _eventStore;

        public EventSourcingHandler(IEventStore eventStore)
        {
            _eventStore = eventStore;
        }

        public void Save(AggregateRoot aggregate)
        {
            _eventStore.SaveEvents(aggregate.Id, aggregate.GetUncommittedChanges(), aggregate.Version);
            aggregate.MarkChangesAsCommitted();
        }

        public T GetById(Guid aggregateId)
        {
            var aggregate = new T();
            var events = _eventStore.GetEvents(aggregateId);
            aggregate.ReplayEvents(events);

            if (aggregate != null)
            {
                var latestVersion = events?.Select(x => x.Version)?.Max() ?? -1;
                aggregate.Version = latestVersion;
            }

            return aggregate;
        }
    }
}