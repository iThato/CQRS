using Dvt.Infrastructure.Structures;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dvt.Features.Core.Features.UserManagement.Messages
{
    public class AddPasswordCommandResponse : HandlerResponseBase
    {
        public AddPasswordCommandResponse(Guid messageId) : base(messageId)
        {

        }

        // Try to change it to email address/ Username
        public Guid UserId { get; set; }
    }
}
