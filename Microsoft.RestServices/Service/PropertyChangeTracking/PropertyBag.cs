namespace Microsoft.RestServices.Exchange
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.Reflection;

    /// <summary>
    /// Property bag.
    /// </summary>
    public class PropertyBag : IPropertyChangeTracking
    {
        /// <summary>
        /// List of properties linked with values.
        /// </summary>
        private Dictionary<PropertyDefinition, ObjectChangeTracking> propertyValue;

        /// <summary>
        /// Type property bag is holding.
        /// </summary>
        private Type type;

        /// <summary>
        /// Object schema.
        /// </summary>
        private ObjectSchema objectSchema;

        ///// <summary>
        ///// Create new instance of <see cref="PropertyBag"/>.
        ///// </summary>
        ///// <param name="type">Type.</param>
        //internal PropertyBag(Type type)
        //{
        //    this.type = type;
        //    this.IsNew = false;
        //    this.InitializeBag();
        //}

        internal PropertyBag(ObjectSchema schema)
        {
            this.objectSchema = schema;
            this.InitializeBag(this.objectSchema);
            this.IsNew = false;
        }

        /// <summary>
        /// Indicate bag is new.
        /// </summary>
        internal bool IsNew { get; private set; }

        /// <summary>
        /// Get value based on property definition.
        /// </summary>
        /// <param name="key">Property definition.</param>
        /// <returns></returns>
        public object this[PropertyDefinition key]
        {
            get
            {
                if (this.propertyValue.ContainsKey(key))
                {
                    return this.propertyValue[key].Value;
                }

                throw new KeyNotFoundException(nameof(key));
            }
            set
            {
                if (this.propertyValue.ContainsKey(key))
                {
                    if (value != null)
                    {
                        Type valueType = value.GetType();
                        if (key.IsList && PropertyDefinition.IsGenericList(valueType))
                        {
                            this.InitializeCollectionProperty(
                                key,
                                value);

                            this.propertyValue[key].Changed = true;
                        }
                        else if (key.Type != valueType)
                        {
                            throw new InvalidOperationException("Attempted to store wrong type to the dictionary.");
                        }
                        else
                        {
                            this.propertyValue[key].Value = value;
                        }
                    }
                    else
                    {
                        this.propertyValue[key].Value = key.DefaultValue;
                    }
                }
                else
                {
                    throw new KeyNotFoundException(nameof(key));
                }
            }
        }

        /// <summary>
        /// Indexer for pulling out values.
        /// </summary>
        /// <param name="key">Key.</param>
        /// <returns></returns>
        public object this[string key]
        {
            get
            {
                return this[this.GetPropertyDefinitionByName(key)];
            }

            set
            {
                this[this.GetPropertyDefinitionByName(key)] = value;
            }
        }

        /// <summary>
        /// Clear property bag.
        /// </summary>
        public void Clear()
        {
            this.InitializeBag(this.objectSchema);
        }

        /// <summary>
        /// Indicate if property changed.
        /// </summary>
        /// <param name="key">Property.</param>
        /// <returns></returns>
        internal bool IsPropertyChanged(string key)
        {
            PropertyDefinition def = this.GetPropertyDefinitionByName(key);
            return this.propertyValue[def].Changed;
        }

        /// <summary>
        /// Reset change tracking.
        /// </summary>
        internal void ResetChangeTracking()
        {
            //foreach (string key in this.propertyList.Keys)
            //{
            //    this.propertyList[key].Changed = false;
            //}

            foreach ( PropertyDefinition key in this.propertyValue.Keys )
            {
                this.propertyValue[key].Changed = false;
            }
        }

        /// <summary>
        /// Mark property bag as new.
        /// </summary>
        internal void MarkAsNew()
        {
            this.ResetChangeTracking();
            this.IsNew = true;
        }

        /// <summary>
        /// Returns list of changed properties.
        /// </summary>
        /// <returns></returns>
        public IList<string> GetChangedProperties()
        {
            List<string> changedProperties = new List<string>();
            foreach ( PropertyDefinition prop in this.propertyValue.Keys )
            {
                ObjectChangeTracking objectChangeTracking = this.propertyValue[prop];
                if ( objectChangeTracking.Changed )
                {
                    changedProperties.Add(prop.Name);
                }
            }

            return changedProperties;
        }

        /// <summary>
        /// Initialize bag.
        /// </summary>
        private void InitializeBag(ObjectSchema bagSchema)
        {
            this.propertyValue = new Dictionary<PropertyDefinition, ObjectChangeTracking>();
            if (bagSchema != null)
            {
                foreach (string key in bagSchema.Keys)
                {
                    PropertyDefinition def = bagSchema[key];
                    if (def.IsList)
                    {
                        this.InitializeCollectionProperty(
                            def, 
                            null);
                    }
                    else
                    {
                        this.propertyValue[def] = new ObjectChangeTracking(def.DefaultValue);
                    }
                }
            }
            else
            {
                throw new ArgumentNullException(nameof(bagSchema));
            }
        }

        /// <summary>
        /// Initialize collection property and value.
        /// </summary>
        /// <param name="def">Collection property definition.</param>
        /// <param name="value">Value, if null, create empty collection.</param>
        private void InitializeCollectionProperty(PropertyDefinition def, object value)
        {
            Type observableCollectionType = typeof(ObservableCollection<>);
            Type constructedObservableCollection = observableCollectionType.MakeGenericType(def.GetListUnderlyingType());
            if (value != null)
            {
                this.propertyValue[def] = new ObjectChangeTracking(
                    Activator.CreateInstance(
                    constructedObservableCollection,
                    value));
            }
            else
            {
                this.propertyValue[def] = new ObjectChangeTracking(
                    Activator.CreateInstance(constructedObservableCollection));
            }
            
            INotifyCollectionChanged notifyCollectionChanged = (INotifyCollectionChanged)this.propertyValue[def].Value;
            this.propertyValue[def].RegisterListChangeListener(notifyCollectionChanged);
        }

        /// <summary>
        /// Get property definition by name.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        private PropertyDefinition GetPropertyDefinitionByName(string propertyName)
        {
            foreach (KeyValuePair<PropertyDefinition, ObjectChangeTracking> pair in this.propertyValue)
            {
                if ( pair.Key.Name == propertyName )
                {
                    return pair.Key;
                }
            }

            throw new KeyNotFoundException($"Key doesn't exist: '{propertyName}'.");
        }

        /// <summary>
        /// Track object changes.
        /// </summary>
        private class ObjectChangeTracking
        {
            /// <summary>
            /// Object value.
            /// </summary>
            private object value;

            /// <summary>
            /// Observable collection.
            /// </summary>
            private INotifyCollectionChanged CollectionChanged { get; set; }

            /// <summary>
            /// Create new instance of <see cref="ObjectChangeTracking"/>
            /// </summary>
            /// <param name="objectValue">Object value.</param>
            public ObjectChangeTracking(object objectValue)
            {
                this.value = objectValue;
                this.Changed = false;
            }

            /// <summary>
            /// Object value.
            /// </summary>
            public object Value
            {
                get { return this.value; }
                set
                {
                    this.value = value;
                    this.Changed = true;
                }
            }

            /// <summary>
            /// Indicate if object changed.
            /// </summary>
            public bool Changed { get; set; }

            /// <summary>
            /// Register collection changed.
            /// </summary>
            /// <param name="collectionChanged"></param>
            public void RegisterListChangeListener(INotifyCollectionChanged collectionChanged)
            {
                this.CollectionChanged = collectionChanged;
                this.CollectionChanged.CollectionChanged += (sender, args) => { this.Changed = true; };
            }
        }
    }
}
