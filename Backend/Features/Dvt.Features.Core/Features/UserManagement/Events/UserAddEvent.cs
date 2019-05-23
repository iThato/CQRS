using System;
using MediatR;

namespace Dvt.Features.Core.Features.UserManagement.Events
{
    public class UserAddEvent : INotification
    {
        public Guid MessageId { get; }

        public UserAddEvent()
        {
            MessageId = Guid.NewGuid();
        }

        public Guid UserAccountId { get; set; }

    }
}
