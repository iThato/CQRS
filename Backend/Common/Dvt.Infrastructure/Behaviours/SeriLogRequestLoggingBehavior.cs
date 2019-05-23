using System;
using System.Threading;
using System.Threading.Tasks;
using Dvt.Common.Extensions;
using Dvt.Infrastructure.Interfaces;
using Dvt.Infrastructure.Structures;
using MediatR;
using Serilog.Context;

namespace Dvt.Infrastructure.Behaviours
{
    public static class ConstantsLoggerKeys

    {
        public const string RequestType = "RequestType";
        public const string MessageId = "MessageId";
        public const string RequestTypeFullName = "RequestTypeFullName";
    }

    public class SeriLogRequestLoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : HandlerRequestBase, IRequest<TResponse>
        where TResponse : OperationResult
    {
        public SeriLogRequestLoggingBehavior(IApplicationLogger applicationLogger)

        {
            ApplicationLogger = applicationLogger;
        }

        public IApplicationLogger ApplicationLogger { get; }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            using (LogContext.PushProperty(ConstantsLoggerKeys.RequestType, typeof(TRequest).GetFriendlyName()))
            using (LogContext.PushProperty(ConstantsLoggerKeys.RequestTypeFullName, typeof(TRequest).FullName))
            using (LogContext.PushProperty(ConstantsLoggerKeys.MessageId, request.MessageId))
            {
                TResponse response;
                try
                {
                    LogActionRequest(request);
                    response = await next();
                    LogActionResult(response);
                }
                catch (Exception exception)
                {
                    LogException(exception);
                    throw;
                }

                return response;
            }
        }

        private void LogActionRequest(TRequest request)
        {
            ApplicationLogger.Verbose("{RequestLoggingType} input is {@ActionRequest}", "TRequest", request);
        }

        private void LogActionResult(OperationResult operationResult)
        {
            if (operationResult.Errors.HasItems())
                ApplicationLogger.Info("Action has returned with {OperationResult} and following validation errors: {@ValidationResults}",
                                       operationResult.Errors);
            else
                ApplicationLogger.Info("Action has returned with {OperationResult}", operationResult.Status);

            //Todo: See https://github.com/destructurama/by-ignoring for how you handle property exclusions
            ApplicationLogger.Verbose("{RequestLoggingType} input is {@ActionRequest}", "TResponse", operationResult);
        }

        private void LogException(Exception exception)
        {
            ApplicationLogger.Error("Action has thrown the following {@Exception}", exception);
        }
    }
}
