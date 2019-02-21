namespace Microsoft.RestServices.Exchange
{
     /// <summary>
     /// Defines the MAPI type of an extended property.
     /// Partially copied from Ews managed api.
     /// </summary>
    public enum MapiPropertyType
    {
        /// <summary>
        /// The property is of type ApplicationTime.
        /// </summary>
        [PropertyTypeValue(PropertyValueType.SingleValueExtendedProperties)]
        ApplicationTime,

        /// <summary>
        /// The property is of type ApplicationTimeArray.
        /// </summary>
        [PropertyTypeValue(PropertyValueType.MultiValueExtendedProperties)]
        ApplicationTimeArray,

        /// <summary>
        /// The property is of type Binary.
        /// </summary>
        [PropertyTypeValue(PropertyValueType.SingleValueExtendedProperties)]
        Binary,

        /// <summary>
        /// The property is of type BinaryArray.
        /// </summary>
        [PropertyTypeValue(PropertyValueType.MultiValueExtendedProperties)]
        BinaryArray,

        /// <summary>
        /// The property is of type Boolean.
        /// </summary>
        [PropertyTypeValue(PropertyValueType.SingleValueExtendedProperties)]
        Boolean,

        /// <summary>
        /// The property is of type CLSID.
        /// </summary>
        [PropertyTypeValue(PropertyValueType.SingleValueExtendedProperties)]
        CLSID,

        /// <summary>
        /// The property is of type CLSIDArray.
        /// </summary>
        [PropertyTypeValue(PropertyValueType.MultiValueExtendedProperties)]
        CLSIDArray,

        /// <summary>
        /// The property is of type Currency.
        /// </summary>
        [PropertyTypeValue(PropertyValueType.SingleValueExtendedProperties)]
        Currency,

        /// <summary>
        /// The property is of type CurrencyArray.
        /// </summary>
        [PropertyTypeValue(PropertyValueType.MultiValueExtendedProperties)]
        CurrencyArray,

        /// <summary>
        /// The property is of type Double.
        /// </summary>
        [PropertyTypeValue(PropertyValueType.SingleValueExtendedProperties)]
        Double,

        /// <summary>
        /// The property is of type DoubleArray.
        /// </summary>
        [PropertyTypeValue(PropertyValueType.MultiValueExtendedProperties)]
        DoubleArray,

        /// <summary>
        /// The property is of type Error.
        /// </summary>
        [PropertyTypeValue(PropertyValueType.SingleValueExtendedProperties)]
        Error,

        /// <summary>
        /// The property is of type Float.
        /// </summary>
        [PropertyTypeValue(PropertyValueType.SingleValueExtendedProperties)]
        Float,

        /// <summary>
        /// The property is of type FloatArray.
        /// </summary>
        [PropertyTypeValue(PropertyValueType.MultiValueExtendedProperties)]
        FloatArray,

        /// <summary>
        /// The property is of type Integer.
        /// </summary>
        [PropertyTypeValue(PropertyValueType.SingleValueExtendedProperties)]
        Integer,

        /// <summary>
        /// The property is of type IntegerArray.
        /// </summary>
        [PropertyTypeValue(PropertyValueType.MultiValueExtendedProperties)]
        IntegerArray,

        /// <summary>
        /// The property is of type Long.
        /// </summary>
        [PropertyTypeValue(PropertyValueType.SingleValueExtendedProperties)]
        Long,

        /// <summary>
        /// The property is of type LongArray.
        /// </summary>
        [PropertyTypeValue(PropertyValueType.MultiValueExtendedProperties)]
        LongArray,

        /// <summary>
        /// The property is of type Null.
        /// </summary>
        [PropertyTypeValue(PropertyValueType.SingleValueExtendedProperties)]
        Null,

        /// <summary>
        /// The property is of type Object.
        /// </summary>
        [PropertyTypeValue(PropertyValueType.SingleValueExtendedProperties)]
        Object,

        /// <summary>
        /// The property is of type ObjectArray.
        /// </summary>
        [PropertyTypeValue(PropertyValueType.MultiValueExtendedProperties)]
        ObjectArray,

        /// <summary>
        /// The property is of type Short.
        /// </summary>
        [PropertyTypeValue(PropertyValueType.SingleValueExtendedProperties)]
        Short,

        /// <summary>
        /// The property is of type ShortArray.
        /// </summary>
        [PropertyTypeValue(PropertyValueType.MultiValueExtendedProperties)]
        ShortArray,

        /// <summary>
        /// The property is of type SystemTime.
        /// </summary>
        [PropertyTypeValue(PropertyValueType.SingleValueExtendedProperties)]
        SystemTime,

        /// <summary>
        /// The property is of type SystemTimeArray.
        /// </summary>
        [PropertyTypeValue(PropertyValueType.MultiValueExtendedProperties)]
        SystemTimeArray,

        /// <summary>
        /// The property is of type String.
        /// </summary>
        [PropertyTypeValue(PropertyValueType.SingleValueExtendedProperties)]
        String,

        /// <summary>
        /// The property is of type StringArray.
        /// </summary>
        [PropertyTypeValue(PropertyValueType.MultiValueExtendedProperties)]
        StringArray
    }
}
