namespace CQRS.Simple.Handlers
{
    public interface Handles<T>
    {
        void Handle(T message);
    }
}