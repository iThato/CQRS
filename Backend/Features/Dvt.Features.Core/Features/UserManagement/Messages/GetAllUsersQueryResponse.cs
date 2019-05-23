using System;
using System.Collections.Generic;
using Dvt.Features.Messages.Response;
using Dvt.Infrastructure.Structures;

namespace Dvt.Features.Core.Features.UserManagement.Messages
{
    public class GetAllUsersQueryResponse : HandlerResponseBase
    {
        public GetAllUsersQueryResponse(Guid messageId):base(messageId)
        {
            Result = new List<UserResponse>();
        }
        public List<UserResponse> Result { get; set; }
    }
}
