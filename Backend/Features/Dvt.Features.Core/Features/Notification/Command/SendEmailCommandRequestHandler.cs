using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Dvt.Common.Extensions;
using Dvt.Features.Core.Entities;
using Dvt.Features.Core.Features.Notification.Messages;
using Dvt.Features.Core.Features.Notification.Service;
using Dvt.Features.Messages.Enums;
using Dvt.Infrastructure.Interfaces;
using Dvt.Infrastructure.Structures;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Dvt.Features.Core.Features.Notification.Command
{
    public class SendEmailCommandRequestHandler : RequestHandlerBase, IRequestHandler<SendEmailCommandRequest, OperationResult<Unit>>
    {
        private readonly DvtDatabaseContext _dbContext;
        private readonly IApiInfrastructure _apiInfrastructure;
        public SendEmailCommandRequestHandler(DvtDatabaseContext dbContext, IApiInfrastructure apiInfrastructure) : base(apiInfrastructure,dbContext)
        {
            _dbContext = dbContext;
            _apiInfrastructure = apiInfrastructure;
        }

        public async Task<OperationResult<Unit>> Handle(SendEmailCommandRequest request, CancellationToken cancellationToken)
        {
            var response = new SendEmailCommandResponse(request.MessageId);
            OperationResult<Unit> result;

            var newEmail = await _dbContext.Email.SingleOrDefaultAsync(f => f.EmailId == request.TransferObject.EmailId, cancellationToken);

            if (newEmail.IsNull())
            {
                newEmail = new Email
                {
                    EmailId = request.TransferObject.EmailId,
                    CreateDate = _apiInfrastructure.Clock.NowAsSouthAfrican
                };
                switch (request.TransferObject.EmailType)
                {
                    case EmailType.NewUserAdd:
                        CommandService.NewUserAddEmailGen(request, newEmail, _dbContext, _apiInfrastructure);
                        break;
                    case EmailType.ForgotPassword:
                        CommandService.ForgotPasswordNotificationEmailGen(request, newEmail, _dbContext, _apiInfrastructure);
                        break;
                    default:
                        throw new Exception("Invalid Email Type");
                }
                _dbContext.Email.Add(newEmail);
                await _dbContext.SaveChangesAsync(cancellationToken);
            }


            var apiKey = _apiInfrastructure.ApiConfiguration.GetAppSetting("SendGrid:ApiKey");
            var fromEmail = _apiInfrastructure.ApiConfiguration.GetAppSetting("SendGrid:FromEmail");
            var fromName = _apiInfrastructure.ApiConfiguration.GetAppSetting("SendGrid:FromName");

            var responses = await CommandService.SendEmailViaSendGrid(cancellationToken, newEmail, apiKey, fromEmail, fromName);
            if (responses.StatusCode == HttpStatusCode.Accepted || responses.StatusCode == HttpStatusCode.OK)
            {
                newEmail.SentDate = _apiInfrastructure.Clock.NowAsSouthAfrican;
                newEmail.Result = Newtonsoft.Json.JsonConvert.SerializeObject(responses.Body);
                await _dbContext.SaveChangesAsync(cancellationToken);

                response.Success = true;
                result = new OperationResult<Unit>(EnumOperationResult.Ok);
            }
            else
            {
                result = new OperationResult<Unit>(EnumOperationResult.Error);
            }
            return result;
        }
    }
}
