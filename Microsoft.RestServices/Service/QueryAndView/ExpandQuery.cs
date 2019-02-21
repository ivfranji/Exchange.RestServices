namespace Microsoft.RestServices.Exchange
{
    using System.Text;

    /// <summary>
    /// Expand query.
    /// </summary>
    public class ExpandQuery : IExpandQuery
    {
        /// <summary>
        /// Expand prefix.
        /// </summary>
        private const string ExpandPrefix = "$expand=";

        /// <summary>
        /// Create new instance of <see cref="IExpandQuery"/>
        /// </summary>
        /// <param name="expandObject">Object to expand.</param>
        public ExpandQuery(string expandObject)
        {
            ArgumentValidator.ThrowIfNullOrEmpty(
                expandObject, 
                nameof(expandObject));

            this.ExpandObject = expandObject;
        }

        /// <summary>
        /// Merged list of expanded queries.
        /// </summary>
        /// <param name="expandQueries"></param>
        public ExpandQuery(params IExpandQuery[] expandQueries)
        {
            ArgumentValidator.ThrowIfNullOrEmptyArray(expandQueries, nameof(expandQueries));
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < expandQueries.Length; i++)
            {
                if (expandQueries[i] != null)
                {
                    if (i + 1 == expandQueries.Length)
                    {
                        sb.Append(expandQueries[i].ExpandObject);
                    }
                    else
                    {
                        sb.AppendFormat("{0},", expandQueries[i].ExpandObject);
                    }
                }
            }

            this.ExpandObject = sb.ToString();
        }

        /// <summary>
        /// Create new instance of <see cref="IExpandQuery"/>
        /// </summary>
        protected ExpandQuery()
        {
        }

        /// <inheritdoc cref="IQuery.Query"/>
        public string Query
        {
            get { return $"{ExpandQuery.ExpandPrefix}{this.ExpandObject}"; }
        }

        /// <inheritdoc cref="IExpandQuery.ExpandObject"/>
        public string ExpandObject { get; protected set; }
    }
}