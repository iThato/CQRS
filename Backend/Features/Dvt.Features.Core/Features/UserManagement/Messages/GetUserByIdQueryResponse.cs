using System;
using Dvt.Features.Messages.Response;
using Dvt.Infrastructure.Structures;

namespace Dvt.Features.Core.Features.UserManagement.Messages
{
    public class GetUserByIdQueryResponse : HandlerResponseBase
    {
        public GetUserByIdQueryResponse(Guid messageId):base(messageId)
        {
            Result = new UserResponse();
        }
        public UserResponse Result { get; set; }
    }
}
