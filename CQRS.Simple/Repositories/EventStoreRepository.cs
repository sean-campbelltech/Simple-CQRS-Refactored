using System;
using System.Collections.Generic;
using CQRS.Simple.Events;
using CQRS.Simple.Models;

namespace CQRS.Simple.Repositories
{
    public class EventStoreRepository : IEventStoreRepository
    {
        private readonly Dictionary<Guid, List<EventModel>> _current = new Dictionary<Guid, List<EventModel>>();

        public List<EventModel> FindAllById(Guid aggregateId)
        {
            if (!_current.TryGetValue(aggregateId, out List<EventModel> eventDescriptors))
                _current.Add(aggregateId, new List<EventModel>());

            return eventDescriptors;
        }

        public void Save(Guid aggregateId, EventModel @event)
        {
            var eventDescriptors = FindAllById(aggregateId);
            eventDescriptors.Add(@event);

            _current[aggregateId] = eventDescriptors;
        }
    }
}