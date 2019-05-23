using Dvt.Infrastructure.Structures;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dvt.Features.Core.Features.SystemFunctionManagement.Messages
{
    public class SysProfileFunctionLinkCommandResponse : HandlerResponseBase
    {
        public SysProfileFunctionLinkCommandResponse(Guid messageId) : base(messageId) { }

        public int FunctionId { get; set; }
        public int ProfileId { get; set; }
        public bool Result { get; set; }
    }
}
