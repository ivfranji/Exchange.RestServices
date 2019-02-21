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
        protected ViewBase(int pageSize, Type type, PropertySet propertySet)
            : this(pageSize, 0, type, propertySet)
        {
        }

        /// <summary>
        /// Create new instance of <see cref="ViewBase{T}"/>.
        /// </summary>
        /// <param name="pageSize">Page size.</param>
        /// <param name="offset">Offset.</param>
        protected ViewBase(int pageSize, int offset, Type type, PropertySet propertySet)
        {
            ArgumentValidator.ThrowIfNull(type, nameof(type));
            this.PageQuery = new PageQuery(offset, pageSize);
            this.queries = new List<IQuery>();
            this.queries.Add(this.PageQuery);
            this.selectablePropertyList = new SelectablePropertyList(type);
            this.selectProperties = new List<string>();
            this.Type = type;
            this.PropertySet = propertySet;
            this.ExpandProperties = new List<string>();
        }

        /// <summary>
        /// Type view is holding.
        /// </summary>
        internal Type Type { get; }

        /// <summary>
        /// Property set.
        /// </summary>
        public PropertySet PropertySet { get; }

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
                if (this.PropertySet.Properties != null)
                {
                    compositeQuery.Add(this.PropertySet.Properties);
                }

                ExpandQuery expandQuery = null;
                if (this.ExpandFilter)
                {
                    expandQuery = new ExpandQuery(string.Join(",", this.ExpandProperties));
                }

                if (this.PropertySet.ExpandQuery != null)
                {
                    expandQuery = new ExpandQuery(
                        expandQuery, 
                        this.PropertySet.ExpandQuery);
                }

                if (expandQuery != null)
                {
                    compositeQuery.Add(expandQuery);
                }

                return compositeQuery;
            }
        }
    }
}
