namespace Microsoft.RestServices.Exchange
{
    using System.Collections.Generic;

    /// <summary>
    /// Property change tracking.
    /// </summary>
    internal interface IPropertyChangeTracking
    {
        /// <summary>
        /// Get a list of changed properties.
        /// </summary>
        /// <returns></returns>
        IList<string> GetChangedProperties();

        /// <summary>
        /// Index getter.
        /// </summary>
        /// <param name="key">key.</param>
        /// <returns></returns>
        object this[string key] { get; }
    }
}
