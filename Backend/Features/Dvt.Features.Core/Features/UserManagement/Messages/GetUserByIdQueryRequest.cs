using System.Collections.Generic;
using Dvt.Features.Messages.Request;
using Dvt.Infrastructure.Enums;
using Dvt.Infrastructure.Structures;
using MediatR;

namespace Dvt.Features.Core.Features.UserManagement.Messages
{
    public class GetUserByIdQueryRequest : HandlerRequestBase, IRequest<OperationResult<GetUserByIdQueryResponse>>
    {
        public GetUserByIdRequest TransferObject { get; set; }
        protected override IEnumerable<SystemFunction> SystemFunctionsRequired()
        {
            return new[] { SystemFunction.None };
        }
    }
}
