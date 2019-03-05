namespace Exchange.RestServices
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text;
    using System.Threading.Tasks;
    using Utilities;

    /// <summary>
    /// HttpWebRequest.
    /// </summary>
    internal class HttpWebRequest : IHttpWebRequest
    {
        /// <summary>
        /// Lock object.
        /// </summary>
        private static object lockObject = new object();

        /// <summary>
        /// X-AnchorMailbox header name.
        /// </summary>
        private const string XAnchorMailboxHeaderName = "X-AnchorMailbox";
        
        /// <summary>
        /// Request message.
        /// </summary>
        private HttpRequestMessage httpRequestMessage;
        
        /// <summary>
        /// PATCH method.
        /// </summary>
        internal static readonly HttpMethod PATCH = new HttpMethod("PATCH");

        /// <summary>
        /// Create new instance of <see cref="HttpWebRequest"/>
        /// </summary>
        /// <param name="restUrl"></param>
        /// <param name="method"></param>
        private HttpWebRequest(HttpRestUrl restUrl, HttpWebRequestMethod method, string content = null)
        {
            this.RestUrl = restUrl ?? throw new ArgumentNullException(nameof(restUrl), "RestUrl cannot be null.");
            this.Method = method;
            this.Content = content;
            this.InitializeRequestMessage();
        }

        /// <summary>
        /// Request content.
        /// </summary>
        internal string Content { get;}

        /// <summary>
        /// Request method.
        /// </summary>
        internal HttpWebRequestMethod Method { get;}

        /// <summary>
        /// Request Url.
        /// </summary>
        public HttpRestUrl RestUrl { get; }

        /// <summary>
        /// Request Url.
        /// </summary>
        public Uri RequestUrl
        {
            get { return this.RestUrl.RequestUri; }
        }

        /// <summary>
        /// User agent used for request.
        /// </summary>
        public string UserAgent
        {
            get { return this.httpRequestMessage.Headers.UserAgent.ToString(); }
            set
            {
                this.httpRequestMessage.Headers.UserAgent.Add(
                    new ProductInfoHeaderValue(
                        value, 
                        RestUtils.BuildNumber));
            }
        }

        /// <summary>
        /// Request headers.
        /// </summary>
        public HttpRequestHeaders Headers
        {
            get
            {
                return this.httpRequestMessage.Headers;
            }
        }

        /// <summary>
        /// Authorization header.
        /// </summary>
        public AuthenticationHeaderValue Authorization
        {
            get { return this.httpRequestMessage.Headers.Authorization; }
            set
            {
                lock (HttpWebRequest.lockObject)
                {
                    this.httpRequestMessage.Headers.Authorization = value;
                }
            }
        }

        /// <summary>
        /// Submits request to endpoint and get response back.
        /// </summary>
        /// <returns></returns>
        public virtual IHttpWebResponse GetResponse()
        {
            // service can modify request uri before invoking response in a way to
            // change mailbox id etc., hence initialize url here.
            this.httpRequestMessage.RequestUri = this.RequestUrl;

            if (!string.IsNullOrEmpty(this.RestUrl.XAnchorMailbox))
            {
                this.SetRequestHeader(
                    HttpWebRequest.XAnchorMailboxHeaderName,
                    this.RestUrl.XAnchorMailbox);
            }

            IHttpWebRequestClient httpClient = HttpWebRequestClientProvider.Instance.GetClient();
            using (HttpResponseMessage response = httpClient.SendAsync(this.httpRequestMessage).GetAwaiter().GetResult())
            {
                bool requestSuccessful = false;
                string error = string.Empty;
                string content = string.Empty;

                if (response.Content != null)
                {
                    content = response.Content.ReadAsStringAsync().Result;
                    if (response.IsSuccessStatusCode)
                    {
                        requestSuccessful = true;
                    }
                    else
                    {
                        error = content;
                    }
                }
                else
                {
                    error = "Http content empty.";
                }

                return new HttpWebResponse(
                    content,
                    requestSuccessful,
                    error,
                    response.Headers,
                    response.StatusCode);
            }
        }

        /// <inheritdoc cref="IHttpWebRequest.SetRequestHeader"/>
        public void SetRequestHeader(string headerName, string headerValue)
        {
            if (this.Headers.Contains(headerName))
            {
                this.Headers.Remove(headerName);
            }

            this.Headers.Add(headerName, headerValue);
        }

        /// <summary>
        /// Disposes web request.
        /// </summary>
        public void Dispose()
        {
            this.httpRequestMessage?.Dispose();
        }

        /// <summary>
        /// ToString implementation.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("{0} {1}", this.httpRequestMessage.Method.Method, this.RequestUrl);
            sb.AppendLine();
            sb.AppendLine();
            if (this.httpRequestMessage.Content != null)
            {
                sb.AppendLine();
                sb.AppendFormat("Content: {0}", this.httpRequestMessage.Content.ReadAsStringAsync().Result);
                sb.AppendLine();
                sb.AppendLine();
            }

            return sb.ToString();
        }

        /// <summary>
        /// Sets prefer header.
        /// </summary>
        /// <param name="value"></param>
        public void SetPreferHeader(IEnumerable<string> value)
        {
            if (null != value)
            {
                this.httpRequestMessage.Headers.Add("Prefer", value);
            }
        }

        /// <summary>
        /// Create instance of GET <see cref="IHttpWebRequest"/>.
        /// </summary>
        /// <param name="restUrl">Request url.</param>
        /// <returns></returns>
        internal static IHttpWebRequest Get(HttpRestUrl restUrl)
        {
            return new HttpWebRequest(
                restUrl,
                HttpWebRequestMethod.GET);
        }

        /// <summary>
        /// Create instance of DELETE <see cref="IHttpWebRequest"/>.
        /// </summary>
        /// <param name="restUrl">Request url.</param>
        /// <returns></returns>
        internal static IHttpWebRequest Delete(HttpRestUrl restUrl)
        {
            return new HttpWebRequest(
                restUrl,
                HttpWebRequestMethod.DELETE);
        }

        internal static IHttpWebRequest Patch(HttpRestUrl restUrl, string content)
        {
            return new HttpWebRequest(
                restUrl, 
                HttpWebRequestMethod.PATCH, 
                content);
        }

        internal static IHttpWebRequest Post(HttpRestUrl restUrl, string content)
        {
            return new HttpWebRequest(
                restUrl, 
                HttpWebRequestMethod.POST, 
                content);
        }

        /// <summary>
        /// Create HttpContent.
        /// </summary>
        /// <param name="value">Content value.</param>
        /// <returns></returns>
        private HttpContent CreateContent(string value)
        {
            value = value ?? string.Empty;

            return new StringContent(
                value, 
                Encoding.UTF8, 
                "application/json");
        }

        private void InitializeRequestMessage()
        {
            switch (this.Method)
            {
                case HttpWebRequestMethod.GET:

                    this.httpRequestMessage = new HttpRequestMessage(
                        HttpMethod.Get,
                        this.RequestUrl);

                    break;

                case HttpWebRequestMethod.DELETE:

                    this.httpRequestMessage = new HttpRequestMessage(
                        HttpMethod.Delete,
                        this.RequestUrl);

                    break;

                case HttpWebRequestMethod.PATCH:

                    // PATCH shouldn't have empty content.
                    if (string.IsNullOrEmpty(this.Content))
                    {
                        throw new ArgumentNullException(nameof(this.Content),
                            "Content cannot be empty for PATCH request.");
                    }

                    this.httpRequestMessage = new HttpRequestMessage(
                        HttpWebRequest.PATCH,
                        this.RequestUrl);

                    this.httpRequestMessage.Content = this.CreateContent(this.Content);

                    break;

                case HttpWebRequestMethod.POST:

                    this.httpRequestMessage = new HttpRequestMessage(
                        HttpMethod.Post,
                        this.RequestUrl);
                    this.httpRequestMessage.Content = this.CreateContent(this.Content);

                    break;

                default:
                    throw new NotImplementedException(this.Method.ToString());
            }
        }
    }
}
