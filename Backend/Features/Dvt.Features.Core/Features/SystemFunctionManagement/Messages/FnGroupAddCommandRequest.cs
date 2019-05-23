using Dvt.Features.Messages.Request;
using Dvt.Infrastructure.Structures;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Dvt.Infrastructure.Enums;

namespace Dvt.Features.Core.Features.SystemFunctionManagement.Messages
{
    public class FnGroupAddCommandRequest : HandlerRequestBase, IRequest<OperationResult<FnGroupAddCommandResponse>>
    {
        public AddSystemGroupRequest TransferObject { get; set; }
        protected override IEnumerable<SystemFunction> SystemFunctionsRequired()
        {
            return new[] { SystemFunction.None };
        }
    }
}
