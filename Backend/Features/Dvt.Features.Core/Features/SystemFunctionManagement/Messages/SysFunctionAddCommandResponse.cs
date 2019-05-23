using Dvt.Infrastructure.Structures;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dvt.Features.Core.Features.SystemFunctionManagement.Messages
{
    public sealed class SysFunctionAddCommandResponse : HandlerResponseBase
    {
        public SysFunctionAddCommandResponse(Guid messageId) : base(messageId) { }

        public int FunctionId { get; set; }
    }
}
