using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Dvt.Common.Extensions;
using MediatR;

namespace Dvt.Infrastructure.Structures
{
    public enum EnumOperationResult
    {
        None = 0,
        Ok = 1, // Only to be used for queries
        Added = 2,
        Updated = 3,
        NotFound = 4,
        Duplicate = 5,
        Error = 6,
        Forbidden = 7,
        PreconditionFailed = 8,
        Accepted = 9,
        ValidAttachments = 10,
        InternalServerError = 11,
        Redelivered = 12
    }

    public class OperationResult
    {
        public EnumOperationResult Status { get; }

        public OperationResult(EnumOperationResult enumOperationResult)
        {
            Status = enumOperationResult;
            Errors = new List<ValidationError>();
            DomainEvents = new List<INotification>();
        }

        public OperationResult(EnumOperationResult enumOperationResult, IList<ValidationError> errors)
        {
            Status = enumOperationResult;
            Errors = errors.HasNoItems() ? new List<ValidationError>() : errors;
            DomainEvents = new List<INotification>();
            if (Errors.HasItems() && Status == EnumOperationResult.None)
                Status = EnumOperationResult.Error;
        }

        public IEnumerable<ValidationError> Errors { get; }

        protected IList<INotification> DomainEvents { get; }

        public IEnumerable<INotification> DomainEventsList => DomainEvents;

        public void AddDomainEvent(INotification notificationEvent)
        {
            DomainEvents.Add(notificationEvent);
        }

    }

    public static class OperationResultExtensions
    {
        // ReSharper disable  PossibleMultipleEnumeration
        public static void EnsureExpectedResults(this OperationResult operationResult, IEnumerable<EnumOperationResult> expectedList)
        {
            if (!expectedList.Contains(operationResult.Status))
            {
                var errorList = operationResult.Errors.Any()
                    ? operationResult.Errors.Concatenate("|", e => e.ErrorMessage + "(" + e.PropertyName + ")")
                    : "None";
                throw new ArgumentException(
                    $"{operationResult.Status} was not in expected result list. ExpectedStatuses: '{expectedList.Concatenate(";", s => s.ToString())}'. ErrorsReturn: '{errorList}' ");
            }
        }
    }

    /// <summary>
    ///     Generic service result for commands that have to return results in addition to success or failure
    /// </summary>
    public class OperationResult<T> : OperationResult
    {
        public T Entity { get; }

        public OperationResult(EnumOperationResult enumOperationResult, IList<ValidationError> errors, T entity)
            : base(enumOperationResult, errors)
        {
            Entity = entity;
        }

        public OperationResult(EnumOperationResult enumOperationResult, IList<ValidationError> errors)
            : base(enumOperationResult, errors) {}

        public OperationResult(EnumOperationResult enumOperationResult, params ValidationError[] errors)
            : base(enumOperationResult, errors) {}
    }

    public static class OperationResultType
    {
        public static EnumOperationResult ConvertFrom(HttpStatusCode httpCode)
        {
            EnumOperationResult result;
            switch (httpCode)
            {
                case 0:
                    result = EnumOperationResult.None;
                    break;
                case HttpStatusCode.OK:
                    result = EnumOperationResult.Ok;
                    break;
                case HttpStatusCode.Created:
                    result = EnumOperationResult.Added;
                    break;
                case HttpStatusCode.NotFound:
                    result = EnumOperationResult.NotFound;
                    break;
                case HttpStatusCode.Conflict:
                    result = EnumOperationResult.Duplicate;
                    break;
                case HttpStatusCode.BadRequest:
                    result = EnumOperationResult.Error;
                    break;
                case HttpStatusCode.Forbidden:
                    result = EnumOperationResult.Forbidden;
                    break;
                case HttpStatusCode.Unauthorized:
                    result = EnumOperationResult.Forbidden;
                    break;
                case HttpStatusCode.MethodNotAllowed:
                    result = EnumOperationResult.Forbidden;
                    break;
                case HttpStatusCode.Accepted:
                    result = EnumOperationResult.Accepted;
                    break;
                case HttpStatusCode.InternalServerError:
                    result = EnumOperationResult.InternalServerError;
                    break;
                default:
                    throw new ArgumentException($"Unable to convert EnumOperationResult from string HttpStatusCode '{httpCode}'");
            }
            return result;
        }
    }
}
