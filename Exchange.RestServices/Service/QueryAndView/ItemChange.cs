namespace Exchange.RestServices
{
    using Microsoft.OutlookServices;

    /// <summary>
    /// Contains sync item with corresponding change type.
    /// </summary>
    /// <typeparam name="T">Outlook item.</typeparam>
    public class ItemChange<T> where T : Entity
    {
        /// <summary>
        /// Create new instance of <see cref="ItemChange{T}"/>.
        /// </summary>
        /// <param name="item">Outlook item.</param>
        internal ItemChange(T item)
        {
            this.Item = item;

            // Not ideal solution, but as change type is not communicated
            // to the caller this is only thing that can be done. Within
            // property set we are ensuring that required number of properties
            // is always queried.
            if (this.IsDeleteChange())
            {
                this.ChangeType = SyncChangeType.Deleted;
            }
            else if (this.IsReadChange())
            {
                this.ChangeType = SyncChangeType.ReadFlagChanged;
            }
            else
            {
                this.ChangeType = SyncChangeType.Created;
            }
        }

        /// <summary>
        /// Outlook item.
        /// </summary>
        public T Item { get; }

        /// <summary>
        /// Change type.
        /// </summary>
        public SyncChangeType ChangeType { get; }

        /// <summary>
        /// Indicate this is delete change.
        /// </summary>
        /// <returns></returns>
        private bool IsDeleteChange()
        {
            // Only Id will be available. 
            return this.Item.GetChangedPropertyNames().Count == 1;
        }

        /// <summary>
        /// Indicate if change is read / Unread.
        /// </summary>
        /// <returns></returns>
        private bool IsReadChange()
        {
            // 3 properties will be listed - Id, IsRead and ParentFolderId
            return this.Item.GetChangedPropertyNames().Count == 3;
        }
    }

    /// <summary>
    /// Sync change type.
    /// </summary>
    public enum SyncChangeType
    {
        /// <summary>
        /// Item created.
        /// </summary>
        Created,

        /// <summary>
        /// Item deleted.
        /// </summary>
        Deleted,

        /// <summary>
        /// Read flag changed.
        /// </summary>
        ReadFlagChanged
    }
}
