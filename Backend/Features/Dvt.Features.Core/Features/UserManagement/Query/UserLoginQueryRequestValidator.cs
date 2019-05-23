using Dvt.Features.Core.Features.UserManagement.Messages;
using FluentValidation;

namespace Dvt.Features.Core.Features.UserManagement.Query
{
    public class UserLoginQueryRequestValidator : AbstractValidator<UserLoginQueryRequest>
    {
        public UserLoginQueryRequestValidator()
        {
            RuleFor(x => x.TransferObject.Password).NotEmpty();
            RuleFor(x => x.TransferObject.Username).NotEmpty();
        }
    }
}
