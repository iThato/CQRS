using Dvt.Common.Extensions;
using Dvt.Common.Validators;
using Dvt.Features.Core.Features.UserManagement.Messages;
using FluentValidation;

namespace Dvt.Features.Core.Features.UserManagement.Command
{
    public class UserAddCommandRequestValidator : AbstractValidator<UserAddCommandRequest>
    {
        const string RegexgForValidatingMobileNumber = @"0((60[3-9]|64[0-5]|66[0-5])\d{6}|(7[1-4689]|6[1-3]|8[1-4])\d{7})";
        const string NoNumericsAllowedRegex = @"^\D+$";
        public UserAddCommandRequestValidator()
        {
            RuleFor(x => x.TransferObject).NotNull();
            When(x => x.TransferObject.NotNull(), () =>
                                                  {
                                                      RuleFor(x => x.TransferObject.ContactNumber).Matches(RegexgForValidatingMobileNumber)
                                                     .Length(10).WithMessage("The Contact Number provided is not valid");
                                                      RuleFor(x => x.TransferObject.LastName).NotEmpty().Matches(NoNumericsAllowedRegex);
                                                      RuleFor(x => x.TransferObject.FirstName).NotEmpty().Matches(NoNumericsAllowedRegex);
                                                      RuleFor(x => x.TransferObject.Email).EmailAddress();

                                                  });

        }

        public bool IsSouthAfricanIDNumberValid(string identityNumber)
        {
            return new SouthAfricanIdNumberValidator(identityNumber).IsValid;
        }

    }
}
