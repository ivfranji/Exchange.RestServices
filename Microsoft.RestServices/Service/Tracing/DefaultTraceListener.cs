namespace Microsoft.RestServices.Exchange
{
    using System;
    using System.IO;

    /// <summary>
    /// Default implementation of <see cref="ITraceListener"/>.
    /// </summary>
    public class DefaultTraceListener : ITraceListener
    {
        /// <summary>
        /// Text writer.
        /// </summary>
        private TextWriter textWriter;

        /// <summary>
        /// Create new instance of <see cref="DefaultTraceListener"/>.
        /// </summary>
        internal DefaultTraceListener()
        {
            this.textWriter = Console.Out;
        }

        /// <summary>
        /// Trace message.
        /// </summary>
        /// <param name="traceType"></param>
        /// <param name="traceMessage"></param>
        public void Trace(string traceType, string traceMessage)
        {
            this.textWriter.Write(traceMessage);
        }

        /// <summary>
        /// ToString implementation.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return nameof(DefaultTraceListener);
        }
    }
}
