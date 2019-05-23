using System.Collections.Generic;
using Dvt.Features.Messages.Request;
using Dvt.Infrastructure.Enums;
using Dvt.Infrastructure.Structures;
using MediatR;

namespace Dvt.Features.Core.Features.Notification.Messages
{
    public class SendEmailCommandRequest : HandlerRequestBase, IRequest<OperationResult<Unit>>
    {
        public SendEmailRequest TransferObject { get; set; }
        protected override IEnumerable<SystemFunction> SystemFunctionsRequired()
        {
            return new[] { SystemFunction.None };
        }
    }
}
