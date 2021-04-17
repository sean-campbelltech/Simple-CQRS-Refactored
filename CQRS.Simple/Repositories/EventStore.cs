using System;
using System.Collections.Generic;
using System.Linq;
using CQRS.Simple.Events;
using CQRS.Simple.Exceptions;
using CQRS.Simple.Models;
using CQRS.Simple.Publishers;

namespace CQRS.Simple.Repositories
{
    public class EventStore : IEventStore
    {
        private readonly IEventStoreRepository _eventStoreRepository;
        private readonly IEventPublisher _publisher;

        public EventStore(IEventStoreRepository eventStoreRepository, IEventPublisher publisher)
        {
            _eventStoreRepository = eventStoreRepository;
            _publisher = publisher;
        }

        public void SaveEvents(Guid aggregateId, IEnumerable<Event> events, int expectedVersion)
        {
            var eventDescriptors = _eventStoreRepository.FindAllById(aggregateId);

            // check whether latest event version matches current aggregate version
            // otherwise -> throw exception
            if (expectedVersion != -1 && eventDescriptors[eventDescriptors.Count - 1].Version != expectedVersion)
            {
                throw new ConcurrencyException();
            }

            var i = expectedVersion;

            // iterate through current aggregate events increasing version with each processed event
            foreach (var @event in events)
            {
                i++;
                @event.Version = i;

                // persist event to the event store for current aggregate
                _eventStoreRepository.Save(aggregateId, new EventModel(aggregateId, @event, i));

                // publish current event to the bus for further processing by subscribers
                _publisher.Publish(@event);
            }
        }

        // collect all processed events for given aggregate and return them as a list
        // used to build up an aggregate from its history (Domain.LoadsFromHistory)
        public List<Event> Replay(Guid aggregateId)
        {
            var eventDescriptors = _eventStoreRepository.FindAllById(aggregateId);

            if (eventDescriptors == null)
                throw new AggregateNotFoundException();

            return eventDescriptors.Select(desc => desc.EventData).ToList();
        }

    }

}