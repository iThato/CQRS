using System;
using Dvt.Infrastructure.Structures;

namespace Dvt.Features.Core.Features.Notification.Messages
{
    public class SendEmailCommandResponse : HandlerResponseBase
    {
        public SendEmailCommandResponse(Guid messageId) : base(messageId) { }

        public bool Success { get; set; }
    }
}
