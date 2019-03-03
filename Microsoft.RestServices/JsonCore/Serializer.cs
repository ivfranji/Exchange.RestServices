namespace Microsoft.RestServices.Exchange
{
    using System.Collections.Generic;
    using Microsoft.OutlookServices;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using Newtonsoft.Json.Linq;

    internal class Serializer
    {
        /// <summary>
        /// Singleton.
        /// </summary>
        private static Serializer instance = new Serializer();

        /// <summary>
        /// Create new instance of <see cref="Serializer"/>
        /// </summary>
        private Serializer()
        {
            this.StringEnumSerializer = new JsonSerializer()
            {
                Converters = { new StringEnumConverter() },
                NullValueHandling =  NullValueHandling.Ignore
            };
        }

        /// <summary>
        /// String enum serializer.
        /// </summary>
        private JsonSerializer StringEnumSerializer { get; }
        
        /// <summary>
        /// Singleton.
        /// </summary>
        internal static Serializer Instance
        {
            get { return Serializer.instance; }
        }

        /// <summary>
        /// Serialize Property change tracking contract.
        /// </summary>
        /// <param name="obj">Object to serialize.</param>
        /// <param name="additionalProperties">Additional properties to serialize.</param>
        /// <param name="appendRootObject">Append root object name.</param>
        /// <returns></returns>
        public string Serialize(IPropertyChangeTracking obj, Dictionary<string, object> additionalProperties, bool appendRootObject = true)
        {
            /*
               Two behaviors appendRootObject covers.
               1. appendRootObject = false

                {
                    "Property1": "Value",
                    "Property2": "Value",
                    etc..
                }

                2. appendRootObject = true
                It appends object name. For an instance, object Message with two properties

                {
                    "message": {
                        "Property1": "Value",
                        "Property2": "Value",
                        etc...
                    }
                }
            */

            JObject rootObject = new JObject();
            if (appendRootObject)
            {
                rootObject.Add(
                    obj.GetType().Name, 
                    this.BuildObjectFromIPropertyChangeTracking(obj));
            }
            else
            {
                rootObject = this.BuildObjectFromIPropertyChangeTracking(obj);
            }

            // Additional properties aren't part of initial object, for example "Comment" in SendMail.
            if (null != additionalProperties && additionalProperties.Count > 0)
            {
                foreach (KeyValuePair<string, object> additionalProperty in additionalProperties)
                {
                    // Outlook api is case sensitive with parameters so
                    // caps first letter. Other casing is ok from generated
                    // model. Graph doesn't care about casing.
                    rootObject.Add(
                        this.CapsFirstLetter(additionalProperty.Key),
                        JToken.FromObject(additionalProperty.Value));
                }
            }

            return JsonConvert.SerializeObject(rootObject);
        }

        /// <summary>
        /// Serialize dictionary.
        /// </summary>
        /// <param name="properties">Properties.</param>
        /// <returns></returns>
        public string Serialize(Dictionary<string, object> properties)
        {
            if (null != properties && properties.Count > 0)
            {
                JObject rootObject = new JObject();
                foreach (KeyValuePair<string, object> property in properties)
                {
                    rootObject.Add(
                        this.CapsFirstLetter(property.Key),
                        JToken.FromObject(property.Value));
                }

                return JsonConvert.SerializeObject(rootObject);
            }

            return string.Empty;
        }

        /// <summary>
        /// Serialize object.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public string Serialize(object obj)
        {
            return JsonConvert.SerializeObject(
                obj, new JsonSerializerSettings()
                {
                    NullValueHandling = NullValueHandling.Ignore
                });
        }

        /// <summary>
        /// Capitalize first letter of the string.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private string CapsFirstLetter(string value)
        {
            return $"{value.Substring(0, 1).ToUpper()}{value.Substring(1)}";
        }

        private JObject BuildObjectFromIPropertyChangeTracking(IPropertyChangeTracking obj, bool appendODataType = false, string odataType = null)
        {
            JObject jObject = new JObject();
            if (appendODataType)
            {
                if (!string.IsNullOrEmpty(odataType))
                {
                    jObject["@odata.type"] = odataType;
                }
            }

            foreach (PropertyDefinition changedProperty in obj.GetChangedProperies())
            {
                object property = obj[changedProperty];
                if (changedProperty.ChangeTrackable)
                {
                    jObject[changedProperty.Name] = JToken.FromObject(
                        this.BuildObjectFromIPropertyChangeTracking(property as IPropertyChangeTracking,
                            changedProperty.Type.IsAbstract,
                            changedProperty.GetODataType(property)),
                        this.StringEnumSerializer);
                }
                else if (changedProperty.ListChangeTrackable)
                {
                    IList<object> list = changedProperty.ActivateIList(obj[changedProperty]);
                    JArray jArray = new JArray();
                    foreach (object entry in list)
                    {
                        jArray.Add(
                            this.BuildObjectFromIPropertyChangeTracking(
                                entry as IPropertyChangeTracking, 
                                changedProperty.GetListUnderlyingType().IsAbstract,
                                changedProperty.GetODataType(entry)));
                    }

                    jObject.Add(
                        changedProperty.Name, 
                        jArray);
                }
                else
                {
                    jObject[changedProperty.Name] = JToken.FromObject(
                        property,
                        this.StringEnumSerializer);
                }
            }

            return jObject;
        }
    }
}
