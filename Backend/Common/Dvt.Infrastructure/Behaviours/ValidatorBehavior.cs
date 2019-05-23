using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dvt.Common.Extensions;
using Dvt.Infrastructure.Extensions;
using Dvt.Infrastructure.Structures;
using FluentValidation;
using MediatR;
using ValidationContext = FluentValidation.ValidationContext;

namespace Dvt.Infrastructure.Behaviours
{
    public class ValidatorBehavior<TRequest, TResponse> : IPipelineBehavior <TRequest, TResponse> where TResponse : OperationResult
        where TRequest : IRequest<TResponse>
    {
        private readonly IValidator<TRequest>[] _validators;
        public ValidatorBehavior(IValidator<TRequest>[] validators) => _validators = validators;

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var context = new ValidationContext(request);

            var failures = _validators
                .Select(v => v.Validate(context))
                .SelectMany(result => result.Errors)
                .Where(f => f.NotNull())
                .ToList();

            if (failures.Any())
            {
                var response = typeof(TResponse);
                if (response.IsGenericType)
                    return Activator.CreateInstance(response, EnumOperationResult.Error, failures.MapFromValidationFailure(), null) as TResponse;
                return Activator.CreateInstance(response, EnumOperationResult.Error, failures.MapFromValidationFailure()) as TResponse;
            }
            return await next();
        }
    }
}
