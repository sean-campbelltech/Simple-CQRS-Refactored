using System;
using CQRS.Simple.Aggregates;

namespace CQRS.Simple.Handlers
{
    public interface IEventSourcingHandler<T> where T : AggregateRoot, new()
    {
        void Save(AggregateRoot aggregate);
        T GetById(Guid aggregateId);
    }
}