namespace CQRS.Simple.Commands
{
    public interface ICommandSender
    {
        void Send<T>(T command) where T : Command;
    }
}