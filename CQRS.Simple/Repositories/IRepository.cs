using System;
using CQRS.Simple.Aggregates;

namespace CQRS.Simple.Repositories
{
    public interface IRepository<T> where T : AggregateRoot, new()
    {
        void Save(AggregateRoot aggregate, int expectedVersion);
        T GetById(Guid id);
    }
}