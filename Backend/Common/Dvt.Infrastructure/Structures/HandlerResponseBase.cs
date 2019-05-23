using System;

namespace Dvt.Infrastructure.Structures
{
    public class HandlerResponseBase
    {
        public HandlerResponseBase()
        {
            MessageId = Guid.Empty;
        }

        public HandlerResponseBase(Guid messageId)
        {
            MessageId = messageId;
        }

        public Guid MessageId { get; set; }


    }
}
