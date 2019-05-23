using System;
using Dvt.Infrastructure.Structures;

namespace Dvt.Features.Core.Features.UserManagement.Messages
{
   public class DisableUserCommandResponse:HandlerResponseBase
    {
        public DisableUserCommandResponse(Guid messageId) : base(messageId) { }

        public bool Result { get; set; }
    }
  
}
