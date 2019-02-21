namespace Microsoft.RestServices.Exchange
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Graph;

    /// <summary>
    /// Property set.
    /// </summary>
    public abstract class PropertySet
    {
        /// <summary>
        /// List of properties supported by this type.
        /// </summary>
        private SelectablePropertyList selectablePropertyList;

        /// <summary>
        /// Collection holding single value extended properties.
        /// </summary>
        private SearchFilter.SearchFilterCollection singleValueExtendedProperties;

        /// <summary>
        /// Collection holding multi value extended properties.
        /// </summary>
        private SearchFilter.SearchFilterCollection multiValueExtendedProperties;

        /// <summary>
        /// List holding added properties.
        /// </summary>
        private List<string> addedProperties;

        /// <summary>
        /// Create new instance of <see cref="PropertySet{T}"/>
        /// </summary>
        protected PropertySet(Type type)
        {
            ArgumentValidator.ThrowIfNull(type, nameof(type));
            this.Type = type;
            this.multiValueExtendedProperties = new SearchFilter.SearchFilterCollection(FilterOperator.or);
            this.singleValueExtendedProperties = new SearchFilter.SearchFilterCollection(FilterOperator.or);
            this.selectablePropertyList = new SelectablePropertyList(type);
            this.addedProperties = new List<string>();

            // Ensuring that, in case of selecting particular properties
            // we at least have 'first class properties' and ones that
            // can help us determine sync change.
            this.FirstClassProperties = new List<string>();
            this.FirstClassProperties.Add(nameof(Entity.Id));
        }

        /// <summary>
        /// Type this set is holding.
        /// </summary>
        internal Type Type { get; }

        /// <summary>
        /// First class properties.
        /// </summary>
        internal List<string> FirstClassProperties { get; }

        /// <summary>
        /// Selected properties. Returns null if no properties selected
        /// which indicates all properties should be returned.
        /// </summary>
        internal ISelectQuery Properties
        {
            get
            {
                if (this.addedProperties.Count > 0)
                {
                    return new SelectQuery(this.addedProperties.ToArray());
                }

                return null;
            }
        }

        /// <summary>
        /// Expand query.
        /// </summary>
        internal IExpandQuery ExpandQuery
        {
            get
            {
                if (!this.singleValueExtendedProperties.CollectionEmpty && !this.multiValueExtendedProperties.CollectionEmpty)
                {
                    ExpandExtendedPropertyQuery singleValue = new ExpandExtendedPropertyQuery(
                        this.singleValueExtendedProperties, 
                        PropertyValueType.SingleValueExtendedProperties);

                    ExpandExtendedPropertyQuery multiValue = new ExpandExtendedPropertyQuery(
                        this.multiValueExtendedProperties,
                        PropertyValueType.MultiValueExtendedProperties);

                    return new ExpandQuery(
                        singleValue, 
                        multiValue);
                }

                else if (!this.singleValueExtendedProperties.CollectionEmpty)
                {
                    return new ExpandExtendedPropertyQuery(
                        this.singleValueExtendedProperties,
                        PropertyValueType.SingleValueExtendedProperties);
                }

                else if (!this.multiValueExtendedProperties.CollectionEmpty)
                {
                    return new ExpandExtendedPropertyQuery(
                        this.multiValueExtendedProperties,
                        PropertyValueType.MultiValueExtendedProperties);
                }

                return null;
            }
        }

        /// <summary>
        /// Add property to the collection.
        /// </summary>
        /// <param name="property"></param>
        public void Add(string property)
        {
            this.AddProperty(property);
        }

        /// <summary>
        /// Add array of properties to the collection.
        /// </summary>
        /// <param name="properties"></param>
        public void Add(string[] properties)
        {
            this.AddProperties(properties);
        }

        /// <summary>
        /// Add extended property to the list.
        /// </summary>
        /// <param name="extendedProperty"></param>
        public void Add(ExtendedPropertyDefinition extendedProperty)
        {
            ArgumentValidator.ThrowIfNull(extendedProperty, nameof(extendedProperty));
            switch (extendedProperty.PropertyValueType)
            {
                case PropertyValueType.SingleValueExtendedProperties:
                    this.singleValueExtendedProperties.AddFilter(new SearchFilter.IsEqualTo(
                        "Id",
                        extendedProperty.Definition));
                    break;
                case PropertyValueType.MultiValueExtendedProperties:
                    this.multiValueExtendedProperties.AddFilter(new SearchFilter.IsEqualTo(
                        "Id",
                        extendedProperty.Definition));
                    break;
            }
        }

        /// <summary>
        /// Adds property to the collection.
        /// </summary>
        /// <param name="propertyName">Property name.</param>
        internal void AddProperty(string propertyName)
        {
            ArgumentValidator.ThrowIfNullOrEmpty(propertyName, nameof(propertyName));
            if (!this.selectablePropertyList.ContainsProperty(propertyName))
            {
                throw new ArgumentException(
                    $"Property '{propertyName}' cannot be added to the collection as it is not part of the schema.");
            }

            if (this.addedProperties.Count < this.FirstClassProperties.Count)
            {
                // we only add first class properties upon first request
                // if addedProperties are empty, all properties will be
                // returned, but if not, we need to make sure that basic
                // set of properties are there.
                this.addedProperties.AddRange(this.FirstClassProperties);
            }

            if (!this.PropertyInTheList(propertyName))
            {
                this.addedProperties.Add(propertyName);
            }
        }

        /// <summary>
        /// Add properties to the collection.
        /// </summary>
        /// <param name="properties">Properties to add.</param>
        internal void AddProperties(string[] properties)
        {
            ArgumentValidator.ThrowIfNullOrEmptyArray(properties, nameof(properties));
            for (int i = 0; i < properties.Length; i++)
            {
                this.AddProperty(properties[i]);
            }
        }

        /// <summary>
        /// Test if property already in the list.
        /// </summary>
        /// <param name="propertyName">Property name.</param>
        /// <returns></returns>
        private bool PropertyInTheList(string propertyName)
        {
            if (this.addedProperties.Count == 0)
            {
                return false;
            }

            foreach (string property in this.addedProperties)
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