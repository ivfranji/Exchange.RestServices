namespace Microsoft.RestServices.Exchange
{
    using System;
    using System.Collections.Generic;
    using Microsoft.OutlookServices;

    /// <summary>
    /// Property definition.
    /// </summary>
    public class PropertyDefinition
    {
        /// <summary>
        /// Object formatter.
        /// </summary>
        private static FormatterProvider formatterProvider = new FormatterProvider();

        /// <summary>
        /// Create new instance of <see cref="PropertyDefinition"/>.
        /// </summary>
        /// <param name="name">Name of the property.</param>
        /// <param name="type">Type of the property.</param>
        internal PropertyDefinition(string name, Type type)
        {
            this.Name = name;
            this.Type = type;

            if ( this.Type.IsValueType )
            {
                this.DefaultValue = Activator.CreateInstance(this.Type);
            }
            else
            {
                this.DefaultValue = null;
            }
        }

        /// <summary>
        /// Name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Type.
        /// </summary>
        public Type Type { get; }

        /// <summary>
        /// Default value.
        /// </summary>
        public object DefaultValue { get; }

        /// <summary>
        /// Is value type.
        /// </summary>
        public bool IsValueType
        {
            get { return this.Type.IsValueType; }
        }

        /// <summary>
        /// Indicate if type is list.
        /// </summary>
        public bool IsList
        {
            get { return PropertyDefinition.IsGenericList(this.Type); }
        }

        /// <summary>
        /// If property definition is collection, it will return
        /// underlying type of the collection, otherwise null.
        /// </summary>
        /// <returns></returns>
        public Type GetListUnderlyingType()
        {
            if (!this.IsList)
            {
                return null;
            }

            return this.Type.GetGenericArguments()[0];
        }

        /// <summary>
        /// Get hash code.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return this.Name.GetHashCode();
        }

        /// <summary>
        /// To String impl.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this.Name;
        }

        /// <summary>
        /// Format filter.
        /// </summary>
        /// <param name="obj">Object.</param>
        /// <param name="filterOperator">Filter operator.</param>
        /// <returns></returns>
        internal string FormatFilter(object obj, FilterOperator filterOperator)
        {
            IFilterFormatter formatter = PropertyDefinition.formatterProvider[this.Type.FullName];
            return formatter.Format(
                obj, 
                filterOperator, 
                this);
        }

        /// <summary>
        /// Validate if formatting between types is supported.
        /// </summary>
        /// <param name="inputObject"></param>
        internal void ValidateFormattingSupportedOrThrow(object inputObject)
        {
            ArgumentValidator.ThrowIfNull(
                inputObject, 
                nameof(inputObject));

            if (inputObject is string || this.Type.IsInstanceOfType(inputObject))
            {
                return;
            }

            throw new ArgumentException(
                $"Cannot format type '{inputObject.GetType().FullName}' with definition '{this.Type.FullName}'.");
        }

        internal static bool IsGenericList(Type type)
        {
            ArgumentValidator.ThrowIfNull(type, nameof(type));
            foreach (Type typeInterface in type.GetInterfaces())
            {
                if (typeInterface.IsGenericType && typeInterface.GetGenericTypeDefinition() == typeof(ICollection<>))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
