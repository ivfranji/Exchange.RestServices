namespace Microsoft.RestServices.Exchange
{
    using System.Collections.Generic;
    using Microsoft.OutlookServices;
    using Newtonsoft.Json;

    /// <summary>
    /// Contains collection of the elements.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ResponseCollection<T> where T : Entity
    {
        /// <summary>
        /// OData context.
        /// </summary>
        [JsonProperty("@odata.context")]
        internal string ODataContext { get; set; }

        /// <summary>
        /// OData next link.
        /// </summary>
        [JsonProperty("@odata.nextLink")]
        internal string ODataNextLink { get; set; }

        /// <summary>
        /// List of values.
        /// </summary>
        public List<T> Value { get; set; }

        /// <summary>
        /// More available.
        /// </summary>
        public virtual bool MoreAvailable
        {
            get { return !string.IsNullOrEmpty(this.ODataNextLink); }
        }

        /// <summary>
        /// Register service with objects and reset change tracking.
        /// </summary>
        /// <param name="service"></param>
        internal void RegisterServiceAndResetChangeTracking(ExchangeService service, MailboxId mailboxId)
        {
            if (this.Value != null && this.Value.Count > 0)
            {
                foreach (T entity in this.Value)
                {
                    entity.Service = service;
                    entity.MailboxId = mailboxId;
                    entity.ResetChangeTracking();
                }
            }
        }

        /// <summary>
        /// Page size that was used to grab results.
        /// TODO: THis needs to come from REST URL!!!
        /// </summary>
        internal int PageSize { get; set; }

    }

    /// <summary>
    /// Sync response collection.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SyncResponseCollection<T> : ResponseCollection<T> where T : Entity
    {
        /// <summary>
        /// Odata delta link.
        /// </summary>
        [JsonProperty("@odata.deltaLink")]
        internal string ODataDeltaLink { get; set; }

        /// <summary>
        /// With sync response collection, need to make sure that
        /// returned collection is empty before it is marked as sync
        /// done.
        /// </summary>
        public override bool MoreAvailable
        {
            get
            {
                if (!string.IsNullOrEmpty(this.ODataDeltaLink))
                {
                    return this.Value.Count >= this.PageSize;
                }

                return base.MoreAvailable;
            }
        }
    }

    /// <summary>
    /// Sync mail folder response collection.
    /// </summary>
    public class SyncMailFolderResponseCollection : ResponseCollection<MailFolder>
    {
        /// <summary>
        /// Odata delta link.
        /// </summary>
        [JsonProperty("@odata.deltaLink")]
        internal string ODataDeltaLink
        {
            get; set;
        }

        /// <summary>
        /// With folder sync response collection, need to make sure that
        /// returned collection is empty before it is marked as sync
        /// done.
        /// </summary>
        public override bool MoreAvailable
        {
            get
            {
                return !string.IsNullOrEmpty(this.ODataDeltaLink) && this.Value.Count > 0;
            }
        }
    }
}
