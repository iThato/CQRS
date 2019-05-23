using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dvt.Features.Core.Features.SystemFunctionManagement.Events
{
    public class SystemFunctionGroupAddEvent: INotification
    {
        public Guid MessageId { get; }

        public SystemFunctionGroupAddEvent()
        {
            MessageId = Guid.NewGuid();
        }

        public int GroupId { get; set; }

    }
}
