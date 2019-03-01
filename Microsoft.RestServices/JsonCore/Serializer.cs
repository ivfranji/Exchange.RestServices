namespace Microsoft.RestServices.Exchange
{
    using System.Collections.Generic;
    using Graph;
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
            JObject appendObject = rootObject;

            if (appendRootObject)
            {
                appendObject = new JObject();
                rootObject.Add(obj.GetType().Name, appendObject);
            }

            foreach (string changedProperty in obj.GetChangedProperties())
            {
                object property = obj[changedProperty];
                appendObject[changedProperty] = JToken.FromObject(
                    property, 
                    this.StringEnumSerializer);
                
            }

            // Additional properties aren't part of initial object, for example "Comment" in SendMail.
            if (null != additionalProperties && additionalProperties.Count > 0)
            {
                foreach (KeyValuePair<string, object> additionalProperty in additionalProperties)
                {
                    rootObject.Add(
                        additionalProperty.Key,
                        JToken.FromObject(additionalProperty.Value));
                }
            }

            return JsonConvert.SerializeObject(rootObject);
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
    }
}
