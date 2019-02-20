namespace Microsoft.RestServices.Exchange
{
    using System;
    using System.Collections.Specialized;

    /// <summary>
    /// Collection property definition.
    /// </summary>
    internal class CollectionPropertyDefinition : PropertyDefinition
    {
        public CollectionPropertyDefinition(string name, Type type) 
            : base(name, type)
        {
        }

        /// <summary>
        /// Observable collection.
        /// </summary>
        public INotifyCollectionChanged ObservableCollection { get; private set; }

        /// <summary>
        /// Register change listener.
        /// </summary>
        /// <param name="observableCollection">Observable collection.</param>
        public void RegisterChangeListener(INotifyCollectionChanged observableCollection)
        {
            this.ObservableCollection = observableCollection;
            this.ObservableCollection.CollectionChanged += CollectionChanged;
        }

        /// <summary>
        /// Collection changed.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Arguments.</param>
        private void CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            this.Changed = true;
        }
    }
}
