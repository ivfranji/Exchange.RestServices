namespace Exchange.RestServices
{
    /// <summary>
    /// Trace listener contract.
    /// </summary>
    public interface ITraceListener
    {
        /// <summary>
        /// Trace message.
        /// </summary>
        /// <param name="traceType">Trace type.</param>
        /// <param name="traceMessage">Trace message.</param>
        void Trace(string traceType, string traceMessage);
    }
}
