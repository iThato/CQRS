namespace Dvt.Common.Structures
{
    public class ValidationError
    {
        public ValidationError() {}

        public ValidationError(string errorMessage)
        {
            ErrorMessage = errorMessage;
            PropertyName = string.Empty;
        }

        public ValidationError(string propertyName, string errorMessage)
        {
            ErrorMessage = errorMessage;
            PropertyName = propertyName;
        }

        public ValidationError(string propertyName, string errorMessageFormat, params object[] args)
        {
            ErrorMessage = string.Format(errorMessageFormat, args);
            PropertyName = propertyName;
        }

        public override string ToString()
        {
            return $"PropertyName: {PropertyName}, ErrorMessage: {ErrorMessage}";
        }

        public string PropertyName { get; set; }

        public string ErrorMessage { get; set; }
    }
}
