using System.Reflection;

namespace Dvt.Infrastructure.Structures
{
    public static class ApplicationVersion
    {
        public static string AssemblyVersion { get; }

        static ApplicationVersion()
        {
            var version = Assembly.GetEntryAssembly().GetName().Version;
            AssemblyVersion = version.ToString();
        }
    }
}
