using System;
using System.Threading;
using System.Threading.Tasks;
using Dvt.Features.Core.Features.Notification.Messages;
using Dvt.Features.Core.Features.UserManagement.Events;
using Dvt.Features.Core.Hangfire;
using Dvt.Features.Messages.Enums;
using Dvt.Features.Messages.Request;
using Dvt.Infrastructure.Interfaces;
using MediatR;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using Newtonsoft.Json;

namespace Dvt.Features.Core.Features.Notification.EventConsumers
{
    public class UserAddEventEmailConsumer : INotificationHandler<UserAddEvent>
    {
        public IApplicationLogger _applicationlogger { get; }
        private readonly IApiConfiguration _configuration;
        private readonly JobHandler _jobHandler;

        public UserAddEventEmailConsumer(IApplicationLogger applicationlogger, IApiConfiguration configuration, JobHandler jobHandler)
        {
            _applicationlogger = applicationlogger;
            _configuration = configuration;
            _jobHandler = jobHandler;
        }

        public async Task Handle(UserAddEvent notification, CancellationToken cancellationToken)
        {
            _applicationlogger.Info("Send Document Event, userid:{UserId}", notification.UserAccountId);

            var newEmailId = Guid.NewGuid();
            var objMessage = new SendEmailRequest
            {
                EmailId = newEmailId,
                EmailType = EmailType.NewUserAdd,
                UserAccountId = notification.UserAccountId
            };

            _jobHandler.Queue(new SendEmailCommandRequest
            {
                TransferObject = objMessage
            });

        }

    }
}
