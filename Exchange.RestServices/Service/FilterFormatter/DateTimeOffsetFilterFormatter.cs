namespace Exchange.RestServices
{
    using System;

    /// <summary>
    /// DateTimeOffset filter formatter.
    /// </summary>
    internal sealed class DateTimeOffsetFilterFormatter : BaseFilterFormatter
    {
        /// <inheritdoc cref="BaseFilterFormatter.Type"/>
        public override string Type
        {
            get { return typeof(DateTimeOffset).FullName; }
        }

        /// <summary>
        /// Date time format.
        /// </summary>
        private const string dateTimeFormat = "yyyy-MM-ddThh:mm:ssZ";

        /// <inheritdoc cref="BaseFilterFormatter.FormatInternal"/>
        protected override string FormatInternal(object obj, FilterOperator filterOperator, PropertyDefinition propertyDefinition)
        {
            DateTimeOffset dateTimeOffset = (DateTimeOffset) obj;
            return this.Format(
                dateTimeOffset.Date.ToString(DateTimeOffsetFilterFormatter.dateTimeFormat),
                filterOperator, 
                propertyDefinition);
        }
    }
}