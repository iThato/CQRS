using System.Collections.Generic;
using Dvt.Features.Messages.Request;
using Dvt.Infrastructure.Enums;
using Dvt.Infrastructure.Structures;
using MediatR;

namespace Dvt.Features.Core.Features.Course.Messages
{
    public class GetAllCoursesQueryRequest : HandlerRequestBase, IRequest<OperationResult<GetAllCoursesQueryResponse>>
    {
        public GetAllCourseRequest TransferObject { get; set; }

        protected override IEnumerable<SystemFunction> SystemFunctionsRequired()
        {
            return new[] { SystemFunction.None };
        }
    }
}
