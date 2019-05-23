using System.Collections.Generic;
using Dvt.Features.Messages.Request;
using Dvt.Infrastructure.Enums;
using Dvt.Infrastructure.Structures;
using MediatR;

namespace Dvt.Features.Core.Features.Course.Messages
{
    public class GetCourseByIdQueryRequest : HandlerRequestBase, IRequest<OperationResult<GetCourseByIdQueryResponse>>
    {
        public GetCourseByIdRequest TransferObject { get; set; }
        protected override IEnumerable<SystemFunction> SystemFunctionsRequired()
        {
            return new[] { SystemFunction.None };
        }
    }
}
