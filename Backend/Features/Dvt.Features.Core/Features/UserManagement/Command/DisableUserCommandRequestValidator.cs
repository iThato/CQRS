using Dvt.Common.Extensions;
using Dvt.Features.Core.Features.UserManagement.Messages;
using FluentValidation;

namespace Dvt.Features.Core.Features.UserManagement.Command
{
    public class DisableUserCommandRequestValidator:AbstractValidator<DisableUserCommandRequest>
    {
        public DisableUserCommandRequestValidator()
        {
            RuleFor( u => u.TransferObject.NotNull());
            RuleFor(u => u.TransferObject.userAccountId);
        }
    }
}
