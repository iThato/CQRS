using System;
using MediatR;

namespace Dvt.Features.Core.Features.UserManagement.Events {
    public class UserRegisterEvent : INotification
    {
        public Guid MessageId { get; }

        public UserRegisterEvent()
        {
            MessageId = Guid.NewGuid();
        }
        
        public int UserId { get; set; }
    }
}
