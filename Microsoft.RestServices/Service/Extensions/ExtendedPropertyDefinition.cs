namespace Microsoft.RestServices.Exchange
{
    using System;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// https://docs.microsoft.com/en-us/graph/api/resources/extended-properties-overview?view=graph-rest-beta
    /// </summary>
    public class ExtendedPropertyDefinition
    {
        /// <summary>
        /// {type} {propertyset} Name {name}
        /// </summary>
        /// <param name="type">Type.</param>
        /// <param name="name">Name.</param>
        /// <param name="propertySet">Property set.</param>
        public ExtendedPropertyDefinition(MapiPropertyType type, string name, Guid propertySet)
            : this(type)
        {
            ArgumentValidator.ThrowIfNullOrEmpty(name, nameof(name));
            ArgumentValidator.ThrowIfGuidEmpty(propertySet, nameof(propertySet));

            this.Name = name;
            this.PropertySet = propertySet;
            this.Definition = $"{this.Type} {{{this.PropertySet}}} Name {this.Name}";
        }

        /// <summary>
        /// {type} {guid} Id {tag}
        /// </summary>
        /// <param name="type"></param>
        /// <param name="tag"></param>
        /// <param name="propertySet"></param>
        public ExtendedPropertyDefinition(MapiPropertyType type, int tag, Guid propertySet)
            : this(type)
        {
            ArgumentValidator.ThrowIfGuidEmpty(
                propertySet, 
                nameof(propertySet));

            this.Tag = tag;
            this.PropertySet = propertySet;
            this.Definition = $"{this.Type} {{{this.PropertySet}}} Id 0x{this.TagHex}";
        }

        /// <summary>
        /// {type} {proptag}
        /// </summary>
        /// <param name="type">Type.</param>
        /// <param name="tag">Prop tag.</param>
        public ExtendedPropertyDefinition(MapiPropertyType type, int tag)
            : this(type)
        {
            this.Tag = tag;
            this.PropertySet = null;
            this.Definition = $"{this.Type} 0x{this.TagHex}";
        }

        /// <summary>
        /// Default constructor. 
        /// </summary>
        /// <param name="type">Type of property.</param>
        private ExtendedPropertyDefinition(MapiPropertyType type)
        {
            this.Type = type;
        }

        /// <summary>
        /// Hex tag.
        /// </summary>
        private string TagHex
        {
            get
            {
                if (this.Tag.HasValue)
                {
                    return this.Tag.Value.ToString("X4");
                }

                return string.Empty;
            }
        }

        /// <summary>
        /// Mapi property type.
        /// </summary>
        public MapiPropertyType Type { get; }

        /// <summary>
        /// Mapi tag.
        /// </summary>
        public int? Tag { get; }

        /// <summary>
        /// Property set.
        /// </summary>
        public Guid? PropertySet { get; }

        /// <summary>
        /// Definition.
        /// </summary>
        public string Definition { get; }

        /// <summary>
        /// Property name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Property value type.
        /// </summary>
        public PropertyValueType PropertyValueType
        {
            get { return this.GetMapiPropertyValueType(this.Type); }
        }

        /// <summary>
        /// Get mapi property type value.
        /// </summary>
        /// <param name="mapiPropertyType">Mapi property type.</param>
        /// <returns></returns>
        private PropertyValueType GetMapiPropertyValueType(MapiPropertyType mapiPropertyType)
        {
            MemberInfo memberInfo = typeof(MapiPropertyType).GetMember(mapiPropertyType.ToString()).FirstOrDefault();
            if (memberInfo != null)
            {
                PropertyTypeValueAttribute propertyType = (PropertyTypeValueAttribute)memberInfo.GetCustomAttributes(
                    typeof(PropertyTypeValueAttribute), 
                    false).FirstOrDefault();

                return propertyType != null
                    ? propertyType.PropertyValueType
                    : PropertyValueType.SingleValue;
            }

            return PropertyValueType.SingleValue;
        }
    }
}
