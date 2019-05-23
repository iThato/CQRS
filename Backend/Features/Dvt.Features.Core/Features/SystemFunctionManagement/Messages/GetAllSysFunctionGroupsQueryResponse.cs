using Dvt.Features.Messages.Response;
using Dvt.Infrastructure.Structures;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dvt.Features.Core.Features.SystemFunctionManagement.Messages
{
    public class GetAllSysFunctionGroupsQueryResponse : HandlerResponseBase
    {
        public GetAllSysFunctionGroupsQueryResponse(Guid messageId) : base(messageId)
        {
            Result = new List<SystemGroupResponse>();
        }
        public List<SystemGroupResponse> Result { get; set; }
    }
}
