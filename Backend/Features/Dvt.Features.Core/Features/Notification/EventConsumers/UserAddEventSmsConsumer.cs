using System.Threading;
using System.Threading.Tasks;
using Dvt.Features.Core.Features.UserManagement.Events;
using Dvt.Infrastructure.Interfaces;
using MediatR;

namespace Dvt.Features.Core.Features.Notification.EventConsumers
{
    public class UserAddEventSmsConsumer : INotificationHandler<UserAddEvent>
    {
        public IApplicationLogger Applicationlogger { get; }
        public UserAddEventSmsConsumer(IApplicationLogger applicationlogger)
        {
            Applicationlogger = applicationlogger;
        }

        public Task Handle(UserAddEvent notification, CancellationToken cancellationToken)
        {
            Applicationlogger.Info("New User sms event, userid:{UserId}", notification.UserAccountId);
            //Build Sms and add it to queue
            return Task.CompletedTask;
        }
    }
}
