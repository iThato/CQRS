using Dvt.Features.Messages.Request;
using Dvt.Infrastructure.Enums;
using Dvt.Infrastructure.Structures;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dvt.Features.Core.Features.SystemFunctionManagement.Messages
{
    public sealed class SysFunctionAddCommandRequest : HandlerRequestBase, IRequest<OperationResult<SysFunctionAddCommandResponse>>
    {
        public AddSystemFunctionRequest TransferObject { get; set; }

        protected override IEnumerable<SystemFunction> SystemFunctionsRequired()
        {
            return new[] { SystemFunction.None };
        }
    }
}
