using System;
using System.Collections.Generic;

namespace Dvt.Features.Core.Entities
{
    public partial class Email
    {
        public Guid EmailId { get; set; }
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? SentDate { get; set; }
        public string Result { get; set; }
    }
}
