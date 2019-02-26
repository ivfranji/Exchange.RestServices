namespace Microsoft.RestServices.Exchange
{
    using System;

    /// <summary>
    /// Base filter formatter.
    /// </summary>
    internal abstract class BaseFilterFormatter : IFilterFormatter
    {
        /// <summary>
        /// Indicate if quotes are required around value.
        /// </summary>
        protected virtual bool QuoteRequired
        {
            get { return false; }
        }

        /// <inheritdoc cref="IFilterFormatter.Format"/>
        public string Format(object obj, FilterOperator filterOperator, PropertyDefinition propertyDefinition)
        {
            this.ThrowIfNull(obj);
            if (obj is string)
            {
                return this.FormatString(
                    obj.ToString(),
                    filterOperator,
                    this.FormatPropertyName(
                        obj.ToString(), 
                        propertyDefinition));
            }

            this.ValidateIfObjectInstanceOfType(
                obj, 
                propertyDefinition);

            return this.FormatInternal(
                obj, 
                filterOperator, 
                propertyDefinition);
        }

        /// <summary>
        /// Format internal.
        /// </summary>
        /// <param name="obj">Object to format.</param>
        /// <param name="filterOperator">Filter operator.</param>
        /// <param name="propertyDefinition">Property definition.</param>
        /// <returns></returns>
        protected abstract string FormatInternal(object obj, FilterOperator filterOperator, PropertyDefinition propertyDefinition);

        /// <summary>
        /// Format string.
        /// </summary>
        /// <param name="obj">Object value.</param>
        /// <param name="filterOperator">Filter operator.</param>
        /// <param name="propertyPath">Property path.</param>
        /// <returns></returns>
        protected string FormatString(string obj, FilterOperator filterOperator, string propertyPath)
        {
            return this.QuoteRequired
                ? $"{propertyPath} {filterOperator} '{obj}'"
                : $"{propertyPath} {filterOperator} {obj}";
        }

        /// <summary>
        /// Format property name if required.
        /// </summary>
        /// <param name="obj">Object.</param>
        /// <param name="propertyDefinition">Property definition.</param>
        /// <returns></returns>
        protected virtual string FormatPropertyName(string obj, PropertyDefinition propertyDefinition)
        {
            return propertyDefinition.Name;
        }

        /// <summary>
        /// Validate object instance of type.
        /// </summary>
        /// <param name="obj">Object.</param>
        /// <param name="propertyDefinition">Property definition.</param>
        private void ValidateIfObjectInstanceOfType(object obj, PropertyDefinition propertyDefinition)
        {
            if (!propertyDefinition.Type.IsInstanceOfType(obj))
            {
                throw new ArgumentException(
                    $"'{obj.GetType().FullName}' cannot be formatted with '{this.GetType().FullName}'.");
            }
        }

        /// <summary>
        /// Throw if object null.
        /// </summary>
        /// <param name="obj"></param>
        private void ThrowIfNull(object obj)
        {
            ArgumentValidator.ThrowIfNull(
                obj, 
                nameof(obj));
        }
    }
}
