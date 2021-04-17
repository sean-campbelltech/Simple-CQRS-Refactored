using System;
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

        public void Save(AggregateRoot aggregate, int expectedVersion)
        {
            _eventStore.SaveEvents(aggregate.Id, aggregate.GetUncommittedChanges(), expectedVersion);
        }

        public T GetById(Guid id)
        {
            var obj = new T();//lots of ways to do this
            var e = _eventStore.Replay(id);
            obj.LoadsFromHistory(e);

            return obj;
        }
    }
}