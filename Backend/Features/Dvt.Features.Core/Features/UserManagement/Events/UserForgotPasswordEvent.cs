using System;
using MediatR;

namespace Dvt.Features.Core.Features.UserManagement.Events
{
    public class UserForgotPasswordEvent : INotification
    {
        public Guid MessageId { get; }

        public UserForgotPasswordEvent()
        {
            MessageId = Guid.NewGuid();
        }
        
        public int TokenId { get; set; }
    }
}
