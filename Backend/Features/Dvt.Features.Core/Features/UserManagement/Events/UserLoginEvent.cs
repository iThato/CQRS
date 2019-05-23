using System;
using MediatR;

namespace Dvt.Features.Core.Features.UserManagement.Events
{
    public class UserLoginEvent : INotification
    {
        public Guid MessageId { get; }

        public UserLoginEvent()
        {
            MessageId = Guid.NewGuid();
        }

        public string FirstName { get; set; }

    }
}
