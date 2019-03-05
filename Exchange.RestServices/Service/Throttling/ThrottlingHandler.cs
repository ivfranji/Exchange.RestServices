namespace Exchange.RestServices
{
    using System;
    using System.Net;

    /// <summary>
    /// Throttling handler.
    /// </summary>
    internal static class ThrottlingHandler
    {
        /// <summary>
        /// Execute request under throttling guard.
        /// </summary>
        /// <param name="webResponseRetrival"></param>
        /// <param name="retryCount"></param>
        /// <param name="tracer"></param>
        /// <returns></returns>
        public static IHttpWebResponse ExecuteRequestUnderThrottlingGuard(
            Func<IHttpWebResponse> webResponseRetrival,
            int retryCount, 
            Action<IHttpWebResponse, int> tracer)
        {
            // TODO: Retry-After
            bool throttled = false;
            IHttpWebResponse response = null;
            int counter = 0;

            do
            {
                response = webResponseRetrival();
                if (ResponseThrottled(response))
                {
                    throttled = true;
                    tracer?.Invoke(response, counter);
                }
                else
                {
                    throttled = false;
                }

                counter++;
            } while (throttled &&
                     retryCount > counter);

            return response;
        }

        /// <summary>
        /// 429 is request throttled.
        /// </summary>
        /// <returns></returns>
        private static bool ResponseThrottled(IHttpWebResponse webResponse)
        {
            if (webResponse == null)
            {
                return false;
            }

            return webResponse.StatusCode == (HttpStatusCode)429;
        }
    }
}