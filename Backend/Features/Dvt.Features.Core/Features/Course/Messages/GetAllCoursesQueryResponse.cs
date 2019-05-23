using System;
using System.Collections.Generic;
using Dvt.Features.Messages.Response;
using Dvt.Infrastructure.Structures;

namespace Dvt.Features.Core.Features.Course.Messages
{
    public class GetAllCoursesQueryResponse : HandlerResponseBase
    {
        public GetAllCoursesQueryResponse(Guid messageId) : base(messageId)
        {
            Result = new List<CourseResponse>();
        }
        public List<CourseResponse> Result { get; set; }
    }
}
