namespace Microsoft.RestServices.Exchange
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Http request Uri.
    /// </summary>
    internal class HttpRestUrl
    {
        /// <summary>
        /// Request uri builder.
        /// </summary>
        private UriBuilder requestUriBuilder;

        /// <summary>
        /// Request query.
        /// </summary>
        private IQuery query;

        /// <summary>
        /// Relative path.
        /// </summary>
        private string relativePath;

        /// <summary>
        /// MailboxId id.
        /// </summary>
        private MailboxId mailboxId;

        /// <summary>
        /// Create new instance of <see cref="HttpRestUrl"/>
        /// </summary>
        /// <param name="baseRequestUri">Base request uri.</param>
        /// <param name="mailboxId">Mailbox id.</param>
        internal HttpRestUrl(Uri baseRequestUri)
        {
            ArgumentValidator.ThrowIfNull(baseRequestUri, nameof(baseRequestUri));
            this.requestUriBuilder = new UriBuilder(baseRequestUri);

            // always start with 'me', service will determine later
            // if this needs to be changed.
            this.MailboxId = new MailboxId("me");
        }

        /// <summary>
        /// Anchor mailbox for this rest url.
        /// </summary>
        public string XAnchorMailbox { get; private set; }

        /// <summary>
        /// Request Uri.
        /// </summary>
        public Uri RequestUri
        {
            get { return this.requestUriBuilder.Uri; }
        }

        /// <summary>
        /// Relative path.
        /// </summary>
        public string RelativePath
        {
            get { return this.relativePath; }
            set
            {
                ArgumentValidator.ThrowIfNullOrEmpty(value, nameof(this.RelativePath));
                this.relativePath = value;
                this.AppendPathToUri(value);
            }
        }

        /// <summary>
        /// Uri query.
        /// </summary>
        public IQuery Query
        {
            get { return this.query; }
            set
            {
                // Query can be null at request level.
                if (null != value)
                {
                    this.query = value;
                    this.requestUriBuilder.Query = query.Query;
                }
            }
        }

        /// <summary>
        /// MailboxId Id.
        /// </summary>
        public MailboxId MailboxId
        {
            get { return this.mailboxId; }
            set
            {
                ArgumentValidator.ThrowIfNull(value, nameof(this.MailboxId));

                if (value.IdInEmailAddressForm)
                {
                    this.XAnchorMailbox = value.Id;
                }

                if (this.mailboxId == null)
                {
                    this.AppendPathToUri(value.Path);
                    this.mailboxId = value;
                }
                else
                {
                    if (!this.MailboxId.Equals(value))
                    {
                        // First we need to remove previous reference then add new one.
                        this.SwapId(
                            this.mailboxId,
                            value);
                        this.mailboxId = value;
                    }
                }
            }
        }
        
        /// <summary>
        /// Appends path to Uri.
        /// </summary>
        /// <param name="path"></param>
        private void AppendPathToUri(string value)
        {
            this.requestUriBuilder.Path = $"{this.requestUriBuilder.Path}/{value}";
        }

        /// <summary>
        /// Swap old id with new one.
        /// </summary>
        /// <param name="oldValue">Old value.</param>
        /// <param name="newValue">New value.</param>
        private void SwapId(MailboxId oldValue, MailboxId newValue)
        {
            string[] pathSegments = this.requestUriBuilder.Path.Split('/');
            List<string> newSegments = new List<string>(pathSegments.Length);

            for (int i = 0; i < pathSegments.Length; i++)
            {
                // 'me' id shouldn't contain 'users'
                if (newValue.IdInMeForm && 
                    pathSegments[i].Equals("users", StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }

                if (pathSegments[i].Equals(oldValue.Id, StringComparison.OrdinalIgnoreCase))
                {
                    if (oldValue.IdInMeForm && 
                        !newValue.IdInMeForm)
                    {
                        newSegments.Add(newValue.MailboxType);
                        newSegments.Add(newValue.Id);
                    }
                    else
                    {
                        newSegments.Add(newValue.Id);
                    }

                    continue;
                }

                newSegments.Add(pathSegments[i]);
            }

            this.requestUriBuilder.Path = string.Join("/", newSegments);
        }
    }
}
