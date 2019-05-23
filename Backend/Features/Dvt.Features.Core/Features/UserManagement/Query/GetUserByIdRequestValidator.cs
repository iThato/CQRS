using Dvt.Common.Extensions;
using Dvt.Features.Core.Features.UserManagement.Messages;
using FluentValidation;

namespace Dvt.Features.Core.Features.UserManagement.Query
{
    public class GetUserByIdRequestValidator : AbstractValidator<GetUserByIdQueryRequest>
    {
        public GetUserByIdRequestValidator()
        {
            RuleFor(x => x.TransferObject.NotNull());
            RuleFor(x => x.TransferObject.Id).NotEmpty();
        }
    }
}
