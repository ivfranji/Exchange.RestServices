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
            : this(new TraceContext(exchangeService), exchangeService.AuthorizationTokenProvider)
        {
        }

        /// <summary>
        /// Create new instance of <see cref="HttpRequestContext"/>
        /// </summary>
        /// <param name="traceContext"></param>
        public HttpRequestContext(TraceContext traceContext, IAuthorizationTokenProvider authorizationProvider)
        {
            ArgumentValidator.ThrowIfNull(
                traceContext, 
                nameof(traceContext));

            this.TraceContext = traceContext;
            this.AuthorizationProvider = authorizationProvider;
        }

        /// <summary>
        /// Trace context.
        /// </summary>
        public TraceContext TraceContext { get; }

        /// <summary>
        /// Authorization provider. 
        /// </summary>
        public IAuthorizationTokenProvider AuthorizationProvider { get; }
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
            : this(exchangeService.TraceEnabled, exchangeService.TraceFlags, exchangeService.TraceListener)
        {
        }

        /// <summary>
        /// Create new instance of <see cref="TraceContext"/>
        /// </summary>
        /// <param name="traceEnabled"></param>
        /// <param name="traceFlags"></param>
        /// <param name="traceListener"></param>
        public TraceContext(bool traceEnabled, TraceFlags traceFlags, ITraceListener traceListener)
        {
            this.TraceEnabled = traceEnabled;
            this.TraceFlags = traceFlags;
            this.TraceListener = traceListener;
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
