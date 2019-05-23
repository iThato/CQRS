using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Dvt.Common.Extensions;
using Dvt.Common.Structures;

// ReSharper disable RedundantArgumentNameForLiteralExpression

namespace Dvt.Common.Validators
{
    public static class DataAnnotationsValidator
    {
        [SuppressMessage("Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "1#",
            Justification = "We do not want to create a result class for bool and the a error list")]
        public static bool ValidateDataAnnotations(this object source, ref IList<ValidationError> results)
        {
            var context = new ValidationContext(source, serviceProvider: null, items: null);
            var internalResults = new List<ValidationResult>();
            var result = Validator.TryValidateObject(
                source, context, internalResults,
                validateAllProperties: true);
            if (result) return true;
            foreach (var error in internalResults)
            {
                results.Add(new ValidationError(error.MemberNames.FirstOrDefault().EmptyIfNullOrEmptyTrimmed(), error.ErrorMessage));
            }
            return false;
        }
    }
}
