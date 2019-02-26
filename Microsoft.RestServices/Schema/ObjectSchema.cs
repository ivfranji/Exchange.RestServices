namespace Microsoft.RestServices.Exchange
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    /// <summary>
    /// Defines schema for an object.
    /// </summary>
    public abstract class ObjectSchema
    {
        /// <summary>
        /// Properties.
        /// </summary>
        private Dictionary<string, PropertyDefinition> properties;

        /// <summary>
        /// Create new instance of <see cref="ObjectSchema"/>
        /// </summary>
        protected ObjectSchema()
        {
            this.properties = new Dictionary<string, PropertyDefinition>();
            Type schemaType = this.GetType();

            foreach (FieldInfo fieldInfo in schemaType.GetFields(
                BindingFlags.Static | 
                BindingFlags.Public | 
                BindingFlags.FlattenHierarchy))
            {
                PropertyDefinition propertyDefinition = fieldInfo.GetValue(null) as PropertyDefinition;
                if (null != propertyDefinition)
                {
                    this.properties.Add(fieldInfo.Name, propertyDefinition);
                }
            }
        }

        /// <summary>
        /// Indexer.
        /// </summary>
        /// <param name="key">Key.</param>
        /// <returns></returns>
        public PropertyDefinition this[string key]
        {
            get
            {
                if (this.properties.ContainsKey(key))
                {
                    return this.properties[key];
                }

                throw new KeyNotFoundException(key);
            }
        }

        /// <summary>
        /// Property definition values.
        /// </summary>
        internal IEnumerable<PropertyDefinition> Values
        {
            get { return this.properties.Values; }
        }

        /// <summary>
        /// Property definition keys.
        /// </summary>
        internal IEnumerable<string> Keys
        {
            get { return this.properties.Keys; }
        }
    }
}