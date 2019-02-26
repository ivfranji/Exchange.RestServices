namespace Microsoft.RestServices.Exchange
{
    using System;
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
        /// Simple propertyname / propertyvalue matching.
        /// </summary>
        public abstract class SimplePropertyMatchingFilter : SearchFilter
        {
            /// <summary>
            /// Date time format.
            /// </summary>
            private const string dateTimeFormat = "yyyy-MM-ddThh:mm:ss";

            /// <summary>
            /// Create new instance of <see cref="SearchFilter.SimplePropertyMatchingFilter"/>
            /// </summary>
            /// <param name="filterOperator">Filter operator.</param>
            /// <param name="propertyValue">Property value.</param>
            /// <param name="propertyDefinition">Property name.</param>
            protected SimplePropertyMatchingFilter(PropertyDefinition propertyDefinition, object propertyValue, FilterOperator filterOperator)
                : base(filterOperator)
            {
                ArgumentValidator.ThrowIfNull(
                    propertyDefinition,
                    nameof(propertyDefinition));

                ArgumentValidator.ThrowIfNull(
                    propertyValue,
                    nameof(propertyValue));

                propertyDefinition.ValidateFormattingSupportedOrThrow(
                    propertyValue);

                this.PropertyDefinition = propertyDefinition;
                this.PropertyValue = propertyValue;
            }

            /// <summary>
            /// Property name.
            /// </summary>
            public PropertyDefinition PropertyDefinition { get; }

            /// <summary>
            /// Property value.
            public object PropertyValue { get; }

            /// <inheritdoc cref="SearchFilter.ToString(StringBuilder)"/>
            protected internal sealed override void ToString(StringBuilder sb)
            {
                sb.Append(
                    this.PropertyDefinition.FormatFilter(
                        this.PropertyValue,
                        this.FilterOperator));
            }
        }

        /// <summary>
        /// Is equal to filter.
        /// </summary>
        public sealed class IsEqualTo : SimplePropertyMatchingFilter
        {
            /// <summary>
            /// Create new instance of <see cref="IsEqualTo"/>.
            /// </summary>
            /// <param name="propertyDefinition">Property name.</param>
            /// <param name="propertyValue">Property value.</param>
            public IsEqualTo(PropertyDefinition propertyDefinition, object propertyValue)
                : base(propertyDefinition, propertyValue, FilterOperator.eq)
            {
            }
        }

        /// <summary>
        /// Is greater than filter.
        /// </summary>
        public sealed class IsGreaterThan : SimplePropertyMatchingFilter
        {
            /// <summary>
            /// Create new instance of <see cref="SearchFilter.IsGreaterThan"/>
            /// </summary>
            /// <param name="propertyDefinition">Property name.</param>
            /// <param name="propertyValue">Property value.</param>
            public IsGreaterThan(PropertyDefinition propertyDefinition, object propertyValue)
                : base(propertyDefinition, propertyValue, FilterOperator.gt)
            {
            }
        }

        /// <summary>
        /// Is greater than or equal to filter.
        /// </summary>
        public sealed class IsGreaterThanOrEqualTo : SimplePropertyMatchingFilter
        {
            /// <summary>
            /// Create new instance of <see cref="SearchFilter.IsGreaterThanOrEqualTo"/>
            /// </summary>
            /// <param name="propertyDefinition">Property name.</param>
            /// <param name="propertyValue">Property value.</param>
            public IsGreaterThanOrEqualTo(PropertyDefinition propertyDefinition, object propertyValue)
                : base(propertyDefinition, propertyValue, FilterOperator.ge)
            {
            }
        }

        /// <summary>
        /// Is less than filter.
        /// </summary>
        public sealed class IsLessThan : SimplePropertyMatchingFilter
        {
            /// <summary>
            /// Create new instance of <see cref="SearchFilter.IsLessThan"/>
            /// </summary>
            /// <param name="propertyDefinition">Property name.</param>
            /// <param name="propertyValue">Property value.</param>
            public IsLessThan(PropertyDefinition propertyDefinition, object propertyValue)
                : base(propertyDefinition, propertyValue, FilterOperator.lt)
            {
            }
        }

        /// <summary>
        /// Is less than or equal to filter.
        /// </summary>
        public sealed class IsLessThanOrEqualTo : SimplePropertyMatchingFilter
        {
            /// <summary>
            /// Create new instance of <see cref="SearchFilter.IsLessThanOrEqualTo"/>
            /// </summary>
            /// <param name="propertyDefinition">Property name.</param>
            /// <param name="propertyValue">Property value.</param>
            public IsLessThanOrEqualTo(PropertyDefinition propertyDefinition, object propertyValue)
                : base(propertyDefinition, propertyValue, FilterOperator.le)
            {
            }
        }

        /// <summary>
        /// Not equal to filter.
        /// </summary>
        public sealed class NotEqualTo : SimplePropertyMatchingFilter
        {
            /// <summary>
            /// Create new instance of <see cref="SearchFilter.NotEqualTo"/>
            /// </summary>
            /// <param name="propertyDefinition">Property name.</param>
            /// <param name="propertyValue">Property value.</param>
            public NotEqualTo(PropertyDefinition propertyDefinition, object propertyValue)
                : base(propertyDefinition, propertyValue, FilterOperator.ne)
            {
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
                // search collection can only have 'or' or 'and' operator
                if (!(filterOperator == FilterOperator.and || filterOperator == FilterOperator.or))
                {
                    throw new ArgumentException(
                        $"Search filter collection can only contain 'and' or 'or' operator. Actual: '{filterOperator}'.");
                }

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