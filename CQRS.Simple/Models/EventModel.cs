using System;
using CQRS.Simple.Events;

namespace CQRS.Simple.Models
{
    public class EventModel
    {
        public readonly Event EventData;
        public readonly Guid Id;
        public readonly int Version;

        public EventModel(Guid id, Event eventData, int version)
        {
            EventData = eventData;
            Version = version;
            Id = id;
        }
    }
}