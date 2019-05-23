using Dvt.Features.Messages.Request;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dvt.Features.Core.Tests.Unit.CourseManagement
{
    public class CourseHelper
    {
        public static GetAllCourseRequest GetAllCourses()
        {
            return new GetAllCourseRequest { };
        }

        public static GetCourseByIdRequest GetUserByInvalidId()
        {
            return new GetCourseByIdRequest
            {
                Id = 1
            };
        }

        public static GetCourseByIdRequest GetUserByValidId()
        {
            return new GetCourseByIdRequest
            {
                Id = 5
            };
        }
    }
}
