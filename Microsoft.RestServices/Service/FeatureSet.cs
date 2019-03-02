namespace Microsoft.RestServices.Exchange.Service
{
    using System;

    [Flags]
    public enum FeatureSet : int
    {
        None = 0,
        DeltaFolderId = 1,

        All = DeltaFolderId
    }
}
