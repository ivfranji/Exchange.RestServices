namespace Exchange.RestServices
{
    using System;
    using Microsoft.OutlookServices;

    public sealed class ContactFolderId : FolderId
    {
        /// <summary>
        /// Tasks.
        /// </summary>
        private const string Contacts = "contacts";

        public ContactFolderId(string mailboxId) 
            : this(ContactFolderId.Contacts, mailboxId)
        {
            this.RootContainer = "contacts";
            this.Id = string.Empty;
        }

        public ContactFolderId(string id, string mailbox) 
            : base(id, mailbox, typeof(ContactFolder))
        {
        }

        /// <summary>
        /// Child folders container.
        /// </summary>
        public override string ChildFoldersContainer
        {
            get
            {
                return this.IdPath;
            }
        }

        /// <summary>
        /// Messages (Contacts) container.
        /// </summary>
        public override string MessagesContainer
        {
            get
            {
                // Contacts can be stored in /contacts and /contactfolders/id/contacts
                if (this.IdPath == ContactFolderId.Contacts)
                {
                    return this.IdPath;
                }

                return $"{this.IdPath}/{ContactFolderId.Contacts}";
            }
        }
    }

    /// <summary>
    /// Outlook task folder id.
    /// </summary>
    public class TaskFolderId : FolderId
    {
        /// <summary>
        /// Tasks.
        /// </summary>
        private const string Tasks = "tasks";

        /// <summary>
        /// Create new instance of <see cref="TaskFolderId"/>
        /// </summary>
        /// <param name="id"></param>
        /// <param name="mailboxId"></param>
        public TaskFolderId(string id, string mailboxId)
            : base(id, mailboxId, typeof(TaskFolder))
        {
        }

        /// <summary>
        /// Create default task folders id.
        /// </summary>
        /// <param name="mailbox"></param>
        public TaskFolderId(string mailbox)
            : this(TaskFolderId.Tasks, mailbox)
        {
            this.RootContainer = TaskFolderId.Tasks;
            this.Id = string.Empty;
        }

        /// <summary>
        /// Child folders container.
        /// </summary>
        public override string ChildFoldersContainer
        {
            get { return this.IdPath; }
        }

        /// <summary>
        /// Messages (Tasks) container.
        /// </summary>
        public override string MessagesContainer
        {
            get
            {
                if (this.IdPath == TaskFolderId.Tasks)
                {
                    return this.IdPath;
                }

                return $"{this.IdPath}/{TaskFolderId.Tasks}";
            }
        }
    }

    /// <summary>
    /// Calendar folder id.
    /// </summary>
    public class CalendarFolderId : FolderId
    {
        /// <summary>
        /// Events entity name.
        /// </summary>
        private const string Events = "events";

        /// <summary>
        /// Create new instance of <see cref="CalendarFolderId"/>
        /// </summary>
        /// <param name="id"></param>
        /// <param name="mailboxId"></param>
        public CalendarFolderId(string id, string mailboxId)
            : base(id, mailboxId, typeof(Calendar))
        {
        }

        /// <summary>
        /// Create default calendar id.
        /// </summary>
        /// <param name="mailbox"></param>
        public CalendarFolderId(string mailbox)
            : base("calendar", mailbox)
        {
            this.RootContainer = "calendar";
            this.Id = string.Empty;
        }

        /// <summary>
        /// Child folders container.
        /// </summary>
        public override string ChildFoldersContainer
        {
            get { return this.IdPath; }
        }

        /// <summary>
        /// Messages (Events) container.
        /// Messages (Events) container.
        /// </summary>
        public override string MessagesContainer
        {
            get
            {
                return $"{this.IdPath}/{CalendarFolderId.Events}";
            }
        }
    }

    /// <summary>
    /// Folder Id.
    /// </summary>
    public class FolderId : EntityId
    {
        /// <summary>
        /// Child folders entity name.
        /// </summary>
        private const string ChildFolders = "childfolders";

        /// <summary>
        /// Messages entity name.
        /// </summary>
        private const string Messages = "messages";

        /// <summary>
        /// Delta part.
        /// </summary>
        private const string Delta = "delta";

        /// <summary>
        /// Create new instance of <see cref="FolderId"/> based on raw id.
        /// </summary>
        /// <param name="id">Raw id.</param>
        public FolderId(string id)
            : this(id, string.Empty)
        {
        }

        /// <summary>
        /// Create new instance of <see cref="FolderId"/> based on <see cref="WellKnownFolderName"/>.
        /// </summary>
        /// <param name="wellKnownFolderName">Well known folder name.</param>
        public FolderId(WellKnownFolderName wellKnownFolderName)
            : this(wellKnownFolderName, string.Empty)
        {
        }

        /// <summary>
        /// Create new instance of <see cref="FolderId"/> base on <see cref="WellKnownFolderName"/> and mailbox.
        /// </summary>
        /// <param name="wellKnownFolderName">Well known folder name.</param>
        /// <param name="mailbox">MailboxId.</param>
        public FolderId(WellKnownFolderName wellKnownFolderName, string mailbox)
            : this(wellKnownFolderName.ToString(), mailbox)
        {
        }

        /// <summary>
        /// Create new instance of <see cref="FolderId"/> base on raw id and mailbox.
        /// </summary>
        /// <param name="id">Folder id.</param>
        /// <param name="mailbox">MailboxId.</param>
        public FolderId(string id, string mailbox)
            : this(id, mailbox, typeof(MailFolder))
        {
        }

        protected FolderId(string id, string mailbox, Type folderType)
            : base(id, mailbox, folderType)
        {
        }

        /// <summary>
        /// Child folders path.
        /// </summary>
        public virtual string ChildFoldersContainer
        {
            get { return $"{this.IdPath}/{FolderId.ChildFolders}"; }
        }

        /// <summary>
        /// Messages path.
        /// </summary>
        public virtual string MessagesContainer
        {
            get { return $"{this.IdPath}/{FolderId.Messages}"; }
        }

        /// <summary>
        /// Delta path.
        /// </summary>
        public string MessagesDelta
        {
            get { return $"{this.MessagesContainer}/{FolderId.Delta}"; }
        }

        /// <summary>
        /// Message rules.
        /// </summary>
        internal string MessageRules
        {
            get { return $"{this.IdPath}/messagerules"; }
        }
        
        /// <summary>
        /// ToString impl.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this.Id;
        }
    }
}
