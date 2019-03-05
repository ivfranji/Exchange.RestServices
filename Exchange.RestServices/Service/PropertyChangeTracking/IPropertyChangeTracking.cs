namespace Exchange.RestServices
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
        IList<string> GetChangedPropertyNames();

        /// <summary>
        /// Get changed properties.
        /// </summary>
        /// <returns></returns>
        IList<PropertyDefinition> GetChangedProperies();

        /// <summary>
        /// Index getter.
        /// </summary>
        /// <param name="key">key.</param>
        /// <returns></returns>
        object this[string key] { get; }

        /// <summary>
        /// Index getter.
        /// </summary>
        /// <param name="key">Key.</param>
        /// <returns></returns>
        object this[PropertyDefinition key] { get; }
    }
}
