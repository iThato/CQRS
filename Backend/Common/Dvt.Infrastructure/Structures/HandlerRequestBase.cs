using System;
using System.Collections.Generic;
using Dvt.Infrastructure.Enums;

namespace Dvt.Infrastructure.Structures
{
    public interface IHandlerRequestBase : IRequiredSystemFuctions
    {
        Guid MessageId { get; }
    }
    public interface IRequiredSystemFuctions
    {
        IEnumerable<SystemFunction> SystemFunctions { get; }
    }
    public abstract class HandlerRequestBase : IHandlerRequestBase
    {
        public Guid MessageId { get; }

        public HandlerRequestBase()
        {
            MessageId = Guid.NewGuid();
        }

        public HandlerRequestBase(Guid messageId)
        {
            MessageId = messageId;
        }

        public IEnumerable<SystemFunction> SystemFunctions => SystemFunctionsRequired();
        protected abstract IEnumerable<SystemFunction> SystemFunctionsRequired();
    }

    public abstract class HandlerRequestBase<T> : HandlerRequestBase
    {
        public HandlerRequestBase() { }
        public HandlerRequestBase(Guid messageId) : base(messageId) { }

        public T TransferObject { get; set; }
    }
}
