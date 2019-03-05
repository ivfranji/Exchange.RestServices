namespace Exchange.RestServices
{
    /// <summary>
    /// Select and page composite query.
    /// </summary>
    internal class SelectAndPageQuery : CompositeQuery
    {
        /// <summary>
        /// Create new instance of <see cref="SelectAndPageQuery"/>
        /// </summary>
        /// <param name="selectQuery">Select query.</param>
        /// <param name="pageQuery">Page query.</param>
        public SelectAndPageQuery(ISelectQuery selectQuery, IPageQuery pageQuery)
            : base(new IQuery[] {selectQuery, pageQuery})
        {
        }
    }
}