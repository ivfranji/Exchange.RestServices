namespace Exchange.RestServices
{
    using System.Collections;
    using System.Collections.Generic;
    using Microsoft.OutlookServices;

    /// <summary>
    /// Class holding find results.
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    public abstract class FindResults<TItem> : IEnumerable<TItem> where TItem : Entity
    {
        /// <summary>
        /// Response collection.
        /// </summary>
        private ResponseCollection<TItem> responseCollection;

        /// <summary>
        /// Create new instance of <see cref="FindResults{TItem}"/>.
        /// </summary>
        /// <param name="responseCollection">Response collection.</param>
        protected FindResults(ResponseCollection<TItem> responseCollection)
        {
            if (responseCollection == null)
            {
                // to prevent null refs.
                this.responseCollection = new ResponseCollection<TItem>();
                this.responseCollection.Value = new List<TItem>();
            }
            else
            {
                this.responseCollection = responseCollection;
            }

            if (null == this.responseCollection.Value)
            {
                // prevent nullrefs
                this.responseCollection.Value = new List<TItem>();
            }
        }

        /// <summary>
        /// Total count.
        /// </summary>
        public int TotalCount
        {
            get { return this.responseCollection.Value.Count; }
        }

        /// <summary>
        /// Items.
        /// </summary>
        public List<TItem> Items
        {
            get { return this.responseCollection.Value; }
        }

        /// <summary>
        /// More available. 
        /// </summary>
        public bool MoreAvailable
        {
            get { return this.responseCollection.MoreAvailable; }
        }

        /// <summary>
        /// Get enumerator.
        /// </summary>
        /// <returns></returns>
        public IEnumerator<TItem> GetEnumerator()
        {
            return this.responseCollection.Value.GetEnumerator();
        }

        /// <summary>
        /// Get enumerator.
        /// </summary>
        /// <returns></returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }

    /// <summary>
    /// Change item collection.
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    public abstract class ChangeResults<TItem> : IEnumerable<ItemChange<TItem>> where TItem : Entity
    {
        /// <summary>
        /// Item change.
        /// </summary>
        private readonly List<ItemChange<TItem>> changeItems;
        
        /// <summary>
        /// Create new instance of <see cref="ChangeResults{TItem}"/>.
        /// </summary>
        /// <param name="responseCollection">Response collection.</param>
        protected ChangeResults(ResponseCollection<TItem> responseCollection)
        {
            // In case of nulls we want to make sure null ref isn't thrown.
            if (responseCollection == null)
            {
                responseCollection = new ResponseCollection<TItem>();
                responseCollection.Value = new List<TItem>(0);
            }

            this.changeItems = new List<ItemChange<TItem>>(responseCollection.Value.Count);
            foreach (TItem item in responseCollection.Value)
            {
                this.changeItems.Add(new ItemChange<TItem>(item));
            }

            this.MoreAvailable = responseCollection.MoreAvailable;
        }

        /// <summary>
        /// Total count.
        /// </summary>
        public int TotalCount
        {
            get { return this.changeItems.Count; }
        }

        /// <summary>
        /// Items.
        /// </summary>
        public List<ItemChange<TItem>> Items
        {
            get { return this.changeItems; }
        }

        /// <summary>
        /// More available. 
        /// </summary>
        public bool MoreAvailable
        {
            get;
            private set;
        }

        /// <summary>
        /// Get enumerator.
        /// </summary>
        /// <returns></returns>
        public IEnumerator<ItemChange<TItem>> GetEnumerator()
        {
            return this.changeItems.GetEnumerator();
        }

        /// <summary>
        /// Get enumerator.
        /// </summary>
        /// <returns></returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}