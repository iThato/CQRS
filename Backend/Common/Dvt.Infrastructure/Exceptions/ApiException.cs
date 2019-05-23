using System;
using System.Runtime.Serialization;
using Dvt.Common.Extensions;

namespace Dvt.Infrastructure.Exceptions
{
    [Serializable]
    public class ApiException : Exception
    {
        public string ErrorNo { get; protected set; }

        public string AdditionalInformation { get; protected set; }

        public ApiException(string message) : base(message) {}
        public ApiException(string message, Exception inner) : base(message, inner) {}
        public ApiException(string message, string errorNumber) : this(message, errorNumber, null) {}

        public ApiException(string message, string errorNumber, Exception inner)
            : base(message, inner)
        {
            ErrorNo = errorNumber;
        }

        public ApiException(string message, string errorNumber, string additionInformation, Exception inner)
            : this(message, errorNumber, inner)
        {
            AdditionalInformation = additionInformation;
        }

        protected ApiException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            if (info.IsNull()) return;
            ErrorNo = info.GetString("ErrorNo");
            AdditionalInformation = info.GetString("AdditionalInformation");
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            if (info.IsNull()) return;
            info.AddValue("ErrorNo", ErrorNo.EmptyIfNull());
            info.AddValue("AdditionalInformation", AdditionalInformation.EmptyIfNull());
        }
    }

    [Serializable]
    public class ApiSecurityException : ApiException
    {
        public ApiSecurityException(string message) : base(message) {}
        public ApiSecurityException(string message, Exception inner) : base(message, inner) {}
        public ApiSecurityException(string message, string errorNumber) : this(message, errorNumber, null) {}

        public ApiSecurityException(string message, string errorNumber, Exception inner)
            : base(message, errorNumber, inner) {}
    }

    [Serializable]
    public class ApiNotAuthorisedException : ApiException
    {
        public ApiNotAuthorisedException(string message) : base(message, ApiExceptionCode.SecurityRightsViolation) {}
        public ApiNotAuthorisedException(string message, Exception inner) : base(message, ApiExceptionCode.SecurityNotAuthenticated, inner) {}

        public ApiNotAuthorisedException(string message, string additionInformation, Exception inner)
            : base(message, ApiExceptionCode.SecurityNotAuthenticated, additionInformation, inner) {}
    }

    [Serializable]
    public class ApiInsufficientRightsException : ApiException
    {
        public ApiInsufficientRightsException(string message) : base(message, ApiExceptionCode.SecurityRightsViolation) {}
        public ApiInsufficientRightsException(string message, Exception inner) : base(message, ApiExceptionCode.SecurityRightsViolation, inner) {}

        public ApiInsufficientRightsException(string message, string additionInformation, Exception inner)
            : base(message, ApiExceptionCode.SecurityRightsViolation, additionInformation, inner) {}
    }

    [Serializable]
    public class ApiRecordNotFoundException : ApiException
    {
        public ApiRecordNotFoundException(string message) : base(message, ApiExceptionCode.RecordNotFound) {}
        public ApiRecordNotFoundException(string message, Exception inner) : base(message, ApiExceptionCode.RecordNotFound, inner) {}

        public ApiRecordNotFoundException(string message, string additionInformation, Exception inner)
            : base(message, ApiExceptionCode.RecordNotFound, additionInformation, inner) {}
    }

    [Serializable]
    public class ApiServerConfigurationException : ApiException
    {
        public ApiServerConfigurationException(string message) : base(message, ApiExceptionCode.ConfigurationError) {}
        public ApiServerConfigurationException(string message, Exception inner) : base(message, ApiExceptionCode.ConfigurationError, inner) {}

        public ApiServerConfigurationException(string message, string additionInformation, Exception inner)
            : base(message, ApiExceptionCode.ConfigurationError, additionInformation, inner) {}
    }

    [Serializable]
    public class ApiServerErrorException : ApiException
    {
        public ApiServerErrorException(string message) : base(message, ApiExceptionCode.ServerError) {}
        public ApiServerErrorException(string message, Exception inner) : base(message, ApiExceptionCode.ServerError, inner) {}

        public ApiServerErrorException(string message, string additionInformation, Exception inner)
            : base(message, ApiExceptionCode.ServerError, additionInformation, inner) {}
    }

    [Serializable]
    public class ApiServerTimeoutException : ApiException
    {
        public ApiServerTimeoutException(string message) : base(message, ApiExceptionCode.Timeout) {}
        public ApiServerTimeoutException(string message, Exception inner) : base(message, ApiExceptionCode.Timeout, inner) {}

        public ApiServerTimeoutException(string message, string additionInformation, Exception inner)
            : base(message, ApiExceptionCode.Timeout, additionInformation, inner) {}
    }

    [Serializable]
    public class ApiServerUnavailableException : ApiException
    {
        public ApiServerUnavailableException(string message) : base(message, ApiExceptionCode.ServerUnavailable) {}
        public ApiServerUnavailableException(string message, Exception inner) : base(message, ApiExceptionCode.ServerUnavailable, inner) {}

        public ApiServerUnavailableException(string message, string additionInformation, Exception inner)
            : base(message, ApiExceptionCode.ServerUnavailable, additionInformation, inner) {}
    }



}
