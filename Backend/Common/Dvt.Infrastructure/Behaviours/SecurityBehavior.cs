using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Dvt.Infrastructure.Enums;
using Dvt.Infrastructure.Interfaces;
using Dvt.Infrastructure.Structures;
using MediatR;

namespace Dvt.Infrastructure.Behaviours
{
    public class SecurityBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TResponse : OperationResult
        where TRequest : HandlerRequestBase, IRequest<TResponse>
    {
        private readonly IPrincipal _principal;

        public SecurityBehavior(IPrincipal principal)
        {
            _principal = principal;
        }

        /// <summary>
        /// Proceeds to next pipeline action only if requesting user has roles stipulated in the request.
        /// </summary>
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken,
            RequestHandlerDelegate<TResponse> next)
        {
            if (!request.SystemFunctions.Any() || request.SystemFunctions.Contains(SystemFunction.None) || _principal.SystemFunctions.Any(role => request.SystemFunctions.Contains(role))) return await next();
            var response = typeof(TResponse);
            if (response.IsGenericType)
                return Activator.CreateInstance(response, EnumOperationResult.Forbidden, null) as TResponse;
            return Activator.CreateInstance(response, EnumOperationResult.Forbidden) as TResponse;
        }
    }
}
