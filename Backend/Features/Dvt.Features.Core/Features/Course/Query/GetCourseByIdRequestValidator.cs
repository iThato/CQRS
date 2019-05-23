using Dvt.Common.Extensions;
using Dvt.Features.Core.Features.Course.Messages;
using FluentValidation;

namespace Dvt.Features.Core.Features.Course.Query
{
    public class GetCourseByIdRequestValidator : AbstractValidator<GetCourseByIdQueryRequest>
    {
        public GetCourseByIdRequestValidator()
        {
            RuleFor(x => x.TransferObject.NotNull());
            RuleFor(x => x.TransferObject.Id).NotEmpty();
        }
    }
}
