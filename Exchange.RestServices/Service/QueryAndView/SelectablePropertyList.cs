namespace Exchange.RestServices
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    /// <summary>
    /// Contains list of properties of an entity.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal class SelectablePropertyList
    {
        /// <summary>
        /// List of properties.
        /// </summary>
        private HashSet<string> properties;

        /// <summary>
        /// Create new instance of <see cref="Search.SelectablePropertyList{T}"/>
        /// </summary>
        internal SelectablePropertyList(Type type)
        {
            this.properties = new HashSet<string>();

            foreach (PropertyInfo property in type.GetProperties((BindingFlags.Public | BindingFlags.Instance)))
            {
                this.properties.Add(property.Name);
                this.Count++;
            }
        }

        /// <summary>
        /// Count of the properties.
        /// </summary>
        internal int Count { get; private set; }

        /// <summary>
        /// Check if property exist on entity.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        internal bool ContainsProperty(string propertyName)
        {
            // case doesn't matter.
            foreach (string property in this.properties)
            {
                if (property.Equals(propertyName, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }

            return false;
        }
    }
}