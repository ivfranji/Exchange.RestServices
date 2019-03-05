namespace Exchange.RestServices
{
    using System.Collections.Generic;

    /// <summary>
    /// Pre-request executor prefer header setter.
    /// </summary>
    internal interface IPreferenceHeaderSetter
    {
        /// <summary>
        /// Sets prefer header.
        /// </summary>
        /// <param name="value"></param>
        void SetPreferHeader(IEnumerable<string> value);
    }
}
