using System.Collections.Generic;
using Dvt.Features.Messages.Request;
using Dvt.Infrastructure.Enums;
using Dvt.Infrastructure.Structures;
using MediatR;

namespace Dvt.Features.Core.Features.UserManagement.Messages
{
    public class GetAllUsersQueryRequest : HandlerRequestBase, IRequest<OperationResult<GetAllUsersQueryResponse>>
    {
        public GetAllUsersRequest TransferObject { get; set; }

        protected override IEnumerable<SystemFunction> SystemFunctionsRequired()
        {
            return new[] { SystemFunction.None };
        }
    }
}
