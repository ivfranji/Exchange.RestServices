namespace Microsoft.RestServices.Exchange
{
    using System;

    /// <summary>
    /// Base class for sync requests.
    /// </summary>
    /// <typeparam name="T">Type of the sync. Message or Folder.</typeparam>
    internal class SyncRequestBase<T> : GetRequestBase<T>
    {
        /// <summary>
        /// Prefer header.
        /// </summary>
        private const string PreferHeaderName = "Prefer";

        /// <summary>
        /// Sync query.
        /// </summary>
        private ISyncQuery syncQuery;

        /// <summary>
        /// Create new instance of <see cref="SyncRequestBase{T}"/>
        /// </summary>
        /// <param name="exchangeService">Exchange service.</param>
        /// <param name="syncQuery">Sync query.</param>
        internal SyncRequestBase(ExchangeService exchangeService, ISyncQuery syncQuery)
            : base(exchangeService)
        {
            ArgumentValidator.ThrowIfNull(syncQuery, nameof(syncQuery));
            this.syncQuery = syncQuery;
        }

        /// <summary>
        /// Create new instance of <see cref="SyncRequestBase{T}"/>
        /// </summary>
        /// <param name="exchangeService">Exchange service.</param>
        /// <param name="syncQuery">Sync query.</param>
        internal SyncRequestBase(ExchangeService exchangeService, ISyncQuery syncQuery, Action<HttpRestUrl> httpRestUrlPreProcess)
            : base(exchangeService, httpRestUrlPreProcess)
        {
            ArgumentValidator.ThrowIfNull(syncQuery, nameof(syncQuery));
            this.syncQuery = syncQuery;
        }

        /// <summary>
        /// Needs to be sealed to prevent potential removing preferences in child classes.
        /// </summary>
        /// <param name="httpWebRequest">Web request to preprocess.</param>
        protected sealed override void PreProcessHttpWebRequest(IHttpWebRequest httpWebRequest)
        {
            base.PreProcessHttpWebRequest(httpWebRequest);
            httpWebRequest.Headers.Add(
                SyncRequestBase<T>.PreferHeaderName, 
                this.syncQuery.Preferences);
        }
    }
}
