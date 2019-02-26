namespace Microsoft.RestServices.Exchange
{
    /// <summary>
    /// Bool filter formatter.
    /// </summary>
    internal sealed class BoolFilterFormatter : BaseFilterFormatter
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