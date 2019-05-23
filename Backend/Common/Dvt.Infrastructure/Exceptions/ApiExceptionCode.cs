namespace Dvt.Infrastructure.Exceptions
{
    public static class ApiExceptionCode
    {
        public const string SecurityNotAuthenticated = "401";
        public const string SecurityRightsViolation = "403";
        public const string Timeout = "inf2";
        public const string ServerError = "inf3";
        public const string ServerUnavailable = "inf4";
        public const string RecordNotFound = "rec1";
        public const string ConfigurationError = "cfg1";
    }
}
