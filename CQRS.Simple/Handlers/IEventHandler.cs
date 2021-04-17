using CQRS.Simple.Events;

namespace CQRS.Simple.Handlers
{
    public interface IEventHandler
    {
        void Handle(ItemCreatedEvent @event);
        void Handle(ItemRenamedEvent @event);
        void Handle(ItemsRemovedEvent @event);
        void Handle(ItemsCheckedInEvent @event);
        void Handle(ItemDeactivatedEvent @event);
    }
}