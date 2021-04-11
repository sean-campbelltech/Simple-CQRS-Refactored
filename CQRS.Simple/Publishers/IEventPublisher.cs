using CQRS.Simple.Events;

namespace CQRS.Simple.Publishers
{
    public interface IEventPublisher
    {
        void Publish<T>(T @event) where T : Event;
    }
}