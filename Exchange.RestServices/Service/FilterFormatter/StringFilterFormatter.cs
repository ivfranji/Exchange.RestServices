namespace Exchange.RestServices
{
    /// <summary>
    /// String formatter. Should be base for all non-implemented formatters.
    /// </summary>
    internal sealed class StringFilterFormatter : BaseFilterFormatter
    {
        /// <inheritdoc cref="BaseFilterFormatter.Type"/>
        public override string Type
        {
            get { return typeof(string).FullName; }
        }

        /// <inheritdoc cref="BaseFilterFormatter.QuoteRequired"/>
        protected override bool QuoteRequired
        {
            get { return true; }
        }

        /// <inheritdoc cref="BaseFilterFormatter.FormatInternal"/>
        protected override string FormatInternal(object obj, FilterOperator filterOperator, PropertyDefinition propertyDefinition)
        {
            // This won't be hit, base class will take care of it
            // this class overrides quote required behavior.
            return this.FormatString(
                obj.ToString(), 
                filterOperator, 
                propertyDefinition.Name);
        }
    }
}