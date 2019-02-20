namespace Microsoft.RestServices.Exchange
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// View base for Search requests.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class ViewBase
    {
        /// <summary>
        /// Extended property filter collection.
        /// </summary>
        private SearchFilter.SearchFilterCollection extendedPropertyCollection;

        /// <summary>
        /// Queries.
        /// </summary>
        private List<IQuery> queries;

        /// <summary>
        /// Property set.
        /// </summary>
        private SelectablePropertyList selectablePropertyList;

        /// <summary>
        /// Select properties.
        /// </summary>
        private List<string> selectProperties;

        /// <summary>
        /// Create new instance of <see cref="Search.ViewBase{T}"/>
        /// </summary>
        /// <param name="pageSize">Page size.</param>
        protected ViewBase(int pageSize, Type type)
            : this(pageSize, 0, type)
        {
        }

        /// <summary>
        /// Create new instance of <see cref="ViewBase{T}"/>.
        /// </summary>
        /// <param name="pageSize">Page size.</param>
        /// <param name="offset">Offset.</param>
        protected ViewBase(int pageSize, int offset, Type type)
        {
            ArgumentValidator.ThrowIfNull(type, nameof(type));
            this.PageQuery = new PageQuery(offset, pageSize);
            this.queries = new List<IQuery>();
            this.queries.Add(this.PageQuery);
            this.selectablePropertyList = new SelectablePropertyList(type);
            this.selectProperties = new List<string>();
            this.Type = type;
            this.extendedPropertyCollection = new SearchFilter.SearchFilterCollection(FilterOperator.or);

            this.ExpandProperties = new List<string>();
        }

        /// <summary>
        /// Type view is holding.
        /// </summary>
        internal Type Type { get; }

        /// <summary>
        /// Create filter with select.
        /// </summary>
        private bool FilterWithSelect
        {
            get { return this.selectProperties.Count > 0; }
        }

        /// <summary>
        /// Expand filter.
        /// </summary>
        private bool ExpandFilter
        {
            get { return this.ExpandProperties.Count > 0; }
        }

        /// <summary>
        /// Page query.
        /// </summary>
        private IPageQuery PageQuery { get; }

        /// <summary>
        /// Expand properties.
        /// </summary>
        protected List<string> ExpandProperties { get; }

        /// <summary>
        /// Page size.
        /// </summary>
        public int PageSize
        {
            get { return this.PageQuery.PageSize; }
            set { this.PageQuery.PageSize = value; }
        }

        public int Offset
        {
            get { return this.PageQuery.Offset; }
            set { this.PageQuery.Offset = value; }
        }

        /// <summary>
        /// Get view query.
        /// </summary>
        /// <returns></returns>
        public virtual IQuery ViewQuery
        {
            get
            {
                CompositeQuery compositeQuery = new CompositeQuery(new IQuery[] { this.PageQuery });
                if (this.FilterWithSelect)
                {
                    compositeQuery.Add(new SelectQuery(this.selectProperties.ToArray()));
                }

                string expandFilter = string.Empty;
                if (this.ExpandFilter)
                {
                    expandFilter = string.Join(",", this.ExpandProperties);
                }

                if (!this.extendedPropertyCollection.CollectionEmpty)
                {
                    string extendedPropertyFilter = $"singleValueExtendedProperties({this.extendedPropertyCollection.Query})";
                    if (string.IsNullOrEmpty(expandFilter))
                    {
                        expandFilter = extendedPropertyFilter;
                    }
                    else
                    {
                        expandFilter = $"{expandFilter},{extendedPropertyFilter}";
                    }
                }

                if (!string.IsNullOrEmpty(expandFilter))
                {
                    compositeQuery.Add(new ExpandQuery(expandFilter));
                }

                return compositeQuery;
            }
        }

        /// <summary>
        /// Returns list of properties to be included in request. If list empty it will include all properties of entity.
        /// </summary>
        public IReadOnlyCollection<string> SelectProperties
        {
            get { return this.selectProperties.AsReadOnly(); }
        }

        /// <summary>
        /// Include set of properties in the query.
        /// </summary>
        /// <param name="properties">Properties to include in request.</param>
        public void SelectProperty(string[] properties)
        {
            ArgumentValidator.ThrowIfNullOrEmptyArray(properties, nameof(properties));
            for (int i = 0; i < properties.Length; i++)
            {
                this.SelectProperty(properties[i]);
            }
        }

        /// <summary>
        /// Select particular property to be included in request.
        /// </summary>
        /// <param name="propertyName">Property name.</param>
        public void SelectProperty(string propertyName)
        {
            if (!this.selectablePropertyList.ContainsProperty(propertyName))
            {
                throw new ArgumentException($"Property '{propertyName}' doesn't exist for entity.");
            }

            if (!this.PropertyInTheList(propertyName))
            {
                this.selectProperties.Add(propertyName);
            }
        }

        /// <summary>
        /// Select single value extended property.
        /// </summary>
        /// <param name="extendedProperty">Extended property.</param>
        public void SelectProperty(ExtendedPropertyDefinition extendedProperty)
        {
            ArgumentValidator.ThrowIfNull(extendedProperty, nameof(extendedProperty));

            SearchFilter filter = new SearchFilter.IsEqualTo(
                "Id", $"{extendedProperty.Definition}");

            this.extendedPropertyCollection.AddFilter(filter);
        }

        /// <summary>
        /// Test if property within the list.
        /// </summary>
        /// <param name="propertyName">Property name.</param>
        /// <returns></returns>
        private bool PropertyInTheList(string propertyName)
        {
            if (this.selectProperties.Count == 0)
            {
                return false;
            }

            foreach (string property in this.selectProperties)
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
