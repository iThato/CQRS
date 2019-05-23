using Dvt.Features.Messages.Request;
using Dvt.Infrastructure.Enums;
using Dvt.Infrastructure.Structures;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dvt.Features.Core.Features.UserManagement.Messages
{
    public class AddPasswordCommandRequest : HandlerRequestBase, IRequest<OperationResult<AddPasswordCommandResponse>>
    {
        public AddPasswordRequest TransferObject { get; set; }

        protected override IEnumerable<SystemFunction> SystemFunctionsRequired()
        {
            return new[] { SystemFunction.None };
        }
    }
}
