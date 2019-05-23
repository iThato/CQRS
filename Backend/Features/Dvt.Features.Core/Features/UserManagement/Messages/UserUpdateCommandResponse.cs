using System;
using Dvt.Features.Messages.Response;
using Dvt.Infrastructure.Structures;

namespace Dvt.Features.Core.Features.UserManagement.Messages
{
    public class UserUpdateCommandResponse : HandlerResponseBase
    {
        public UserUpdateCommandResponse(Guid messageId) : base(messageId) { }
        
        public Guid UserAccountId { get; set; }

    }
}
