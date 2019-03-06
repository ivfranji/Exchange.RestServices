namespace Exchange.RestServices
{
    /// <summary>
    /// Retry handler options.
    /// </summary>
    internal class RetryOptions
    {
        /// <summary>
        /// Default retry options.
        /// </summary>
        private static RetryOptions defaultRetryOptions = new RetryOptions(
            3, 
            RetryOptions.DefaultDelaySeconds);

        /// <summary>
        /// Default delay.
        /// </summary>
        private const int DefaultDelaySeconds = 10;

        /// <summary>
        /// Create new instance of <see cref="RetryOptions"/>
        /// </summary>
        /// <param name="retryCount"></param>
        public RetryOptions(int retryCount, int delaySeconds)
        {
            this.RetryCount = retryCount;
            this.DelaySeconds = delaySeconds;
        }

        /// <summary>
        /// Retry count
        /// </summary>
        public int RetryCount { get; }

        /// <summary>
        /// Default delay;
        /// </summary>
        public int DelaySeconds { get; }

        /// <summary>
        /// Default retry options.
        /// </summary>
        internal static RetryOptions DefaultRetryOptions
        {
            get { return RetryOptions.defaultRetryOptions; }
        }
    }
}