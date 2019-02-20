namespace Microsoft.RestServices.Exchange
{
    using System;

    /// <summary>
    /// Helper for validating arguments.
    /// </summary>
    internal static class ArgumentValidator
    {
        /// <summary>
        /// Throws if string is null or empty.
        /// </summary>
        /// <param name="obj">String to validate.</param>
        /// <param name="argName">Argument name.</param>
        internal static void ThrowIfNullOrEmpty(string obj, string argName)
        {
            if (string.IsNullOrEmpty(obj))
            {
                throw new ArgumentNullException(argName);
            }
        }

        /// <summary>
        /// Throws if array is null or doesn't contain any argument.
        /// </summary>
        /// <param name="array">Array to validate.</param>
        /// <param name="argName">Argument name.</param>
        internal static void ThrowIfNullOrEmptyArray(object[] array, string argName)
        {
            if (array == null)
            {
                throw new ArgumentNullException(argName, "Argument cannot be null.");
            }

            if (array.Length == 0)
            {
                throw new ArgumentException("Array must contain at least one element.");
            }
        }

        /// <summary>
        /// Throw if object is null.
        /// </summary>
        /// <param name="obj">Object to validate.</param>
        /// <param name="argName">Argument name.</param>
        internal static void ThrowIfNull(object obj, string argName)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(argName);
            }
        }

        /// <summary>
        /// Throws if guid is empty.
        /// </summary>
        /// <param name="guid">Guid.</param>
        /// <param name="argName">Arg name.</param>
        internal static void ThrowIfGuidEmpty(Guid guid, string argName)
        {
            if (guid == Guid.Empty)
            {
                throw new ArgumentNullException(argName);
            }
        }
    }
}