using System;
using System.Collections.Generic;
using System.Text;

namespace Dvt.Features.Messages.Response
{
    public class CourseResponse
    {
        public int Id { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public List<CourseResponse> result { get; set; }
    }
}
