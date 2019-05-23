using System;
using System.Threading;
using System.Threading.Tasks;
using Dvt.Features.Core.Features.UserManagement.Events;
using Dvt.Features.Messages.Enums;
using Dvt.Features.Messages.Request;
using Dvt.Infrastructure.Interfaces;
using MediatR;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using Newtonsoft.Json;

namespace Dvt.Features.Core.Features.Notification.EventConsumers
{
    public class UserForgotPasswordEmailEventConsumer : INotificationHandler<UserForgotPasswordEvent>
    {
        public IApplicationLogger Applicationlogger { get; }
        private readonly IApiConfiguration _configuration;

        public UserForgotPasswordEmailEventConsumer(IApplicationLogger  applicationlogger, IApiConfiguration configuration)
        {
            Applicationlogger = applicationlogger;
            _configuration = configuration;
        }

        public async Task Handle(UserForgotPasswordEvent notification, CancellationToken cancellationToken)
        {
            Applicationlogger.Info("New User email event, userid:{TokenId}", notification.TokenId);
            var newEmailId = Guid.NewGuid();

            var storageAccount = CloudStorageAccount.Parse(_configuration.GetAppSetting("ConnectionStrings:AzureWebJobsDashboard"));
            var queueClient = storageAccount.CreateCloudQueueClient();

            var queue = queueClient.GetQueueReference(_configuration.GetAppSetting("Queue:Email"));

            await queue.CreateIfNotExistsAsync();
            var objMessage = new SendEmailRequest
            {
                EmailId = newEmailId,
                TokenId = notification.TokenId,
                EmailType = EmailType.ForgotPassword
            };
            CloudQueueMessage message = new CloudQueueMessage(JsonConvert.SerializeObject(objMessage));
            await queue.AddMessageAsync(message);
        }
    }
}
