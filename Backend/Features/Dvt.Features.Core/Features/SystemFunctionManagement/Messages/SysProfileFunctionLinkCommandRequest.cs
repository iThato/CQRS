using Dvt.Features.Messages.Request;
using Dvt.Infrastructure.Enums;
using Dvt.Infrastructure.Structures;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dvt.Features.Core.Features.SystemFunctionManagement.Messages
{
    public class SysProfileFunctionLinkCommandRequest : HandlerRequestBase, IRequest<OperationResult<SysProfileFunctionLinkCommandResponse>>
    {
        public LinkSystemProfileFunctionRequest TransferObject { get; set; }

        protected override IEnumerable<SystemFunction> SystemFunctionsRequired()
        {
            return new[] { SystemFunction.None };
        }
    }
}
