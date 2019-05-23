namespace Dvt.Infrastructure.Constants
{
    public static class ConstantsConfiguration
    {
        public const string MagicBoom = "MagicBoom";
        public const string FormatConfigurationError = "Unable to retrieve '{0}' in configuration settings";
        public const string FormatPathError = "Path not found. '{0}'";
        public const string FormatCodeError = "The {0} code is invalid.";
        public const string FormatDescriptionError = "The {0} is invalid.";

        public static string ConfigurationError(string configurationNode)
        {
            return string.Format(FormatConfigurationError, configurationNode);
        }

        public static string PathError(string path)
        {
            return string.Format(FormatPathError, path);
        }

        public static string CodeError(string code)
        {
            return string.Format(FormatCodeError, code);
        }

        public static string DescriptionError(string code)
        {
            return string.Format(FormatDescriptionError, code);
        }

        public static string UnexpectedError()
        {
            return "An unexpected error has occurred";
        }
    }
}
