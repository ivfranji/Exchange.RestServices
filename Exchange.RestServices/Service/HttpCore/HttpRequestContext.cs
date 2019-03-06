namespace Exchange.RestServices
{
    /// <summary>
    /// Represents request context.
    /// </summary>
    internal class HttpRequestContext
    {
        /// <summary>
        /// Create new instance of <see cref="HttpRequestContext"/>
        /// </summary>
        /// <param name="exchangeService">Exchange service.</param>
        public HttpRequestContext(ExchangeService exchangeService)
        {
            this.TraceContext = new TraceContext(exchangeService);
        }

        /// <summary>
        /// Trace context.
        /// </summary>
        public TraceContext TraceContext { get; }
    }

    /// <summary>
    /// Trace context.
    /// </summary>
    internal class TraceContext
    {
        /// <summary>
        /// Create new instance of <see cref="TraceContext"/>
        /// </summary>
        /// <param name="exchangeService"></param>
        public TraceContext(ExchangeService exchangeService)
        {
            this.TraceEnabled = exchangeService.TraceEnabled;
            this.TraceFlags = exchangeService.TraceFlags;
            this.TraceListener = exchangeService.TraceListener;
        }
        
        /// <summary>
        /// Trace enabled.
        /// </summary>
        public bool TraceEnabled
        {
            get;
        }

        /// <summary>
        /// Trace flags.
        /// </summary>
        public TraceFlags TraceFlags
        {
            get;
        }

        /// <summary>
        /// Trace listener.
        /// </summary>
        public ITraceListener TraceListener
        {
            get;
        }
    }
}
