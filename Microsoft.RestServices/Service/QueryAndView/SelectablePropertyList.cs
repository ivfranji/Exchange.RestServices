namespace Microsoft.RestServices.Exchange
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using Microsoft.Graph;

    /// <summary>
    /// Contains list of properties of an entity.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal class SelectablePropertyList
    {
        /// <summary>
        /// List of properties.
        /// </summary>
        private HashSet<string> properties;

        /// <summary>
        /// Create new instance of <see cref="Search.SelectablePropertyList{T}"/>
        /// </summary>
        internal SelectablePropertyList(Type type)
        {
            this.properties = new HashSet<string>();

            foreach (PropertyInfo property in type.GetProperties((BindingFlags.Public | BindingFlags.Instance)))
            {
                this.properties.Add(property.Name);
            }
        }

        /// <summary>
        /// Check if property exist on entity.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        internal bool ContainsProperty(string propertyName)
        {
            // case doesn't matter.
            foreach (string property in this.properties)
            {
                if (property.Equals(propertyName, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }

            return false;
        }
    }

    /// <summary>
    /// Property set.
    /// </summary>
    public abstract class PropertySet
    {
        /// <summary>
        /// List of properties supported by this type.
        /// </summary>
        private SelectablePropertyList selectablePropertyList;

        /// <summary>
        /// List holding added properties.
        /// </summary>
        private List<string> addedProperties;

        /// <summary>
        /// Create new instance of <see cref="PropertySet{T}"/>
        /// </summary>
        protected PropertySet(Type type)
        {
            ArgumentValidator.ThrowIfNull(type, nameof(type));
            this.Type = type;
            this.selectablePropertyList = new SelectablePropertyList(type);
            this.addedProperties = new List<string>();

            // Ensuring that, in case of selecting particular properties
            // we at least have 'first class properties' and ones that
            // can help us determine sync change.
            this.FirstClassProperties = new List<string>();
            this.FirstClassProperties.Add(nameof(Entity.Id));
        }

        /// <summary>
        /// Type this set is holding.
        /// </summary>
        internal Type Type { get; }

        /// <summary>
        /// First class properties.
        /// </summary>
        internal List<string> FirstClassProperties { get; }

        /// <summary>
        /// Selected properties. Returns null if no properties selected
        /// which indicates all properties should be returned.
        /// </summary>
        internal ISelectQuery SelectProperties
        {
            get
            {
                if (this.addedProperties.Count > 0)
                {
                    return new SelectQuery(this.addedProperties.ToArray());
                }

                return null;
            }
        }

        /// <summary>
        /// Adds property to the collection.
        /// </summary>
        /// <param name="propertyName">Property name.</param>
        public void AddProperty(string propertyName)
        {
            ArgumentValidator.ThrowIfNullOrEmpty(propertyName, nameof(propertyName));
            if (!this.selectablePropertyList.ContainsProperty(propertyName))
            {
                throw new ArgumentException(
                    $"Property '{propertyName}' cannot be added to the collection as it is not part of the schema.");
            }

            if (this.addedProperties.Count < this.FirstClassProperties.Count)
            {
                // we only add first class properties upon first request
                // if addedProperties are empty, all properties will be
                // returned, but if not, we need to make sure that basic
                // set of properties are there.
                this.addedProperties.AddRange(this.FirstClassProperties);
            }

            if (!this.addedProperties.Contains(propertyName))
            {

                this.addedProperties.Add(propertyName);
            }
        }

        /// <summary>
        /// Add properties to the collection.
        /// </summary>
        /// <param name="properties">Properties to add.</param>
        public void AddProperties(string[] properties)
        {
            ArgumentValidator.ThrowIfNullOrEmptyArray(properties, nameof(properties));
            for (int i = 0; i < properties.Length; i++)
            {
                this.AddProperty(properties[i]);
            }
        }
    }

    /// <summary>
    /// Message property set.
    /// </summary>
    public class MessagePropertySet : PropertySet
    {
        /// <summary>
        /// Create new instance of <see cref="MessagePropertySet"/>
        /// </summary>
        public MessagePropertySet()
            : base(typeof(Message))
        {
            this.FirstClassProperties.Add(nameof(Message.IsRead));
            this.FirstClassProperties.Add(nameof(Message.Subject));
            this.FirstClassProperties.Add(nameof(Message.ParentFolderId));
        }
    }

    /// <summary>
    /// Folder property set,
    /// </summary>
    public class MailFolderPropertySet : PropertySet
    {
        /// <summary>
        /// Create new instance of <see cref="MailFolderPropertySet"/>
        /// </summary>
        public MailFolderPropertySet()
            : base(typeof(MailFolder))
        {
            this.FirstClassProperties.Add(nameof(MailFolder.ChildFolderCount));
            this.FirstClassProperties.Add(nameof(MailFolder.DisplayName));
            this.FirstClassProperties.Add(nameof(MailFolder.TotalItemCount));
        }
    }
}