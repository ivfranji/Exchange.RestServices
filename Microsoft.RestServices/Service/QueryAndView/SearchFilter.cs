namespace Microsoft.RestServices.Exchange
{
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Represents search filter.
    /// </summary>
    public abstract class SearchFilter : IFilterQuery
    {
        /// <summary>
        /// Filter prefix.
        /// </summary>
        private const string FilterPrefix = "$filter=";

        /// <summary>
        /// Create new instance of <see cref="SearchFilter"/>
        /// </summary>
        /// <param name="filterOperator">Filter operator.</param>
        protected SearchFilter(FilterOperator filterOperator)
        {
            this.FilterOperator = filterOperator;
        }

        /// <inheritdoc cref="IQuery.Query"/>
        public string Query
        {
            get
            {
                StringBuilder stringBuilder = new StringBuilder();
                this.ToString(stringBuilder);

                string filter = stringBuilder.ToString();
                if (filter.StartsWith(SearchFilter.FilterPrefix))
                {
                    return filter;
                }

                return $"{SearchFilter.FilterPrefix}{filter}";
            }
        }

        /// <inheritdoc cref="IFilterQuery.FilterOperator"/>
        public FilterOperator FilterOperator { get; }

        #region Filter implementations

        /// <summary>
        /// Is equal to filter.
        /// </summary>
        public sealed class IsEqualTo : SearchFilter
        {
            /// <summary>
            /// Create new instance of <see cref="IsEqualTo"/>.
            /// </summary>
            /// <param name="propertyName">Property name.</param>
            /// <param name="propertyValue">Property value.</param>
            public IsEqualTo(string propertyName, string propertyValue)
                : base(FilterOperator.eq)
            {
                this.PropertyName = propertyName;
                this.PropertyValue = propertyValue;
            }

            /// <summary>
            /// Property name.
            /// </summary>
            public string PropertyName { get; }

            /// <summary>
            /// Property value.
            /// </summary>
            public string PropertyValue { get; }

            /// <inheritdoc cref="SearchFilter.ToString(System.Text.StringBuilder)"/>
            protected internal override void ToString(StringBuilder sb)
            {
                sb.AppendFormat("{0} {1} '{2}'", this.PropertyName, this.FilterOperator, this.PropertyValue);
            }
        }

        /// <summary>
        /// Search filter collection.
        /// </summary>
        public sealed class SearchFilterCollection : SearchFilter
        {
            /// <summary>
            /// Search filter collection.
            /// </summary>
            private List<SearchFilter> searchFilters;

            /// <summary>
            /// 
            /// </summary>
            /// <param name="filterOperator"></param>
            /// <param name="searchFilters"></param>
            public SearchFilterCollection(FilterOperator filterOperator, params SearchFilter[] searchFilters) 
                : base(filterOperator)
            {
                this.searchFilters = new List<SearchFilter>();
                if (null != searchFilters && searchFilters.Length > 0)
                {
                    this.searchFilters.AddRange(searchFilters);
                }
            }

            /// <summary>
            /// Indicate if filter collection is empty.
            /// </summary>
            public bool CollectionEmpty
            {
                get { return this.searchFilters.Count == 0; }
            }

            /// <summary>
            /// Adds filter into collection.
            /// </summary>
            /// <param name="searchFilter"></param>
            public void AddFilter(SearchFilter searchFilter)
            {
                if (null != searchFilter)
                {
                    this.searchFilters.Add(searchFilter);
                }
            }

            /// <inheritdoc cref="SearchFilter.ToString(StringBuilder)"/>
            protected internal override void ToString(StringBuilder sb)
            {
                for (int i = 0; i < this.searchFilters.Count; i++)
                {
                    if (i + 1 == this.searchFilters.Count)
                    {
                        this.searchFilters[i].ToString(sb);
                    }
                    else
                    {
                        this.searchFilters[i].ToString(sb);
                        sb.Append($" {this.FilterOperator} ");
                    }
                }
            }
        }

        #endregion

        /// <summary>
        /// Create filter string.
        /// </summary>
        /// <param name="sb"></param>
        protected internal abstract void ToString(StringBuilder sb);
    }
}