namespace Microsoft.RestServices.Exchange
{
    /// <summary>
    /// Filter and page composite query.
    /// </summary>
    internal class FilterAndPageQuery : CompositeQuery
    {
        /// <summary>
        /// Create new instance of <see cref="FilterAndPageQuery"/>
        /// </summary>
        /// <param name="filterQuery">Filter query.</param>
        /// <param name="pageQuery">Page query.</param>
        public FilterAndPageQuery(IFilterQuery filterQuery, IPageQuery pageQuery)
            : base(new IQuery[] {filterQuery, pageQuery})
        {
        }
    }
}