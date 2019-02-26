namespace Microsoft.RestServices.Exchange
{
    /// <summary>
    /// Int filter formatter.
    /// </summary>
    internal sealed class IntFilterFormatter : BaseFilterFormatter
    {
        /// <inheritdoc cref="BaseFilterFormatter.FormatInternal"/>
        protected override string FormatInternal(object obj, FilterOperator filterOperator, PropertyDefinition propertyDefinition)
        {
            return this.FormatString(
                obj.ToString(),
                filterOperator,
                propertyDefinition.Name);
        }
    }
}