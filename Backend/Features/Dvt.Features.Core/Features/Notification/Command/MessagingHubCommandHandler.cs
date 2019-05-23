using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace Dvt.Features.Core.Features.Notification.Command
{
    public class MessagingHubCommandHandler : Hub
    {
        public async Task NewMessage(string username, string message)
        {
            await Clients.All.SendAsync("messageReceived", username, message);
        }
    }
}
