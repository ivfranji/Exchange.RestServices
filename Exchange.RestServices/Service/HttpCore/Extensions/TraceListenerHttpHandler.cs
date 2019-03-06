namespace Exchange.RestServices
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Trace listener http client handler.
    /// </summary>
    internal sealed class TraceListenerHttpHandler : DelegatingHandler
    {
        public TraceListenerHttpHandler()
        {
        }

        /// <inheritdoc cref="HttpClientHandler.SendAsync"/>
        protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage httpRequest,CancellationToken cancellationToken)
        {
            HttpRequestContext requestContext = this.GetRequestContext(httpRequest);
            if (null == requestContext || !requestContext.TraceContext.TraceEnabled)
            {
                // logging not enabled, send it do next in pipeline.
                return await base.SendAsync(httpRequest, cancellationToken);
            }
            else
            {
                await this.TraceRequest(
                    requestContext, 
                    httpRequest);

                HttpResponseMessage httpResponseMessage = await base.SendAsync(
                    httpRequest, 
                    cancellationToken);

                await this.TraceResponse(
                    requestContext,
                    httpResponseMessage);

                return httpResponseMessage;
            }
        }

        /// <summary>
        /// Trace request.
        /// </summary>
        /// <param name="httpRequestContext">Http request context.</param>
        /// <param name="httpRequestMessage">Http request message.</param>
        private async Task TraceRequest(HttpRequestContext httpRequestContext, HttpRequestMessage httpRequestMessage)
        {
            if ((httpRequestContext.TraceContext.TraceFlags & TraceFlags.HttpRequestHeaders) == 
                TraceFlags.HttpRequestHeaders)
            {
                IDictionary<string, string> httpHeaders = new Dictionary<string, string>();
                foreach (KeyValuePair<string, IEnumerable<string>> header in httpRequestMessage.Headers)
                {
                    httpHeaders.Add(header.Key, this.FormatHttpHeaderValue(header.Value));
                }

                string formattedHeaders = string.Join(
                    Environment.NewLine, httpHeaders.Select(x => x.Key + ": " + x.Value));

                httpRequestContext.TraceContext.TraceListener.Trace(
                    TraceFlags.HttpRequestHeaders.ToString(),
                    formattedHeaders);
            }

            if ((httpRequestContext.TraceContext.TraceFlags & TraceFlags.HttpRequest) == 
                TraceFlags.HttpRequest)
            {
                string requestContent = await httpRequestMessage.Content.ReadAsStringAsync();
                httpRequestContext.TraceContext.TraceListener.Trace(
                    TraceFlags.HttpRequest.ToString(),
                    requestContent);
            }
        }

        /// <summary>
        /// Trace response.
        /// </summary>
        /// <param name="httpRequestContext">Http request context.</param>
        /// <param name="httpResponseMessage">Http response message.</param>
        private async Task TraceResponse(HttpRequestContext httpRequestContext, HttpResponseMessage httpResponseMessage)
        {
            if ((httpRequestContext.TraceContext.TraceFlags & TraceFlags.HttpResponseHeaders) == 
                TraceFlags.HttpResponseHeaders)
            {
                string formattedHeaders = string.Join(
                    Environment.NewLine, httpResponseMessage.Headers.Select(
                        x => x.Key + ": " + this.FormatHttpHeaderValue(x.Value)));

                httpRequestContext.TraceContext.TraceListener.Trace(
                    TraceFlags.HttpResponseHeaders.ToString(),
                    formattedHeaders);
            }

            if ((httpRequestContext.TraceContext.TraceFlags & TraceFlags.HttpResponse) ==
                TraceFlags.HttpResponse)
            {
                string traceContent = string.Empty;
                if (httpResponseMessage.Content != null)
                {
                    traceContent = await httpResponseMessage.Content.ReadAsStringAsync();
                }
                else
                {
                    traceContent = "Error: Http content empty.";
                }

                httpRequestContext.TraceContext.TraceListener.Trace(
                    TraceFlags.HttpResponse.ToString(),
                    traceContent);
            }
        }

        /// <summary>
        /// Get http request context from request message.
        /// </summary>
        /// <param name="httpRequestMessage">Http request message.</param>
        /// <returns></returns>
        private HttpRequestContext GetRequestContext(HttpRequestMessage httpRequestMessage)
        {
            if (httpRequestMessage.Properties.ContainsKey(nameof(HttpRequestContext)))
            {
                return (HttpRequestContext) httpRequestMessage.Properties[nameof(HttpRequestContext)];
            }

            return null;
        }

        /// <summary>
        /// Joins header values and separate them by comma ','
        /// </summary>
        /// <param name="value">Header value.</param>
        /// <returns></returns>
        private string FormatHttpHeaderValue(IEnumerable<string> value)
        {
            if (null == value)
            {
                return string.Empty;
            }

            return string.Join(", ", value);
        }
    }
}
