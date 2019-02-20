namespace Microsoft.RestServices.Exchange
{
    using System;

    /// <summary>
    /// Trace tags enabled for logging.
    /// </summary>
    [Flags]
    public enum TraceFlags : int
    {
        /// <summary>
        /// Tracing not enabled.
        /// </summary>
        None = 0,

        /// <summary>
        /// Traces http request.
        /// </summary>
        HttpRequest = 1,

        /// <summary>
        /// Traces http response.
        /// </summary>
        HttpResponse = 2,

        /// <summary>
        /// Trace http request headers.
        /// </summary>
        HttpRequestHeaders = 4,

        /// <summary>
        /// Trace http response headers.
        /// </summary>
        HttpResponseHeaders = 8,

        /// <summary>
        /// Trace throttling.
        /// </summary>
        Throttling = TraceFlags.HttpResponseHeaders | TraceFlags.HttpResponse,

        /// <summary>
        /// All tags enabled.
        /// </summary>
        All = TraceFlags.HttpRequest | TraceFlags.HttpResponse | TraceFlags.HttpRequestHeaders | TraceFlags.HttpResponseHeaders | TraceFlags.Throttling
    }
}
