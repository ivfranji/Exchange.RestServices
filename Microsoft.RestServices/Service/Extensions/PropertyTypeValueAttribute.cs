namespace Microsoft.RestServices.Exchange
{
    using System;

    /// <summary>
    /// Type of the mapi property.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    internal class PropertyTypeValueAttribute : Attribute
    {
        /// <summary>
        /// Create new instance of <see cref="PropertyTypeValueAttribute"/>
        /// </summary>
        /// <param name="propertyValueType">Property value type.</param>
        public PropertyTypeValueAttribute(PropertyValueType propertyValueType)
        {
            this.PropertyValueType = propertyValueType;
        }

        /// <summary>
        /// Property value type.
        /// </summary>
        internal PropertyValueType PropertyValueType { get; }
    }
}
