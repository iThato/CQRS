using System.Threading;
using System.Threading.Tasks;
using Dvt.Features.Core.Features.UserManagement.Events;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace Dvt.Features.Core.Features.Notification.Command
{
    public class UserLoginEventClientDispatcherHandler : INotificationHandler<UserLoginEvent>
    {
        private readonly IHubContext<MessagingHubCommandHandler> _hubContext;

        public UserLoginEventClientDispatcherHandler(IHubContext<MessagingHubCommandHandler> hubContext)
        {
            _hubContext = hubContext;
        }

        public Task Handle(UserLoginEvent @event, CancellationToken cancellationToken)
        {
            return _hubContext.Clients.All.SendAsync("NewUserLogin", @event, cancellationToken);
        }
    }
}
