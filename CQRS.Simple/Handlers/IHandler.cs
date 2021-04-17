namespace CQRS.Simple.Handlers
{
    public interface IHandler<T>
    {
        void Handle(T message);
    }
}