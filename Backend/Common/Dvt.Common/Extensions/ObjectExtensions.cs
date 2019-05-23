using System.Diagnostics;
using JetBrains.Annotations;

namespace Dvt.Common.Extensions
{
    public static class ObjectExtensions
    {
        [DebuggerStepThrough]
        [ContractAnnotation("null=>true")]
        public static bool IsNull(this object source)
        {
            return source == null;
        }

        [DebuggerStepThrough]
        [ContractAnnotation("null=>false")]
        public static bool NotNull(this object source)
        {
            return source != null;
        }
    }
}
