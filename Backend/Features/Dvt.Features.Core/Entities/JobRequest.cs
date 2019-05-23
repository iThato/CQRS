using System;
using System.Collections.Generic;

namespace Dvt.Features.Core.Entities
{
    public partial class JobRequest
    {
        public Guid Id { get; set; }
        public string Body { get; set; }
        public string Type { get; set; }
        public string Cron { get; set; }
        public bool? Deleted { get; set; }
    }
}
