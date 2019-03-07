namespace Exchange.RestServices
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http.Headers;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using Microsoft.OutlookServices;
    using Service.Auth;
    using Task = Microsoft.OutlookServices.Task;

    /// <summary>
    /// Base service for Exchange operations.
    /// </summary>
    public class ExchangeService
    {
        #region Privates

        /// <summary>
        /// Lock object.
        /// </summary>
        private static object lockObject = new object();

        /// <summary>
        /// Prefer header name.
        /// </summary>
        private const string PreferHeaderName = "Prefer";

        /// <summary>
        /// Mailbox id.
        /// </summary>
        private MailboxId mailboxId;

        /// <summary>
        /// Default user agent.
        /// </summary>
        private static readonly string defaultUserAgent = "ExchangeRestClient";

        /// <summary>
        /// User agent backing field.
        /// </summary>
        private string userAgent;

        /// <summary>
        /// Trace enabled.
        /// </summary>
        private bool traceEnabled;

        /// <summary>
        /// Trace listener.
        /// </summary>
        private ITraceListener traceListener;

        /// <summary>
        /// Action mapper.
        /// </summary>
        private ActionMapper actionMapper;

        /// <summary>
        /// Http response headers.
        /// </summary>
        private IDictionary<string, string> httpResponseHeaders;

        /// <summary>
        /// Rest environment.
        /// </summary>
        private RestEnvironment restEnvironment;

        #endregion

        #region ..ctors

        /// <summary>
        /// Create new instance of <see cref="ExchangeService"/>.
        /// </summary>
        /// <param name="bearerToken">Token provider.</param>
        /// <param name="xAnchorMailbox">Anchor mailbox.</param>
        /// <param name="restEnvironment">Rest environment.</param>
        public ExchangeService(string bearerToken, string mailboxId, RestEnvironment restEnvironment = null)
            : this(new SimpleAuthorizationTokenProvider(bearerToken), mailboxId, restEnvironment)
        {
        }

        /// <summary>
        /// Create new instance of <see cref="ExchangeService"/>.
        /// </summary>
        /// <param name="tokenProvider">Token provider.</param>
        /// <param name="xAnchorMailbox">Anchor mailbox.</param>
        /// <param name="restEnvironment">Rest environment.</param>
        public ExchangeService(IAuthorizationTokenProvider tokenProvider, string mailboxId, RestEnvironment restEnvironment = null)
        {
            ArgumentValidator.ThrowIfNull(
                tokenProvider,
                nameof(tokenProvider));

            if (string.IsNullOrEmpty(mailboxId))
            {
                this.MailboxId = new MailboxId("me");
            }
            else
            {
                this.MailboxId = new MailboxId(mailboxId);
            }

            if (AuthFactory.GetTokenProvider() == null)
            {
                lock (ExchangeService.lockObject)
                {
                    if (AuthFactory.GetTokenProvider() == null)
                    {
                        ExchangeService.SetAuthZProvider(tokenProvider);
                    }
                }
            }

            //this.AuthorizationTokenProvider = tokenProvider;
            this.TraceListener = new DefaultTraceListener();
            this.TraceFlags = TraceFlags.None;
            this.TraceEnabled = false;
            this.actionMapper = new ActionMapper(this.GetType());
            this.userAgent = ExchangeService.defaultUserAgent;
            this.restEnvironment = restEnvironment ?? RestEnvironment.OutlookBeta;
            this.Preferences = new List<Preference>();
        }

        #endregion

        #region Private properties

        /// <summary>
        /// Serializer.
        /// </summary>
        private Serializer Serializer
        {
            get { return Serializer.Instance; }
        }

        /// <summary>
        /// Indicate if preferences exists.
        /// </summary>
        private bool HasPreferences
        {
            get { return this.Preferences.Count > 0; }
        }

        #endregion

        #region Statics

        /// <summary>
        /// Set authorization provider.
        /// </summary>
        /// <param name="authorizationTokenProvider"></param>
        public static void SetAuthZProvider(IAuthorizationTokenProvider authorizationTokenProvider)
        {
            AuthFactory.SetTokenProvider(authorizationTokenProvider);
        }

        #endregion

        #region Public properties

        /// <inheritdoc cref="IRestService.UserAgent"/>
        public string UserAgent
        {
            get { return this.userAgent; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    Regex userAgentRegex = new Regex("^[a-zA-Z]+$");
                    if (userAgentRegex.IsMatch(value))
                    {
                        this.userAgent = $"{value}-{ExchangeService.defaultUserAgent}";
                    }
                }
            }
        }

        /// <inheritdoc cref="IExchangeService.Url"/>
        public Uri Url
        {
            get { return this.restEnvironment.BaseUri; }
        }

        /// <inheritdoc cref="IExchangeService.TraceEnabled"/>
        public bool TraceEnabled
        {
            get { return this.traceEnabled; }
            set
            {
                this.traceEnabled = value;
                if (this.traceEnabled && this.TraceListener == null)
                {
                    this.TraceListener = new DefaultTraceListener();
                }
            }
        }

        /// <inheritdoc cref="IExchangeService.TraceFlags"/>
        public TraceFlags TraceFlags { get; set; }

        /// <inheritdoc cref="IExchangeService.TraceListener"/>
        public ITraceListener TraceListener
        {
            get { return this.traceListener; }
            set
            {
                this.traceEnabled = value != null;
                this.traceListener = value;
            }
        }

        /// <inheritdoc cref="IExchangeService.HttpResponseHeaders"/>
        public IDictionary<string, string> HttpResponseHeaders
        {
            get { return this.httpResponseHeaders; }
        }

        /// <summary>
        /// Set or clear proxy server for requests.
        /// </summary>
        public IWebProxy ProxyServer
        {
            set { HttpWebRequestClientProvider.Instance.ProxyChanged(value); }
        }

        /// <summary>
        /// MailboxId.
        /// </summary>
        public MailboxId MailboxId
        {
            get { return this.mailboxId; }
            set
            {
                if (value == null)
                {
                    this.mailboxId = new MailboxId("me");
                }
                else
                {
                    this.mailboxId = value;
                }
            }
        }

        /// <summary>
        /// Authorization token provider. 
        /// </summary>
        public IAuthorizationTokenProvider AuthorizationTokenProvider { get; }

        /// <summary>
        /// Session based preferences applied on every request.
        /// </summary>
        public List<Preference> Preferences { get; }

        #endregion
        
        #region Actions

        /// <summary>
        /// Invokes action against entity.
        /// </summary>
        /// <param name="actionName">Action name.</param>
        /// <param name="entity">Entity.</param>
        /// <param name="additionalParameters">Additional parameters.</param>
        /// <returns></returns>
        internal object Invoke(string actionName, Entity entity, Dictionary<string, object> additionalParameters)
        {
            // TODO: Implement way to know which action requires new entity.
            string action = $"{actionName}~{entity.GetType().Name}";
            return this.actionMapper[action].Invoke(this, new object[] {entity, additionalParameters});
        }

        [Action(ActionMapper.SendMessageAction)]
        internal void SendMail(Message message, Dictionary<string, object> additionalProperties)
        {
            string content = this.Serializer.Serialize(
                message,
                additionalProperties);

            PostRequestBase request = new PostRequestBase(
                this,
                content,
                (httpRestUri) =>
                {
                    httpRestUri.RelativePath = "sendMail";
                    this.EnsureCorrectEndpoint(
                        httpRestUri,
                        null);
                });

            request.Execute();
        }

        [Action(ActionMapper.ReplyMessageAction)]
        internal void ReplyEmail(Message message, Dictionary<string, object> additionalParameters)
        {
            // We need to make bag 'dirty' to support serialization
            // and we only respond to "From" person.
            message.ToRecipients = new List<Recipient>();
            message.ToRecipients.Add(message.From);
            string content = this.Serializer.Serialize(additionalParameters);

            ItemId itemId = new MessageId(
                message.Id,
                this.MailboxId.Id);

            PostRequestBase request = new PostRequestBase(this,
                content,
                (httpRestUri) =>
                {
                    httpRestUri.RelativePath = $"{itemId.IdPath}/reply";
                    this.EnsureCorrectEndpoint(
                        httpRestUri,
                        itemId);
                });

            request.Execute();
        }

        [Action(ActionMapper.ForwardMessageAction)]
        internal void ForwardEmail(Message message, Dictionary<string, object> additionalParameters)
        {
            if (!additionalParameters.ContainsKey("toRecipients"))
            {
                throw new ArgumentException("ToRecipients not specified.");
            }

            message.ToRecipients = (IList<Recipient>) additionalParameters["toRecipients"];
            string content = this.Serializer.Serialize(additionalParameters);
            ItemId itemId = new MessageId(
                message.Id,
                this.MailboxId.Id);

            PostRequestBase request = new PostRequestBase(this,
                content,
                (httpRestUri) =>
                {
                    httpRestUri.RelativePath = $"{itemId.IdPath}/forward";
                    this.EnsureCorrectEndpoint(
                        httpRestUri,
                        itemId);
                });

            request.Execute();
        }

        [Action(ActionMapper.CopyMessageAction)]
        internal Message CopyEmail(Message message, Dictionary<string, object> additionalParameters)
        {
            MessageId itemId = new MessageId(
                message.Id,
                this.MailboxId.Id);

            string content = this.Serializer.Serialize(additionalParameters);

            PostRequestBase request = new PostRequestBase(
                this,
                content,
                (httpRestUrl) =>
                {
                    httpRestUrl.RelativePath = $"{itemId.IdPath}/copy";
                    this.EnsureCorrectEndpoint(
                        httpRestUrl,
                        itemId);
                });

            return request.Execute<Message>();
        }

        [Action(ActionMapper.MoveMessageAction)]
        internal Message MoveEmail(Message message, Dictionary<string, object> additionalParameters)
        {
            MessageId itemId = new MessageId(
                message.Id,
                this.MailboxId.Id);

            string content = this.Serializer.Serialize(additionalParameters);
            PostRequestBase request = new PostRequestBase(
                this,
                content,
                (httpRestUrl) =>
                {
                    httpRestUrl.RelativePath = $"{itemId.IdPath}/move";
                    this.EnsureCorrectEndpoint(
                        httpRestUrl,
                        itemId);
                });

            return request.Execute<Message>();
        }

        /// <summary>
        /// Move mail folder action.
        /// </summary>
        /// <param name="folder">Folder to move.</param>
        /// <param name="additionalParameters">Additional parameters.</param>
        /// <returns></returns>
        [Action(ActionMapper.MoveMailFolderAction)]
        internal MailFolder MoveMailFolder(MailFolder folder, Dictionary<string, object> additionalParameters)
        {
            ArgumentValidator.ThrowIfNull(folder, nameof(folder));

            // no changes other than destination id accepted.
            folder.ResetChangeTracking();
            string content = this.Serializer.Serialize(
                folder,
                additionalParameters,
                false);

            PostRequestBase request = new PostRequestBase(
                this,
                content,
                (httpRestUrl) =>
                {
                    httpRestUrl.RelativePath = $"{folder.FolderId.IdPath}/move";
                    this.EnsureCorrectEndpoint(
                        httpRestUrl,
                        folder.FolderId);
                });


            MailFolder mailFolder = request.Execute<MailFolder>();
            mailFolder.MailboxId = folder.MailboxId;
            mailFolder.Service = this;
            mailFolder.ResetChangeTracking();

            return mailFolder;
        }

        /// <summary>
        /// Decline meeting event.
        /// </summary>
        /// <param name="meetingEvent">Meeting event.</param>
        /// <param name="additionalParameters">Additional parameters.</param>
        [Action(ActionMapper.DeclineEventAction)]
        internal void DeclineEvent(Event meetingEvent, Dictionary<string, object> additionalParameters)
        {
            ArgumentValidator.ThrowIfNull(
                meetingEvent,
                nameof(meetingEvent));

            string content = this.Serializer.Serialize(
                meetingEvent,
                additionalParameters,
                false);

            PostRequestBase request = new PostRequestBase(
                this,
                content,
                (httpRestUrl) =>
                {
                    httpRestUrl.RelativePath = $"{meetingEvent.ItemId.IdPath}/decline";
                    this.EnsureCorrectEndpoint(
                        httpRestUrl,
                        meetingEvent.ItemId);
                });

            request.Execute();
        }

        [Action(ActionMapper.CompleteTaskAction)]
        internal IList<Task> CompleteTask(Task task, Dictionary<string, object> additionalParameters)
        {
            ArgumentValidator.ThrowIfNull(
                task,
                nameof(task));

            PostRequestBase request = new PostRequestBase(
                this,
                string.Empty,
                (httpRestUrl) =>
                {
                    httpRestUrl.RelativePath = $"{task.ItemId.IdPath}/complete";
                    this.EnsureCorrectEndpoint(
                        httpRestUrl, 
                        task.ItemId);
                });

            ResponseCollection<Task> response = request.Execute<ResponseCollection<Task>>();
            response.RegisterServiceAndResetChangeTracking(
                this, 
                task.MailboxId);

            return response.Value;
        }

        #endregion

        #region Message operations

        /// <inheritdoc cref="IExchangeService.FindItems(FolderId,ViewBase)"/>
        public FindItemsResults<Item> FindItems(FolderId parentFolderId, ViewBase itemView)
        {
            return this.FindItems(
                parentFolderId,
                null,
                itemView);
        }

        /// <inheritdoc cref="IExchangeService.FindItems(FolderId,ViewBase)"/>
        public async Task<FindItemsResults<Item>> FindItemsAsync(FolderId parentFolderId, ViewBase itemView)
        {
            return await this.FindItemsAsync(
                parentFolderId,
                null,
                itemView);
        }

        /// <inheritdoc cref="IExchangeService.FindItems(WellKnownFolderName,ViewBase)"/>
        public FindItemsResults<Item> FindItems(WellKnownFolderName wellKnownFolderName, ViewBase itemView)
        {
            return this.FindItems(
                wellKnownFolderName,
                null,
                itemView);
        }

        /// <inheritdoc cref="IExchangeService.FindItems(WellKnownFolderName,SearchFilter,ViewBase)"/>
        public FindItemsResults<Item> FindItems(WellKnownFolderName wellKnownFolderName, SearchFilter searchFilter,
            ViewBase itemView)
        {
            FolderId parentFolderId = new FolderId(wellKnownFolderName);
            if (wellKnownFolderName == WellKnownFolderName.Calendar)
            {
                parentFolderId = new CalendarFolderId("me");
            }

            if (wellKnownFolderName == WellKnownFolderName.Contacts)
            {
                parentFolderId = new ContactFolderId("me");
            }

            if (wellKnownFolderName == WellKnownFolderName.Tasks)
            {
                parentFolderId = new TaskFolderId("me");
            }

            return this.FindItems(
                parentFolderId,
                searchFilter,
                itemView);
        }

        /// <inheritdoc cref="IExchangeService.FindItems(FolderId,SearchFilter,ViewBase)"/>
        public FindItemsResults<Item> FindItems(FolderId parentFolderId, SearchFilter searchFilter, ViewBase itemView)
        {
            ArgumentValidator.ThrowIfNull(
                parentFolderId,
                nameof(parentFolderId));

            ArgumentValidator.ThrowIfNull(
                itemView,
                nameof(itemView));

            IQuery searchQuery = itemView.ViewQuery;
            if (null != searchFilter)
            {
                searchQuery = new CompositeQuery(new[] {searchFilter, itemView.ViewQuery});
            }
            
            GetRequestBase<ResponseCollection<Item>> request = new GetRequestBase<ResponseCollection<Item>>(
                this,
                (httpRestUrl) =>
                {
                    if (itemView.FollowODataNextLink &&
                        !string.IsNullOrEmpty(itemView.ODataNextLink))
                    {
                        httpRestUrl.ODataNextUri = itemView.ODataNextLink;
                    }
                    else
                    {
                        httpRestUrl.RelativePath = parentFolderId.MessagesContainer;
                        httpRestUrl.Query = searchQuery;
                        this.EnsureCorrectEndpoint(
                            httpRestUrl,
                            parentFolderId);
                    }
                });

            request.DeserializationType = itemView.Type;
            ResponseCollection<Item> response = request.Execute();


            // for null response return empty collection.
            if (null == response)
            {
                return new FindItemsResults<Item>(
                    new ResponseCollection<Item>(),
                    this,
                    this.GetMailboxId(parentFolderId));
            }
            else
            {
                if (itemView.FollowODataNextLink)
                {
                    itemView.ODataNextLink = response.ODataNextLink;
                }
            }

            return new FindItemsResults<Item>(
                response,
                this,
                this.GetMailboxId(parentFolderId));
        }

        /// <inheritdoc cref="IExchangeService.FindItems(FolderId,SearchFilter,ViewBase)"/>
        public async Task<FindItemsResults<Item>> FindItemsAsync(FolderId parentFolderId, SearchFilter searchFilter, ViewBase itemView)
        {
            ArgumentValidator.ThrowIfNull(
                parentFolderId,
                nameof(parentFolderId));

            ArgumentValidator.ThrowIfNull(
                itemView,
                nameof(itemView));

            IQuery searchQuery = itemView.ViewQuery;
            if (null != searchFilter)
            {
                searchQuery = new CompositeQuery(new[] { searchFilter, itemView.ViewQuery });
            }

            GetRequestBase<ResponseCollection<Item>> request = new GetRequestBase<ResponseCollection<Item>>(
                this,
                (httpRestUrl) =>
                {
                    if (itemView.FollowODataNextLink &&
                        !string.IsNullOrEmpty(itemView.ODataNextLink))
                    {
                        httpRestUrl.ODataNextUri = itemView.ODataNextLink;
                    }
                    else
                    {
                        httpRestUrl.RelativePath = parentFolderId.MessagesContainer;
                        httpRestUrl.Query = searchQuery;
                        this.EnsureCorrectEndpoint(
                            httpRestUrl,
                            parentFolderId);
                    }
                });

            request.DeserializationType = itemView.Type;
            ResponseCollection<Item> response = await request.ExecuteAsync();

            // for null response return empty collection.
            if (null == response)
            {
                return new FindItemsResults<Item>(
                    new ResponseCollection<Item>(),
                    this,
                    this.GetMailboxId(parentFolderId));
            }
            else
            {
                if (itemView.FollowODataNextLink)
                {
                    itemView.ODataNextLink = response.ODataNextLink;
                }
            }

            return new FindItemsResults<Item>(
                response,
                this,
                this.GetMailboxId(parentFolderId));
        }

        /// <summary>
        /// Sync folder items.
        /// </summary>
        /// <param name="syncFolderId">Sync folder id.</param>
        /// <param name="propertySet">Property set.</param>
        /// <param name="maxChangesReturned">Max changes returned.</param>
        /// <param name="syncState">Sync state.</param>
        /// <returns></returns>
        public SyncFolderItemsCollection<Item> SyncFolderItems(FolderId syncFolderId, PropertySet propertySet,
            int maxChangesReturned, string syncState)
        {
            ArgumentValidator.ThrowIfNull(syncFolderId, nameof(syncFolderId));
            ArgumentValidator.ThrowIfNull(propertySet, nameof(propertySet));
            ISyncQuery syncQuery = null;
            if (string.IsNullOrEmpty(syncState))
            {
                // Initial sync.
                syncQuery = new SyncQuery(
                    maxChangesReturned,
                    null);
            }
            else
            {
                ISyncToken syncToken = SyncToken.Deserialize(syncState);
                if (syncToken == null)
                {
                    throw new ArgumentException("Invalid sync state provided.");
                }

                syncQuery = new SyncQuery(
                    maxChangesReturned,
                    syncToken);
            }

            if (null != propertySet && null != propertySet.Properties)
            {
                syncQuery.SelectedProperties = propertySet.Properties;
            }

            SyncRequestBase<SyncResponseCollection<Item>> request =
                new SyncRequestBase<SyncResponseCollection<Item>>(
                    this,
                    syncQuery,
                    (httpRestUrl) =>
                    {
                        string deltaFolder = syncFolderId.MessagesContainer;

                        httpRestUrl.RelativePath = deltaFolder;
                        httpRestUrl.Query = syncQuery;
                        this.EnsureCorrectEndpoint(
                            httpRestUrl,
                            syncFolderId);
                    });

            request.DeserializationType = propertySet.Type;
            SyncResponseCollection<Item> response = request.Execute();
            response.PageSize = maxChangesReturned;
            return new SyncFolderItemsCollection<Item>(
                response,
                this,
                this.GetMailboxId(syncFolderId));
        }

        /// <summary>
        /// Get item request.
        /// </summary>
        /// <param name="itemId">Item id.</param>
        /// <returns></returns>
        internal Message GetItem(ItemId itemId)
        {
            ArgumentValidator.ThrowIfNull(
                itemId,
                nameof(itemId));

            GetRequestBase<Message> request = new GetRequestBase<Message>(
                this,
                (httpRestUrl) =>
                {
                    httpRestUrl.RelativePath = itemId.IdPath;
                    this.EnsureCorrectEndpoint(httpRestUrl, itemId);
                });

            return this.ProcessOutlookItemRequest(request.Execute, itemId) as Message;
        }

        /// <summary>
        /// Create Outlook item Async.
        /// </summary>
        /// <param name="item"></param>
        /// <param name="parentFolderId"></param>
        /// <param name="deserializationType"></param>
        /// <returns></returns>
        internal async Task<Item> CreateItemAsync(Item item, FolderId parentFolderId)
        {
            ArgumentValidator.ThrowIfNull(item, nameof(item));
            ArgumentValidator.ThrowIfNull(parentFolderId, nameof(parentFolderId));

            string content = this.Serializer.Serialize(
                item,
                null,
                false);

            PostRequestBase request = new PostRequestBase(
                this,
                content,
                (httpRestUrl) =>
                {
                    httpRestUrl.RelativePath = parentFolderId.MessagesContainer;
                    this.EnsureCorrectEndpoint(
                        httpRestUrl,
                        parentFolderId);
                });

            request.DeserializationType = item.GetType();
            return await this.ProcessOutlookItemRequestAsync(
                request.ExecuteAsync<Item>,
                parentFolderId);
        }

        /// <summary>
        /// Create Outlook item.
        /// </summary>
        /// <param name="item"></param>
        /// <param name="parentFolderId"></param>
        /// <param name="deserializationType"></param>
        /// <returns></returns>
        internal Item CreateItem(Item item, FolderId parentFolderId)
        {
            ArgumentValidator.ThrowIfNull(item, nameof(item));
            ArgumentValidator.ThrowIfNull(parentFolderId, nameof(parentFolderId));

            string content = this.Serializer.Serialize(
                item,
                null,
                false);

            PostRequestBase request = new PostRequestBase(
                this,
                content,
                (httpRestUrl) =>
                {
                    httpRestUrl.RelativePath = parentFolderId.MessagesContainer;
                    this.EnsureCorrectEndpoint(
                        httpRestUrl,
                        parentFolderId);
                });

            request.DeserializationType = item.GetType();
            return this.ProcessOutlookItemRequest(
                request.Execute<Item>,
                parentFolderId);
        }

        /// <summary>
        /// Update outlook item - Async.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        internal async Task<Item> UpdateItemAsync(Item item)
        {
            ArgumentValidator.ThrowIfNull(item, nameof(item));
            ArgumentValidator.ThrowIfNullOrEmpty(item.Id, nameof(item.Id));

            string content = this.Serializer.Serialize(
                item,
                null,
                false);

            PatchRequestBase request = new PatchRequestBase(
                this,
                content,
                (httpResturl) =>
                {
                    httpResturl.RelativePath = item.ItemId.IdPath;
                    this.EnsureCorrectEndpoint(
                        httpResturl,
                        item.ItemId);
                });

            request.DeserializationType = item.GetType();
            return await this.ProcessOutlookItemRequestAsync(
                request.ExecuteAsync<Item>,
                item.ItemId);
        }

        /// <summary>
        /// Update outlook item.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        internal Item UpdateItem(Item item)
        {
            ArgumentValidator.ThrowIfNull(item, nameof(item));
            ArgumentValidator.ThrowIfNullOrEmpty(item.Id, nameof(item.Id));

            string content = this.Serializer.Serialize(
                item,
                null,
                false);

            PatchRequestBase request = new PatchRequestBase(
                this,
                content,
                (httpResturl) =>
                {
                    httpResturl.RelativePath = item.ItemId.IdPath;
                    this.EnsureCorrectEndpoint(
                        httpResturl,
                        item.ItemId);
                });

            request.DeserializationType = item.GetType();
            return this.ProcessOutlookItemRequest(
                request.Execute<Item>,
                item.ItemId);
        }

        /// <summary>
        /// Delete item from the store - Async.
        /// </summary>
        /// <param name="itemId">Item id.</param>
        public async System.Threading.Tasks.Task DeleteItemAsync(ItemId itemId)
        {
            await this.DeleteEntityAsync(itemId);
        }

        /// <summary>
        /// Delete item from the store.
        /// </summary>
        /// <param name="itemId">Item id.</param>
        public void DeleteItem(ItemId itemId)
        {
            this.DeleteEntity(itemId);
        }

        #endregion

        #region Attachment operations

        /// <summary>
        /// Get attachments from message.
        /// </summary>
        /// <param name="itemId"></param>
        /// <returns></returns>
        public List<Attachment> GetAttachments(ItemId itemId)
        {
            ArgumentValidator.ThrowIfNull(
                itemId,
                nameof(itemId));

            GetRequestBase<ResponseCollection<Attachment>> request = new GetRequestBase<ResponseCollection<Attachment>>(
                this,
                (httpRestUrl) =>
                {
                    httpRestUrl.RelativePath = $"{itemId.IdPath}/attachments";
                    this.EnsureCorrectEndpoint(
                        httpRestUrl,
                        itemId);
                });

            ResponseCollection<Attachment> response = request.Execute();
            return response?.Value;
        }

        public Attachment GetAttachment(AttachmentId attachmentId, IExpandQuery expandQuery = null)
        {
            ArgumentValidator.ThrowIfNull(
                attachmentId,
                nameof(attachmentId));

            GetRequestBase<Attachment> request = new GetRequestBase<Attachment>(
                this,
                (httpRestUrl) =>
                {
                    httpRestUrl.RelativePath = $"{attachmentId.IdPath}";
                    if (null != expandQuery)
                    {
                        httpRestUrl.Query = expandQuery;
                    }

                    this.EnsureCorrectEndpoint(
                        httpRestUrl,
                        attachmentId);
                });

            return request.Execute();
        }

        #endregion

        #region Folder operations

        /// <inheritdoc cref="IExchangeService.FindFolders(FolderId,FolderView)"/>
        public FindFoldersResults FindFolders(FolderId parentFolderId, FolderView folderView)
        {
            return this.FindFolders(
                parentFolderId,
                null,
                folderView);
        }

        /// <inheritdoc cref="IExchangeService.FindFolders(WellKnownFolderName,FolderView)"/>
        public FindFoldersResults FindFolders(WellKnownFolderName wellKnownFolderName, FolderView folderView)
        {
            return this.FindFolders(
                wellKnownFolderName,
                null,
                folderView);
        }

        /// <inheritdoc cref="IExchangeService.FindFolders(WellKnownFolderName,SearchFilter,FolderView)"/>
        public FindFoldersResults FindFolders(WellKnownFolderName wellKnownFolderName, SearchFilter searchFilter,
            FolderView folderView)
        {
            FolderId parentFolderId = new FolderId(wellKnownFolderName);
            return this.FindFolders(
                parentFolderId,
                searchFilter,
                folderView);
        }

        /// <inheritdoc cref="IExchangeService.FindFolders(FolderId,SearchFilter,FolderView)"/>
        public FindFoldersResults FindFolders(FolderId parentFolderId, SearchFilter searchFilter, FolderView folderView)
        {
            ArgumentValidator.ThrowIfNull(
                parentFolderId,
                nameof(parentFolderId));

            ArgumentValidator.ThrowIfNull(
                folderView,
                nameof(folderView));

            IQuery searchQuery = folderView.ViewQuery;
            if (null != searchFilter)
            {
                searchQuery = new CompositeQuery(new[] {searchFilter, folderView.ViewQuery});
            }

            GetRequestBase<ResponseCollection<MailFolder>> request = new GetRequestBase<ResponseCollection<MailFolder>>(
                this,
                (httpRestUrl) =>
                {
                    httpRestUrl.RelativePath = parentFolderId.ChildFoldersContainer;
                    httpRestUrl.MailboxId = parentFolderId.MailboxId;
                    httpRestUrl.Query = searchQuery;
                    this.EnsureCorrectEndpoint(
                        httpRestUrl,
                        parentFolderId);
                });

            ResponseCollection<MailFolder> response = request.Execute();
            if (null == response)
            {
                response = new ResponseCollection<MailFolder>();
            }
            else
            {
                response.RegisterServiceAndResetChangeTracking(
                    this,
                    this.GetMailboxId(parentFolderId));
            }

            return new FindFoldersResults(response);
        }

        /// <inheritdoc cref="IExchangeService.FindFolders(FolderId,SearchFilter,FolderView)"/>
        public async Task<FindFoldersResults> FindFoldersAsync(FolderId parentFolderId, SearchFilter searchFilter, FolderView folderView)
        {
            ArgumentValidator.ThrowIfNull(
                parentFolderId,
                nameof(parentFolderId));

            ArgumentValidator.ThrowIfNull(
                folderView,
                nameof(folderView));

            IQuery searchQuery = folderView.ViewQuery;
            if (null != searchFilter)
            {
                searchQuery = new CompositeQuery(new[] { searchFilter, folderView.ViewQuery });
            }

            GetRequestBase<ResponseCollection<MailFolder>> request = new GetRequestBase<ResponseCollection<MailFolder>>(
                this,
                (httpRestUrl) =>
                {
                    httpRestUrl.RelativePath = parentFolderId.ChildFoldersContainer;
                    httpRestUrl.MailboxId = parentFolderId.MailboxId;
                    httpRestUrl.Query = searchQuery;
                    this.EnsureCorrectEndpoint(
                        httpRestUrl,
                        parentFolderId);
                });

            ResponseCollection<MailFolder> response = await request.ExecuteAsync();
            if (null == response)
            {
                response = new ResponseCollection<MailFolder>();
            }
            else
            {
                response.RegisterServiceAndResetChangeTracking(
                    this,
                    this.GetMailboxId(parentFolderId));
            }

            return new FindFoldersResults(response);
        }

        /// <summary>
        /// Sync folder hierarchy. 
        /// </summary>
        /// <param name="syncState">Sync state.</param>
        /// <returns></returns>
        public SyncMailFolderHierarchyResponse SyncFolderHierarchy(string syncState)
        {
            return this.SyncFolderHierarchy(
                new MailFolderPropertySet(),
                syncState);
        }

        /// <summary>
        /// Sync folder hierarchy. 
        /// </summary>
        /// <param name="propertySet">Property set.</param>
        /// <param name="syncState">Sync state.</param>
        /// <returns></returns>
        public SyncMailFolderHierarchyResponse SyncFolderHierarchy(MailFolderPropertySet propertySet, string syncState)
        {
            ISyncQuery syncQuery = null;
            if (string.IsNullOrEmpty(syncState))
            {
                // Initial sync
                syncQuery = new SyncQuery(
                    10,
                    null);
            }
            else
            {
                // ODataDelta link shouldn't be empty at this point.
                byte[] syncStateBytes = Convert.FromBase64String(syncState);
                string deserializedSyncState = Encoding.UTF8.GetString(syncStateBytes);

                Uri uri = null;
                try
                {
                    uri = new Uri(deserializedSyncState);
                }
                catch (Exception e)
                {
                    throw new ArgumentException("Invalid sync state provided.");
                }

                ISyncToken syncToken;
                if (!SyncToken.TryParseFromUrl(uri, SyncTokenType.DeltaToken, out syncToken))
                {
                    throw new ArgumentException("Sync state doesn't contain sync delta token.");
                }

                syncQuery = new SyncQuery(
                    10,
                    null);

                syncQuery.ODataDeltaLink = uri.ToString();
                //ISyncToken syncToken = SyncToken.Deserialize(syncState);
                //if (syncToken == null)
                //{
                //    throw new ArgumentException("Invalid sync state provided.");
                //}

                //syncQuery = new SyncQuery(
                //    10,
                //    syncToken);
            }

            if (null != propertySet && null != propertySet.Properties)
            {
                syncQuery.SelectedProperties = propertySet.Properties;
            }

            SyncRequestBase<SyncMailFolderResponseCollection> request =
                new SyncRequestBase<SyncMailFolderResponseCollection>(
                    this,
                    syncQuery,
                    (httpRestUrl) =>
                    {
                        if (!string.IsNullOrEmpty(syncQuery.ODataDeltaLink))
                        {
                            httpRestUrl.ODataNextUri = syncQuery.ODataDeltaLink;
                        }
                        else
                        {
                            httpRestUrl.RelativePath = "mailfolders";
                            httpRestUrl.Query = syncQuery;
                        }
                        
                        this.EnsureCorrectEndpoint(
                            httpRestUrl,
                            null);
                    });

            SyncMailFolderResponseCollection response = request.Execute();
            return new SyncMailFolderHierarchyResponse(
                response,
                this,
                this.GetMailboxId(null));
        }

        /// <summary>
        /// Get folder request.
        /// </summary>
        /// <param name="folderId">Folder id.</param>
        /// <returns></returns>
        internal MailFolder GetFolder(FolderId folderId)
        {
            ArgumentValidator.ThrowIfNull(
                folderId,
                nameof(folderId));

            GetRequestBase<MailFolder> request = new GetRequestBase<MailFolder>(
                this,
                (httpRestUrl) =>
                {
                    httpRestUrl.MailboxId = folderId.MailboxId;
                    httpRestUrl.RelativePath = folderId.IdPath;
                    this.EnsureCorrectEndpoint(
                        httpRestUrl,
                        folderId);
                });

            return this.ProcessMailFolderRequest(
                request.Execute,
                folderId);
        }

        /// <summary>
        /// Update folder properties.
        /// </summary>
        /// <param name="folder"></param>
        /// <returns></returns>
        internal MailFolder UpdateFolder(MailFolder folder)
        {
            ArgumentValidator.ThrowIfNull(folder, nameof(folder));
            ArgumentValidator.ThrowIfNullOrEmpty(folder.Id, nameof(folder.Id));

            string content = this.Serializer.Serialize(
                folder,
                null,
                false);

            PatchRequestBase request = new PatchRequestBase(
                this,
                content,
                (httpResturl) =>
                {
                    httpResturl.RelativePath = folder.FolderId.IdPath;
                    this.EnsureCorrectEndpoint(
                        httpResturl,
                        folder.FolderId);
                });

            return this.ProcessMailFolderRequest(
                request.Execute<MailFolder>,
                folder.FolderId);
        }

        /// <summary>
        /// Create mail folder.
        /// </summary>
        /// <param name="folder">Folder to create.</param>
        /// <returns></returns>
        internal MailFolder CreateFolder(MailFolder folder, FolderId parentFolderId)
        {
            ArgumentValidator.ThrowIfNull(folder, nameof(folder));
            folder.ResetChangeTracking();

            // only care about display name.
            folder.DisplayName = folder.DisplayName;
            string content = this.Serializer.Serialize(
                folder,
                null,
                false);

            PostRequestBase request = new PostRequestBase(
                this,
                content,
                (httpRestUrl) =>
                {
                    httpRestUrl.RelativePath = parentFolderId.ChildFoldersContainer;
                    this.EnsureCorrectEndpoint(
                        httpRestUrl,
                        parentFolderId);
                });

            return this.ProcessMailFolderRequest(
                request.Execute<MailFolder>,
                folder.FolderId);
        }

        /// <summary>
        /// Delete folder.
        /// </summary>
        /// <param name="folder"></param>
        internal void DeleteFolder(MailFolder folder)
        {
            this.DeleteEntity(folder.FolderId);
        }

        /// <summary>
        /// Delete folder from the store.
        /// </summary>
        /// <param name="folderId">Folder id.</param>
        public void DeleteFolder(FolderId folderId)
        {
            this.DeleteEntity(folderId);
        }

        #endregion

        #region InboxRule operations

        /// <summary>
        /// Get inbox rules in async fashion.
        /// </summary>
        /// <returns></returns>
        public async Task<List<MessageRule>> GetInboxRulesAsync()
        {
            FolderId folderId = new FolderId(
                WellKnownFolderName.Inbox,
                this.MailboxId.Id);

            GetRequestBase<ResponseCollection<MessageRule>> request =
                new GetRequestBase<ResponseCollection<MessageRule>>(
                    this,
                    (httpRestUrl) =>
                    {
                        httpRestUrl.RelativePath = folderId.MessageRules;
                        this.EnsureCorrectEndpoint(
                            httpRestUrl,
                            null);
                    });

            ResponseCollection<MessageRule> response = await request.ExecuteAsync();
            if (null == response)
            {
                return new List<MessageRule>();
            }

            response.RegisterServiceAndResetChangeTracking(
                this,
                this.GetMailboxId(folderId));
            return response.Value;
        }

        /// <summary>
        /// Get inbox rules.
        /// </summary>
        /// <returns></returns>
        public List<MessageRule> GetInboxRules()
        {
            FolderId folderId = new FolderId(
                WellKnownFolderName.Inbox,
                this.MailboxId.Id);

            GetRequestBase<ResponseCollection<MessageRule>> request =
                new GetRequestBase<ResponseCollection<MessageRule>>(
                    this,
                    (httpRestUrl) =>
                    {
                        httpRestUrl.RelativePath = folderId.MessageRules;
                        this.EnsureCorrectEndpoint(
                            httpRestUrl,
                            null);
                    });

            ResponseCollection<MessageRule> response = request.Execute();
            if (null == response)
            {
                return new List<MessageRule>();
            }

            response.RegisterServiceAndResetChangeTracking(
                this,
                this.GetMailboxId(folderId));
            return response.Value;
        }

        /// <summary>
        /// Get specific inbox rule - Async.
        /// </summary>
        /// <param name="ruleId">Rule id.</param>
        /// <returns></returns>
        public async Task<MessageRule> GetInboxRuleAsync(string ruleId)
        {
            ArgumentValidator.ThrowIfNullOrEmpty(
                ruleId,
                nameof(ruleId));

            FolderId folderId = new FolderId(
                WellKnownFolderName.Inbox,
                this.MailboxId.Id);

            GetRequestBase<MessageRule> request = new GetRequestBase<MessageRule>(
                this,
                (httpRestUrl) =>
                {
                    httpRestUrl.RelativePath = $"{folderId.MessageRules}/{ruleId}";
                    this.EnsureCorrectEndpoint(
                        httpRestUrl,
                        null);
                });

            MessageRule rule = await request.ExecuteAsync();
            if (null != rule)
            {
                rule.Service = this;
                rule.ResetChangeTracking();
            }

            return rule;
        }

        /// <summary>
        /// Get specific inbox rule.
        /// </summary>
        /// <param name="ruleId">Rule id.</param>
        /// <returns></returns>
        public MessageRule GetInboxRule(string ruleId)
        {
            ArgumentValidator.ThrowIfNullOrEmpty(
                ruleId,
                nameof(ruleId));

            FolderId folderId = new FolderId(
                WellKnownFolderName.Inbox,
                this.MailboxId.Id);

            GetRequestBase<MessageRule> request = new GetRequestBase<MessageRule>(
                this,
                (httpRestUrl) =>
                {
                    httpRestUrl.RelativePath = $"{folderId.MessageRules}/{ruleId}";
                    this.EnsureCorrectEndpoint(
                        httpRestUrl,
                        null);
                });

            MessageRule rule = request.Execute();
            if (null != rule)
            {
                rule.Service = this;
                rule.ResetChangeTracking();
            }

            return rule;
        }

        /// <summary>
        /// Update inbox rule - Async.
        /// </summary>
        /// <param name="rule"></param>
        /// <returns></returns>
        internal async Task<MessageRule> UpdateInboxRuleAsync(MessageRule rule)
        {
            ArgumentValidator.ThrowIfNull(rule, nameof(rule));
            ArgumentValidator.ThrowIfNullOrEmpty(rule.Id, "ruleId");

            if (rule.IsNew)
            {
                throw new ArgumentException("Cannot update non-existing rules. Please create rule first.");
            }

            FolderId folderId = new FolderId(
                WellKnownFolderName.Inbox,
                this.MailboxId.Id);
            string content = this.Serializer.Serialize(
                rule,
                null,
                false);

            PatchRequestBase request = new PatchRequestBase(
                this,
                content,
                (httpRestUrl) =>
                {
                    httpRestUrl.RelativePath = $"{folderId.MessageRules}/{rule.Id}";
                    this.EnsureCorrectEndpoint(
                        httpRestUrl,
                        folderId);
                });

            MessageRule updatedRule = await request.ExecuteAsync<MessageRule>();
            updatedRule.Service = this;
            updatedRule.ResetChangeTracking();

            return updatedRule;
        }

        /// <summary>
        /// Updates inbox rule.
        /// </summary>
        /// <param name="rule"></param>
        internal MessageRule UpdateInboxRule(MessageRule rule)
        {
            ArgumentValidator.ThrowIfNull(rule, nameof(rule));
            ArgumentValidator.ThrowIfNullOrEmpty(rule.Id, "ruleId");

            if (rule.IsNew)
            {
                throw new ArgumentException("Cannot update non-existing rules. Please create rule first.");
            }

            FolderId folderId = new FolderId(
                WellKnownFolderName.Inbox,
                this.MailboxId.Id);
            string content = this.Serializer.Serialize(
                rule,
                null,
                false);

            PatchRequestBase request = new PatchRequestBase(
                this,
                content,
                (httpRestUrl) =>
                {
                    httpRestUrl.RelativePath = $"{folderId.MessageRules}/{rule.Id}";
                    this.EnsureCorrectEndpoint(
                        httpRestUrl,
                        folderId);
                });

            MessageRule updatedRule = request.Execute<MessageRule>();
            updatedRule.Service = this;
            updatedRule.ResetChangeTracking();

            return updatedRule;
        }

        /// <summary>
        /// Delete inbox rule - Async.
        /// </summary>
        /// <param name="rule"></param>
        /// <returns></returns>
        internal async System.Threading.Tasks.Task DeleteInboxRuleAsync(MessageRule rule)
        {
            FolderId folderId = new FolderId(WellKnownFolderName.Inbox);
            DeleteRequestBase request = new DeleteRequestBase(
                this,
                (httpRestUrl) =>
                {
                    httpRestUrl.RelativePath = $"{folderId.MessageRules}/{rule.Id}";
                    this.EnsureCorrectEndpoint(
                        httpRestUrl,
                        folderId);
                });

            await request.ExecuteAsync();
        }

        /// <summary>
        /// Delete inbox rule.
        /// </summary>
        /// <param name="rule">Rule to delete.</param>
        internal void DeleteInboxRule(MessageRule rule)
        {
            FolderId folderId = new FolderId(WellKnownFolderName.Inbox);
            DeleteRequestBase request = new DeleteRequestBase(
                this,
                (httpRestUrl) =>
                {
                    httpRestUrl.RelativePath = $"{folderId.MessageRules}/{rule.Id}";
                    this.EnsureCorrectEndpoint(
                        httpRestUrl,
                        folderId);
                });

            request.Execute();
        }

        /// <summary>
        /// Create inbox rule - Async.
        /// </summary>
        /// <param name="rule"></param>
        /// <returns></returns>
        internal async Task<MessageRule> CreateInboxRuleAsync(MessageRule rule)
        {
            ArgumentValidator.ThrowIfNull(
                rule,
                nameof(rule));

            FolderId folderId = new FolderId(WellKnownFolderName.Inbox);
            string content = this.Serializer.Serialize(
                rule,
                null,
                false);

            PostRequestBase request = new PostRequestBase(
                this, content,
                (httpRestUrl) =>
                {
                    httpRestUrl.RelativePath = folderId.MessageRules;
                    this.EnsureCorrectEndpoint(
                        httpRestUrl,
                        folderId);
                });

            MessageRule messageRule = await request.ExecuteAsync<MessageRule>();
            messageRule.Service = this;
            messageRule.ResetChangeTracking();

            return messageRule;
        }

        /// <summary>
        /// Create inbox rule.
        /// </summary>
        /// <param name="rule"></param>
        /// <returns></returns>
        internal MessageRule CreateInboxRule(MessageRule rule)
        {
            ArgumentValidator.ThrowIfNull(
                rule,
                nameof(rule));

            FolderId folderId = new FolderId(WellKnownFolderName.Inbox);
            string content = this.Serializer.Serialize(
                rule,
                null,
                false);

            PostRequestBase request = new PostRequestBase(
                this, content,
                (httpRestUrl) =>
                {
                    httpRestUrl.RelativePath = folderId.MessageRules;
                    this.EnsureCorrectEndpoint(
                        httpRestUrl,
                        folderId);
                });

            MessageRule messageRule = request.Execute<MessageRule>();
            messageRule.Service = this;
            messageRule.ResetChangeTracking();

            return messageRule;
        }

        #endregion

        #region Inference Classification operations

        /// <summary>
        /// List inference classification overrides in async fasion.
        /// </summary>
        /// <returns></returns>
        public async Task<List<InferenceClassificationOverride>> GetInferenceClassificationOverridesAsync()
        {
            GetRequestBase<ResponseCollection<InferenceClassificationOverride>> request =
                new GetRequestBase<ResponseCollection<InferenceClassificationOverride>>(
                    this,
                    (httpRestUrl) =>
                    {
                        httpRestUrl.RelativePath = $"{nameof(InferenceClassification)}/overrides";
                        this.EnsureCorrectEndpoint(
                            httpRestUrl,
                            null);
                    });

            ResponseCollection<InferenceClassificationOverride> response = await request.ExecuteAsync();
            if (null != response)
            {
                foreach (InferenceClassificationOverride inferenceOverride in response.Value)
                {
                    inferenceOverride.Service = this;
                    inferenceOverride.ResetChangeTracking();
                }
            }
            else
            {
                response = new ResponseCollection<InferenceClassificationOverride>();
            }

            return response.Value;
        }

        /// <summary>
        /// List inference classification overrides.
        /// </summary>
        /// <returns></returns>
        public List<InferenceClassificationOverride> GetInferenceClassificationOverrides()
        {
            GetRequestBase<ResponseCollection<InferenceClassificationOverride>> request =
                new GetRequestBase<ResponseCollection<InferenceClassificationOverride>>(
                    this,
                    (httpRestUrl) =>
                    {
                        httpRestUrl.RelativePath = $"{nameof(InferenceClassification)}/overrides";
                        this.EnsureCorrectEndpoint(
                            httpRestUrl,
                            null);
                    });

            ResponseCollection<InferenceClassificationOverride> response = request.Execute();
            if (null != response)
            {
                foreach (InferenceClassificationOverride inferenceOverride in response.Value)
                {
                    inferenceOverride.Service = this;
                    inferenceOverride.ResetChangeTracking();
                }
            }
            else
            {
                response = new ResponseCollection<InferenceClassificationOverride>();
            }

            return response.Value;
        }

        /// <summary>
        /// Update inference classification override.
        /// </summary>
        /// <param name="inferenceOverride"></param>
        /// <returns></returns>
        internal async Task<InferenceClassificationOverride> UpdateInferenceClassificationOverrideAsync(InferenceClassificationOverride inferenceOverride)
        {
            ArgumentValidator.ThrowIfNull(
                inferenceOverride,
                nameof(inferenceOverride));

            string content = this.Serializer.Serialize(
                inferenceOverride,
                null,
                false);

            PatchRequestBase request = new PatchRequestBase(
                this,
                content,
                (httpRestUrl) =>
                {
                    httpRestUrl.RelativePath = $"inferenceClassification/overrides/{inferenceOverride.Id}";
                    this.EnsureCorrectEndpoint(
                        httpRestUrl,
                        null);
                });

            InferenceClassificationOverride inferenceClassification =
                await request.ExecuteAsync<InferenceClassificationOverride>();
            inferenceClassification.Service = this;
            inferenceClassification.ResetChangeTracking();

            return inferenceClassification;
        }

        /// <summary>
        /// Update inference classification override.
        /// </summary>
        /// <param name="inferenceOverride"></param>
        /// <returns></returns>
        internal InferenceClassificationOverride UpdateInferenceClassificationOverride(
            InferenceClassificationOverride inferenceOverride)
        {
            ArgumentValidator.ThrowIfNull(
                inferenceOverride,
                nameof(inferenceOverride));

            string content = this.Serializer.Serialize(
                inferenceOverride,
                null,
                false);

            PatchRequestBase request = new PatchRequestBase(
                this,
                content,
                (httpRestUrl) =>
                {
                    httpRestUrl.RelativePath = $"inferenceClassification/overrides/{inferenceOverride.Id}";
                    this.EnsureCorrectEndpoint(
                        httpRestUrl,
                        null);
                });

            InferenceClassificationOverride inferenceClassification =
                request.Execute<InferenceClassificationOverride>();
            inferenceClassification.Service = this;
            inferenceClassification.ResetChangeTracking();

            return inferenceClassification;
        }

        /// <summary>
        /// Create inference classification override.
        /// </summary>
        /// <param name="inferenceOverride"></param>
        /// <returns></returns>
        internal async Task<InferenceClassificationOverride> CreateInferenceClassificationOverrideAsync(InferenceClassificationOverride inferenceOverride)
        {
            ArgumentValidator.ThrowIfNull(
                inferenceOverride,
                nameof(inferenceOverride));

            string content = this.Serializer.Serialize(
                inferenceOverride,
                null,
                false);

            PostRequestBase request = new PostRequestBase(
                this,
                content,
                (httpRestUrl) =>
                {
                    httpRestUrl.RelativePath = "inferenceClassification/overrides";
                    this.EnsureCorrectEndpoint(
                        httpRestUrl,
                        null);
                });

            InferenceClassificationOverride inferenceClassification =
                await request.ExecuteAsync<InferenceClassificationOverride>();
            inferenceClassification.Service = this;
            inferenceClassification.ResetChangeTracking();

            return inferenceClassification;
        }

        /// <summary>
        /// Create inference classification override.
        /// </summary>
        /// <param name="inferenceOverride"></param>
        /// <returns></returns>
        internal InferenceClassificationOverride CreateInferenceClassificationOverride(
            InferenceClassificationOverride inferenceOverride)
        {
            ArgumentValidator.ThrowIfNull(
                inferenceOverride,
                nameof(inferenceOverride));

            string content = this.Serializer.Serialize(
                inferenceOverride,
                null,
                false);

            PostRequestBase request = new PostRequestBase(
                this,
                content,
                (httpRestUrl) =>
                {
                    httpRestUrl.RelativePath = "inferenceClassification/overrides";
                    this.EnsureCorrectEndpoint(
                        httpRestUrl,
                        null);
                });

            InferenceClassificationOverride inferenceClassification =
                request.Execute<InferenceClassificationOverride>();
            inferenceClassification.Service = this;
            inferenceClassification.ResetChangeTracking();

            return inferenceClassification;
        }

        /// <summary>
        /// Delete inference classification override.
        /// </summary>
        /// <param name="inferenceOverride"></param>
        internal async System.Threading.Tasks.Task DeleteInferenceClassificationOverrideAsync(InferenceClassificationOverride inferenceOverride)
        {
            ArgumentValidator.ThrowIfNull(
                inferenceOverride,
                nameof(inferenceOverride));

            DeleteRequestBase request = new DeleteRequestBase(
                this,
                (httpRestUrl) =>
                {
                    httpRestUrl.RelativePath = $"inferenceClassification/overrides/{inferenceOverride.Id}";
                    this.EnsureCorrectEndpoint(
                        httpRestUrl,
                        null);
                });

            await request.ExecuteAsync();
        }

        /// <summary>
        /// Delete inference classification override.
        /// </summary>
        /// <param name="inferenceOverride"></param>
        internal void DeleteInferenceClassificationOverride(InferenceClassificationOverride inferenceOverride)
        {
            ArgumentValidator.ThrowIfNull(
                inferenceOverride,
                nameof(inferenceOverride));

            DeleteRequestBase request = new DeleteRequestBase(
                this,
                (httpRestUrl) =>
                {
                    httpRestUrl.RelativePath = $"inferenceClassification/overrides/{inferenceOverride.Id}";
                    this.EnsureCorrectEndpoint(
                        httpRestUrl,
                        null);
                });

            request.Execute();
        }

        #endregion

        #region Request / Response processing

        /// <summary>
        /// Prepare web request.
        /// </summary>
        /// <param name="webRequest"></param>
        internal void PrepareHttpWebRequest(IHttpWebRequest webRequest)
        {
            webRequest.UserAgent = this.UserAgent;

            if (this.HasPreferences)
            {
                webRequest.Headers.Add(
                    ExchangeService.PreferHeaderName,
                    this.Preferences.Select(p => p.Prefer));
            }
        }

        /// <summary>
        /// Process web response.
        /// </summary>
        /// <param name="webResponse"></param>
        internal void ProcessHttpWebResponse(IHttpWebResponse webResponse)
        {
        }

        #endregion

        #region Private methods
        
        /// <summary>
        /// Ensure request are sent to correct endpoint - /me or /users/email@domain.com
        /// </summary>
        /// <param name="restUrl"></param>
        /// <param name="entity"></param>
        private void EnsureCorrectEndpoint(HttpRestUrl restUrl, EntityId entity)
        {
            restUrl.MailboxId = this.GetMailboxId(entity);
        }

        /// <summary>
        /// Get mailbox id.
        /// </summary>
        /// <param name="entityId"></param>
        /// <returns></returns>
        private MailboxId GetMailboxId(EntityId entityId)
        {
            if (null == entityId?.MailboxId)
            {
                return this.MailboxId;
            }

            if (this.MailboxId.Equals(entityId.MailboxId))
            {
                return this.MailboxId;
            }

            if (entityId.MailboxId.IdInMeForm)
            {
                return this.MailboxId;
            }

            return entityId.MailboxId;
        }

        /// <summary>
        /// Delete specified entity - Async.
        /// </summary>
        /// <param name="entityId"></param>
        private async System.Threading.Tasks.Task DeleteEntityAsync(EntityId entityId)
        {
            ArgumentValidator.ThrowIfNull(
                entityId,
                nameof(entityId));

            DeleteRequestBase deleteRequest = new DeleteRequestBase(
                this,
                (httpRestUrl) =>
                {
                    httpRestUrl.RelativePath = entityId.IdPath;
                    this.EnsureCorrectEndpoint(
                        httpRestUrl,
                        entityId);
                });

            await deleteRequest.ExecuteAsync();
        }

        /// <summary>
        /// Delete specified entity.
        /// </summary>
        /// <param name="entityId"></param>
        private void DeleteEntity(EntityId entityId)
        {
            ArgumentValidator.ThrowIfNull(
                entityId,
                nameof(entityId));

            DeleteRequestBase deleteRequest = new DeleteRequestBase(
                this,
                (httpRestUrl) =>
                {
                    httpRestUrl.RelativePath = entityId.IdPath;
                    this.EnsureCorrectEndpoint(
                        httpRestUrl,
                        entityId);
                });

            deleteRequest.Execute();
        }

        /// <summary>
        /// Process Outlook item request - Async
        /// </summary>
        /// <param name="outlookItemRequest"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        private async Task<Item> ProcessOutlookItemRequestAsync(Func<Task<Item>> outlookItemRequest, EntityId entity)
        {
            ArgumentValidator.ThrowIfNull(outlookItemRequest, nameof(outlookItemRequest));
            Item outlookItem = await outlookItemRequest();

            if (null == outlookItem)
            {
                return default(Item);
            }

            outlookItem.Service = this;
            outlookItem.MailboxId = this.GetMailboxId(entity);
            outlookItem.ResetChangeTracking();

            return outlookItem;
        }

        /// <summary>
        /// Process Outlook item request.
        /// </summary>
        /// <param name="outlookItemRequest"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        private Item ProcessOutlookItemRequest(Func<Item> outlookItemRequest, EntityId entity)
        {
            ArgumentValidator.ThrowIfNull(outlookItemRequest, nameof(outlookItemRequest));
            Item outlookItem = outlookItemRequest();

            if (null == outlookItem)
            {
                return null;
            }

            outlookItem.Service = this;
            outlookItem.MailboxId = this.GetMailboxId(entity);
            outlookItem.ResetChangeTracking();

            return outlookItem;
        }

        /// <summary>
        /// Process mail folder request.
        /// </summary>
        /// <param name="mailFolderRequest"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        private MailFolder ProcessMailFolderRequest(Func<MailFolder> mailFolderRequest, EntityId entity)
        {
            ArgumentValidator.ThrowIfNull(mailFolderRequest, nameof(mailFolderRequest));
            MailFolder mailFolder = mailFolderRequest();

            if (null == mailFolder)
            {
                return null;
            }

            mailFolder.Service = this;
            mailFolder.MailboxId = this.GetMailboxId(entity);
            mailFolder.ResetChangeTracking();

            return mailFolder;
        }

        #endregion
    }
}