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
        [PropertyTypeValue(PropertyValueType.SingleValue)]
        ApplicationTime,

        /// <summary>
        /// The property is of type ApplicationTimeArray.
        /// </summary>
        [PropertyTypeValue(PropertyValueType.MultiValue)]
        ApplicationTimeArray,

        /// <summary>
        /// The property is of type Binary.
        /// </summary>
        [PropertyTypeValue(PropertyValueType.SingleValue)]
        Binary,

        /// <summary>
        /// The property is of type BinaryArray.
        /// </summary>
        [PropertyTypeValue(PropertyValueType.MultiValue)]
        BinaryArray,

        /// <summary>
        /// The property is of type Boolean.
        /// </summary>
        [PropertyTypeValue(PropertyValueType.SingleValue)]
        Boolean,

        /// <summary>
        /// The property is of type CLSID.
        /// </summary>
        [PropertyTypeValue(PropertyValueType.SingleValue)]
        CLSID,

        /// <summary>
        /// The property is of type CLSIDArray.
        /// </summary>
        [PropertyTypeValue(PropertyValueType.MultiValue)]
        CLSIDArray,

        /// <summary>
        /// The property is of type Currency.
        /// </summary>
        [PropertyTypeValue(PropertyValueType.SingleValue)]
        Currency,

        /// <summary>
        /// The property is of type CurrencyArray.
        /// </summary>
        [PropertyTypeValue(PropertyValueType.MultiValue)]
        CurrencyArray,

        /// <summary>
        /// The property is of type Double.
        /// </summary>
        [PropertyTypeValue(PropertyValueType.SingleValue)]
        Double,

        /// <summary>
        /// The property is of type DoubleArray.
        /// </summary>
        [PropertyTypeValue(PropertyValueType.MultiValue)]
        DoubleArray,

        /// <summary>
        /// The property is of type Error.
        /// </summary>
        [PropertyTypeValue(PropertyValueType.SingleValue)]
        Error,

        /// <summary>
        /// The property is of type Float.
        /// </summary>
        [PropertyTypeValue(PropertyValueType.SingleValue)]
        Float,

        /// <summary>
        /// The property is of type FloatArray.
        /// </summary>
        [PropertyTypeValue(PropertyValueType.MultiValue)]
        FloatArray,

        /// <summary>
        /// The property is of type Integer.
        /// </summary>
        [PropertyTypeValue(PropertyValueType.SingleValue)]
        Integer,

        /// <summary>
        /// The property is of type IntegerArray.
        /// </summary>
        [PropertyTypeValue(PropertyValueType.MultiValue)]
        IntegerArray,

        /// <summary>
        /// The property is of type Long.
        /// </summary>
        [PropertyTypeValue(PropertyValueType.SingleValue)]
        Long,

        /// <summary>
        /// The property is of type LongArray.
        /// </summary>
        [PropertyTypeValue(PropertyValueType.MultiValue)]
        LongArray,

        /// <summary>
        /// The property is of type Null.
        /// </summary>
        [PropertyTypeValue(PropertyValueType.SingleValue)]
        Null,

        /// <summary>
        /// The property is of type Object.
        /// </summary>
        [PropertyTypeValue(PropertyValueType.SingleValue)]
        Object,

        /// <summary>
        /// The property is of type ObjectArray.
        /// </summary>
        [PropertyTypeValue(PropertyValueType.MultiValue)]
        ObjectArray,

        /// <summary>
        /// The property is of type Short.
        /// </summary>
        [PropertyTypeValue(PropertyValueType.SingleValue)]
        Short,

        /// <summary>
        /// The property is of type ShortArray.
        /// </summary>
        [PropertyTypeValue(PropertyValueType.MultiValue)]
        ShortArray,

        /// <summary>
        /// The property is of type SystemTime.
        /// </summary>
        [PropertyTypeValue(PropertyValueType.SingleValue)]
        SystemTime,

        /// <summary>
        /// The property is of type SystemTimeArray.
        /// </summary>
        [PropertyTypeValue(PropertyValueType.MultiValue)]
        SystemTimeArray,

        /// <summary>
        /// The property is of type String.
        /// </summary>
        [PropertyTypeValue(PropertyValueType.SingleValue)]
        String,

        /// <summary>
        /// The property is of type StringArray.
        /// </summary>
        [PropertyTypeValue(PropertyValueType.MultiValue)]
        StringArray
    }
}
