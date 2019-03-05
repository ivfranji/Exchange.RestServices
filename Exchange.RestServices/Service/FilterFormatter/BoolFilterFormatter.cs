namespace Exchange.RestServices
{
    /// <summary>
    /// Bool filter formatter.
    /// </summary>
    internal sealed class BoolFilterFormatter : BaseFilterFormatter
    {
        /// <inheritdoc cref="BaseFilterFormatter.Type"/>
        public override string Type
        {
            get { return typeof(bool).FullName; }
        }

        /// <inheritdoc cref="BaseFilterFormatter.FormatInternal"/>
        protected override string FormatInternal(object obj, FilterOperator filterOperator, PropertyDefinition propertyDefinition)
        {
            return this.Format(
                obj.ToString(), 
                filterOperator, 
                propertyDefinition);
        }
    }
}