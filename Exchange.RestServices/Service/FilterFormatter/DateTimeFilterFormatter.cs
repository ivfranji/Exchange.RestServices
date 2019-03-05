namespace Exchange.RestServices
{
    using System;

    /// <summary>
    /// DateTimeOffset filter formatter.
    /// </summary>
    internal sealed class DateTimeFilterFormatter : BaseFilterFormatter
    {
        /// <inheritdoc cref="BaseFilterFormatter.Type"/>
        public override string Type
        {
            get { return typeof(DateTime).FullName; }
        }

        /// <summary>
        /// Date time format.
        /// </summary>
        private const string dateTimeFormat = "yyyy-MM-ddThh:mm:ssZ";

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