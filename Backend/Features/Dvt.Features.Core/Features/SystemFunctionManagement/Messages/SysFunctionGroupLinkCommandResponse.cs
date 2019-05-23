using Dvt.Infrastructure.Structures;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dvt.Features.Core.Features.SystemFunctionManagement.Messages
{
    public class SysFunctionGroupLinkCommandResponse : HandlerResponseBase
    {
        public SysFunctionGroupLinkCommandResponse(Guid messageId) : base(messageId) { }

        public int FunctionId { get; set; }
        public bool Result { get; set; }
    }
}
