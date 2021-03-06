using System;
using Dvt.Features.Messages.Response;
using Dvt.Infrastructure.Structures;

namespace Dvt.Features.Core.Features.Course.Messages
{
    public class GetCourseByIdQueryResponse : HandlerResponseBase
    {
        public GetCourseByIdQueryResponse(Guid messageId) : base(messageId)
        {
            Result = new CourseResponse();
        }
        public CourseResponse Result { get; set; }
    }
}
