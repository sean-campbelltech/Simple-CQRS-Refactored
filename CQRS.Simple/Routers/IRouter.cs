using System;
using CQRS.Simple.Messages;

namespace CQRS.Simple.Routers
{
    public interface IRouter
    {
        void RegisterHandler<T>(Action<T> handler) where T : Message;
    }
}