using System;
using System.Reflection;

namespace Dvt.Common.Extensions
{
    // Borrowed from http://stackoverflow.com/a/1565766/314291
    public static class ReflectionExtensions
    {
        public static bool HasDefaultConstructor(this Type t)
        {
            return t.IsValueType || t.GetConstructor(Type.EmptyTypes) != null;
        }

        public static bool OnlyHasDefaultConstructor(this Type t)
        {
            var constructours = t.GetConstructors(BindingFlags.Instance | BindingFlags.Public);
            if (constructours.Length > 1) return false;

            return t.HasDefaultConstructor();
        }

        /// <summary>
        ///     Sets the value of any object (Public or Private). Uses Reflection.
        ///     Throws a ArgumentOutOfRangeException if the Property is not found.
        /// </summary>
        /// <param name="target">Object from where the Property Value is returned</param>
        /// <param name="propertyName">Property name as string.</param>
        /// <param name="newValue">Value to set</param>
        /// <returns>void</returns>
        public static void SetProperty(this object target, string propertyName, object newValue)
        {
            if (target.IsNull()) throw new ArgumentNullException(nameof(target));
            var type = target.GetType();
            var prop = type.GetProperty(propertyName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            if (prop.IsNull())
                throw new ArgumentOutOfRangeException(nameof(propertyName), $"Property {propertyName} was not found in Type {target.GetType().FullName}");
            prop.SetValue(target, newValue, null);
        }

        /// <summary>
        ///     Invokes a method of any object (Public or Private). Uses Reflection.
        /// </summary>
        /// <param name="target">Object from where the Property Value is returned</param>
        /// <param name="methodName">Method name to invoke as string.</param>
        /// <returns>void</returns>
        public static object GetInstanceMethod(object target, string methodName)
        {
            var method = target.GetType().GetMethod(methodName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            return method;
        }

        /// <summary>
        ///     Invokes a method of any object (Public or Private). Uses Reflection.
        /// </summary>
        /// <param name="target">Object from where the Property Value is returned</param>
        /// <param name="methodName">Method name to invoke as string.</param>
        /// <param name="args">Arguments to be supplied to the target method</param>
        /// <returns>void</returns>
        public static object InvokeInstanceMethod(object target, string methodName, params object[] args)
        {
            var method = target.GetType().GetMethod(methodName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            if (method.IsNull())
                throw new ArgumentOutOfRangeException(nameof(methodName), $"Method {methodName} was not found in Type {target.GetType().FullName}");
            return method.Invoke(target, args);
        }

        /// <summary>
        ///     Returns a _private_ Property Value from a given Object. Uses Reflection.
        ///     Throws a ArgumentOutOfRangeException if the Property is not found.
        /// </summary>
        /// <typeparam name="T">Type of the Property</typeparam>
        /// <param name="target">Object from where the Property Value is returned</param>
        /// <param name="propertyName">Property name as string.</param>
        /// <returns>PropertyValue</returns>
        public static T GetPrivatePropertyValue<T>(this object target, string propertyName)
        {
            if (target.IsNull()) throw new ArgumentNullException(nameof(target));
            var pi = target.GetType().GetProperty(propertyName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            if (pi.IsNull())
                throw new ArgumentOutOfRangeException(nameof(propertyName), $"Property {propertyName} was not found in Type {target.GetType().FullName}");
            return (T) pi.GetValue(target, null);
        }

        /// <summary>
        ///     Returns a _private_ Property Value from a given Object. Uses Reflection.
        ///     Returns the default value if the Property is not found.
        /// </summary>
        /// <typeparam name="T">Type of the Property</typeparam>
        /// <param name="target">Object from where the Property Value is returned</param>
        /// <param name="propertyName">Property name as string.</param>
        /// <returns>PropertyValue</returns>
        public static T GetPropertyValue<T>(this object target, string propertyName)
        {
            if (target.IsNull()) throw new ArgumentNullException(nameof(target));
            var pi = target.GetType().GetProperty(propertyName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            if (pi.IsNull())
                return default(T);
            return (T)pi.GetValue(target, null);
        }

        /// <summary>
        ///     Returns a private Property Value from a given Object. Uses Reflection.
        ///     Throws a ArgumentOutOfRangeException if the Property is not found.
        /// </summary>
        /// <typeparam name="T">Type of the Property</typeparam>
        /// <param name="target">Object from where the Property Value is returned</param>
        /// <param name="propertyName">Propertyname as string.</param>
        /// <returns>PropertyValue</returns>
        public static T GetPrivateFieldValue<T>(this object target, string propertyName)
        {
            if (target.IsNull()) throw new ArgumentNullException(nameof(target));
            var t = target.GetType();
            FieldInfo fi = null;
            while (fi.IsNull() && t.NotNull())
            {
                fi = t.GetField(propertyName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                t = t.BaseType;
            }
            if (fi.IsNull())
                throw new ArgumentOutOfRangeException(nameof(propertyName), $"Field {propertyName} was not found in Type {target.GetType().FullName}");
            return (T) fi.GetValue(target);
        }

        /// <summary>
        ///     Sets a _private_ Property Value from a given Object. Uses Reflection.
        ///     Throws a ArgumentOutOfRangeException if the Property is not found.
        /// </summary>
        /// <typeparam name="T">Type of the Property</typeparam>
        /// <param name="target">Object from where the Property Value is set</param>
        /// <param name="propertyName">Propertyname as string.</param>
        /// <param name="val">Value to set.</param>
        /// <returns>PropertyValue</returns>
        public static void SetPrivatePropertyValue<T>(this object target, string propertyName, T val)
        {
            if (target.IsNull()) throw new ArgumentNullException(nameof(target));
            var t = target.GetType();
            if (t.GetProperty(propertyName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).IsNull())
                throw new ArgumentOutOfRangeException(nameof(propertyName), $"Property {propertyName} was not found in Type {target.GetType().FullName}");
            t.InvokeMember(propertyName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.SetProperty | BindingFlags.Instance, null, target,
                           new object[] {val});
        }

        /// <summary>
        ///     Set a private Property Value on a given Object. Uses Reflection.
        /// </summary>
        /// <typeparam name="T">Type of the Property</typeparam>
        /// <param name="target">Object from where the Property Value is returned</param>
        /// <param name="propertyName">Propertyname as string.</param>
        /// <param name="val">the value to set</param>
        /// <exception cref="ArgumentOutOfRangeException">if the Property is not found</exception>
        public static void SetPrivateFieldValue<T>(this object target, string propertyName, T val)
        {
            if (target.IsNull()) throw new ArgumentNullException(nameof(target));
            var t = target.GetType();
            FieldInfo fi = null;
            while (fi.IsNull() && t.NotNull())
            {
                fi = t.GetField(propertyName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                t = t.BaseType;
            }
            if (fi.IsNull())
                throw new ArgumentOutOfRangeException(nameof(propertyName), $"Field {propertyName} was not found in Type {target.GetType().FullName}");
            fi.SetValue(target, val);
        }
    }
}
