namespace CQRS.Simple.Commands
{
    public interface ICommandDispatcher
    {
        void Send<T>(T command) where T : Command;
    }
}