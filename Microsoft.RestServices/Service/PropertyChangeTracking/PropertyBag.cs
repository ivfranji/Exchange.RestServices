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
        private Dictionary<PropertyDefinition, object> propertyValue;

        /// <summary>
        /// List of properties.
        /// </summary>
        private Dictionary<string, PropertyDefinition> propertyList;

        /// <summary>
        /// Type property bag is holding.
        /// </summary>
        private Type type;

        /// <summary>
        /// Create new instance of <see cref="PropertyBag"/>.
        /// </summary>
        /// <param name="type">Type.</param>
        public PropertyBag(Type type)
        {
            this.type = type;
            this.IsNew = false;
            this.InitializeBag();
        }

        /// <summary>
        /// Indicate bag is new.
        /// </summary>
        internal bool IsNew { get; private set; }

        /// <summary>
        /// Indexer for pulling out values.
        /// </summary>
        /// <param name="key">Key.</param>
        /// <returns></returns>
        public object this[string key]
        {
            get
            {
                if (this.propertyList.ContainsKey(key))
                {
                    return this.propertyValue[this.propertyList[key]];
                }

                throw new KeyNotFoundException(nameof(key));
            }

            set
            {
                if (this.propertyList.ContainsKey(key))
                {
                    PropertyDefinition def = this.propertyList[key];
                    if (value != null)
                    {
                        Type valueType = value.GetType();
                        if (def.IsList && PropertyDefinition.IsGenericList(valueType))
                        {
                            this.InitializeCollectionProperty(
                                def,
                                value);
                        }
                        else if (def.Type != valueType)
                        {
                            throw new InvalidOperationException("Attempted to store wrong type to the dictionary.");
                        }
                        else
                        {
                            this.propertyValue[def] = value;
                        }
                    }
                    else
                    {
                        if (def.IsValueType)
                        {
                            this.propertyValue[def] = Activator.CreateInstance(def.Type);
                        }
                        else
                        {
                            this.propertyValue[def] = null;
                        }
                    }

                    def.Changed = true;
                }
                else
                {
                    throw new KeyNotFoundException(nameof(key));
                }
            }
        }

        /// <summary>
        /// Clear property bag.
        /// </summary>
        public void Clear()
        {
            this.InitializeBag();
        }

        /// <summary>
        /// Indicate if property changed.
        /// </summary>
        /// <param name="key">Property.</param>
        /// <returns></returns>
        internal bool IsPropertyChanged(string key)
        {
            PropertyDefinition def = this.propertyList[key];
            return def.Changed;
        }

        /// <summary>
        /// Reset change tracking.
        /// </summary>
        internal void ResetChangeTracking()
        {
            foreach (string key in this.propertyList.Keys)
            {
                this.propertyList[key].Changed = false;
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

            foreach (string property in this.propertyList.Keys)
            {
                if (this.propertyList[property].Changed)
                {
                    changedProperties.Add(property);
                }
            }

            return changedProperties;
        }

        /// <summary>
        /// Initialize bag.
        /// </summary>
        private void InitializeBag()
        {
            this.propertyValue = new Dictionary<PropertyDefinition, object>();
            this.propertyList = new Dictionary<string, PropertyDefinition>();

            PropertyInfo[] properties = this.type.GetProperties((BindingFlags.Public | BindingFlags.Instance));
            foreach (PropertyInfo propertyInfo in properties)
            {
                PropertyDefinition def = new PropertyDefinition(propertyInfo.Name, propertyInfo.PropertyType);
                if (def.IsValueType)
                {
                    this.propertyValue[def] = Activator.CreateInstance(def.Type);
                }
                else if (def.IsList)
                {
                    def = new CollectionPropertyDefinition(propertyInfo.Name, propertyInfo.PropertyType);
                    this.InitializeCollectionProperty(
                        def, 
                        null);
                }
                else
                {
                    this.propertyValue[def] = null;
                }

                this.propertyList.Add(def.Name, def);
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
                this.propertyValue[def] = Activator.CreateInstance(
                    constructedObservableCollection, 
                    value);
            }
            else
            {
                this.propertyValue[def] = Activator.CreateInstance(constructedObservableCollection);
            }
            
            INotifyCollectionChanged notifyCollectionChanged = (INotifyCollectionChanged)this.propertyValue[def];
            ((CollectionPropertyDefinition)def).RegisterChangeListener(notifyCollectionChanged);
        }
    }
}
