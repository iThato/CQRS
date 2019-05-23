using Dvt.Common.Extensions;
using Dvt.Features.Core.Features.Notification.Messages;
using FluentValidation;

namespace Dvt.Features.Core.Features.Notification.Command
{
    public class SendEmailCommandRequestValidator : AbstractValidator<SendEmailCommandRequest>
    {
        public SendEmailCommandRequestValidator()
        {
            RuleFor(x => x.TransferObject).NotNull();
            When(x => x.TransferObject.NotNull(), () =>
                                                  {
                                                      RuleFor(x => x.TransferObject.EmailId).NotEmpty();
                                                  });
        }
    }
}
