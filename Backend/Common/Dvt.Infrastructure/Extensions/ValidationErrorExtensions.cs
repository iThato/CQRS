using System.Collections.Generic;
using System.Linq;
using Dvt.Common.Extensions;
using Dvt.Infrastructure.Structures;
using FluentValidation.Results;

namespace Dvt.Infrastructure.Extensions
{
    public static class ValidationErrorExtensions
    {
        public static IList<ValidationError> MapFromValidationFailure(this IEnumerable<ValidationFailure> errors, IList<ValidationError> currentErrorList)
        {
            // ReSharper disable PossibleMultipleEnumeration
            if (errors.HasNoItems()) return currentErrorList;
            if (currentErrorList.IsNull()) return currentErrorList;

            foreach (var error in errors)
            {
                currentErrorList.Add(new ValidationError(error.PropertyName.EmptyIfNullOrEmptyTrimmed(), error.ErrorMessage));
            }

            return currentErrorList;
            // ReSharper restore PossibleMultipleEnumeration
        }

        public static IList<ValidationError> MapFromValidationFailure(this IEnumerable<ValidationFailure> errors)
        {
            var result = new List<ValidationError>();

            // ReSharper disable PossibleMultipleEnumeration
            if (errors.HasNoItems()) return result;

            result.AddRange(errors.Select(error => new ValidationError(error.PropertyName.EmptyIfNullOrEmptyTrimmed(), error.ErrorMessage)));

            // ReSharper restore PossibleMultipleEnumeration
            return result;
        }
    }
}
