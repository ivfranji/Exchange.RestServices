namespace Microsoft.RestServices.Exchange
{
    /// <summary>
    /// Filter formatter.
    /// </summary>
    interface IFilterFormatter
    {
        /// <summary>
        /// Format object to filterable string.
        /// </summary>
        /// <param name="obj">Object.</param>
        /// <param name="filterOperator">Filter operator.</param>
        /// <param name="propertyDefinition">Property definition.</param>
        /// <returns></returns>
        string Format(object obj, FilterOperator filterOperator, PropertyDefinition propertyDefinition);

        /// <summary>
        /// Type of filter it supports.
        /// </summary>
        string Type { get; }
    }
}