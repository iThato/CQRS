using System;
using System.Diagnostics;
using JetBrains.Annotations;

namespace Dvt.Common.Extensions
{
    public static class ThrowIfExtensions
    {
        [DebuggerStepThrough]
        [ContractAnnotation("parameter:null=>halt")]
        public static void ThrowIfNull(this object parameter, string parameterName)
        {
            parameter.ThrowIfNull(parameterName, null);
        }

        [DebuggerStepThrough]
        [ContractAnnotation("parameter:null=>halt")]
        public static void ThrowIfNull(this object parameter, string parameterName, string message)
        {
            if (parameter.IsNull())
                throw new ArgumentNullException(parameterName, message);
        }

        [DebuggerStepThrough]
        [ContractAnnotation("parameter:null=>halt")]
        public static void ThrowIfNullOrEmpty(this string parameter, string parameterName)
        {
            parameter.ThrowIfNullOrEmpty(parameterName, null);
        }

        [DebuggerStepThrough]
        [ContractAnnotation("parameter:null=>halt")]
        public static void ThrowIfNullOrEmpty(this string parameter, string parameterName, string message)
        {
            if (parameter.IsNullOrEmpty())
                throw new ArgumentNullException(parameterName, message);
        }

        [DebuggerStepThrough]
        [ContractAnnotation("parameter:null=>halt")]
        public static void ThrowIfNullOrEmptyTrimmed(this string parameter, string parameterName)
        {
            parameter.ThrowIfNullOrEmptyTrimmed(parameterName, null);
        }

        [DebuggerStepThrough]
        [ContractAnnotation("parameter:null=>halt")]
        public static void ThrowIfNullOrEmptyTrimmed(this string parameter, string parameterName, string message)
        {
            if (parameter.IsNullOrEmptyTrimmed())
                throw new ArgumentNullException(parameterName, message);
        }

        [DebuggerStepThrough]
        public static void ThrowIfFalse(this bool parameter, string parameterName)
        {
            parameter.ThrowIfFalse(parameterName, null);
        }

        [DebuggerStepThrough]
        public static void ThrowIfFalse(this bool parameter, string parameterName, string message)
        {
            if (parameter == false)
                throw new ArgumentException(parameterName, message);
        }

        [DebuggerStepThrough]
        public static void ThrowIfTrue(this bool parameter, string parameterName)
        {
            parameter.ThrowIfTrue(parameterName, null);
        }

        [DebuggerStepThrough]
        public static void ThrowIfTrue(this bool parameter, string parameterName, string message)
        {
            if (parameter)
                throw new ArgumentException(parameterName, message);
        }
    }
}
