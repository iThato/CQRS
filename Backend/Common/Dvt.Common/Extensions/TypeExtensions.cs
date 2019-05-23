using System;
using System.Collections.Generic;
using System.Linq;

namespace Dvt.Common.Extensions
{
    public static class TypeExtensions
    {
        public static IEnumerable<Type> TypesImplementingInterface(this Type desiredType)
        {
            return AppDomain.CurrentDomain.GetAssemblies().SelectMany(assembly => assembly.GetTypes()).Where(desiredType.IsAssignableFrom);
        }

        public static string GetFriendlyName(this Type type)
        {
            var friendlyName = type.Name;
            if (!type.IsGenericType) return friendlyName;
            var iBacktick = friendlyName.IndexOf('`');
            if (iBacktick > 0)
                friendlyName = friendlyName.Remove(iBacktick);
            friendlyName += "<";
            var typeParameters = type.GetGenericArguments();
            friendlyName += typeParameters.Concatenate(",", t => t.Name) + ">";

            return friendlyName;
        }
    }
}
