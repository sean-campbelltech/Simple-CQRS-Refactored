using System;
using System.Collections.Generic;
using CQRS.Simple.Events;

namespace CQRS.Simple.Repositories
{
    public interface IEventStore
    {
        void SaveEvents(Guid aggregateId, IEnumerable<Event> events, int expectedVersion);
        List<Event> GetEventsForAggregate(Guid aggregateId);
    }
}