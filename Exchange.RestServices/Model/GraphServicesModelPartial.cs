namespace Microsoft.Graph
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using Microsoft.RestServices.Exchange;
    using Newtonsoft.Json;

    /// <summary>
    /// Partial entity.
    /// </summary>
    public abstract partial class Entity : IPropertyChangeTracking
    {
        private ExchangeService service;

        /// <summary>
        /// Property bag.
        /// </summary>
        protected PropertyBag propertyBag;

        /// <summary>
        /// Entity.
        /// </summary>
        protected Entity()
        {
            Type schemaType = Assembly.GetExecutingAssembly().GetType(
                this.GetType().FullName + "ObjectSchema");
            if (schemaType != null)
            {
                object instance = Activator.CreateInstance(schemaType);
                if (instance is ObjectSchema)
                {
                    this.propertyBag = new PropertyBag(instance as ObjectSchema);
                }
            }
            else
            {
                throw new NullReferenceException($"Cannot find schema definition '{this.GetType().FullName}ObjectSchema'.");
            }
        }

        /// <summary>
        /// When entity is created outside this ..ctor needs to be used.
        /// </summary>
        /// <param name="service"></param>
        protected Entity(ExchangeService service)
            : this()
        {
            this.service = service;
            this.propertyBag.MarkAsNew();
        }

        /// <summary>
        /// When data retrieved from the server, service needs to be set
        /// in order for a method invocation to work.
        /// </summary>
        internal ExchangeService Service
        {
            get { return this.service; }
            set { this.service = value; }
        }

        /// <summary>
        /// Indicate if bag is new.
        /// </summary>
        internal bool IsNew
        {
            get { return this.propertyBag.IsNew; }
        }
        
        [JsonProperty()]
        /// <summary>
        /// Mailbox id.
        /// </summary>
        internal MailboxId MailboxId { get; set; }

        /// <summary>
        /// Get a list of changed properties.
        /// </summary>
        /// <returns></returns>
        public IList<string> GetChangedProperties()
        {
            return this.propertyBag.GetChangedProperties();
        }

        /// <summary>
        /// Indexer.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public object this[string key]
        {
            get { return this.propertyBag[key]; }
        }

        /// <summary>
        /// Reset change tracking.
        /// </summary>
        internal void ResetChangeTracking()
        {
            this.propertyBag.ResetChangeTracking();
        }

        /// <summary>
        /// Validates if update can occur.
        /// </summary>
        protected void PreValidateUpdate()
        {
            if (this.IsNew)
            {
                throw new ArgumentException("Cannot perform update on newly created item. Sync item from server and try again.");
            }

            if (this.propertyBag.GetChangedProperties().Count == 0)
            {
                throw new ArgumentException("No changed properties detected.");
            }

            ArgumentValidator.ThrowIfNull(
                this.Service, 
                nameof(this.Service));
        }

        /// <summary>
        /// Pre validate save.
        /// </summary>
        protected void PreValidateSave()
        {
            if (!this.IsNew)
            {
                throw new ArgumentException("Cannot call 'Save' on existing object.");
            }

            ArgumentValidator.ThrowIfNull(
                this.Service, 
                nameof(this.Service));
        }

        /// <summary>
        /// Pre validate delete.
        /// </summary>
        protected void PreValidateDelete()
        {
            if (this.IsNew)
            {
                throw new ArgumentException("Cannot perform delete operation on non existing object. Sync folder with server and try again.");
            }

            ArgumentValidator.ThrowIfNull(
                this.Service, 
                nameof(this.Service));
        }
    }

    /// <summary>
    /// Message item partial.
    /// </summary>
    public partial class Message
    {
        /// <summary>
        /// Create new instance of <see cref="Message"/>
        /// </summary>
        /// <param name="service">Exchange service.</param>
        public Message(ExchangeService service)
            : base(service)
        {
        }

        internal Message()
        {
        }

        /// <summary>
        /// Binds to a message.
        /// </summary>
        /// <param name="service"></param>
        /// <param name="itemId"></param>
        /// <returns></returns>
        public static Message Bind(ExchangeService service, ItemId itemId)
        {
            ArgumentValidator.ThrowIfNull(service, nameof(service));
            ArgumentValidator.ThrowIfNull(itemId, nameof(itemId));

            return service.GetItem(itemId);
        }

        /// <summary>
        /// Id type.
        /// </summary>
        protected override Type IdType
        {
            get { return typeof(MessageId); }
        }
    }

    /// <summary>
    /// Event item partial.
    /// </summary>
    public partial class Event
    {
        /// <summary>
        /// Create new instance of <see cref="Event"/>
        /// </summary>
        /// <param name="service">Exchange service.</param>
        public Event(ExchangeService service)
            : base(service)
        {
        }

        internal Event()
            : base()
        {
        }

        /// <summary>
        /// Id type.
        /// </summary>
        protected override Type IdType
        {
            get { return typeof(EventId); }
        }
    }

    /// <summary>
    /// Contact item partial.
    /// </summary>
    public partial class Contact
    {
        /// <summary>
        /// Create new instance of <see cref="Contact"/>
        /// </summary>
        /// <param name="service">Exchange service.</param>
        public Contact(ExchangeService service)
            : base(service)
        {
        }

        /// <summary>
        /// Id type.
        /// </summary>
        protected override Type IdType { get; }
    }

    /// <summary>
    /// OutlookTask item partial.
    /// </summary>
    public partial class OutlookTask
    {
        /// <summary>
        /// Create new instance of <see cref="OutlookTask"/>
        /// </summary>
        /// <param name="service">Exchange service.</param>
        public OutlookTask(ExchangeService service)
            : base(service)
        {
        }

        internal OutlookTask()
            : base()
        {
        }

        /// <summary>
        /// Id type.
        /// </summary>
        protected override Type IdType
        {
            get { return typeof(OutlookTaskId); }
        }
    }

    /// <summary>
    /// EventMessage item partial.
    /// </summary>
    public partial class EventMessage
    {
        /// <summary>
        /// Create new instance of <see cref="EventMessage"/>
        /// </summary>
        /// <param name="service">Exchange service.</param>
        public EventMessage(ExchangeService service)
            : base(service)
        {
        }
    }

    /// <summary>
    /// Post item partial.
    /// </summary>
    public partial class Post
    {
        /// <summary>
        /// Create new instance of <see cref="Post"/>
        /// </summary>
        /// <param name="service">Exchange service.</param>
        public Post(ExchangeService service)
            : base(service)
        {
        }

        /// <summary>
        /// Id type.
        /// </summary>
        protected override Type IdType { get; }
    }

    /// <summary>
    /// EventMessageRequest item partial.
    /// </summary>
    public partial class EventMessageRequest
    {
        /// <summary>
        /// Create new instance of <see cref="EventMessageRequest"/>
        /// </summary>
        /// <param name="service">Exchange service.</param>
        public EventMessageRequest(ExchangeService service)
            : base(service)
        {
        }
    }

    /// <summary>
    /// Outlook item partial.
    /// </summary>
    public abstract partial class OutlookItem
    {
        /// <summary>
        /// Create new instance of <see cref="OUtlookItem"/>
        /// </summary>
        /// <param name="service">Exchange service.</param>
        public OutlookItem(ExchangeService service)
            : base(service)
        {
        }

        /// <summary>
        /// Create new instance of <see cref="OutlookItem"/>
        /// </summary>
        internal OutlookItem()
        {
        }
        
        /// <summary>
        /// Type id this item implements.
        /// </summary>
        protected abstract Type IdType { get; }

        /// <summary>
        /// Item id.
        /// </summary>
        internal virtual ItemId ItemId
        {
            get
            {
                if (null == this.IdType)
                {
                    throw new NotImplementedException(this.GetType().FullName);
                }

                if (this.IsNew)
                {
                    return null;
                }

                if (string.IsNullOrEmpty(this.Id))
                {
                    return null;
                }

                if (this.MailboxId == null)
                {
                    return (ItemId) Activator.CreateInstance(
                        this.IdType,
                        this.Id,
                        MailboxId.Me);
                }

                return (ItemId)Activator.CreateInstance(
                    this.IdType,
                    this.Id,
                    this.MailboxId);
            }
        }

        /// <summary>
        /// Update outlook item.
        /// </summary>
        public void Update()
        {
            if (this.IsNew)
            {
                throw new ArgumentException("Cannot perform update on newly created item. Sync item from server and try again.");
            }

            if (this.propertyBag.GetChangedProperties().Count == 0)
            {
                throw new ArgumentException("No changed properties detected.");
            }

            OutlookItem outlookItem = this.Service.UpdateItem(this);
            this.propertyBag = outlookItem.propertyBag;
            this.MailboxId = outlookItem.MailboxId;
        }

        /// <summary>
        /// Save item on the server.
        /// </summary>
        /// <param name="parentFolderId">Parent folder id.</param>
        /// <returns></returns>
        public void Save(FolderId parentFolderId)
        {
            this.PreValidateSave();
            ArgumentValidator.ThrowIfNull(parentFolderId, nameof(parentFolderId));

            OutlookItem outlookItem = this.Service.CreateItem(
                this, 
                parentFolderId);

            this.propertyBag = outlookItem.propertyBag;
            this.MailboxId = outlookItem.MailboxId;
        }

        /// <summary>
        /// Save item on the server.
        /// </summary>
        /// <param name="wellKnownFolderName">Well known folder name.</param>
        /// <returns></returns>
        public void Save(WellKnownFolderName wellKnownFolderName)
        {
            FolderId parentFolderId = new FolderId(wellKnownFolderName);
            this.Save(parentFolderId);
        }

        /// <summary>
        /// Delete item.
        /// </summary>
        public void Delete()
        {
            this.PreValidateDelete();
            this.Service.DeleteItem(this.ItemId);
            this.propertyBag.Clear();
        }
    }

    /// <summary>
    /// Mail folder.
    /// </summary>
    public partial class MailFolder
    {
        /// <summary>
        /// Create new instance of <see cref="MailFolder"/>
        /// </summary>
        internal MailFolder()
            : base()
        {
        }

        /// <summary>
        /// Create new instance of <see cref="MailFolder"/>
        /// </summary>
        /// <param name="exchangeService"></param>
        public MailFolder(ExchangeService exchangeService)
            : base(exchangeService)
        {
        }

        /// <summary>
        /// Folder id.
        /// </summary>
        internal FolderId FolderId
        {
            get
            {
                if (this.IsNew)
                {
                    return null;
                }

                if (string.IsNullOrEmpty(this.Id))
                {
                    return null;
                }

                if (this.MailboxId == null)
                {
                    return new FolderId(this.Id);
                }

                return new FolderId(
                    this.Id, 
                    this.MailboxId.Id);
            }
        }

        /// <summary>
        /// Bind to a particular folder.
        /// </summary>
        /// <param name="service"></param>
        /// <param name="folderId"></param>
        /// <returns></returns>
        public static MailFolder Bind(ExchangeService service, FolderId folderId)
        {
            return service.GetFolder(folderId);
        }

        /// <summary>
        /// Update mail folder.
        /// </summary>
        public void Update()
        {
            this.PreValidateUpdate();
            MailFolder mailFolder = this.Service.UpdateFolder(this);
            this.propertyBag = mailFolder.propertyBag;
            this.MailboxId = mailFolder.MailboxId;
        }

        /// <summary>
        /// Save folder (Create new one).
        /// </summary>
        /// <param name="parentFolderId"></param>
        public void Save(FolderId parentFolderId)
        {
            this.PreValidateSave();
            ArgumentValidator.ThrowIfNullOrEmpty(this.DisplayName, nameof(this.DisplayName));
            ArgumentValidator.ThrowIfNull(parentFolderId, nameof(parentFolderId));

            MailFolder mailFolder = this.Service.CreateFolder(this, parentFolderId);
            this.propertyBag = mailFolder.propertyBag;
            this.MailboxId = mailFolder.MailboxId;
        }

        /// <summary>
        /// Save folder (Create new one).
        /// </summary>
        /// <param name="wellKnownFolderName"></param>
        public void Save(WellKnownFolderName wellKnownFolderName)
        {
            FolderId parentFolderId = new FolderId(wellKnownFolderName);
            this.Save(parentFolderId);
        }

        /// <summary>
        /// Delete folder.
        /// </summary>
        public void Delete()
        {
            this.PreValidateDelete();
            ArgumentValidator.ThrowIfNullOrEmpty(this.Id, nameof(this.Id));

            this.Service.DeleteFolder(this);
            this.propertyBag.Clear();
        }
    }

    /// <summary>
    /// Message rule partial.
    /// </summary>
    public partial class MessageRule
    {
        /// <summary>
        /// Create new instance of <see cref="MessageRule"/>
        /// <param name="service">Exchange service.</param>
        /// </summary>
        public MessageRule(ExchangeService service)
            : base(service)
        {
        }

        /// <summary>
        /// Create new instance of <see cref="MessageRule"/>
        /// </summary>
        internal MessageRule()
            : base()
        {
        }

        /// <summary>
        /// Update inbox rule.
        /// </summary>
        public void Update()
        {
            this.PreValidateUpdate();
            MessageRule rule = this.Service.UpdateInboxRule(this);
            this.propertyBag = rule.propertyBag;
            this.MailboxId = rule.MailboxId;
        }

        /// <summary>
        /// Delete inbox rule.
        /// </summary>
        public void Delete()
        {
            this.PreValidateDelete();
            ArgumentValidator.ThrowIfNullOrEmpty(this.Id, nameof(this.Id));

            this.Service.DeleteInboxRule(this);
            this.propertyBag.Clear();
        }

        /// <summary>
        /// Save message rule.
        /// </summary>
        /// <returns></returns>
        public void Save()
        {
            this.PreValidateSave();
            MessageRule rule = this.Service.CreateInboxRule(this);
            this.propertyBag = rule.propertyBag;
            this.MailboxId = rule.MailboxId;
        }
    }

    /// <summary>
    /// Inference classification override.
    /// </summary>
    public partial class InferenceClassificationOverride
    {
        /// <summary>
        /// Create new instance of <see cref="InferenceClassificationOverride"/>
        /// </summary>
        internal InferenceClassificationOverride()
            : base()
        {
        }

        /// <summary>
        /// Create new instance of <see cref="InferenceClassificationOverride"/>
        /// </summary>
        /// <param name="exchangeService">Exchange service.</param>
        public InferenceClassificationOverride(ExchangeService exchangeService)
            : base(exchangeService)
        {
        }

        /// <summary>
        /// Update override. 
        /// </summary>
        public void Update()
        {
            this.PreValidateUpdate();
            InferenceClassificationOverride inferenceClassificationOverride = this.Service.UpdateInferenceClassificationOverride(this);
            this.propertyBag = inferenceClassificationOverride.propertyBag;
        }

        /// <summary>
        /// Create override.
        /// </summary>
        public void Save()
        {
            this.PreValidateSave();
            InferenceClassificationOverride inferenceClassificationOverride = this.Service.CreateInferenceClassificationOverride(this);
            this.propertyBag = inferenceClassificationOverride.propertyBag;
        }

        /// <summary>
        /// Delete override. 
        /// </summary>
        public void Delete()
        {
            this.PreValidateDelete();
            this.Service.DeleteInferenceClassificationOverride(this);
            this.propertyBag.Clear();
        }
    }
}