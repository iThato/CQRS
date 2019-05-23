using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dvt.Features.Core.Features.SystemFunctionManagement.Events
{
    class SystemFunctionAddEvent : INotification
    {
        public Guid MessageId { get; }

        public SystemFunctionAddEvent()
        {
            MessageId = Guid.NewGuid();
        }

        public int Id { get; set; }

    }
}
