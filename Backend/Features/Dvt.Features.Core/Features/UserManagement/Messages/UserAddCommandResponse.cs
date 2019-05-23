using System;
using Dvt.Infrastructure.Structures;

namespace Dvt.Features.Core.Features.UserManagement.Messages
{
    public class UserAddCommandResponse : HandlerResponseBase
    {
        public UserAddCommandResponse(Guid messageId) : base(messageId) { }

        public Guid UserAccountId { get; set; }
    }
}
