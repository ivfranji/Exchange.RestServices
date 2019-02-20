namespace Microsoft.RestServices.Exchange
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Property definition.
    /// </summary>
    internal class PropertyDefinition
    {
        /// <summary>
        /// Create new instance of <see cref="PropertyDefinition"/>.
        /// </summary>
        /// <param name="name">Name of the property.</param>
        /// <param name="type">Type of the property.</param>
        public PropertyDefinition(string name, Type type)
        {
            this.Name = name;
            this.Type = type;
            this.Changed = false;
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
        /// Is changed.
        /// </summary>
        public bool Changed { get; set; }

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
