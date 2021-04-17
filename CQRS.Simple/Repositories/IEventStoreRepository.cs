using System;
using System.Collections.Generic;
using CQRS.Simple.Models;

namespace CQRS.Simple.Repositories
{
    public interface IEventStoreRepository
    {
        List<EventModel> FindAllById(Guid aggregateId);
        void Save(Guid aggregateId, EventModel @event);
    }
}