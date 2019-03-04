namespace Microsoft.RestServices.Exchange
{
    using System;

    /// <summary>
    /// Entity Id.
    /// </summary>
    public abstract class EntityId
    {
        /// <summary>
        /// Message type name.
        /// </summary>
        private const string MessageTypeName = "Message";

        /// <summary>
        /// Mail folder type name.
        /// </summary>
        private const string MailFolderTypeName = "MailFolder";

        /// <summary>
        /// Attachment type name.
        /// </summary>
        private const string AttachmentTypeName = "Attachment";

        /// <summary>
        /// Calendar type name.
        /// </summary>
        private const string CalendarFolderTypeName = "Calendar";

        /// <summary>
        /// Event type name.
        /// </summary>
        private const string EventsTypeName = "Event";

        /// <summary>
        /// Outlook task type name.
        /// </summary>
        private const string TaskTypeName = "Task";

        /// <summary>
        /// Outlook task folder type name.
        /// </summary>
        private const string TaskFolderTypeName = "TaskFolder";

        /// <summary>
        /// Inference classification.
        /// </summary>
        private const string InferenceClassificationName = "InferenceClassification";

        /// <summary>
        /// Contacts folder
        /// </summary>
        private const string ContactFolderTypeName = "ContactFolder";

        /// <summary>
        /// Contacts folder
        /// </summary>
        private const string ContactsTypeName = "Contact";

        /// <summary>
        /// Create new instance of <see cref="EntityId"/>
        /// </summary>
        /// <param name="entityId">Entity id.</param>
        /// <param name="mailboxId">Mailbox id.</param>
        protected EntityId(string entityId, string mailboxId, Type idType)
            : this(entityId, new MailboxId(mailboxId), idType)
        {
        }

        /// <summary>
        /// Create new instance of <see cref="EntityId"/>
        /// </summary>
        /// <param name="entityId">Entity id.</param>
        /// <param name="mailboxId">Mailbox id.</param>
        protected EntityId(string entityId, MailboxId mailboxId, Type idType)
        {
            ArgumentValidator.ThrowIfNullOrEmpty(entityId, nameof(entityId));
            ArgumentValidator.ThrowIfNull(mailboxId, nameof(mailboxId));
            ArgumentValidator.ThrowIfNull(idType, nameof(idType));

            switch (idType.Name)
            {
                case EntityId.MessageTypeName:
                    this.RootContainer = "messages";
                    break;

                case EntityId.MailFolderTypeName:
                    this.RootContainer = "mailfolders";
                    break;

                case EntityId.AttachmentTypeName:
                    this.RootContainer = "attachments";
                    break;

                case EntityId.CalendarFolderTypeName:
                    this.RootContainer = "calendars";
                    break;

                case EntityId.EventsTypeName:
                    this.RootContainer = "events";
                    break;

                case EntityId.TaskTypeName:
                    this.RootContainer = "tasks";
                    break;

                case EntityId.TaskFolderTypeName:
                    this.RootContainer = "tasksFolders";
                    break;

                case EntityId.InferenceClassificationName:
                    this.RootContainer = "inferenceClassification";
                    break;

                case EntityId.ContactFolderTypeName:
                    this.RootContainer = "contactfolders";
                    break;

                case EntityId.ContactsTypeName:
                    this.RootContainer = "contacts";
                    break;

                default:
                    throw new NotImplementedException(idType.Name);
            }

            this.MailboxId = mailboxId;
            this.Id = entityId;
        }

        /// <summary>
        /// Mailbox id.
        /// </summary>
        public MailboxId MailboxId { get; }

        /// <summary>
        /// Entity id.
        /// </summary>
        public string Id { get; protected set; }

        /// <summary>
        /// Root container for item.
        /// </summary>
        public string RootContainer { get; protected set; }

        /// <summary>
        /// Id path, without mailbox.
        /// </summary>
        public string IdPath
        {
            get
            {
                // calendar for an instance may have default calendar as /calendar
                // without root and specified id.
                if (string.IsNullOrEmpty(this.Id))
                {
                    return this.RootContainer;
                }

                return $"{this.RootContainer}/{this.Id}";
            }
        }
    }
}