namespace Microsoft.RestServices.Exchange
{
    using System;

    /// <summary>
    /// DateTimeOffset filter formatter.
    /// </summary>
    internal sealed class DateTimeFilterFormatter : BaseFilterFormatter
    {
        /// <summary>
        /// Date time format.
        /// </summary>
        private const string dateTimeFormat = "yyyy-MM-ddThh:mm:ss";

        /// <inheritdoc cref="BaseFilterFormatter.FormatInternal"/>
        protected override string FormatInternal(object obj, FilterOperator filterOperator, PropertyDefinition propertyDefinition)
        {
            DateTime dateTime = (DateTime) obj;
            return this.Format(
                dateTime.Date.ToString(DateTimeFilterFormatter.dateTimeFormat),
                filterOperator,
                propertyDefinition);
        }
    }
}