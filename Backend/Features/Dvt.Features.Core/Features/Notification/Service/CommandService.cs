using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dvt.Features.Core.Entities;
using Dvt.Features.Core.Features.Notification.Messages;
using Dvt.Features.Core.Features.Notification.TemplateModels;
using Dvt.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using RazorLight;
using SendGrid;
using SendGrid.Helpers.Mail;
using TokenType = Dvt.Features.Messages.Enums.TokenType;

namespace Dvt.Features.Core.Features.Notification.Service
{
    public class CommandService
    {
        public static async Task<Response> SendEmailViaSendGrid(CancellationToken cancellationToken, Email email, string apiKey, string fromEmail, string fromName)
        {
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress(fromEmail, fromName);
            var subject = email.Subject;
            var to = new EmailAddress(email.To);
            var plainTextContent = string.Empty;
            var htmlContent = email.Body;
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
           
            var responses = await client.SendEmailAsync(msg, cancellationToken);
            return responses;
        }

        public static void ForgotPasswordNotificationEmailGen(SendEmailCommandRequest request, Email newEmail, DvtDatabaseContext dbContext, IApiInfrastructure apiInfrastructure)
        {

            var token = dbContext.Token.Include(o => o.Id).Single(o => o.Id == request.TransferObject.TokenId);

            var model = new ForgotPasswordModel
            {
                Name = token.User.FirstName,
                HostUrl = apiInfrastructure.ApiConfiguration.GetAppSetting("HostUrl"),
                Token = token.Value.ToString()

            };
            var emailBody = CompileEmailTemplate.Compile(model, "ForgotPassword.cshtml");
            newEmail.Body = emailBody;
            newEmail.Subject = "Password Reset";
            newEmail.To = token.User.Email;
        }

        public static void NewUserAddEmailGen(SendEmailCommandRequest request, Email email, DvtDatabaseContext dbContext, IApiInfrastructure apiInfrastructure)
        {
            var user = dbContext.UserAccount.Include(g => g.Token).Single(f => f.UserAccountId == request.TransferObject.UserAccountId);
            var model = new NewUserAddModel
            {
                Name = user.FirstName,
                Email = user.Email,
                HostUrl = apiInfrastructure.ApiConfiguration.GetAppSetting("HostUrl"),
                Token = user.Token.Single(g => g.ExpiryDate > apiInfrastructure.Clock.NowAsSouthAfrican && g.TokenTypeId == (int)TokenType.SetPassword).Value.ToString()
            };

            var emailBody = CompileEmailTemplate.Compile(model, "NewUserAdd.cshtml");
            email.Body = emailBody;
            email.Subject = "User Invitation";
            email.To = user.Email;
        }

        public static class CompileEmailTemplate
        {
            public static string Compile(object model, string templateName)
            {
                var templatename = "Features\\Notification\\Templates\\" + templateName;

                Task<string> file = null;

                if (string.IsNullOrEmpty(templatename) || model == null) throw new Exception("Invalid Email compilation request.");

                var templateFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, templatename);

                if (!File.Exists(templateFilePath)) throw new Exception("File was not found");

                var template = File.ReadAllText(templateFilePath);

                var engine = new RazorLightEngineBuilder()
                    .UseMemoryCachingProvider()
                    .Build();

                var templatekey = Guid.NewGuid().ToString();
                var t = new Task(() =>
                                  {
                                      file = engine.CompileRenderAsync(templatekey, template, model);
                                  });
                t.RunSynchronously();

                return file.Result;
            }
        }

        public static void UserAddedToStokvelEmailGen(SendEmailCommandRequest request, Object email, DvtDatabaseContext dbContext, IApiInfrastructure apiInfrastructure)
        {
            //var user = dbContext.Users.Include(g => g.UserTokens).Single(f => f.Id == request.TransferObject.UserId);
            //var model = new StokvelWelcomeModel
            //{
            //    Name = user.FirstName
            //};

            //var emailBody = CompileEmailTemplate.Compile(model, "StokvelWelcome.cshtml");
            //email.Body = emailBody;
            //email.Subject = "eStokvel Invitation";
            //email.To = user.Email;
        }
    }
}
