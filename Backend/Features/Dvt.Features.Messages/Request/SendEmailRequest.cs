using System;
using Dvt.Features.Messages.Enums;

namespace Dvt.Features.Messages.Request
{
    public class SendEmailRequest
    {
        public Guid EmailId { get; set; }
        public Guid UserAccountId { get; set; }
        public EmailType EmailType { get; set; }
        public int TokenId { get; set; }
    }
}
