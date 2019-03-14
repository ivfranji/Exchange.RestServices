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
        private EntityResponseCollection<TItem> entityResponseCollection;

        /// <summary>
        /// Create new instance of <see cref="FindResults{TItem}"/>.
        /// </summary>
        /// <param name="entityResponseCollection">Response collection.</param>
        protected FindResults(EntityResponseCollection<TItem> entityResponseCollection)
        {
            if (entityResponseCollection == null)
            {
                // to prevent null refs.
                this.entityResponseCollection = new EntityResponseCollection<TItem>();
                this.entityResponseCollection.Value = new List<TItem>();
            }
            else
            {
                this.entityResponseCollection = entityResponseCollection;
            }

            if (null == this.entityResponseCollection.Value)
            {
                // prevent nullrefs
                this.entityResponseCollection.Value = new List<TItem>();
            }
        }

        /// <summary>
        /// Total count.
        /// </summary>
        public int TotalCount
        {
            get { return this.entityResponseCollection.Value.Count; }
        }

        /// <summary>
        /// Items.
        /// </summary>
        public List<TItem> Items
        {
            get { return this.entityResponseCollection.Value; }
        }

        /// <summary>
        /// More available. 
        /// </summary>
        public bool MoreAvailable
        {
            get { return this.entityResponseCollection.MoreAvailable; }
        }

        /// <summary>
        /// Get enumerator.
        /// </summary>
        /// <returns></returns>
        public IEnumerator<TItem> GetEnumerator()
        {
            return this.entityResponseCollection.Value.GetEnumerator();
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
        /// <param name="entityResponseCollection">Response collection.</param>
        protected ChangeResults(EntityResponseCollection<TItem> entityResponseCollection)
        {
            // In case of nulls we want to make sure null ref isn't thrown.
            if (entityResponseCollection == null)
            {
                entityResponseCollection = new EntityResponseCollection<TItem>();
                entityResponseCollection.Value = new List<TItem>(0);
            }

            this.changeItems = new List<ItemChange<TItem>>(entityResponseCollection.Value.Count);
            foreach (TItem item in entityResponseCollection.Value)
            {
                this.changeItems.Add(new ItemChange<TItem>(item));
            }

            this.MoreAvailable = entityResponseCollection.MoreAvailable;
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