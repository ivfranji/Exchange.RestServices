namespace Microsoft.RestServices.Exchange
{
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Composite query.
    /// </summary>
    internal class CompositeQuery : IQuery
    {
        /// <summary>
        /// Queries.
        /// </summary>
        private List<IQuery> queries;

        /// <summary>
        /// Create new instance of <see cref="CompositeQuery"/>.
        /// </summary>
        /// <param name="queries">Queries.</param>
        internal CompositeQuery(IQuery[] queries)
        {
            ArgumentValidator.ThrowIfNullOrEmptyArray(queries, nameof(queries));
            this.queries = new List<IQuery>();
            this.queries.AddRange(queries);
        }

        /// <summary>
        /// Composite query string.
        /// </summary>
        public string Query
        {
            get
            {
                if (queries.Count == 1)
                {
                    return queries[0].Query;
                }

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < this.queries.Count; i++)
                {
                    if (i + 1 == this.queries.Count)
                    {
                        sb.Append(this.queries[i].Query);
                    }
                    else
                    {
                        sb.AppendFormat("{0}&", this.queries[i].Query);
                    }
                }

                return sb.ToString();
            }
        }

        /// <summary>
        /// Add query to the list.
        /// </summary>
        /// <param name="query"></param>
        internal void Add(IQuery query)
        {
            if (null != query)
            {
                this.queries.Add(query);
            }
        }
    }
}