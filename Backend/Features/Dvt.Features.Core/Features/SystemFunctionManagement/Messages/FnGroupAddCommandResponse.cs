using Dvt.Infrastructure.Structures;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dvt.Features.Core.Features.SystemFunctionManagement.Messages
{
    public class FnGroupAddCommandResponse: HandlerResponseBase
    {
        public FnGroupAddCommandResponse(Guid messageId) : base(messageId) { }

        public int GroupId { get; set; }
    }
}
