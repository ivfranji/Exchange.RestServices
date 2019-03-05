namespace Exchange.RestServices
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Reflection;
    using Microsoft.OutlookServices;

    /// <summary>
    /// Property definition.
    /// </summary>
    public class PropertyDefinition
    {
        /// <summary>
        /// Object formatter.
        /// </summary>
        private static FormatterProvider formatterProvider = new FormatterProvider();

        /// <summary>
        /// Create new instance of <see cref="PropertyDefinition"/>.
        /// </summary>
        /// <param name="name">Name of the property.</param>
        /// <param name="type">Type of the property.</param>
        internal PropertyDefinition(string name, Type type)
        {
            this.Name = name;
            this.Type = type;

            if (this.Type.IsValueType)
            {
                this.DefaultValue = Activator.CreateInstance(this.Type);
            }
            else
            {
                this.DefaultValue = null;
            }
        }

        /// <summary>
        /// Create new instance of <see cref="PropertyDefinition"/>
        /// </summary>
        /// <param name="name">Name of the property.</param>
        /// <param name="type">Type of the property.</param>
        /// <param name="defaultValue">Default value.</param>
        internal PropertyDefinition(string name, Type type, object defaultValue)
        {
            this.Name = name;
            this.Type = type;
            this.DefaultValue = defaultValue;
        }

        /// <summary>
        /// Indicate if underlying type implements property change tracking.
        /// </summary>
        internal bool ChangeTrackable
        {
            get { return this.TypeImplementsChangeTracking(this.Type); }
        }

        /// <summary>
        /// List change trackable.
        /// </summary>
        internal bool ListChangeTrackable
        {
            get
            {
                if (!this.IsList)
                {
                    return false;
                }

                Type underlyingListType = this.GetListUnderlyingType();
                return this.TypeImplementsChangeTracking(underlyingListType);
            }
        }

        /// <summary>
        /// Name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Type.
        /// </summary>
        public Type Type { get; }

        /// <summary>
        /// Default value.
        /// </summary>
        public object DefaultValue { get; }

        /// <summary>
        /// Is value type.
        /// </summary>
        public bool IsValueType
        {
            get { return this.Type.IsValueType; }
        }

        /// <summary>
        /// Indicate if type is list.
        /// </summary>
        public bool IsList
        {
            get { return PropertyDefinition.IsGenericList(this.Type); }
        }

        /// <summary>
        /// If property definition is collection, it will return
        /// underlying type of the collection, otherwise null.
        /// </summary>
        /// <returns></returns>
        public Type GetListUnderlyingType()
        {
            if (!this.IsList)
            {
                return null;
            }

            return this.Type.GetGenericArguments()[0];
        }

        /// <summary>
        /// Get hash code.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return this.Name.GetHashCode();
        }

        /// <summary>
        /// To String impl.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this.Name;
        }

        /// <summary>
        /// Create observable collection.
        /// </summary>
        /// <param name="value">Value.</param>
        /// <returns></returns>
        internal object ActivateObservableList(object value)
        {
            return this.ActivateList(this.GetListUnderlyingType(), value);
        }

        /// <summary>
        /// Activate IList.
        /// </summary>
        /// <param name="value">Value of list.</param>
        /// <returns></returns>
        internal IList<object> ActivateIList(object value)
        {
            return (IList<object>) this.ActivateList(typeof(object), value);
        }

        /// <summary>
        /// Format filter.
        /// </summary>
        /// <param name="obj">Object.</param>
        /// <param name="filterOperator">Filter operator.</param>
        /// <returns></returns>
        internal string FormatFilter(object obj, FilterOperator filterOperator)
        {
            IFilterFormatter formatter = PropertyDefinition.formatterProvider[this.Type.FullName];
            return formatter.Format(
                obj, 
                filterOperator, 
                this);
        }

        /// <summary>
        /// Validate if formatting between types is supported.
        /// </summary>
        /// <param name="inputObject"></param>
        internal void ValidateFormattingSupportedOrThrow(object inputObject)
        {
            ArgumentValidator.ThrowIfNull(
                inputObject, 
                nameof(inputObject));

            if (inputObject is string || this.Type.IsInstanceOfType(inputObject))
            {
                return;
            }

            throw new ArgumentException(
                $"Cannot format type '{inputObject.GetType().FullName}' with definition '{this.Type.FullName}'.");
        }

        /// <summary>
        /// Get OData type for object.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        internal string GetODataType(object value)
        {
            if (value == null)
            {
                return string.Empty;
            }

            Type valueType = value.GetType();
            if (valueType.IsSubclassOf(typeof(Entity)))
            {
                return $"#{valueType.FullName}";
            }

            return string.Empty;
        }

        /// <summary>
        /// Invokes ResetChangeTracking against property.
        /// </summary>
        /// <param name="obj"></param>
        internal void InvokeResetChangeTrackingOnProperty(object obj)
        {
            if (null == obj)
            {
                return;
            }

            Type objectType = obj.GetType();
            if (!this.IsTypeOrListUnderlyingType(objectType))
            {
                throw new InvalidOperationException($"Object of type '{objectType.FullName}' is not of correct type. Expected '{this.Type.FullName}'.");
            }

            if (this.IsList)
            {
                objectType = this.GetListUnderlyingType();
            }

            this.InvokeResetChangeTracking(
                objectType, 
                obj);
        }

        internal static bool IsGenericList(Type type)
        {
            ArgumentValidator.ThrowIfNull(type, nameof(type));
            foreach (Type typeInterface in type.GetInterfaces())
            {
                if (typeInterface.IsGenericType && typeInterface.GetGenericTypeDefinition() == typeof(ICollection<>))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Type implements change tracking.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private bool TypeImplementsChangeTracking(Type type)
        {
            foreach (Type interfaceType in type.GetInterfaces())
            {
                if (interfaceType == typeof(IPropertyChangeTracking))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Indicate if specified type implements this one or if
        /// it is underlying of the list that this property implements.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private bool IsTypeOrListUnderlyingType(Type type)
        {
            if (type == this.Type || type.IsSubclassOf(this.Type))
            {
                return true;
            }

            if (this.IsList)
            {
                Type listUnderlyingType = this.GetListUnderlyingType();
                return listUnderlyingType == type || type.IsSubclassOf(listUnderlyingType);
            }

            return false;
        }

        /// <summary>
        /// Invoke reset change tracking against object.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="obj"></param>
        private void InvokeResetChangeTracking(Type type, object obj)
        {
            foreach (MethodInfo methodInfo in type.GetMethods((BindingFlags.Instance | BindingFlags.NonPublic)))
            {
                if (methodInfo.Name == "ResetChangeTracking")
                {
                    methodInfo.Invoke(
                        obj,
                        null);
                    break;
                }
            }
        }

        /// <summary>
        /// Activate list.
        /// </summary>
        /// <param name="type">Type of list.</param>
        /// <param name="value">Value of the list.</param>
        /// <returns></returns>
        private object ActivateList(Type type, object value)
        {
            if (!this.IsList)
            {
                throw new InvalidOperationException("Cannot activate observablelist on non-list.");
            }

            Type observableCollectionType = typeof(ObservableCollection<>);
            Type constructedObservableCollection = observableCollectionType.MakeGenericType(type);

            if (null == value)
            {
                return Activator.CreateInstance(constructedObservableCollection);
            }
            else
            {
                return Activator.CreateInstance(
                    constructedObservableCollection,
                    value);
            }
        }
    }
}
