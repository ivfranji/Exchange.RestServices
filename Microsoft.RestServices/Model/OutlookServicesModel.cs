namespace Microsoft.OutlookServices
{
    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json;

    /// <summary>
    /// Entity
    /// </summary>
    public abstract partial class Entity
    {

        /// <summary>
        /// Id
        /// </summary>
        [JsonProperty("Id", NullValueHandling = NullValueHandling.Ignore)]
        public string Id
        {
            get { return (string) this.propertyBag[EntityObjectSchema.Id]; }
            set { this.propertyBag[EntityObjectSchema.Id] = value; }
        }
    }


    /// <summary>
    /// DirectoryObject
    /// </summary>
    public abstract partial class DirectoryObject : Entity
    {
    }


    /// <summary>
    /// User
    /// </summary>
    public partial class User : DirectoryObject
    {

        /// <summary>
        /// EmailAddress
        /// </summary>
        [JsonProperty("EmailAddress", NullValueHandling = NullValueHandling.Ignore)]
        public string EmailAddress
        {
            get { return (string) this.propertyBag[UserObjectSchema.EmailAddress]; }
            set { this.propertyBag[UserObjectSchema.EmailAddress] = value; }
        }

        /// <summary>
        /// DisplayName
        /// </summary>
        [JsonProperty("DisplayName", NullValueHandling = NullValueHandling.Ignore)]
        public string DisplayName
        {
            get { return (string) this.propertyBag[UserObjectSchema.DisplayName]; }
            set { this.propertyBag[UserObjectSchema.DisplayName] = value; }
        }

        /// <summary>
        /// Alias
        /// </summary>
        [JsonProperty("Alias", NullValueHandling = NullValueHandling.Ignore)]
        public string Alias
        {
            get { return (string) this.propertyBag[UserObjectSchema.Alias]; }
            set { this.propertyBag[UserObjectSchema.Alias] = value; }
        }

        /// <summary>
        /// MailboxGuid
        /// </summary>
        [JsonProperty("MailboxGuid", NullValueHandling = NullValueHandling.Ignore)]
        public Guid MailboxGuid
        {
            get { return (Guid) this.propertyBag[UserObjectSchema.MailboxGuid]; }
            set { this.propertyBag[UserObjectSchema.MailboxGuid] = value; }
        }

        /// <summary>
        /// MailboxSettings
        /// </summary>
        [JsonProperty("MailboxSettings", NullValueHandling = NullValueHandling.Ignore)]
        public MailboxSettings MailboxSettings
        {
            get { return (MailboxSettings) this.propertyBag[UserObjectSchema.MailboxSettings]; }
            set { this.propertyBag[UserObjectSchema.MailboxSettings] = value; }
        }

        /// <summary>
        /// Subscriptions
        /// </summary>
        [JsonProperty("Subscriptions", NullValueHandling = NullValueHandling.Ignore)]
        public IList<Subscription> Subscriptions
        {
            get { return (IList<Subscription>) this.propertyBag[UserObjectSchema.Subscriptions]; }
            set { this.propertyBag[UserObjectSchema.Subscriptions] = value; }
        }

        /// <summary>
        /// TaskGroups
        /// </summary>
        [JsonProperty("TaskGroups", NullValueHandling = NullValueHandling.Ignore)]
        public IList<TaskGroup> TaskGroups
        {
            get { return (IList<TaskGroup>) this.propertyBag[UserObjectSchema.TaskGroups]; }
            set { this.propertyBag[UserObjectSchema.TaskGroups] = value; }
        }

        /// <summary>
        /// TaskFolders
        /// </summary>
        [JsonProperty("TaskFolders", NullValueHandling = NullValueHandling.Ignore)]
        public IList<TaskFolder> TaskFolders
        {
            get { return (IList<TaskFolder>) this.propertyBag[UserObjectSchema.TaskFolders]; }
            set { this.propertyBag[UserObjectSchema.TaskFolders] = value; }
        }

        /// <summary>
        /// Tasks
        /// </summary>
        [JsonProperty("Tasks", NullValueHandling = NullValueHandling.Ignore)]
        public IList<Task> Tasks
        {
            get { return (IList<Task>) this.propertyBag[UserObjectSchema.Tasks]; }
            set { this.propertyBag[UserObjectSchema.Tasks] = value; }
        }

        /// <summary>
        /// Messages
        /// </summary>
        [JsonProperty("Messages", NullValueHandling = NullValueHandling.Ignore)]
        public IList<Message> Messages
        {
            get { return (IList<Message>) this.propertyBag[UserObjectSchema.Messages]; }
            set { this.propertyBag[UserObjectSchema.Messages] = value; }
        }

        /// <summary>
        /// JoinedGroups
        /// </summary>
        [JsonProperty("JoinedGroups", NullValueHandling = NullValueHandling.Ignore)]
        public IList<Group> JoinedGroups
        {
            get { return (IList<Group>) this.propertyBag[UserObjectSchema.JoinedGroups]; }
            set { this.propertyBag[UserObjectSchema.JoinedGroups] = value; }
        }

        /// <summary>
        /// MailFolders
        /// </summary>
        [JsonProperty("MailFolders", NullValueHandling = NullValueHandling.Ignore)]
        public IList<MailFolder> MailFolders
        {
            get { return (IList<MailFolder>) this.propertyBag[UserObjectSchema.MailFolders]; }
            set { this.propertyBag[UserObjectSchema.MailFolders] = value; }
        }

        /// <summary>
        /// Calendar
        /// </summary>
        [JsonProperty("Calendar", NullValueHandling = NullValueHandling.Ignore)]
        public Calendar Calendar
        {
            get { return (Calendar) this.propertyBag[UserObjectSchema.Calendar]; }
            set { this.propertyBag[UserObjectSchema.Calendar] = value; }
        }

        /// <summary>
        /// Calendars
        /// </summary>
        [JsonProperty("Calendars", NullValueHandling = NullValueHandling.Ignore)]
        public IList<Calendar> Calendars
        {
            get { return (IList<Calendar>) this.propertyBag[UserObjectSchema.Calendars]; }
            set { this.propertyBag[UserObjectSchema.Calendars] = value; }
        }

        /// <summary>
        /// CalendarGroups
        /// </summary>
        [JsonProperty("CalendarGroups", NullValueHandling = NullValueHandling.Ignore)]
        public IList<CalendarGroup> CalendarGroups
        {
            get { return (IList<CalendarGroup>) this.propertyBag[UserObjectSchema.CalendarGroups]; }
            set { this.propertyBag[UserObjectSchema.CalendarGroups] = value; }
        }

        /// <summary>
        /// CalendarView
        /// </summary>
        [JsonProperty("CalendarView", NullValueHandling = NullValueHandling.Ignore)]
        public IList<Event> CalendarView
        {
            get { return (IList<Event>) this.propertyBag[UserObjectSchema.CalendarView]; }
            set { this.propertyBag[UserObjectSchema.CalendarView] = value; }
        }

        /// <summary>
        /// Events
        /// </summary>
        [JsonProperty("Events", NullValueHandling = NullValueHandling.Ignore)]
        public IList<Event> Events
        {
            get { return (IList<Event>) this.propertyBag[UserObjectSchema.Events]; }
            set { this.propertyBag[UserObjectSchema.Events] = value; }
        }

        /// <summary>
        /// People
        /// </summary>
        [JsonProperty("People", NullValueHandling = NullValueHandling.Ignore)]
        public IList<Person> People
        {
            get { return (IList<Person>) this.propertyBag[UserObjectSchema.People]; }
            set { this.propertyBag[UserObjectSchema.People] = value; }
        }

        /// <summary>
        /// Contacts
        /// </summary>
        [JsonProperty("Contacts", NullValueHandling = NullValueHandling.Ignore)]
        public IList<Contact> Contacts
        {
            get { return (IList<Contact>) this.propertyBag[UserObjectSchema.Contacts]; }
            set { this.propertyBag[UserObjectSchema.Contacts] = value; }
        }

        /// <summary>
        /// ContactFolders
        /// </summary>
        [JsonProperty("ContactFolders", NullValueHandling = NullValueHandling.Ignore)]
        public IList<ContactFolder> ContactFolders
        {
            get { return (IList<ContactFolder>) this.propertyBag[UserObjectSchema.ContactFolders]; }
            set { this.propertyBag[UserObjectSchema.ContactFolders] = value; }
        }

        /// <summary>
        /// MasterCategories
        /// </summary>
        [JsonProperty("MasterCategories", NullValueHandling = NullValueHandling.Ignore)]
        public IList<OutlookCategory> MasterCategories
        {
            get { return (IList<OutlookCategory>) this.propertyBag[UserObjectSchema.MasterCategories]; }
            set { this.propertyBag[UserObjectSchema.MasterCategories] = value; }
        }

        /// <summary>
        /// InferenceClassification
        /// </summary>
        [JsonProperty("InferenceClassification", NullValueHandling = NullValueHandling.Ignore)]
        public InferenceClassification InferenceClassification
        {
            get { return (InferenceClassification) this.propertyBag[UserObjectSchema.InferenceClassification]; }
            set { this.propertyBag[UserObjectSchema.InferenceClassification] = value; }
        }

        /// <summary>
        /// Photo
        /// </summary>
        [JsonProperty("Photo", NullValueHandling = NullValueHandling.Ignore)]
        public Photo Photo
        {
            get { return (Photo) this.propertyBag[UserObjectSchema.Photo]; }
            set { this.propertyBag[UserObjectSchema.Photo] = value; }
        }

        /// <summary>
        /// Photos
        /// </summary>
        [JsonProperty("Photos", NullValueHandling = NullValueHandling.Ignore)]
        public IList<Photo> Photos
        {
            get { return (IList<Photo>) this.propertyBag[UserObjectSchema.Photos]; }
            set { this.propertyBag[UserObjectSchema.Photos] = value; }
        }

        /// <summary>
        /// SendMail
        /// </summary>
        public void SendMail(Message message, bool saveToSentItems)
        {
            Dictionary<string, object> additionalParameters = new Dictionary<string, object>();
            additionalParameters.Add(nameof(message), message);
            additionalParameters.Add(nameof(saveToSentItems), saveToSentItems);
            this.Service.Invoke(nameof(this.SendMail), this, additionalParameters);
        }


        /// <summary>
        /// GetMailTips
        /// </summary>
        public IList<MailTips> GetMailTips(IList<String> emailAddresses, MailTipsType mailTipsOptions)
        {
            Dictionary<string, object> additionalParameters = new Dictionary<string, object>();
            additionalParameters.Add(nameof(emailAddresses), emailAddresses);
            additionalParameters.Add(nameof(mailTipsOptions), mailTipsOptions);
            return (IList<MailTips>)this.Service.Invoke(nameof(this.GetMailTips), this, additionalParameters);
        }


        /// <summary>
        /// FindMeetingTimes
        /// </summary>
        public MeetingTimeSuggestionsResult FindMeetingTimes(IList<AttendeeBase> attendees, LocationConstraint locationConstraint, TimeConstraint timeConstraint, TimeSpan meetingDuration, int maxCandidates, bool isOrganizerOptional, bool returnSuggestionReasons, Double minimumAttendeePercentage)
        {
            Dictionary<string, object> additionalParameters = new Dictionary<string, object>();
            additionalParameters.Add(nameof(attendees), attendees);
            additionalParameters.Add(nameof(locationConstraint), locationConstraint);
            additionalParameters.Add(nameof(timeConstraint), timeConstraint);
            additionalParameters.Add(nameof(meetingDuration), meetingDuration);
            additionalParameters.Add(nameof(maxCandidates), maxCandidates);
            additionalParameters.Add(nameof(isOrganizerOptional), isOrganizerOptional);
            additionalParameters.Add(nameof(returnSuggestionReasons), returnSuggestionReasons);
            additionalParameters.Add(nameof(minimumAttendeePercentage), minimumAttendeePercentage);
            return (MeetingTimeSuggestionsResult)this.Service.Invoke(nameof(this.FindMeetingTimes), this, additionalParameters);
        }


        /// <summary>
        /// ReminderView
        /// </summary>
        public IList<Reminder> ReminderView(DateTime startDateTime, DateTime endDateTime)
        {
            Dictionary<string, object> additionalParameters = new Dictionary<string, object>();
            additionalParameters.Add(nameof(startDateTime), startDateTime);
            additionalParameters.Add(nameof(endDateTime), endDateTime);
            return (IList<Reminder>)this.Service.Invoke(nameof(this.ReminderView), this, additionalParameters);
        }


        /// <summary>
        /// SupportedLanguages
        /// </summary>
        public IList<LocaleInfo> SupportedLanguages()
        {
            Dictionary<string, object> additionalParameters = new Dictionary<string, object>();
            return (IList<LocaleInfo>)this.Service.Invoke(nameof(this.SupportedLanguages), this, additionalParameters);
        }


        /// <summary>
        /// SupportedTimeZones
        /// </summary>
        public IList<TimeZoneInformation> SupportedTimeZones()
        {
            Dictionary<string, object> additionalParameters = new Dictionary<string, object>();
            return (IList<TimeZoneInformation>)this.Service.Invoke(nameof(this.SupportedTimeZones), this, additionalParameters);
        }


        /// <summary>
        /// SupportedTimeZones
        /// </summary>
        public IList<TimeZoneInformation> SupportedTimeZones(TimeZoneStandard timeZoneStandard)
        {
            Dictionary<string, object> additionalParameters = new Dictionary<string, object>();
            additionalParameters.Add(nameof(timeZoneStandard), timeZoneStandard);
            return (IList<TimeZoneInformation>)this.Service.Invoke(nameof(this.SupportedTimeZones), this, additionalParameters);
        }

    }


    /// <summary>
    /// Group
    /// </summary>
    public partial class Group : DirectoryObject
    {

        /// <summary>
        /// AccessType
        /// </summary>
        [JsonProperty("AccessType", NullValueHandling = NullValueHandling.Ignore)]
        public GroupAccessType AccessType
        {
            get { return (GroupAccessType) this.propertyBag[GroupObjectSchema.AccessType]; }
            set { this.propertyBag[GroupObjectSchema.AccessType] = value; }
        }

        /// <summary>
        /// AllowExternalSenders
        /// </summary>
        [JsonProperty("AllowExternalSenders", NullValueHandling = NullValueHandling.Ignore)]
        public bool AllowExternalSenders
        {
            get { return (bool) this.propertyBag[GroupObjectSchema.AllowExternalSenders]; }
            set { this.propertyBag[GroupObjectSchema.AllowExternalSenders] = value; }
        }

        /// <summary>
        /// AutoSubscribeNewMembers
        /// </summary>
        [JsonProperty("AutoSubscribeNewMembers", NullValueHandling = NullValueHandling.Ignore)]
        public bool AutoSubscribeNewMembers
        {
            get { return (bool) this.propertyBag[GroupObjectSchema.AutoSubscribeNewMembers]; }
            set { this.propertyBag[GroupObjectSchema.AutoSubscribeNewMembers] = value; }
        }

        /// <summary>
        /// Description
        /// </summary>
        [JsonProperty("Description", NullValueHandling = NullValueHandling.Ignore)]
        public string Description
        {
            get { return (string) this.propertyBag[GroupObjectSchema.Description]; }
            set { this.propertyBag[GroupObjectSchema.Description] = value; }
        }

        /// <summary>
        /// DisplayName
        /// </summary>
        [JsonProperty("DisplayName", NullValueHandling = NullValueHandling.Ignore)]
        public string DisplayName
        {
            get { return (string) this.propertyBag[GroupObjectSchema.DisplayName]; }
            set { this.propertyBag[GroupObjectSchema.DisplayName] = value; }
        }

        /// <summary>
        /// EmailAddress
        /// </summary>
        [JsonProperty("EmailAddress", NullValueHandling = NullValueHandling.Ignore)]
        public string EmailAddress
        {
            get { return (string) this.propertyBag[GroupObjectSchema.EmailAddress]; }
            set { this.propertyBag[GroupObjectSchema.EmailAddress] = value; }
        }

        /// <summary>
        /// Alias
        /// </summary>
        [JsonProperty("Alias", NullValueHandling = NullValueHandling.Ignore)]
        public string Alias
        {
            get { return (string) this.propertyBag[GroupObjectSchema.Alias]; }
            set { this.propertyBag[GroupObjectSchema.Alias] = value; }
        }

        /// <summary>
        /// IsFavorite
        /// </summary>
        [JsonProperty("IsFavorite", NullValueHandling = NullValueHandling.Ignore)]
        public bool IsFavorite
        {
            get { return (bool) this.propertyBag[GroupObjectSchema.IsFavorite]; }
            set { this.propertyBag[GroupObjectSchema.IsFavorite] = value; }
        }

        /// <summary>
        /// IsMember
        /// </summary>
        [JsonProperty("IsMember", NullValueHandling = NullValueHandling.Ignore)]
        public bool IsMember
        {
            get { return (bool) this.propertyBag[GroupObjectSchema.IsMember]; }
            set { this.propertyBag[GroupObjectSchema.IsMember] = value; }
        }

        /// <summary>
        /// IsSubscribedByMail
        /// </summary>
        [JsonProperty("IsSubscribedByMail", NullValueHandling = NullValueHandling.Ignore)]
        public bool IsSubscribedByMail
        {
            get { return (bool) this.propertyBag[GroupObjectSchema.IsSubscribedByMail]; }
            set { this.propertyBag[GroupObjectSchema.IsSubscribedByMail] = value; }
        }

        /// <summary>
        /// LastVisitedDateTime
        /// </summary>
        [JsonProperty("LastVisitedDateTime", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset LastVisitedDateTime
        {
            get { return (DateTimeOffset) this.propertyBag[GroupObjectSchema.LastVisitedDateTime]; }
            set { this.propertyBag[GroupObjectSchema.LastVisitedDateTime] = value; }
        }

        /// <summary>
        /// UnseenCount
        /// </summary>
        [JsonProperty("UnseenCount", NullValueHandling = NullValueHandling.Ignore)]
        public int UnseenCount
        {
            get { return (int) this.propertyBag[GroupObjectSchema.UnseenCount]; }
            set { this.propertyBag[GroupObjectSchema.UnseenCount] = value; }
        }

        /// <summary>
        /// Threads
        /// </summary>
        [JsonProperty("Threads", NullValueHandling = NullValueHandling.Ignore)]
        public IList<ConversationThread> Threads
        {
            get { return (IList<ConversationThread>) this.propertyBag[GroupObjectSchema.Threads]; }
            set { this.propertyBag[GroupObjectSchema.Threads] = value; }
        }

        /// <summary>
        /// Calendar
        /// </summary>
        [JsonProperty("Calendar", NullValueHandling = NullValueHandling.Ignore)]
        public Calendar Calendar
        {
            get { return (Calendar) this.propertyBag[GroupObjectSchema.Calendar]; }
            set { this.propertyBag[GroupObjectSchema.Calendar] = value; }
        }

        /// <summary>
        /// CalendarView
        /// </summary>
        [JsonProperty("CalendarView", NullValueHandling = NullValueHandling.Ignore)]
        public IList<Event> CalendarView
        {
            get { return (IList<Event>) this.propertyBag[GroupObjectSchema.CalendarView]; }
            set { this.propertyBag[GroupObjectSchema.CalendarView] = value; }
        }

        /// <summary>
        /// Events
        /// </summary>
        [JsonProperty("Events", NullValueHandling = NullValueHandling.Ignore)]
        public IList<Event> Events
        {
            get { return (IList<Event>) this.propertyBag[GroupObjectSchema.Events]; }
            set { this.propertyBag[GroupObjectSchema.Events] = value; }
        }

        /// <summary>
        /// Conversations
        /// </summary>
        [JsonProperty("Conversations", NullValueHandling = NullValueHandling.Ignore)]
        public IList<Conversation> Conversations
        {
            get { return (IList<Conversation>) this.propertyBag[GroupObjectSchema.Conversations]; }
            set { this.propertyBag[GroupObjectSchema.Conversations] = value; }
        }

        /// <summary>
        /// Subscriptions
        /// </summary>
        [JsonProperty("Subscriptions", NullValueHandling = NullValueHandling.Ignore)]
        public IList<Subscription> Subscriptions
        {
            get { return (IList<Subscription>) this.propertyBag[GroupObjectSchema.Subscriptions]; }
            set { this.propertyBag[GroupObjectSchema.Subscriptions] = value; }
        }

        /// <summary>
        /// Photo
        /// </summary>
        [JsonProperty("Photo", NullValueHandling = NullValueHandling.Ignore)]
        public Photo Photo
        {
            get { return (Photo) this.propertyBag[GroupObjectSchema.Photo]; }
            set { this.propertyBag[GroupObjectSchema.Photo] = value; }
        }

        /// <summary>
        /// Photos
        /// </summary>
        [JsonProperty("Photos", NullValueHandling = NullValueHandling.Ignore)]
        public IList<Photo> Photos
        {
            get { return (IList<Photo>) this.propertyBag[GroupObjectSchema.Photos]; }
            set { this.propertyBag[GroupObjectSchema.Photos] = value; }
        }

        /// <summary>
        /// AcceptedSenders
        /// </summary>
        [JsonProperty("AcceptedSenders", NullValueHandling = NullValueHandling.Ignore)]
        public IList<DirectoryObject> AcceptedSenders
        {
            get { return (IList<DirectoryObject>) this.propertyBag[GroupObjectSchema.AcceptedSenders]; }
            set { this.propertyBag[GroupObjectSchema.AcceptedSenders] = value; }
        }

        /// <summary>
        /// RejectedSenders
        /// </summary>
        [JsonProperty("RejectedSenders", NullValueHandling = NullValueHandling.Ignore)]
        public IList<DirectoryObject> RejectedSenders
        {
            get { return (IList<DirectoryObject>) this.propertyBag[GroupObjectSchema.RejectedSenders]; }
            set { this.propertyBag[GroupObjectSchema.RejectedSenders] = value; }
        }

        /// <summary>
        /// SubscribeByMail
        /// </summary>
        public void SubscribeByMail()
        {
            Dictionary<string, object> additionalParameters = new Dictionary<string, object>();
            this.Service.Invoke(nameof(this.SubscribeByMail), this, additionalParameters);
        }


        /// <summary>
        /// UnsubscribeByMail
        /// </summary>
        public void UnsubscribeByMail()
        {
            Dictionary<string, object> additionalParameters = new Dictionary<string, object>();
            this.Service.Invoke(nameof(this.UnsubscribeByMail), this, additionalParameters);
        }


        /// <summary>
        /// AddFavorite
        /// </summary>
        public void AddFavorite()
        {
            Dictionary<string, object> additionalParameters = new Dictionary<string, object>();
            this.Service.Invoke(nameof(this.AddFavorite), this, additionalParameters);
        }


        /// <summary>
        /// RemoveFavorite
        /// </summary>
        public void RemoveFavorite()
        {
            Dictionary<string, object> additionalParameters = new Dictionary<string, object>();
            this.Service.Invoke(nameof(this.RemoveFavorite), this, additionalParameters);
        }


        /// <summary>
        /// ResetUnseenCount
        /// </summary>
        public void ResetUnseenCount()
        {
            Dictionary<string, object> additionalParameters = new Dictionary<string, object>();
            this.Service.Invoke(nameof(this.ResetUnseenCount), this, additionalParameters);
        }


        /// <summary>
        /// SetLastVisitedDateTime
        /// </summary>
        public void SetLastVisitedDateTime(DateTimeOffset lastVisitedDateTime)
        {
            Dictionary<string, object> additionalParameters = new Dictionary<string, object>();
            additionalParameters.Add(nameof(lastVisitedDateTime), lastVisitedDateTime);
            this.Service.Invoke(nameof(this.SetLastVisitedDateTime), this, additionalParameters);
        }

    }


    /// <summary>
    /// NotificationBase
    /// </summary>
    public abstract partial class NotificationBase : Entity
    {
    }


    /// <summary>
    /// Notification
    /// </summary>
    public partial class Notification : NotificationBase
    {

        /// <summary>
        /// SubscriptionId
        /// </summary>
        [JsonProperty("SubscriptionId", NullValueHandling = NullValueHandling.Ignore)]
        public string SubscriptionId
        {
            get { return (string) this.propertyBag[NotificationObjectSchema.SubscriptionId]; }
            set { this.propertyBag[NotificationObjectSchema.SubscriptionId] = value; }
        }

        /// <summary>
        /// SubscriptionExpirationDateTime
        /// </summary>
        [JsonProperty("SubscriptionExpirationDateTime", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset SubscriptionExpirationDateTime
        {
            get { return (DateTimeOffset) this.propertyBag[NotificationObjectSchema.SubscriptionExpirationDateTime]; }
            set { this.propertyBag[NotificationObjectSchema.SubscriptionExpirationDateTime] = value; }
        }

        /// <summary>
        /// SequenceNumber
        /// </summary>
        [JsonProperty("SequenceNumber", NullValueHandling = NullValueHandling.Ignore)]
        public int SequenceNumber
        {
            get { return (int) this.propertyBag[NotificationObjectSchema.SequenceNumber]; }
            set { this.propertyBag[NotificationObjectSchema.SequenceNumber] = value; }
        }

        /// <summary>
        /// ChangeType
        /// </summary>
        [JsonProperty("ChangeType", NullValueHandling = NullValueHandling.Ignore)]
        public ChangeType ChangeType
        {
            get { return (ChangeType) this.propertyBag[NotificationObjectSchema.ChangeType]; }
            set { this.propertyBag[NotificationObjectSchema.ChangeType] = value; }
        }

        /// <summary>
        /// Resource
        /// </summary>
        [JsonProperty("Resource", NullValueHandling = NullValueHandling.Ignore)]
        public string Resource
        {
            get { return (string) this.propertyBag[NotificationObjectSchema.Resource]; }
            set { this.propertyBag[NotificationObjectSchema.Resource] = value; }
        }

        /// <summary>
        /// ResourceData
        /// </summary>
        [JsonProperty("ResourceData", NullValueHandling = NullValueHandling.Ignore)]
        public Entity ResourceData
        {
            get { return (Entity) this.propertyBag[NotificationObjectSchema.ResourceData]; }
            set { this.propertyBag[NotificationObjectSchema.ResourceData] = value; }
        }
    }


    /// <summary>
    /// Subscription
    /// </summary>
    public abstract partial class Subscription : Entity
    {

        /// <summary>
        /// Resource
        /// </summary>
        [JsonProperty("Resource", NullValueHandling = NullValueHandling.Ignore)]
        public string Resource
        {
            get { return (string) this.propertyBag[SubscriptionObjectSchema.Resource]; }
            set { this.propertyBag[SubscriptionObjectSchema.Resource] = value; }
        }

        /// <summary>
        /// ChangeType
        /// </summary>
        [JsonProperty("ChangeType", NullValueHandling = NullValueHandling.Ignore)]
        public ChangeType ChangeType
        {
            get { return (ChangeType) this.propertyBag[SubscriptionObjectSchema.ChangeType]; }
            set { this.propertyBag[SubscriptionObjectSchema.ChangeType] = value; }
        }
    }


    /// <summary>
    /// PushSubscription
    /// </summary>
    public partial class PushSubscription : Subscription
    {

        /// <summary>
        /// NotificationURL
        /// </summary>
        [JsonProperty("NotificationURL", NullValueHandling = NullValueHandling.Ignore)]
        public string NotificationURL
        {
            get { return (string) this.propertyBag[PushSubscriptionObjectSchema.NotificationURL]; }
            set { this.propertyBag[PushSubscriptionObjectSchema.NotificationURL] = value; }
        }

        /// <summary>
        /// SubscriptionExpirationDateTime
        /// </summary>
        [JsonProperty("SubscriptionExpirationDateTime", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset SubscriptionExpirationDateTime
        {
            get { return (DateTimeOffset) this.propertyBag[PushSubscriptionObjectSchema.SubscriptionExpirationDateTime]; }
            set { this.propertyBag[PushSubscriptionObjectSchema.SubscriptionExpirationDateTime] = value; }
        }

        /// <summary>
        /// ClientState
        /// </summary>
        [JsonProperty("ClientState", NullValueHandling = NullValueHandling.Ignore)]
        public string ClientState
        {
            get { return (string) this.propertyBag[PushSubscriptionObjectSchema.ClientState]; }
            set { this.propertyBag[PushSubscriptionObjectSchema.ClientState] = value; }
        }
    }


    /// <summary>
    /// TaskGroup
    /// </summary>
    public partial class TaskGroup : Entity
    {

        /// <summary>
        /// ChangeKey
        /// </summary>
        [JsonProperty("ChangeKey", NullValueHandling = NullValueHandling.Ignore)]
        public string ChangeKey
        {
            get { return (string) this.propertyBag[TaskGroupObjectSchema.ChangeKey]; }
            set { this.propertyBag[TaskGroupObjectSchema.ChangeKey] = value; }
        }

        /// <summary>
        /// IsDefaultGroup
        /// </summary>
        [JsonProperty("IsDefaultGroup", NullValueHandling = NullValueHandling.Ignore)]
        public bool IsDefaultGroup
        {
            get { return (bool) this.propertyBag[TaskGroupObjectSchema.IsDefaultGroup]; }
            set { this.propertyBag[TaskGroupObjectSchema.IsDefaultGroup] = value; }
        }

        /// <summary>
        /// Name
        /// </summary>
        [JsonProperty("Name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name
        {
            get { return (string) this.propertyBag[TaskGroupObjectSchema.Name]; }
            set { this.propertyBag[TaskGroupObjectSchema.Name] = value; }
        }

        /// <summary>
        /// GroupKey
        /// </summary>
        [JsonProperty("GroupKey", NullValueHandling = NullValueHandling.Ignore)]
        public Guid GroupKey
        {
            get { return (Guid) this.propertyBag[TaskGroupObjectSchema.GroupKey]; }
            set { this.propertyBag[TaskGroupObjectSchema.GroupKey] = value; }
        }

        /// <summary>
        /// TaskFolders
        /// </summary>
        [JsonProperty("TaskFolders", NullValueHandling = NullValueHandling.Ignore)]
        public IList<TaskFolder> TaskFolders
        {
            get { return (IList<TaskFolder>) this.propertyBag[TaskGroupObjectSchema.TaskFolders]; }
            set { this.propertyBag[TaskGroupObjectSchema.TaskFolders] = value; }
        }
    }


    /// <summary>
    /// TaskFolder
    /// </summary>
    public partial class TaskFolder : Entity
    {

        /// <summary>
        /// ChangeKey
        /// </summary>
        [JsonProperty("ChangeKey", NullValueHandling = NullValueHandling.Ignore)]
        public string ChangeKey
        {
            get { return (string) this.propertyBag[TaskFolderObjectSchema.ChangeKey]; }
            set { this.propertyBag[TaskFolderObjectSchema.ChangeKey] = value; }
        }

        /// <summary>
        /// Name
        /// </summary>
        [JsonProperty("Name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name
        {
            get { return (string) this.propertyBag[TaskFolderObjectSchema.Name]; }
            set { this.propertyBag[TaskFolderObjectSchema.Name] = value; }
        }

        /// <summary>
        /// IsDefaultFolder
        /// </summary>
        [JsonProperty("IsDefaultFolder", NullValueHandling = NullValueHandling.Ignore)]
        public bool IsDefaultFolder
        {
            get { return (bool) this.propertyBag[TaskFolderObjectSchema.IsDefaultFolder]; }
            set { this.propertyBag[TaskFolderObjectSchema.IsDefaultFolder] = value; }
        }

        /// <summary>
        /// ParentGroupKey
        /// </summary>
        [JsonProperty("ParentGroupKey", NullValueHandling = NullValueHandling.Ignore)]
        public Guid ParentGroupKey
        {
            get { return (Guid) this.propertyBag[TaskFolderObjectSchema.ParentGroupKey]; }
            set { this.propertyBag[TaskFolderObjectSchema.ParentGroupKey] = value; }
        }

        /// <summary>
        /// Tasks
        /// </summary>
        [JsonProperty("Tasks", NullValueHandling = NullValueHandling.Ignore)]
        public IList<Task> Tasks
        {
            get { return (IList<Task>) this.propertyBag[TaskFolderObjectSchema.Tasks]; }
            set { this.propertyBag[TaskFolderObjectSchema.Tasks] = value; }
        }

        /// <summary>
        /// SingleValueExtendedProperties
        /// </summary>
        [JsonProperty("SingleValueExtendedProperties", NullValueHandling = NullValueHandling.Ignore)]
        public IList<SingleValueLegacyExtendedProperty> SingleValueExtendedProperties
        {
            get { return (IList<SingleValueLegacyExtendedProperty>) this.propertyBag[TaskFolderObjectSchema.SingleValueExtendedProperties]; }
            set { this.propertyBag[TaskFolderObjectSchema.SingleValueExtendedProperties] = value; }
        }

        /// <summary>
        /// MultiValueExtendedProperties
        /// </summary>
        [JsonProperty("MultiValueExtendedProperties", NullValueHandling = NullValueHandling.Ignore)]
        public IList<MultiValueLegacyExtendedProperty> MultiValueExtendedProperties
        {
            get { return (IList<MultiValueLegacyExtendedProperty>) this.propertyBag[TaskFolderObjectSchema.MultiValueExtendedProperties]; }
            set { this.propertyBag[TaskFolderObjectSchema.MultiValueExtendedProperties] = value; }
        }
    }


    /// <summary>
    /// SingleValueLegacyExtendedProperty
    /// </summary>
    public partial class SingleValueLegacyExtendedProperty
    {

        /// <summary>
        /// Value
        /// </summary>
        [JsonProperty("Value", NullValueHandling = NullValueHandling.Ignore)]
        public string Value
        {
            get { return (string) this.propertyBag[SingleValueLegacyExtendedPropertyObjectSchema.Value]; }
            set { this.propertyBag[SingleValueLegacyExtendedPropertyObjectSchema.Value] = value; }
        }

        /// <summary>
        /// PropertyId
        /// </summary>
        [JsonProperty("PropertyId", NullValueHandling = NullValueHandling.Ignore)]
        public string PropertyId
        {
            get { return (string) this.propertyBag[SingleValueLegacyExtendedPropertyObjectSchema.PropertyId]; }
            set { this.propertyBag[SingleValueLegacyExtendedPropertyObjectSchema.PropertyId] = value; }
        }
    }


    /// <summary>
    /// MultiValueLegacyExtendedProperty
    /// </summary>
    public partial class MultiValueLegacyExtendedProperty
    {

        /// <summary>
        /// Value
        /// </summary>
        [JsonProperty("Value", NullValueHandling = NullValueHandling.Ignore)]
        public IList<String> Value
        {
            get { return (IList<String>) this.propertyBag[MultiValueLegacyExtendedPropertyObjectSchema.Value]; }
            set { this.propertyBag[MultiValueLegacyExtendedPropertyObjectSchema.Value] = value; }
        }

        /// <summary>
        /// PropertyId
        /// </summary>
        [JsonProperty("PropertyId", NullValueHandling = NullValueHandling.Ignore)]
        public string PropertyId
        {
            get { return (string) this.propertyBag[MultiValueLegacyExtendedPropertyObjectSchema.PropertyId]; }
            set { this.propertyBag[MultiValueLegacyExtendedPropertyObjectSchema.PropertyId] = value; }
        }
    }


    /// <summary>
    /// Item
    /// </summary>
    public abstract partial class Item : Entity
    {

        /// <summary>
        /// CreatedDateTime
        /// </summary>
        [JsonProperty("CreatedDateTime", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset CreatedDateTime
        {
            get { return (DateTimeOffset) this.propertyBag[ItemObjectSchema.CreatedDateTime]; }
            set { this.propertyBag[ItemObjectSchema.CreatedDateTime] = value; }
        }

        /// <summary>
        /// LastModifiedDateTime
        /// </summary>
        [JsonProperty("LastModifiedDateTime", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset LastModifiedDateTime
        {
            get { return (DateTimeOffset) this.propertyBag[ItemObjectSchema.LastModifiedDateTime]; }
            set { this.propertyBag[ItemObjectSchema.LastModifiedDateTime] = value; }
        }

        /// <summary>
        /// ChangeKey
        /// </summary>
        [JsonProperty("ChangeKey", NullValueHandling = NullValueHandling.Ignore)]
        public string ChangeKey
        {
            get { return (string) this.propertyBag[ItemObjectSchema.ChangeKey]; }
            set { this.propertyBag[ItemObjectSchema.ChangeKey] = value; }
        }

        /// <summary>
        /// Categories
        /// </summary>
        [JsonProperty("Categories", NullValueHandling = NullValueHandling.Ignore)]
        public IList<String> Categories
        {
            get { return (IList<String>) this.propertyBag[ItemObjectSchema.Categories]; }
            set { this.propertyBag[ItemObjectSchema.Categories] = value; }
        }
    }


    /// <summary>
    /// Task
    /// </summary>
    public partial class Task : Item
    {

        /// <summary>
        /// AssignedTo
        /// </summary>
        [JsonProperty("AssignedTo", NullValueHandling = NullValueHandling.Ignore)]
        public string AssignedTo
        {
            get { return (string) this.propertyBag[TaskObjectSchema.AssignedTo]; }
            set { this.propertyBag[TaskObjectSchema.AssignedTo] = value; }
        }

        /// <summary>
        /// Body
        /// </summary>
        [JsonProperty("Body", NullValueHandling = NullValueHandling.Ignore)]
        public ItemBody Body
        {
            get { return (ItemBody) this.propertyBag[TaskObjectSchema.Body]; }
            set { this.propertyBag[TaskObjectSchema.Body] = value; }
        }

        /// <summary>
        /// CompletedDateTime
        /// </summary>
        [JsonProperty("CompletedDateTime", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeTimeZone CompletedDateTime
        {
            get { return (DateTimeTimeZone) this.propertyBag[TaskObjectSchema.CompletedDateTime]; }
            set { this.propertyBag[TaskObjectSchema.CompletedDateTime] = value; }
        }

        /// <summary>
        /// DueDateTime
        /// </summary>
        [JsonProperty("DueDateTime", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeTimeZone DueDateTime
        {
            get { return (DateTimeTimeZone) this.propertyBag[TaskObjectSchema.DueDateTime]; }
            set { this.propertyBag[TaskObjectSchema.DueDateTime] = value; }
        }

        /// <summary>
        /// HasAttachments
        /// </summary>
        [JsonProperty("HasAttachments", NullValueHandling = NullValueHandling.Ignore)]
        public bool HasAttachments
        {
            get { return (bool) this.propertyBag[TaskObjectSchema.HasAttachments]; }
            set { this.propertyBag[TaskObjectSchema.HasAttachments] = value; }
        }

        /// <summary>
        /// Importance
        /// </summary>
        [JsonProperty("Importance", NullValueHandling = NullValueHandling.Ignore)]
        public Importance Importance
        {
            get { return (Importance) this.propertyBag[TaskObjectSchema.Importance]; }
            set { this.propertyBag[TaskObjectSchema.Importance] = value; }
        }

        /// <summary>
        /// IsReminderOn
        /// </summary>
        [JsonProperty("IsReminderOn", NullValueHandling = NullValueHandling.Ignore)]
        public bool IsReminderOn
        {
            get { return (bool) this.propertyBag[TaskObjectSchema.IsReminderOn]; }
            set { this.propertyBag[TaskObjectSchema.IsReminderOn] = value; }
        }

        /// <summary>
        /// Owner
        /// </summary>
        [JsonProperty("Owner", NullValueHandling = NullValueHandling.Ignore)]
        public string Owner
        {
            get { return (string) this.propertyBag[TaskObjectSchema.Owner]; }
            set { this.propertyBag[TaskObjectSchema.Owner] = value; }
        }

        /// <summary>
        /// ParentFolderId
        /// </summary>
        [JsonProperty("ParentFolderId", NullValueHandling = NullValueHandling.Ignore)]
        public string ParentFolderId
        {
            get { return (string) this.propertyBag[TaskObjectSchema.ParentFolderId]; }
            set { this.propertyBag[TaskObjectSchema.ParentFolderId] = value; }
        }

        /// <summary>
        /// Recurrence
        /// </summary>
        [JsonProperty("Recurrence", NullValueHandling = NullValueHandling.Ignore)]
        public PatternedRecurrence Recurrence
        {
            get { return (PatternedRecurrence) this.propertyBag[TaskObjectSchema.Recurrence]; }
            set { this.propertyBag[TaskObjectSchema.Recurrence] = value; }
        }

        /// <summary>
        /// ReminderDateTime
        /// </summary>
        [JsonProperty("ReminderDateTime", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeTimeZone ReminderDateTime
        {
            get { return (DateTimeTimeZone) this.propertyBag[TaskObjectSchema.ReminderDateTime]; }
            set { this.propertyBag[TaskObjectSchema.ReminderDateTime] = value; }
        }

        /// <summary>
        /// Sensitivity
        /// </summary>
        [JsonProperty("Sensitivity", NullValueHandling = NullValueHandling.Ignore)]
        public Sensitivity Sensitivity
        {
            get { return (Sensitivity) this.propertyBag[TaskObjectSchema.Sensitivity]; }
            set { this.propertyBag[TaskObjectSchema.Sensitivity] = value; }
        }

        /// <summary>
        /// StartDateTime
        /// </summary>
        [JsonProperty("StartDateTime", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeTimeZone StartDateTime
        {
            get { return (DateTimeTimeZone) this.propertyBag[TaskObjectSchema.StartDateTime]; }
            set { this.propertyBag[TaskObjectSchema.StartDateTime] = value; }
        }

        /// <summary>
        /// Status
        /// </summary>
        [JsonProperty("Status", NullValueHandling = NullValueHandling.Ignore)]
        public TaskStatus Status
        {
            get { return (TaskStatus) this.propertyBag[TaskObjectSchema.Status]; }
            set { this.propertyBag[TaskObjectSchema.Status] = value; }
        }

        /// <summary>
        /// Subject
        /// </summary>
        [JsonProperty("Subject", NullValueHandling = NullValueHandling.Ignore)]
        public string Subject
        {
            get { return (string) this.propertyBag[TaskObjectSchema.Subject]; }
            set { this.propertyBag[TaskObjectSchema.Subject] = value; }
        }

        /// <summary>
        /// SingleValueExtendedProperties
        /// </summary>
        [JsonProperty("SingleValueExtendedProperties", NullValueHandling = NullValueHandling.Ignore)]
        public IList<SingleValueLegacyExtendedProperty> SingleValueExtendedProperties
        {
            get { return (IList<SingleValueLegacyExtendedProperty>) this.propertyBag[TaskObjectSchema.SingleValueExtendedProperties]; }
            set { this.propertyBag[TaskObjectSchema.SingleValueExtendedProperties] = value; }
        }

        /// <summary>
        /// MultiValueExtendedProperties
        /// </summary>
        [JsonProperty("MultiValueExtendedProperties", NullValueHandling = NullValueHandling.Ignore)]
        public IList<MultiValueLegacyExtendedProperty> MultiValueExtendedProperties
        {
            get { return (IList<MultiValueLegacyExtendedProperty>) this.propertyBag[TaskObjectSchema.MultiValueExtendedProperties]; }
            set { this.propertyBag[TaskObjectSchema.MultiValueExtendedProperties] = value; }
        }

        /// <summary>
        /// Attachments
        /// </summary>
        [JsonProperty("Attachments", NullValueHandling = NullValueHandling.Ignore)]
        public IList<Attachment> Attachments
        {
            get { return (IList<Attachment>) this.propertyBag[TaskObjectSchema.Attachments]; }
            set { this.propertyBag[TaskObjectSchema.Attachments] = value; }
        }

        /// <summary>
        /// Complete
        /// </summary>
        public IList<Task> Complete()
        {
            Dictionary<string, object> additionalParameters = new Dictionary<string, object>();
            return (IList<Task>)this.Service.Invoke(nameof(this.Complete), this, additionalParameters);
        }

    }


    /// <summary>
    /// Message
    /// </summary>
    public partial class Message : Item
    {

        /// <summary>
        /// ReceivedDateTime
        /// </summary>
        [JsonProperty("ReceivedDateTime", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset ReceivedDateTime
        {
            get { return (DateTimeOffset) this.propertyBag[MessageObjectSchema.ReceivedDateTime]; }
            set { this.propertyBag[MessageObjectSchema.ReceivedDateTime] = value; }
        }

        /// <summary>
        /// SentDateTime
        /// </summary>
        [JsonProperty("SentDateTime", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset SentDateTime
        {
            get { return (DateTimeOffset) this.propertyBag[MessageObjectSchema.SentDateTime]; }
            set { this.propertyBag[MessageObjectSchema.SentDateTime] = value; }
        }

        /// <summary>
        /// HasAttachments
        /// </summary>
        [JsonProperty("HasAttachments", NullValueHandling = NullValueHandling.Ignore)]
        public bool HasAttachments
        {
            get { return (bool) this.propertyBag[MessageObjectSchema.HasAttachments]; }
            set { this.propertyBag[MessageObjectSchema.HasAttachments] = value; }
        }

        /// <summary>
        /// InternetMessageId
        /// </summary>
        [JsonProperty("InternetMessageId", NullValueHandling = NullValueHandling.Ignore)]
        public string InternetMessageId
        {
            get { return (string) this.propertyBag[MessageObjectSchema.InternetMessageId]; }
            set { this.propertyBag[MessageObjectSchema.InternetMessageId] = value; }
        }

        /// <summary>
        /// InternetMessageHeaders
        /// </summary>
        [JsonProperty("InternetMessageHeaders", NullValueHandling = NullValueHandling.Ignore)]
        public IList<InternetMessageHeader> InternetMessageHeaders
        {
            get { return (IList<InternetMessageHeader>) this.propertyBag[MessageObjectSchema.InternetMessageHeaders]; }
            set { this.propertyBag[MessageObjectSchema.InternetMessageHeaders] = value; }
        }

        /// <summary>
        /// Subject
        /// </summary>
        [JsonProperty("Subject", NullValueHandling = NullValueHandling.Ignore)]
        public string Subject
        {
            get { return (string) this.propertyBag[MessageObjectSchema.Subject]; }
            set { this.propertyBag[MessageObjectSchema.Subject] = value; }
        }

        /// <summary>
        /// Body
        /// </summary>
        [JsonProperty("Body", NullValueHandling = NullValueHandling.Ignore)]
        public ItemBody Body
        {
            get { return (ItemBody) this.propertyBag[MessageObjectSchema.Body]; }
            set { this.propertyBag[MessageObjectSchema.Body] = value; }
        }

        /// <summary>
        /// BodyPreview
        /// </summary>
        [JsonProperty("BodyPreview", NullValueHandling = NullValueHandling.Ignore)]
        public string BodyPreview
        {
            get { return (string) this.propertyBag[MessageObjectSchema.BodyPreview]; }
            set { this.propertyBag[MessageObjectSchema.BodyPreview] = value; }
        }

        /// <summary>
        /// Importance
        /// </summary>
        [JsonProperty("Importance", NullValueHandling = NullValueHandling.Ignore)]
        public Importance Importance
        {
            get { return (Importance) this.propertyBag[MessageObjectSchema.Importance]; }
            set { this.propertyBag[MessageObjectSchema.Importance] = value; }
        }

        /// <summary>
        /// ParentFolderId
        /// </summary>
        [JsonProperty("ParentFolderId", NullValueHandling = NullValueHandling.Ignore)]
        public string ParentFolderId
        {
            get { return (string) this.propertyBag[MessageObjectSchema.ParentFolderId]; }
            set { this.propertyBag[MessageObjectSchema.ParentFolderId] = value; }
        }

        /// <summary>
        /// Sender
        /// </summary>
        [JsonProperty("Sender", NullValueHandling = NullValueHandling.Ignore)]
        public Recipient Sender
        {
            get { return (Recipient) this.propertyBag[MessageObjectSchema.Sender]; }
            set { this.propertyBag[MessageObjectSchema.Sender] = value; }
        }

        /// <summary>
        /// From
        /// </summary>
        [JsonProperty("From", NullValueHandling = NullValueHandling.Ignore)]
        public Recipient From
        {
            get { return (Recipient) this.propertyBag[MessageObjectSchema.From]; }
            set { this.propertyBag[MessageObjectSchema.From] = value; }
        }

        /// <summary>
        /// ToRecipients
        /// </summary>
        [JsonProperty("ToRecipients", NullValueHandling = NullValueHandling.Ignore)]
        public IList<Recipient> ToRecipients
        {
            get { return (IList<Recipient>) this.propertyBag[MessageObjectSchema.ToRecipients]; }
            set { this.propertyBag[MessageObjectSchema.ToRecipients] = value; }
        }

        /// <summary>
        /// CcRecipients
        /// </summary>
        [JsonProperty("CcRecipients", NullValueHandling = NullValueHandling.Ignore)]
        public IList<Recipient> CcRecipients
        {
            get { return (IList<Recipient>) this.propertyBag[MessageObjectSchema.CcRecipients]; }
            set { this.propertyBag[MessageObjectSchema.CcRecipients] = value; }
        }

        /// <summary>
        /// BccRecipients
        /// </summary>
        [JsonProperty("BccRecipients", NullValueHandling = NullValueHandling.Ignore)]
        public IList<Recipient> BccRecipients
        {
            get { return (IList<Recipient>) this.propertyBag[MessageObjectSchema.BccRecipients]; }
            set { this.propertyBag[MessageObjectSchema.BccRecipients] = value; }
        }

        /// <summary>
        /// ReplyTo
        /// </summary>
        [JsonProperty("ReplyTo", NullValueHandling = NullValueHandling.Ignore)]
        public IList<Recipient> ReplyTo
        {
            get { return (IList<Recipient>) this.propertyBag[MessageObjectSchema.ReplyTo]; }
            set { this.propertyBag[MessageObjectSchema.ReplyTo] = value; }
        }

        /// <summary>
        /// ConversationId
        /// </summary>
        [JsonProperty("ConversationId", NullValueHandling = NullValueHandling.Ignore)]
        public string ConversationId
        {
            get { return (string) this.propertyBag[MessageObjectSchema.ConversationId]; }
            set { this.propertyBag[MessageObjectSchema.ConversationId] = value; }
        }

        /// <summary>
        /// UniqueBody
        /// </summary>
        [JsonProperty("UniqueBody", NullValueHandling = NullValueHandling.Ignore)]
        public ItemBody UniqueBody
        {
            get { return (ItemBody) this.propertyBag[MessageObjectSchema.UniqueBody]; }
            set { this.propertyBag[MessageObjectSchema.UniqueBody] = value; }
        }

        /// <summary>
        /// IsDeliveryReceiptRequested
        /// </summary>
        [JsonProperty("IsDeliveryReceiptRequested", NullValueHandling = NullValueHandling.Ignore)]
        public bool IsDeliveryReceiptRequested
        {
            get { return (bool) this.propertyBag[MessageObjectSchema.IsDeliveryReceiptRequested]; }
            set { this.propertyBag[MessageObjectSchema.IsDeliveryReceiptRequested] = value; }
        }

        /// <summary>
        /// IsReadReceiptRequested
        /// </summary>
        [JsonProperty("IsReadReceiptRequested", NullValueHandling = NullValueHandling.Ignore)]
        public bool IsReadReceiptRequested
        {
            get { return (bool) this.propertyBag[MessageObjectSchema.IsReadReceiptRequested]; }
            set { this.propertyBag[MessageObjectSchema.IsReadReceiptRequested] = value; }
        }

        /// <summary>
        /// IsRead
        /// </summary>
        [JsonProperty("IsRead", NullValueHandling = NullValueHandling.Ignore)]
        public bool IsRead
        {
            get { return (bool) this.propertyBag[MessageObjectSchema.IsRead]; }
            set { this.propertyBag[MessageObjectSchema.IsRead] = value; }
        }

        /// <summary>
        /// IsDraft
        /// </summary>
        [JsonProperty("IsDraft", NullValueHandling = NullValueHandling.Ignore)]
        public bool IsDraft
        {
            get { return (bool) this.propertyBag[MessageObjectSchema.IsDraft]; }
            set { this.propertyBag[MessageObjectSchema.IsDraft] = value; }
        }

        /// <summary>
        /// WebLink
        /// </summary>
        [JsonProperty("WebLink", NullValueHandling = NullValueHandling.Ignore)]
        public string WebLink
        {
            get { return (string) this.propertyBag[MessageObjectSchema.WebLink]; }
            set { this.propertyBag[MessageObjectSchema.WebLink] = value; }
        }

        /// <summary>
        /// InferenceClassification
        /// </summary>
        [JsonProperty("InferenceClassification", NullValueHandling = NullValueHandling.Ignore)]
        public InferenceClassificationType InferenceClassification
        {
            get { return (InferenceClassificationType) this.propertyBag[MessageObjectSchema.InferenceClassification]; }
            set { this.propertyBag[MessageObjectSchema.InferenceClassification] = value; }
        }

        /// <summary>
        /// Flag
        /// </summary>
        [JsonProperty("Flag", NullValueHandling = NullValueHandling.Ignore)]
        public FollowupFlag Flag
        {
            get { return (FollowupFlag) this.propertyBag[MessageObjectSchema.Flag]; }
            set { this.propertyBag[MessageObjectSchema.Flag] = value; }
        }

        /// <summary>
        /// SingleValueExtendedProperties
        /// </summary>
        [JsonProperty("SingleValueExtendedProperties", NullValueHandling = NullValueHandling.Ignore)]
        public IList<SingleValueLegacyExtendedProperty> SingleValueExtendedProperties
        {
            get { return (IList<SingleValueLegacyExtendedProperty>) this.propertyBag[MessageObjectSchema.SingleValueExtendedProperties]; }
            set { this.propertyBag[MessageObjectSchema.SingleValueExtendedProperties] = value; }
        }

        /// <summary>
        /// MultiValueExtendedProperties
        /// </summary>
        [JsonProperty("MultiValueExtendedProperties", NullValueHandling = NullValueHandling.Ignore)]
        public IList<MultiValueLegacyExtendedProperty> MultiValueExtendedProperties
        {
            get { return (IList<MultiValueLegacyExtendedProperty>) this.propertyBag[MessageObjectSchema.MultiValueExtendedProperties]; }
            set { this.propertyBag[MessageObjectSchema.MultiValueExtendedProperties] = value; }
        }

        /// <summary>
        /// Attachments
        /// </summary>
        [JsonProperty("Attachments", NullValueHandling = NullValueHandling.Ignore)]
        public IList<Attachment> Attachments
        {
            get { return (IList<Attachment>) this.propertyBag[MessageObjectSchema.Attachments]; }
            set { this.propertyBag[MessageObjectSchema.Attachments] = value; }
        }

        /// <summary>
        /// Extensions
        /// </summary>
        [JsonProperty("Extensions", NullValueHandling = NullValueHandling.Ignore)]
        public IList<Extension> Extensions
        {
            get { return (IList<Extension>) this.propertyBag[MessageObjectSchema.Extensions]; }
            set { this.propertyBag[MessageObjectSchema.Extensions] = value; }
        }

        /// <summary>
        /// CreateReply
        /// </summary>
        public Message CreateReply()
        {
            Dictionary<string, object> additionalParameters = new Dictionary<string, object>();
            return (Message)this.Service.Invoke(nameof(this.CreateReply), this, additionalParameters);
        }


        /// <summary>
        /// CreateReplyAll
        /// </summary>
        public Message CreateReplyAll()
        {
            Dictionary<string, object> additionalParameters = new Dictionary<string, object>();
            return (Message)this.Service.Invoke(nameof(this.CreateReplyAll), this, additionalParameters);
        }


        /// <summary>
        /// CreateForward
        /// </summary>
        public Message CreateForward()
        {
            Dictionary<string, object> additionalParameters = new Dictionary<string, object>();
            return (Message)this.Service.Invoke(nameof(this.CreateForward), this, additionalParameters);
        }


        /// <summary>
        /// Send
        /// </summary>
        public void Send()
        {
            Dictionary<string, object> additionalParameters = new Dictionary<string, object>();
            this.Service.Invoke(nameof(this.Send), this, additionalParameters);
        }


        /// <summary>
        /// Copy
        /// </summary>
        public Message Copy(string destinationId)
        {
            Dictionary<string, object> additionalParameters = new Dictionary<string, object>();
            additionalParameters.Add(nameof(destinationId), destinationId);
            return (Message)this.Service.Invoke(nameof(this.Copy), this, additionalParameters);
        }


        /// <summary>
        /// Move
        /// </summary>
        public Message Move(string destinationId)
        {
            Dictionary<string, object> additionalParameters = new Dictionary<string, object>();
            additionalParameters.Add(nameof(destinationId), destinationId);
            return (Message)this.Service.Invoke(nameof(this.Move), this, additionalParameters);
        }


        /// <summary>
        /// Reply
        /// </summary>
        public void Reply(string comment)
        {
            Dictionary<string, object> additionalParameters = new Dictionary<string, object>();
            additionalParameters.Add(nameof(comment), comment);
            this.Service.Invoke(nameof(this.Reply), this, additionalParameters);
        }


        /// <summary>
        /// ReplyAll
        /// </summary>
        public void ReplyAll(string comment)
        {
            Dictionary<string, object> additionalParameters = new Dictionary<string, object>();
            additionalParameters.Add(nameof(comment), comment);
            this.Service.Invoke(nameof(this.ReplyAll), this, additionalParameters);
        }


        /// <summary>
        /// Forward
        /// </summary>
        public void Forward(IList<Recipient> toRecipients, string comment)
        {
            Dictionary<string, object> additionalParameters = new Dictionary<string, object>();
            additionalParameters.Add(nameof(toRecipients), toRecipients);
            additionalParameters.Add(nameof(comment), comment);
            this.Service.Invoke(nameof(this.Forward), this, additionalParameters);
        }

    }


    /// <summary>
    /// EventMessage
    /// </summary>
    public partial class EventMessage : Message
    {

        /// <summary>
        /// MeetingMessageType
        /// </summary>
        [JsonProperty("MeetingMessageType", NullValueHandling = NullValueHandling.Ignore)]
        public MeetingMessageType MeetingMessageType
        {
            get { return (MeetingMessageType) this.propertyBag[EventMessageObjectSchema.MeetingMessageType]; }
            set { this.propertyBag[EventMessageObjectSchema.MeetingMessageType] = value; }
        }

        /// <summary>
        /// Event
        /// </summary>
        [JsonProperty("Event", NullValueHandling = NullValueHandling.Ignore)]
        public Event Event
        {
            get { return (Event) this.propertyBag[EventMessageObjectSchema.Event]; }
            set { this.propertyBag[EventMessageObjectSchema.Event] = value; }
        }
    }


    /// <summary>
    /// MailFolder
    /// </summary>
    public partial class MailFolder : Entity
    {

        /// <summary>
        /// DisplayName
        /// </summary>
        [JsonProperty("DisplayName", NullValueHandling = NullValueHandling.Ignore)]
        public string DisplayName
        {
            get { return (string) this.propertyBag[MailFolderObjectSchema.DisplayName]; }
            set { this.propertyBag[MailFolderObjectSchema.DisplayName] = value; }
        }

        /// <summary>
        /// ParentFolderId
        /// </summary>
        [JsonProperty("ParentFolderId", NullValueHandling = NullValueHandling.Ignore)]
        public string ParentFolderId
        {
            get { return (string) this.propertyBag[MailFolderObjectSchema.ParentFolderId]; }
            set { this.propertyBag[MailFolderObjectSchema.ParentFolderId] = value; }
        }

        /// <summary>
        /// ChildFolderCount
        /// </summary>
        [JsonProperty("ChildFolderCount", NullValueHandling = NullValueHandling.Ignore)]
        public int ChildFolderCount
        {
            get { return (int) this.propertyBag[MailFolderObjectSchema.ChildFolderCount]; }
            set { this.propertyBag[MailFolderObjectSchema.ChildFolderCount] = value; }
        }

        /// <summary>
        /// UnreadItemCount
        /// </summary>
        [JsonProperty("UnreadItemCount", NullValueHandling = NullValueHandling.Ignore)]
        public int UnreadItemCount
        {
            get { return (int) this.propertyBag[MailFolderObjectSchema.UnreadItemCount]; }
            set { this.propertyBag[MailFolderObjectSchema.UnreadItemCount] = value; }
        }

        /// <summary>
        /// TotalItemCount
        /// </summary>
        [JsonProperty("TotalItemCount", NullValueHandling = NullValueHandling.Ignore)]
        public int TotalItemCount
        {
            get { return (int) this.propertyBag[MailFolderObjectSchema.TotalItemCount]; }
            set { this.propertyBag[MailFolderObjectSchema.TotalItemCount] = value; }
        }

        /// <summary>
        /// SingleValueExtendedProperties
        /// </summary>
        [JsonProperty("SingleValueExtendedProperties", NullValueHandling = NullValueHandling.Ignore)]
        public IList<SingleValueLegacyExtendedProperty> SingleValueExtendedProperties
        {
            get { return (IList<SingleValueLegacyExtendedProperty>) this.propertyBag[MailFolderObjectSchema.SingleValueExtendedProperties]; }
            set { this.propertyBag[MailFolderObjectSchema.SingleValueExtendedProperties] = value; }
        }

        /// <summary>
        /// MultiValueExtendedProperties
        /// </summary>
        [JsonProperty("MultiValueExtendedProperties", NullValueHandling = NullValueHandling.Ignore)]
        public IList<MultiValueLegacyExtendedProperty> MultiValueExtendedProperties
        {
            get { return (IList<MultiValueLegacyExtendedProperty>) this.propertyBag[MailFolderObjectSchema.MultiValueExtendedProperties]; }
            set { this.propertyBag[MailFolderObjectSchema.MultiValueExtendedProperties] = value; }
        }

        /// <summary>
        /// Messages
        /// </summary>
        [JsonProperty("Messages", NullValueHandling = NullValueHandling.Ignore)]
        public IList<Message> Messages
        {
            get { return (IList<Message>) this.propertyBag[MailFolderObjectSchema.Messages]; }
            set { this.propertyBag[MailFolderObjectSchema.Messages] = value; }
        }

        /// <summary>
        /// MessageRules
        /// </summary>
        [JsonProperty("MessageRules", NullValueHandling = NullValueHandling.Ignore)]
        public IList<MessageRule> MessageRules
        {
            get { return (IList<MessageRule>) this.propertyBag[MailFolderObjectSchema.MessageRules]; }
            set { this.propertyBag[MailFolderObjectSchema.MessageRules] = value; }
        }

        /// <summary>
        /// ChildFolders
        /// </summary>
        [JsonProperty("ChildFolders", NullValueHandling = NullValueHandling.Ignore)]
        public IList<MailFolder> ChildFolders
        {
            get { return (IList<MailFolder>) this.propertyBag[MailFolderObjectSchema.ChildFolders]; }
            set { this.propertyBag[MailFolderObjectSchema.ChildFolders] = value; }
        }

        /// <summary>
        /// Copy
        /// </summary>
        public MailFolder Copy(string destinationId)
        {
            Dictionary<string, object> additionalParameters = new Dictionary<string, object>();
            additionalParameters.Add(nameof(destinationId), destinationId);
            return (MailFolder)this.Service.Invoke(nameof(this.Copy), this, additionalParameters);
        }


        /// <summary>
        /// Move
        /// </summary>
        public MailFolder Move(string destinationId)
        {
            Dictionary<string, object> additionalParameters = new Dictionary<string, object>();
            additionalParameters.Add(nameof(destinationId), destinationId);
            return (MailFolder)this.Service.Invoke(nameof(this.Move), this, additionalParameters);
        }

    }


    /// <summary>
    /// Calendar
    /// </summary>
    public partial class Calendar : Entity
    {

        /// <summary>
        /// Name
        /// </summary>
        [JsonProperty("Name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name
        {
            get { return (string) this.propertyBag[CalendarObjectSchema.Name]; }
            set { this.propertyBag[CalendarObjectSchema.Name] = value; }
        }

        /// <summary>
        /// Color
        /// </summary>
        [JsonProperty("Color", NullValueHandling = NullValueHandling.Ignore)]
        public CalendarColor Color
        {
            get { return (CalendarColor) this.propertyBag[CalendarObjectSchema.Color]; }
            set { this.propertyBag[CalendarObjectSchema.Color] = value; }
        }

        /// <summary>
        /// ChangeKey
        /// </summary>
        [JsonProperty("ChangeKey", NullValueHandling = NullValueHandling.Ignore)]
        public string ChangeKey
        {
            get { return (string) this.propertyBag[CalendarObjectSchema.ChangeKey]; }
            set { this.propertyBag[CalendarObjectSchema.ChangeKey] = value; }
        }

        /// <summary>
        /// CanShare
        /// </summary>
        [JsonProperty("CanShare", NullValueHandling = NullValueHandling.Ignore)]
        public bool CanShare
        {
            get { return (bool) this.propertyBag[CalendarObjectSchema.CanShare]; }
            set { this.propertyBag[CalendarObjectSchema.CanShare] = value; }
        }

        /// <summary>
        /// CanViewPrivateItems
        /// </summary>
        [JsonProperty("CanViewPrivateItems", NullValueHandling = NullValueHandling.Ignore)]
        public bool CanViewPrivateItems
        {
            get { return (bool) this.propertyBag[CalendarObjectSchema.CanViewPrivateItems]; }
            set { this.propertyBag[CalendarObjectSchema.CanViewPrivateItems] = value; }
        }

        /// <summary>
        /// CanEdit
        /// </summary>
        [JsonProperty("CanEdit", NullValueHandling = NullValueHandling.Ignore)]
        public bool CanEdit
        {
            get { return (bool) this.propertyBag[CalendarObjectSchema.CanEdit]; }
            set { this.propertyBag[CalendarObjectSchema.CanEdit] = value; }
        }

        /// <summary>
        /// Owner
        /// </summary>
        [JsonProperty("Owner", NullValueHandling = NullValueHandling.Ignore)]
        public EmailAddress Owner
        {
            get { return (EmailAddress) this.propertyBag[CalendarObjectSchema.Owner]; }
            set { this.propertyBag[CalendarObjectSchema.Owner] = value; }
        }

        /// <summary>
        /// SingleValueExtendedProperties
        /// </summary>
        [JsonProperty("SingleValueExtendedProperties", NullValueHandling = NullValueHandling.Ignore)]
        public IList<SingleValueLegacyExtendedProperty> SingleValueExtendedProperties
        {
            get { return (IList<SingleValueLegacyExtendedProperty>) this.propertyBag[CalendarObjectSchema.SingleValueExtendedProperties]; }
            set { this.propertyBag[CalendarObjectSchema.SingleValueExtendedProperties] = value; }
        }

        /// <summary>
        /// MultiValueExtendedProperties
        /// </summary>
        [JsonProperty("MultiValueExtendedProperties", NullValueHandling = NullValueHandling.Ignore)]
        public IList<MultiValueLegacyExtendedProperty> MultiValueExtendedProperties
        {
            get { return (IList<MultiValueLegacyExtendedProperty>) this.propertyBag[CalendarObjectSchema.MultiValueExtendedProperties]; }
            set { this.propertyBag[CalendarObjectSchema.MultiValueExtendedProperties] = value; }
        }

        /// <summary>
        /// Events
        /// </summary>
        [JsonProperty("Events", NullValueHandling = NullValueHandling.Ignore)]
        public IList<Event> Events
        {
            get { return (IList<Event>) this.propertyBag[CalendarObjectSchema.Events]; }
            set { this.propertyBag[CalendarObjectSchema.Events] = value; }
        }

        /// <summary>
        /// CalendarView
        /// </summary>
        [JsonProperty("CalendarView", NullValueHandling = NullValueHandling.Ignore)]
        public IList<Event> CalendarView
        {
            get { return (IList<Event>) this.propertyBag[CalendarObjectSchema.CalendarView]; }
            set { this.propertyBag[CalendarObjectSchema.CalendarView] = value; }
        }
    }


    /// <summary>
    /// CalendarGroup
    /// </summary>
    public partial class CalendarGroup : Entity
    {

        /// <summary>
        /// Name
        /// </summary>
        [JsonProperty("Name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name
        {
            get { return (string) this.propertyBag[CalendarGroupObjectSchema.Name]; }
            set { this.propertyBag[CalendarGroupObjectSchema.Name] = value; }
        }

        /// <summary>
        /// ClassId
        /// </summary>
        [JsonProperty("ClassId", NullValueHandling = NullValueHandling.Ignore)]
        public Guid ClassId
        {
            get { return (Guid) this.propertyBag[CalendarGroupObjectSchema.ClassId]; }
            set { this.propertyBag[CalendarGroupObjectSchema.ClassId] = value; }
        }

        /// <summary>
        /// ChangeKey
        /// </summary>
        [JsonProperty("ChangeKey", NullValueHandling = NullValueHandling.Ignore)]
        public string ChangeKey
        {
            get { return (string) this.propertyBag[CalendarGroupObjectSchema.ChangeKey]; }
            set { this.propertyBag[CalendarGroupObjectSchema.ChangeKey] = value; }
        }

        /// <summary>
        /// Calendars
        /// </summary>
        [JsonProperty("Calendars", NullValueHandling = NullValueHandling.Ignore)]
        public IList<Calendar> Calendars
        {
            get { return (IList<Calendar>) this.propertyBag[CalendarGroupObjectSchema.Calendars]; }
            set { this.propertyBag[CalendarGroupObjectSchema.Calendars] = value; }
        }
    }


    /// <summary>
    /// Event
    /// </summary>
    public partial class Event : Item
    {

        /// <summary>
        /// OriginalStartTimeZone
        /// </summary>
        [JsonProperty("OriginalStartTimeZone", NullValueHandling = NullValueHandling.Ignore)]
        public string OriginalStartTimeZone
        {
            get { return (string) this.propertyBag[EventObjectSchema.OriginalStartTimeZone]; }
            set { this.propertyBag[EventObjectSchema.OriginalStartTimeZone] = value; }
        }

        /// <summary>
        /// OriginalEndTimeZone
        /// </summary>
        [JsonProperty("OriginalEndTimeZone", NullValueHandling = NullValueHandling.Ignore)]
        public string OriginalEndTimeZone
        {
            get { return (string) this.propertyBag[EventObjectSchema.OriginalEndTimeZone]; }
            set { this.propertyBag[EventObjectSchema.OriginalEndTimeZone] = value; }
        }

        /// <summary>
        /// ResponseStatus
        /// </summary>
        [JsonProperty("ResponseStatus", NullValueHandling = NullValueHandling.Ignore)]
        public ResponseStatus ResponseStatus
        {
            get { return (ResponseStatus) this.propertyBag[EventObjectSchema.ResponseStatus]; }
            set { this.propertyBag[EventObjectSchema.ResponseStatus] = value; }
        }

        /// <summary>
        /// ICalUId
        /// </summary>
        [JsonProperty("iCalUId", NullValueHandling = NullValueHandling.Ignore)]
        public string ICalUId
        {
            get { return (string) this.propertyBag[EventObjectSchema.ICalUId]; }
            set { this.propertyBag[EventObjectSchema.ICalUId] = value; }
        }

        /// <summary>
        /// ReminderMinutesBeforeStart
        /// </summary>
        [JsonProperty("ReminderMinutesBeforeStart", NullValueHandling = NullValueHandling.Ignore)]
        public int ReminderMinutesBeforeStart
        {
            get { return (int) this.propertyBag[EventObjectSchema.ReminderMinutesBeforeStart]; }
            set { this.propertyBag[EventObjectSchema.ReminderMinutesBeforeStart] = value; }
        }

        /// <summary>
        /// IsReminderOn
        /// </summary>
        [JsonProperty("IsReminderOn", NullValueHandling = NullValueHandling.Ignore)]
        public bool IsReminderOn
        {
            get { return (bool) this.propertyBag[EventObjectSchema.IsReminderOn]; }
            set { this.propertyBag[EventObjectSchema.IsReminderOn] = value; }
        }

        /// <summary>
        /// HasAttachments
        /// </summary>
        [JsonProperty("HasAttachments", NullValueHandling = NullValueHandling.Ignore)]
        public bool HasAttachments
        {
            get { return (bool) this.propertyBag[EventObjectSchema.HasAttachments]; }
            set { this.propertyBag[EventObjectSchema.HasAttachments] = value; }
        }

        /// <summary>
        /// Subject
        /// </summary>
        [JsonProperty("Subject", NullValueHandling = NullValueHandling.Ignore)]
        public string Subject
        {
            get { return (string) this.propertyBag[EventObjectSchema.Subject]; }
            set { this.propertyBag[EventObjectSchema.Subject] = value; }
        }

        /// <summary>
        /// Body
        /// </summary>
        [JsonProperty("Body", NullValueHandling = NullValueHandling.Ignore)]
        public ItemBody Body
        {
            get { return (ItemBody) this.propertyBag[EventObjectSchema.Body]; }
            set { this.propertyBag[EventObjectSchema.Body] = value; }
        }

        /// <summary>
        /// BodyPreview
        /// </summary>
        [JsonProperty("BodyPreview", NullValueHandling = NullValueHandling.Ignore)]
        public string BodyPreview
        {
            get { return (string) this.propertyBag[EventObjectSchema.BodyPreview]; }
            set { this.propertyBag[EventObjectSchema.BodyPreview] = value; }
        }

        /// <summary>
        /// Importance
        /// </summary>
        [JsonProperty("Importance", NullValueHandling = NullValueHandling.Ignore)]
        public Importance Importance
        {
            get { return (Importance) this.propertyBag[EventObjectSchema.Importance]; }
            set { this.propertyBag[EventObjectSchema.Importance] = value; }
        }

        /// <summary>
        /// Sensitivity
        /// </summary>
        [JsonProperty("Sensitivity", NullValueHandling = NullValueHandling.Ignore)]
        public Sensitivity Sensitivity
        {
            get { return (Sensitivity) this.propertyBag[EventObjectSchema.Sensitivity]; }
            set { this.propertyBag[EventObjectSchema.Sensitivity] = value; }
        }

        /// <summary>
        /// Start
        /// </summary>
        [JsonProperty("Start", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeTimeZone Start
        {
            get { return (DateTimeTimeZone) this.propertyBag[EventObjectSchema.Start]; }
            set { this.propertyBag[EventObjectSchema.Start] = value; }
        }

        /// <summary>
        /// OriginalStart
        /// </summary>
        [JsonProperty("OriginalStart", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset OriginalStart
        {
            get { return (DateTimeOffset) this.propertyBag[EventObjectSchema.OriginalStart]; }
            set { this.propertyBag[EventObjectSchema.OriginalStart] = value; }
        }

        /// <summary>
        /// End
        /// </summary>
        [JsonProperty("End", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeTimeZone End
        {
            get { return (DateTimeTimeZone) this.propertyBag[EventObjectSchema.End]; }
            set { this.propertyBag[EventObjectSchema.End] = value; }
        }

        /// <summary>
        /// Location
        /// </summary>
        [JsonProperty("Location", NullValueHandling = NullValueHandling.Ignore)]
        public Location Location
        {
            get { return (Location) this.propertyBag[EventObjectSchema.Location]; }
            set { this.propertyBag[EventObjectSchema.Location] = value; }
        }

        /// <summary>
        /// Locations
        /// </summary>
        [JsonProperty("Locations", NullValueHandling = NullValueHandling.Ignore)]
        public IList<Location> Locations
        {
            get { return (IList<Location>) this.propertyBag[EventObjectSchema.Locations]; }
            set { this.propertyBag[EventObjectSchema.Locations] = value; }
        }

        /// <summary>
        /// IsAllDay
        /// </summary>
        [JsonProperty("IsAllDay", NullValueHandling = NullValueHandling.Ignore)]
        public bool IsAllDay
        {
            get { return (bool) this.propertyBag[EventObjectSchema.IsAllDay]; }
            set { this.propertyBag[EventObjectSchema.IsAllDay] = value; }
        }

        /// <summary>
        /// IsCancelled
        /// </summary>
        [JsonProperty("IsCancelled", NullValueHandling = NullValueHandling.Ignore)]
        public bool IsCancelled
        {
            get { return (bool) this.propertyBag[EventObjectSchema.IsCancelled]; }
            set { this.propertyBag[EventObjectSchema.IsCancelled] = value; }
        }

        /// <summary>
        /// IsOrganizer
        /// </summary>
        [JsonProperty("IsOrganizer", NullValueHandling = NullValueHandling.Ignore)]
        public bool IsOrganizer
        {
            get { return (bool) this.propertyBag[EventObjectSchema.IsOrganizer]; }
            set { this.propertyBag[EventObjectSchema.IsOrganizer] = value; }
        }

        /// <summary>
        /// Recurrence
        /// </summary>
        [JsonProperty("Recurrence", NullValueHandling = NullValueHandling.Ignore)]
        public PatternedRecurrence Recurrence
        {
            get { return (PatternedRecurrence) this.propertyBag[EventObjectSchema.Recurrence]; }
            set { this.propertyBag[EventObjectSchema.Recurrence] = value; }
        }

        /// <summary>
        /// ResponseRequested
        /// </summary>
        [JsonProperty("ResponseRequested", NullValueHandling = NullValueHandling.Ignore)]
        public bool ResponseRequested
        {
            get { return (bool) this.propertyBag[EventObjectSchema.ResponseRequested]; }
            set { this.propertyBag[EventObjectSchema.ResponseRequested] = value; }
        }

        /// <summary>
        /// SeriesMasterId
        /// </summary>
        [JsonProperty("SeriesMasterId", NullValueHandling = NullValueHandling.Ignore)]
        public string SeriesMasterId
        {
            get { return (string) this.propertyBag[EventObjectSchema.SeriesMasterId]; }
            set { this.propertyBag[EventObjectSchema.SeriesMasterId] = value; }
        }

        /// <summary>
        /// ShowAs
        /// </summary>
        [JsonProperty("ShowAs", NullValueHandling = NullValueHandling.Ignore)]
        public FreeBusyStatus ShowAs
        {
            get { return (FreeBusyStatus) this.propertyBag[EventObjectSchema.ShowAs]; }
            set { this.propertyBag[EventObjectSchema.ShowAs] = value; }
        }

        /// <summary>
        /// Type
        /// </summary>
        [JsonProperty("Type", NullValueHandling = NullValueHandling.Ignore)]
        public EventType Type
        {
            get { return (EventType) this.propertyBag[EventObjectSchema.Type]; }
            set { this.propertyBag[EventObjectSchema.Type] = value; }
        }

        /// <summary>
        /// Attendees
        /// </summary>
        [JsonProperty("Attendees", NullValueHandling = NullValueHandling.Ignore)]
        public IList<Attendee> Attendees
        {
            get { return (IList<Attendee>) this.propertyBag[EventObjectSchema.Attendees]; }
            set { this.propertyBag[EventObjectSchema.Attendees] = value; }
        }

        /// <summary>
        /// Organizer
        /// </summary>
        [JsonProperty("Organizer", NullValueHandling = NullValueHandling.Ignore)]
        public Recipient Organizer
        {
            get { return (Recipient) this.propertyBag[EventObjectSchema.Organizer]; }
            set { this.propertyBag[EventObjectSchema.Organizer] = value; }
        }

        /// <summary>
        /// WebLink
        /// </summary>
        [JsonProperty("WebLink", NullValueHandling = NullValueHandling.Ignore)]
        public string WebLink
        {
            get { return (string) this.propertyBag[EventObjectSchema.WebLink]; }
            set { this.propertyBag[EventObjectSchema.WebLink] = value; }
        }

        /// <summary>
        /// OnlineMeetingUrl
        /// </summary>
        [JsonProperty("OnlineMeetingUrl", NullValueHandling = NullValueHandling.Ignore)]
        public string OnlineMeetingUrl
        {
            get { return (string) this.propertyBag[EventObjectSchema.OnlineMeetingUrl]; }
            set { this.propertyBag[EventObjectSchema.OnlineMeetingUrl] = value; }
        }

        /// <summary>
        /// Attachments
        /// </summary>
        [JsonProperty("Attachments", NullValueHandling = NullValueHandling.Ignore)]
        public IList<Attachment> Attachments
        {
            get { return (IList<Attachment>) this.propertyBag[EventObjectSchema.Attachments]; }
            set { this.propertyBag[EventObjectSchema.Attachments] = value; }
        }

        /// <summary>
        /// SingleValueExtendedProperties
        /// </summary>
        [JsonProperty("SingleValueExtendedProperties", NullValueHandling = NullValueHandling.Ignore)]
        public IList<SingleValueLegacyExtendedProperty> SingleValueExtendedProperties
        {
            get { return (IList<SingleValueLegacyExtendedProperty>) this.propertyBag[EventObjectSchema.SingleValueExtendedProperties]; }
            set { this.propertyBag[EventObjectSchema.SingleValueExtendedProperties] = value; }
        }

        /// <summary>
        /// MultiValueExtendedProperties
        /// </summary>
        [JsonProperty("MultiValueExtendedProperties", NullValueHandling = NullValueHandling.Ignore)]
        public IList<MultiValueLegacyExtendedProperty> MultiValueExtendedProperties
        {
            get { return (IList<MultiValueLegacyExtendedProperty>) this.propertyBag[EventObjectSchema.MultiValueExtendedProperties]; }
            set { this.propertyBag[EventObjectSchema.MultiValueExtendedProperties] = value; }
        }

        /// <summary>
        /// Calendar
        /// </summary>
        [JsonProperty("Calendar", NullValueHandling = NullValueHandling.Ignore)]
        public Calendar Calendar
        {
            get { return (Calendar) this.propertyBag[EventObjectSchema.Calendar]; }
            set { this.propertyBag[EventObjectSchema.Calendar] = value; }
        }

        /// <summary>
        /// Instances
        /// </summary>
        [JsonProperty("Instances", NullValueHandling = NullValueHandling.Ignore)]
        public IList<Event> Instances
        {
            get { return (IList<Event>) this.propertyBag[EventObjectSchema.Instances]; }
            set { this.propertyBag[EventObjectSchema.Instances] = value; }
        }

        /// <summary>
        /// Extensions
        /// </summary>
        [JsonProperty("Extensions", NullValueHandling = NullValueHandling.Ignore)]
        public IList<Extension> Extensions
        {
            get { return (IList<Extension>) this.propertyBag[EventObjectSchema.Extensions]; }
            set { this.propertyBag[EventObjectSchema.Extensions] = value; }
        }

        /// <summary>
        /// DismissReminder
        /// </summary>
        public void DismissReminder()
        {
            Dictionary<string, object> additionalParameters = new Dictionary<string, object>();
            this.Service.Invoke(nameof(this.DismissReminder), this, additionalParameters);
        }


        /// <summary>
        /// SnoozeReminder
        /// </summary>
        public void SnoozeReminder(DateTimeTimeZone newReminderTime)
        {
            Dictionary<string, object> additionalParameters = new Dictionary<string, object>();
            additionalParameters.Add(nameof(newReminderTime), newReminderTime);
            this.Service.Invoke(nameof(this.SnoozeReminder), this, additionalParameters);
        }


        /// <summary>
        /// Accept
        /// </summary>
        public void Accept(bool sendResponse, string comment)
        {
            Dictionary<string, object> additionalParameters = new Dictionary<string, object>();
            additionalParameters.Add(nameof(sendResponse), sendResponse);
            additionalParameters.Add(nameof(comment), comment);
            this.Service.Invoke(nameof(this.Accept), this, additionalParameters);
        }


        /// <summary>
        /// Decline
        /// </summary>
        public void Decline(bool sendResponse, string comment)
        {
            Dictionary<string, object> additionalParameters = new Dictionary<string, object>();
            additionalParameters.Add(nameof(sendResponse), sendResponse);
            additionalParameters.Add(nameof(comment), comment);
            this.Service.Invoke(nameof(this.Decline), this, additionalParameters);
        }


        /// <summary>
        /// TentativelyAccept
        /// </summary>
        public void TentativelyAccept(bool sendResponse, string comment)
        {
            Dictionary<string, object> additionalParameters = new Dictionary<string, object>();
            additionalParameters.Add(nameof(sendResponse), sendResponse);
            additionalParameters.Add(nameof(comment), comment);
            this.Service.Invoke(nameof(this.TentativelyAccept), this, additionalParameters);
        }

    }


    /// <summary>
    /// Person
    /// </summary>
    public partial class Person : Entity
    {

        /// <summary>
        /// DisplayName
        /// </summary>
        [JsonProperty("DisplayName", NullValueHandling = NullValueHandling.Ignore)]
        public string DisplayName
        {
            get { return (string) this.propertyBag[PersonObjectSchema.DisplayName]; }
            set { this.propertyBag[PersonObjectSchema.DisplayName] = value; }
        }

        /// <summary>
        /// GivenName
        /// </summary>
        [JsonProperty("GivenName", NullValueHandling = NullValueHandling.Ignore)]
        public string GivenName
        {
            get { return (string) this.propertyBag[PersonObjectSchema.GivenName]; }
            set { this.propertyBag[PersonObjectSchema.GivenName] = value; }
        }

        /// <summary>
        /// Surname
        /// </summary>
        [JsonProperty("Surname", NullValueHandling = NullValueHandling.Ignore)]
        public string Surname
        {
            get { return (string) this.propertyBag[PersonObjectSchema.Surname]; }
            set { this.propertyBag[PersonObjectSchema.Surname] = value; }
        }

        /// <summary>
        /// Birthday
        /// </summary>
        [JsonProperty("Birthday", NullValueHandling = NullValueHandling.Ignore)]
        public string Birthday
        {
            get { return (string) this.propertyBag[PersonObjectSchema.Birthday]; }
            set { this.propertyBag[PersonObjectSchema.Birthday] = value; }
        }

        /// <summary>
        /// PersonNotes
        /// </summary>
        [JsonProperty("PersonNotes", NullValueHandling = NullValueHandling.Ignore)]
        public string PersonNotes
        {
            get { return (string) this.propertyBag[PersonObjectSchema.PersonNotes]; }
            set { this.propertyBag[PersonObjectSchema.PersonNotes] = value; }
        }

        /// <summary>
        /// IsFavorite
        /// </summary>
        [JsonProperty("IsFavorite", NullValueHandling = NullValueHandling.Ignore)]
        public bool IsFavorite
        {
            get { return (bool) this.propertyBag[PersonObjectSchema.IsFavorite]; }
            set { this.propertyBag[PersonObjectSchema.IsFavorite] = value; }
        }

        /// <summary>
        /// ScoredEmailAddresses
        /// </summary>
        [JsonProperty("ScoredEmailAddresses", NullValueHandling = NullValueHandling.Ignore)]
        public IList<ScoredEmailAddress> ScoredEmailAddresses
        {
            get { return (IList<ScoredEmailAddress>) this.propertyBag[PersonObjectSchema.ScoredEmailAddresses]; }
            set { this.propertyBag[PersonObjectSchema.ScoredEmailAddresses] = value; }
        }

        /// <summary>
        /// Phones
        /// </summary>
        [JsonProperty("Phones", NullValueHandling = NullValueHandling.Ignore)]
        public IList<Phone> Phones
        {
            get { return (IList<Phone>) this.propertyBag[PersonObjectSchema.Phones]; }
            set { this.propertyBag[PersonObjectSchema.Phones] = value; }
        }

        /// <summary>
        /// PostalAddresses
        /// </summary>
        [JsonProperty("PostalAddresses", NullValueHandling = NullValueHandling.Ignore)]
        public IList<Location> PostalAddresses
        {
            get { return (IList<Location>) this.propertyBag[PersonObjectSchema.PostalAddresses]; }
            set { this.propertyBag[PersonObjectSchema.PostalAddresses] = value; }
        }

        /// <summary>
        /// Websites
        /// </summary>
        [JsonProperty("Websites", NullValueHandling = NullValueHandling.Ignore)]
        public IList<Website> Websites
        {
            get { return (IList<Website>) this.propertyBag[PersonObjectSchema.Websites]; }
            set { this.propertyBag[PersonObjectSchema.Websites] = value; }
        }

        /// <summary>
        /// JobTitle
        /// </summary>
        [JsonProperty("JobTitle", NullValueHandling = NullValueHandling.Ignore)]
        public string JobTitle
        {
            get { return (string) this.propertyBag[PersonObjectSchema.JobTitle]; }
            set { this.propertyBag[PersonObjectSchema.JobTitle] = value; }
        }

        /// <summary>
        /// CompanyName
        /// </summary>
        [JsonProperty("CompanyName", NullValueHandling = NullValueHandling.Ignore)]
        public string CompanyName
        {
            get { return (string) this.propertyBag[PersonObjectSchema.CompanyName]; }
            set { this.propertyBag[PersonObjectSchema.CompanyName] = value; }
        }

        /// <summary>
        /// YomiCompany
        /// </summary>
        [JsonProperty("YomiCompany", NullValueHandling = NullValueHandling.Ignore)]
        public string YomiCompany
        {
            get { return (string) this.propertyBag[PersonObjectSchema.YomiCompany]; }
            set { this.propertyBag[PersonObjectSchema.YomiCompany] = value; }
        }

        /// <summary>
        /// Department
        /// </summary>
        [JsonProperty("Department", NullValueHandling = NullValueHandling.Ignore)]
        public string Department
        {
            get { return (string) this.propertyBag[PersonObjectSchema.Department]; }
            set { this.propertyBag[PersonObjectSchema.Department] = value; }
        }

        /// <summary>
        /// OfficeLocation
        /// </summary>
        [JsonProperty("OfficeLocation", NullValueHandling = NullValueHandling.Ignore)]
        public string OfficeLocation
        {
            get { return (string) this.propertyBag[PersonObjectSchema.OfficeLocation]; }
            set { this.propertyBag[PersonObjectSchema.OfficeLocation] = value; }
        }

        /// <summary>
        /// Profession
        /// </summary>
        [JsonProperty("Profession", NullValueHandling = NullValueHandling.Ignore)]
        public string Profession
        {
            get { return (string) this.propertyBag[PersonObjectSchema.Profession]; }
            set { this.propertyBag[PersonObjectSchema.Profession] = value; }
        }

        /// <summary>
        /// PersonType
        /// </summary>
        [JsonProperty("PersonType", NullValueHandling = NullValueHandling.Ignore)]
        public PersonType PersonType
        {
            get { return (PersonType) this.propertyBag[PersonObjectSchema.PersonType]; }
            set { this.propertyBag[PersonObjectSchema.PersonType] = value; }
        }

        /// <summary>
        /// UserPrincipalName
        /// </summary>
        [JsonProperty("UserPrincipalName", NullValueHandling = NullValueHandling.Ignore)]
        public string UserPrincipalName
        {
            get { return (string) this.propertyBag[PersonObjectSchema.UserPrincipalName]; }
            set { this.propertyBag[PersonObjectSchema.UserPrincipalName] = value; }
        }

        /// <summary>
        /// IMAddress
        /// </summary>
        [JsonProperty("IMAddress", NullValueHandling = NullValueHandling.Ignore)]
        public string IMAddress
        {
            get { return (string) this.propertyBag[PersonObjectSchema.IMAddress]; }
            set { this.propertyBag[PersonObjectSchema.IMAddress] = value; }
        }
    }


    /// <summary>
    /// Contact
    /// </summary>
    public partial class Contact : Item
    {

        /// <summary>
        /// ParentFolderId
        /// </summary>
        [JsonProperty("ParentFolderId", NullValueHandling = NullValueHandling.Ignore)]
        public string ParentFolderId
        {
            get { return (string) this.propertyBag[ContactObjectSchema.ParentFolderId]; }
            set { this.propertyBag[ContactObjectSchema.ParentFolderId] = value; }
        }

        /// <summary>
        /// Birthday
        /// </summary>
        [JsonProperty("Birthday", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset Birthday
        {
            get { return (DateTimeOffset) this.propertyBag[ContactObjectSchema.Birthday]; }
            set { this.propertyBag[ContactObjectSchema.Birthday] = value; }
        }

        /// <summary>
        /// FileAs
        /// </summary>
        [JsonProperty("FileAs", NullValueHandling = NullValueHandling.Ignore)]
        public string FileAs
        {
            get { return (string) this.propertyBag[ContactObjectSchema.FileAs]; }
            set { this.propertyBag[ContactObjectSchema.FileAs] = value; }
        }

        /// <summary>
        /// DisplayName
        /// </summary>
        [JsonProperty("DisplayName", NullValueHandling = NullValueHandling.Ignore)]
        public string DisplayName
        {
            get { return (string) this.propertyBag[ContactObjectSchema.DisplayName]; }
            set { this.propertyBag[ContactObjectSchema.DisplayName] = value; }
        }

        /// <summary>
        /// GivenName
        /// </summary>
        [JsonProperty("GivenName", NullValueHandling = NullValueHandling.Ignore)]
        public string GivenName
        {
            get { return (string) this.propertyBag[ContactObjectSchema.GivenName]; }
            set { this.propertyBag[ContactObjectSchema.GivenName] = value; }
        }

        /// <summary>
        /// Initials
        /// </summary>
        [JsonProperty("Initials", NullValueHandling = NullValueHandling.Ignore)]
        public string Initials
        {
            get { return (string) this.propertyBag[ContactObjectSchema.Initials]; }
            set { this.propertyBag[ContactObjectSchema.Initials] = value; }
        }

        /// <summary>
        /// MiddleName
        /// </summary>
        [JsonProperty("MiddleName", NullValueHandling = NullValueHandling.Ignore)]
        public string MiddleName
        {
            get { return (string) this.propertyBag[ContactObjectSchema.MiddleName]; }
            set { this.propertyBag[ContactObjectSchema.MiddleName] = value; }
        }

        /// <summary>
        /// NickName
        /// </summary>
        [JsonProperty("NickName", NullValueHandling = NullValueHandling.Ignore)]
        public string NickName
        {
            get { return (string) this.propertyBag[ContactObjectSchema.NickName]; }
            set { this.propertyBag[ContactObjectSchema.NickName] = value; }
        }

        /// <summary>
        /// Surname
        /// </summary>
        [JsonProperty("Surname", NullValueHandling = NullValueHandling.Ignore)]
        public string Surname
        {
            get { return (string) this.propertyBag[ContactObjectSchema.Surname]; }
            set { this.propertyBag[ContactObjectSchema.Surname] = value; }
        }

        /// <summary>
        /// Title
        /// </summary>
        [JsonProperty("Title", NullValueHandling = NullValueHandling.Ignore)]
        public string Title
        {
            get { return (string) this.propertyBag[ContactObjectSchema.Title]; }
            set { this.propertyBag[ContactObjectSchema.Title] = value; }
        }

        /// <summary>
        /// YomiGivenName
        /// </summary>
        [JsonProperty("YomiGivenName", NullValueHandling = NullValueHandling.Ignore)]
        public string YomiGivenName
        {
            get { return (string) this.propertyBag[ContactObjectSchema.YomiGivenName]; }
            set { this.propertyBag[ContactObjectSchema.YomiGivenName] = value; }
        }

        /// <summary>
        /// YomiSurname
        /// </summary>
        [JsonProperty("YomiSurname", NullValueHandling = NullValueHandling.Ignore)]
        public string YomiSurname
        {
            get { return (string) this.propertyBag[ContactObjectSchema.YomiSurname]; }
            set { this.propertyBag[ContactObjectSchema.YomiSurname] = value; }
        }

        /// <summary>
        /// YomiCompanyName
        /// </summary>
        [JsonProperty("YomiCompanyName", NullValueHandling = NullValueHandling.Ignore)]
        public string YomiCompanyName
        {
            get { return (string) this.propertyBag[ContactObjectSchema.YomiCompanyName]; }
            set { this.propertyBag[ContactObjectSchema.YomiCompanyName] = value; }
        }

        /// <summary>
        /// Generation
        /// </summary>
        [JsonProperty("Generation", NullValueHandling = NullValueHandling.Ignore)]
        public string Generation
        {
            get { return (string) this.propertyBag[ContactObjectSchema.Generation]; }
            set { this.propertyBag[ContactObjectSchema.Generation] = value; }
        }

        /// <summary>
        /// EmailAddresses
        /// </summary>
        [JsonProperty("EmailAddresses", NullValueHandling = NullValueHandling.Ignore)]
        public IList<EmailAddress> EmailAddresses
        {
            get { return (IList<EmailAddress>) this.propertyBag[ContactObjectSchema.EmailAddresses]; }
            set { this.propertyBag[ContactObjectSchema.EmailAddresses] = value; }
        }

        /// <summary>
        /// ImAddresses
        /// </summary>
        [JsonProperty("ImAddresses", NullValueHandling = NullValueHandling.Ignore)]
        public IList<String> ImAddresses
        {
            get { return (IList<String>) this.propertyBag[ContactObjectSchema.ImAddresses]; }
            set { this.propertyBag[ContactObjectSchema.ImAddresses] = value; }
        }

        /// <summary>
        /// JobTitle
        /// </summary>
        [JsonProperty("JobTitle", NullValueHandling = NullValueHandling.Ignore)]
        public string JobTitle
        {
            get { return (string) this.propertyBag[ContactObjectSchema.JobTitle]; }
            set { this.propertyBag[ContactObjectSchema.JobTitle] = value; }
        }

        /// <summary>
        /// CompanyName
        /// </summary>
        [JsonProperty("CompanyName", NullValueHandling = NullValueHandling.Ignore)]
        public string CompanyName
        {
            get { return (string) this.propertyBag[ContactObjectSchema.CompanyName]; }
            set { this.propertyBag[ContactObjectSchema.CompanyName] = value; }
        }

        /// <summary>
        /// Department
        /// </summary>
        [JsonProperty("Department", NullValueHandling = NullValueHandling.Ignore)]
        public string Department
        {
            get { return (string) this.propertyBag[ContactObjectSchema.Department]; }
            set { this.propertyBag[ContactObjectSchema.Department] = value; }
        }

        /// <summary>
        /// OfficeLocation
        /// </summary>
        [JsonProperty("OfficeLocation", NullValueHandling = NullValueHandling.Ignore)]
        public string OfficeLocation
        {
            get { return (string) this.propertyBag[ContactObjectSchema.OfficeLocation]; }
            set { this.propertyBag[ContactObjectSchema.OfficeLocation] = value; }
        }

        /// <summary>
        /// Profession
        /// </summary>
        [JsonProperty("Profession", NullValueHandling = NullValueHandling.Ignore)]
        public string Profession
        {
            get { return (string) this.propertyBag[ContactObjectSchema.Profession]; }
            set { this.propertyBag[ContactObjectSchema.Profession] = value; }
        }

        /// <summary>
        /// BusinessHomePage
        /// </summary>
        [JsonProperty("BusinessHomePage", NullValueHandling = NullValueHandling.Ignore)]
        public string BusinessHomePage
        {
            get { return (string) this.propertyBag[ContactObjectSchema.BusinessHomePage]; }
            set { this.propertyBag[ContactObjectSchema.BusinessHomePage] = value; }
        }

        /// <summary>
        /// AssistantName
        /// </summary>
        [JsonProperty("AssistantName", NullValueHandling = NullValueHandling.Ignore)]
        public string AssistantName
        {
            get { return (string) this.propertyBag[ContactObjectSchema.AssistantName]; }
            set { this.propertyBag[ContactObjectSchema.AssistantName] = value; }
        }

        /// <summary>
        /// Manager
        /// </summary>
        [JsonProperty("Manager", NullValueHandling = NullValueHandling.Ignore)]
        public string Manager
        {
            get { return (string) this.propertyBag[ContactObjectSchema.Manager]; }
            set { this.propertyBag[ContactObjectSchema.Manager] = value; }
        }

        /// <summary>
        /// HomePhones
        /// </summary>
        [JsonProperty("HomePhones", NullValueHandling = NullValueHandling.Ignore)]
        public IList<String> HomePhones
        {
            get { return (IList<String>) this.propertyBag[ContactObjectSchema.HomePhones]; }
            set { this.propertyBag[ContactObjectSchema.HomePhones] = value; }
        }

        /// <summary>
        /// MobilePhone1
        /// </summary>
        [JsonProperty("MobilePhone1", NullValueHandling = NullValueHandling.Ignore)]
        public string MobilePhone1
        {
            get { return (string) this.propertyBag[ContactObjectSchema.MobilePhone1]; }
            set { this.propertyBag[ContactObjectSchema.MobilePhone1] = value; }
        }

        /// <summary>
        /// BusinessPhones
        /// </summary>
        [JsonProperty("BusinessPhones", NullValueHandling = NullValueHandling.Ignore)]
        public IList<String> BusinessPhones
        {
            get { return (IList<String>) this.propertyBag[ContactObjectSchema.BusinessPhones]; }
            set { this.propertyBag[ContactObjectSchema.BusinessPhones] = value; }
        }

        /// <summary>
        /// HomeAddress
        /// </summary>
        [JsonProperty("HomeAddress", NullValueHandling = NullValueHandling.Ignore)]
        public PhysicalAddress HomeAddress
        {
            get { return (PhysicalAddress) this.propertyBag[ContactObjectSchema.HomeAddress]; }
            set { this.propertyBag[ContactObjectSchema.HomeAddress] = value; }
        }

        /// <summary>
        /// BusinessAddress
        /// </summary>
        [JsonProperty("BusinessAddress", NullValueHandling = NullValueHandling.Ignore)]
        public PhysicalAddress BusinessAddress
        {
            get { return (PhysicalAddress) this.propertyBag[ContactObjectSchema.BusinessAddress]; }
            set { this.propertyBag[ContactObjectSchema.BusinessAddress] = value; }
        }

        /// <summary>
        /// OtherAddress
        /// </summary>
        [JsonProperty("OtherAddress", NullValueHandling = NullValueHandling.Ignore)]
        public PhysicalAddress OtherAddress
        {
            get { return (PhysicalAddress) this.propertyBag[ContactObjectSchema.OtherAddress]; }
            set { this.propertyBag[ContactObjectSchema.OtherAddress] = value; }
        }

        /// <summary>
        /// SpouseName
        /// </summary>
        [JsonProperty("SpouseName", NullValueHandling = NullValueHandling.Ignore)]
        public string SpouseName
        {
            get { return (string) this.propertyBag[ContactObjectSchema.SpouseName]; }
            set { this.propertyBag[ContactObjectSchema.SpouseName] = value; }
        }

        /// <summary>
        /// PersonalNotes
        /// </summary>
        [JsonProperty("PersonalNotes", NullValueHandling = NullValueHandling.Ignore)]
        public string PersonalNotes
        {
            get { return (string) this.propertyBag[ContactObjectSchema.PersonalNotes]; }
            set { this.propertyBag[ContactObjectSchema.PersonalNotes] = value; }
        }

        /// <summary>
        /// Children
        /// </summary>
        [JsonProperty("Children", NullValueHandling = NullValueHandling.Ignore)]
        public IList<String> Children
        {
            get { return (IList<String>) this.propertyBag[ContactObjectSchema.Children]; }
            set { this.propertyBag[ContactObjectSchema.Children] = value; }
        }

        /// <summary>
        /// SingleValueExtendedProperties
        /// </summary>
        [JsonProperty("SingleValueExtendedProperties", NullValueHandling = NullValueHandling.Ignore)]
        public IList<SingleValueLegacyExtendedProperty> SingleValueExtendedProperties
        {
            get { return (IList<SingleValueLegacyExtendedProperty>) this.propertyBag[ContactObjectSchema.SingleValueExtendedProperties]; }
            set { this.propertyBag[ContactObjectSchema.SingleValueExtendedProperties] = value; }
        }

        /// <summary>
        /// MultiValueExtendedProperties
        /// </summary>
        [JsonProperty("MultiValueExtendedProperties", NullValueHandling = NullValueHandling.Ignore)]
        public IList<MultiValueLegacyExtendedProperty> MultiValueExtendedProperties
        {
            get { return (IList<MultiValueLegacyExtendedProperty>) this.propertyBag[ContactObjectSchema.MultiValueExtendedProperties]; }
            set { this.propertyBag[ContactObjectSchema.MultiValueExtendedProperties] = value; }
        }

        /// <summary>
        /// Photo
        /// </summary>
        [JsonProperty("Photo", NullValueHandling = NullValueHandling.Ignore)]
        public Photo Photo
        {
            get { return (Photo) this.propertyBag[ContactObjectSchema.Photo]; }
            set { this.propertyBag[ContactObjectSchema.Photo] = value; }
        }

        /// <summary>
        /// Extensions
        /// </summary>
        [JsonProperty("Extensions", NullValueHandling = NullValueHandling.Ignore)]
        public IList<Extension> Extensions
        {
            get { return (IList<Extension>) this.propertyBag[ContactObjectSchema.Extensions]; }
            set { this.propertyBag[ContactObjectSchema.Extensions] = value; }
        }
    }


    /// <summary>
    /// ContactFolder
    /// </summary>
    public partial class ContactFolder : Entity
    {

        /// <summary>
        /// ParentFolderId
        /// </summary>
        [JsonProperty("ParentFolderId", NullValueHandling = NullValueHandling.Ignore)]
        public string ParentFolderId
        {
            get { return (string) this.propertyBag[ContactFolderObjectSchema.ParentFolderId]; }
            set { this.propertyBag[ContactFolderObjectSchema.ParentFolderId] = value; }
        }

        /// <summary>
        /// DisplayName
        /// </summary>
        [JsonProperty("DisplayName", NullValueHandling = NullValueHandling.Ignore)]
        public string DisplayName
        {
            get { return (string) this.propertyBag[ContactFolderObjectSchema.DisplayName]; }
            set { this.propertyBag[ContactFolderObjectSchema.DisplayName] = value; }
        }

        /// <summary>
        /// SingleValueExtendedProperties
        /// </summary>
        [JsonProperty("SingleValueExtendedProperties", NullValueHandling = NullValueHandling.Ignore)]
        public IList<SingleValueLegacyExtendedProperty> SingleValueExtendedProperties
        {
            get { return (IList<SingleValueLegacyExtendedProperty>) this.propertyBag[ContactFolderObjectSchema.SingleValueExtendedProperties]; }
            set { this.propertyBag[ContactFolderObjectSchema.SingleValueExtendedProperties] = value; }
        }

        /// <summary>
        /// MultiValueExtendedProperties
        /// </summary>
        [JsonProperty("MultiValueExtendedProperties", NullValueHandling = NullValueHandling.Ignore)]
        public IList<MultiValueLegacyExtendedProperty> MultiValueExtendedProperties
        {
            get { return (IList<MultiValueLegacyExtendedProperty>) this.propertyBag[ContactFolderObjectSchema.MultiValueExtendedProperties]; }
            set { this.propertyBag[ContactFolderObjectSchema.MultiValueExtendedProperties] = value; }
        }

        /// <summary>
        /// Contacts
        /// </summary>
        [JsonProperty("Contacts", NullValueHandling = NullValueHandling.Ignore)]
        public IList<Contact> Contacts
        {
            get { return (IList<Contact>) this.propertyBag[ContactFolderObjectSchema.Contacts]; }
            set { this.propertyBag[ContactFolderObjectSchema.Contacts] = value; }
        }

        /// <summary>
        /// ChildFolders
        /// </summary>
        [JsonProperty("ChildFolders", NullValueHandling = NullValueHandling.Ignore)]
        public IList<ContactFolder> ChildFolders
        {
            get { return (IList<ContactFolder>) this.propertyBag[ContactFolderObjectSchema.ChildFolders]; }
            set { this.propertyBag[ContactFolderObjectSchema.ChildFolders] = value; }
        }
    }


    /// <summary>
    /// OutlookCategory
    /// </summary>
    public partial class OutlookCategory : Entity
    {

        /// <summary>
        /// DisplayName
        /// </summary>
        [JsonProperty("DisplayName", NullValueHandling = NullValueHandling.Ignore)]
        public string DisplayName
        {
            get { return (string) this.propertyBag[OutlookCategoryObjectSchema.DisplayName]; }
            set { this.propertyBag[OutlookCategoryObjectSchema.DisplayName] = value; }
        }

        /// <summary>
        /// Color
        /// </summary>
        [JsonProperty("Color", NullValueHandling = NullValueHandling.Ignore)]
        public CategoryColor Color
        {
            get { return (CategoryColor) this.propertyBag[OutlookCategoryObjectSchema.Color]; }
            set { this.propertyBag[OutlookCategoryObjectSchema.Color] = value; }
        }
    }


    /// <summary>
    /// InferenceClassification
    /// </summary>
    public partial class InferenceClassification : Entity
    {

        /// <summary>
        /// Overrides
        /// </summary>
        [JsonProperty("Overrides", NullValueHandling = NullValueHandling.Ignore)]
        public IList<InferenceClassificationOverride> Overrides
        {
            get { return (IList<InferenceClassificationOverride>) this.propertyBag[InferenceClassificationObjectSchema.Overrides]; }
            set { this.propertyBag[InferenceClassificationObjectSchema.Overrides] = value; }
        }
    }


    /// <summary>
    /// Photo
    /// </summary>
    public partial class Photo : Entity
    {

        /// <summary>
        /// Height
        /// </summary>
        [JsonProperty("Height", NullValueHandling = NullValueHandling.Ignore)]
        public int Height
        {
            get { return (int) this.propertyBag[PhotoObjectSchema.Height]; }
            set { this.propertyBag[PhotoObjectSchema.Height] = value; }
        }

        /// <summary>
        /// Width
        /// </summary>
        [JsonProperty("Width", NullValueHandling = NullValueHandling.Ignore)]
        public int Width
        {
            get { return (int) this.propertyBag[PhotoObjectSchema.Width]; }
            set { this.propertyBag[PhotoObjectSchema.Width] = value; }
        }
    }


    /// <summary>
    /// ConversationThread
    /// </summary>
    public partial class ConversationThread : Entity
    {

        /// <summary>
        /// ToRecipients
        /// </summary>
        [JsonProperty("ToRecipients", NullValueHandling = NullValueHandling.Ignore)]
        public IList<Recipient> ToRecipients
        {
            get { return (IList<Recipient>) this.propertyBag[ConversationThreadObjectSchema.ToRecipients]; }
            set { this.propertyBag[ConversationThreadObjectSchema.ToRecipients] = value; }
        }

        /// <summary>
        /// Topic
        /// </summary>
        [JsonProperty("Topic", NullValueHandling = NullValueHandling.Ignore)]
        public string Topic
        {
            get { return (string) this.propertyBag[ConversationThreadObjectSchema.Topic]; }
            set { this.propertyBag[ConversationThreadObjectSchema.Topic] = value; }
        }

        /// <summary>
        /// HasAttachments
        /// </summary>
        [JsonProperty("HasAttachments", NullValueHandling = NullValueHandling.Ignore)]
        public bool HasAttachments
        {
            get { return (bool) this.propertyBag[ConversationThreadObjectSchema.HasAttachments]; }
            set { this.propertyBag[ConversationThreadObjectSchema.HasAttachments] = value; }
        }

        /// <summary>
        /// LastDeliveredDateTime
        /// </summary>
        [JsonProperty("LastDeliveredDateTime", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset LastDeliveredDateTime
        {
            get { return (DateTimeOffset) this.propertyBag[ConversationThreadObjectSchema.LastDeliveredDateTime]; }
            set { this.propertyBag[ConversationThreadObjectSchema.LastDeliveredDateTime] = value; }
        }

        /// <summary>
        /// UniqueSenders
        /// </summary>
        [JsonProperty("UniqueSenders", NullValueHandling = NullValueHandling.Ignore)]
        public IList<String> UniqueSenders
        {
            get { return (IList<String>) this.propertyBag[ConversationThreadObjectSchema.UniqueSenders]; }
            set { this.propertyBag[ConversationThreadObjectSchema.UniqueSenders] = value; }
        }

        /// <summary>
        /// CcRecipients
        /// </summary>
        [JsonProperty("CcRecipients", NullValueHandling = NullValueHandling.Ignore)]
        public IList<Recipient> CcRecipients
        {
            get { return (IList<Recipient>) this.propertyBag[ConversationThreadObjectSchema.CcRecipients]; }
            set { this.propertyBag[ConversationThreadObjectSchema.CcRecipients] = value; }
        }

        /// <summary>
        /// Preview
        /// </summary>
        [JsonProperty("Preview", NullValueHandling = NullValueHandling.Ignore)]
        public string Preview
        {
            get { return (string) this.propertyBag[ConversationThreadObjectSchema.Preview]; }
            set { this.propertyBag[ConversationThreadObjectSchema.Preview] = value; }
        }

        /// <summary>
        /// IsLocked
        /// </summary>
        [JsonProperty("IsLocked", NullValueHandling = NullValueHandling.Ignore)]
        public bool IsLocked
        {
            get { return (bool) this.propertyBag[ConversationThreadObjectSchema.IsLocked]; }
            set { this.propertyBag[ConversationThreadObjectSchema.IsLocked] = value; }
        }

        /// <summary>
        /// Posts
        /// </summary>
        [JsonProperty("Posts", NullValueHandling = NullValueHandling.Ignore)]
        public IList<Post> Posts
        {
            get { return (IList<Post>) this.propertyBag[ConversationThreadObjectSchema.Posts]; }
            set { this.propertyBag[ConversationThreadObjectSchema.Posts] = value; }
        }

        /// <summary>
        /// Reply
        /// </summary>
        public void Reply(Post post)
        {
            Dictionary<string, object> additionalParameters = new Dictionary<string, object>();
            additionalParameters.Add(nameof(post), post);
            this.Service.Invoke(nameof(this.Reply), this, additionalParameters);
        }

    }


    /// <summary>
    /// Conversation
    /// </summary>
    public partial class Conversation : Entity
    {

        /// <summary>
        /// Topic
        /// </summary>
        [JsonProperty("Topic", NullValueHandling = NullValueHandling.Ignore)]
        public string Topic
        {
            get { return (string) this.propertyBag[ConversationObjectSchema.Topic]; }
            set { this.propertyBag[ConversationObjectSchema.Topic] = value; }
        }

        /// <summary>
        /// HasAttachments
        /// </summary>
        [JsonProperty("HasAttachments", NullValueHandling = NullValueHandling.Ignore)]
        public bool HasAttachments
        {
            get { return (bool) this.propertyBag[ConversationObjectSchema.HasAttachments]; }
            set { this.propertyBag[ConversationObjectSchema.HasAttachments] = value; }
        }

        /// <summary>
        /// LastDeliveredDateTime
        /// </summary>
        [JsonProperty("LastDeliveredDateTime", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset LastDeliveredDateTime
        {
            get { return (DateTimeOffset) this.propertyBag[ConversationObjectSchema.LastDeliveredDateTime]; }
            set { this.propertyBag[ConversationObjectSchema.LastDeliveredDateTime] = value; }
        }

        /// <summary>
        /// UniqueSenders
        /// </summary>
        [JsonProperty("UniqueSenders", NullValueHandling = NullValueHandling.Ignore)]
        public IList<String> UniqueSenders
        {
            get { return (IList<String>) this.propertyBag[ConversationObjectSchema.UniqueSenders]; }
            set { this.propertyBag[ConversationObjectSchema.UniqueSenders] = value; }
        }

        /// <summary>
        /// Preview
        /// </summary>
        [JsonProperty("Preview", NullValueHandling = NullValueHandling.Ignore)]
        public string Preview
        {
            get { return (string) this.propertyBag[ConversationObjectSchema.Preview]; }
            set { this.propertyBag[ConversationObjectSchema.Preview] = value; }
        }

        /// <summary>
        /// Threads
        /// </summary>
        [JsonProperty("Threads", NullValueHandling = NullValueHandling.Ignore)]
        public IList<ConversationThread> Threads
        {
            get { return (IList<ConversationThread>) this.propertyBag[ConversationObjectSchema.Threads]; }
            set { this.propertyBag[ConversationObjectSchema.Threads] = value; }
        }
    }


    /// <summary>
    /// Attachment
    /// </summary>
    public abstract partial class Attachment : Entity
    {

        /// <summary>
        /// LastModifiedDateTime
        /// </summary>
        [JsonProperty("LastModifiedDateTime", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset LastModifiedDateTime
        {
            get { return (DateTimeOffset) this.propertyBag[AttachmentObjectSchema.LastModifiedDateTime]; }
            set { this.propertyBag[AttachmentObjectSchema.LastModifiedDateTime] = value; }
        }

        /// <summary>
        /// Name
        /// </summary>
        [JsonProperty("Name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name
        {
            get { return (string) this.propertyBag[AttachmentObjectSchema.Name]; }
            set { this.propertyBag[AttachmentObjectSchema.Name] = value; }
        }

        /// <summary>
        /// ContentType
        /// </summary>
        [JsonProperty("ContentType", NullValueHandling = NullValueHandling.Ignore)]
        public string ContentType
        {
            get { return (string) this.propertyBag[AttachmentObjectSchema.ContentType]; }
            set { this.propertyBag[AttachmentObjectSchema.ContentType] = value; }
        }

        /// <summary>
        /// Size
        /// </summary>
        [JsonProperty("Size", NullValueHandling = NullValueHandling.Ignore)]
        public int Size
        {
            get { return (int) this.propertyBag[AttachmentObjectSchema.Size]; }
            set { this.propertyBag[AttachmentObjectSchema.Size] = value; }
        }

        /// <summary>
        /// IsInline
        /// </summary>
        [JsonProperty("IsInline", NullValueHandling = NullValueHandling.Ignore)]
        public bool IsInline
        {
            get { return (bool) this.propertyBag[AttachmentObjectSchema.IsInline]; }
            set { this.propertyBag[AttachmentObjectSchema.IsInline] = value; }
        }
    }


    /// <summary>
    /// FileAttachment
    /// </summary>
    public partial class FileAttachment : Attachment
    {

        /// <summary>
        /// ContentId
        /// </summary>
        [JsonProperty("ContentId", NullValueHandling = NullValueHandling.Ignore)]
        public string ContentId
        {
            get { return (string) this.propertyBag[FileAttachmentObjectSchema.ContentId]; }
            set { this.propertyBag[FileAttachmentObjectSchema.ContentId] = value; }
        }

        /// <summary>
        /// ContentLocation
        /// </summary>
        [JsonProperty("ContentLocation", NullValueHandling = NullValueHandling.Ignore)]
        public string ContentLocation
        {
            get { return (string) this.propertyBag[FileAttachmentObjectSchema.ContentLocation]; }
            set { this.propertyBag[FileAttachmentObjectSchema.ContentLocation] = value; }
        }

        /// <summary>
        /// ContentBytes
        /// </summary>
        [JsonProperty("ContentBytes", NullValueHandling = NullValueHandling.Ignore)]
        public string ContentBytes
        {
            get { return (string) this.propertyBag[FileAttachmentObjectSchema.ContentBytes]; }
            set { this.propertyBag[FileAttachmentObjectSchema.ContentBytes] = value; }
        }
    }


    /// <summary>
    /// ItemAttachment
    /// </summary>
    public partial class ItemAttachment : Attachment
    {

        /// <summary>
        /// Item
        /// </summary>
        [JsonProperty("Item", NullValueHandling = NullValueHandling.Ignore)]
        public Item Item
        {
            get { return (Item) this.propertyBag[ItemAttachmentObjectSchema.Item]; }
            set { this.propertyBag[ItemAttachmentObjectSchema.Item] = value; }
        }
    }


    /// <summary>
    /// ReferenceAttachment
    /// </summary>
    public partial class ReferenceAttachment : Attachment
    {

        /// <summary>
        /// SourceUrl
        /// </summary>
        [JsonProperty("SourceUrl", NullValueHandling = NullValueHandling.Ignore)]
        public string SourceUrl
        {
            get { return (string) this.propertyBag[ReferenceAttachmentObjectSchema.SourceUrl]; }
            set { this.propertyBag[ReferenceAttachmentObjectSchema.SourceUrl] = value; }
        }

        /// <summary>
        /// ProviderType
        /// </summary>
        [JsonProperty("ProviderType", NullValueHandling = NullValueHandling.Ignore)]
        public ReferenceAttachmentProvider ProviderType
        {
            get { return (ReferenceAttachmentProvider) this.propertyBag[ReferenceAttachmentObjectSchema.ProviderType]; }
            set { this.propertyBag[ReferenceAttachmentObjectSchema.ProviderType] = value; }
        }

        /// <summary>
        /// ThumbnailUrl
        /// </summary>
        [JsonProperty("ThumbnailUrl", NullValueHandling = NullValueHandling.Ignore)]
        public string ThumbnailUrl
        {
            get { return (string) this.propertyBag[ReferenceAttachmentObjectSchema.ThumbnailUrl]; }
            set { this.propertyBag[ReferenceAttachmentObjectSchema.ThumbnailUrl] = value; }
        }

        /// <summary>
        /// PreviewUrl
        /// </summary>
        [JsonProperty("PreviewUrl", NullValueHandling = NullValueHandling.Ignore)]
        public string PreviewUrl
        {
            get { return (string) this.propertyBag[ReferenceAttachmentObjectSchema.PreviewUrl]; }
            set { this.propertyBag[ReferenceAttachmentObjectSchema.PreviewUrl] = value; }
        }

        /// <summary>
        /// Permission
        /// </summary>
        [JsonProperty("Permission", NullValueHandling = NullValueHandling.Ignore)]
        public ReferenceAttachmentPermission Permission
        {
            get { return (ReferenceAttachmentPermission) this.propertyBag[ReferenceAttachmentObjectSchema.Permission]; }
            set { this.propertyBag[ReferenceAttachmentObjectSchema.Permission] = value; }
        }

        /// <summary>
        /// IsFolder
        /// </summary>
        [JsonProperty("IsFolder", NullValueHandling = NullValueHandling.Ignore)]
        public bool IsFolder
        {
            get { return (bool) this.propertyBag[ReferenceAttachmentObjectSchema.IsFolder]; }
            set { this.propertyBag[ReferenceAttachmentObjectSchema.IsFolder] = value; }
        }
    }


    /// <summary>
    /// MessageRule
    /// </summary>
    public partial class MessageRule : Entity
    {

        /// <summary>
        /// DisplayName
        /// </summary>
        [JsonProperty("DisplayName", NullValueHandling = NullValueHandling.Ignore)]
        public string DisplayName
        {
            get { return (string) this.propertyBag[MessageRuleObjectSchema.DisplayName]; }
            set { this.propertyBag[MessageRuleObjectSchema.DisplayName] = value; }
        }

        /// <summary>
        /// Sequence
        /// </summary>
        [JsonProperty("Sequence", NullValueHandling = NullValueHandling.Ignore)]
        public int Sequence
        {
            get { return (int) this.propertyBag[MessageRuleObjectSchema.Sequence]; }
            set { this.propertyBag[MessageRuleObjectSchema.Sequence] = value; }
        }

        /// <summary>
        /// Conditions
        /// </summary>
        [JsonProperty("Conditions", NullValueHandling = NullValueHandling.Ignore)]
        public MessageRulePredicates Conditions
        {
            get { return (MessageRulePredicates) this.propertyBag[MessageRuleObjectSchema.Conditions]; }
            set { this.propertyBag[MessageRuleObjectSchema.Conditions] = value; }
        }

        /// <summary>
        /// Actions
        /// </summary>
        [JsonProperty("Actions", NullValueHandling = NullValueHandling.Ignore)]
        public MessageRuleActions Actions
        {
            get { return (MessageRuleActions) this.propertyBag[MessageRuleObjectSchema.Actions]; }
            set { this.propertyBag[MessageRuleObjectSchema.Actions] = value; }
        }

        /// <summary>
        /// Exceptions
        /// </summary>
        [JsonProperty("Exceptions", NullValueHandling = NullValueHandling.Ignore)]
        public MessageRulePredicates Exceptions
        {
            get { return (MessageRulePredicates) this.propertyBag[MessageRuleObjectSchema.Exceptions]; }
            set { this.propertyBag[MessageRuleObjectSchema.Exceptions] = value; }
        }

        /// <summary>
        /// IsEnabled
        /// </summary>
        [JsonProperty("IsEnabled", NullValueHandling = NullValueHandling.Ignore)]
        public bool IsEnabled
        {
            get { return (bool) this.propertyBag[MessageRuleObjectSchema.IsEnabled]; }
            set { this.propertyBag[MessageRuleObjectSchema.IsEnabled] = value; }
        }

        /// <summary>
        /// HasError
        /// </summary>
        [JsonProperty("HasError", NullValueHandling = NullValueHandling.Ignore)]
        public bool HasError
        {
            get { return (bool) this.propertyBag[MessageRuleObjectSchema.HasError]; }
            set { this.propertyBag[MessageRuleObjectSchema.HasError] = value; }
        }

        /// <summary>
        /// IsReadOnly
        /// </summary>
        [JsonProperty("IsReadOnly", NullValueHandling = NullValueHandling.Ignore)]
        public bool IsReadOnly
        {
            get { return (bool) this.propertyBag[MessageRuleObjectSchema.IsReadOnly]; }
            set { this.propertyBag[MessageRuleObjectSchema.IsReadOnly] = value; }
        }
    }


    /// <summary>
    /// InferenceClassificationOverride
    /// </summary>
    public partial class InferenceClassificationOverride : Entity
    {

        /// <summary>
        /// ClassifyAs
        /// </summary>
        [JsonProperty("ClassifyAs", NullValueHandling = NullValueHandling.Ignore)]
        public InferenceClassificationType ClassifyAs
        {
            get { return (InferenceClassificationType) this.propertyBag[InferenceClassificationOverrideObjectSchema.ClassifyAs]; }
            set { this.propertyBag[InferenceClassificationOverrideObjectSchema.ClassifyAs] = value; }
        }

        /// <summary>
        /// SenderEmailAddress
        /// </summary>
        [JsonProperty("SenderEmailAddress", NullValueHandling = NullValueHandling.Ignore)]
        public EmailAddress SenderEmailAddress
        {
            get { return (EmailAddress) this.propertyBag[InferenceClassificationOverrideObjectSchema.SenderEmailAddress]; }
            set { this.propertyBag[InferenceClassificationOverrideObjectSchema.SenderEmailAddress] = value; }
        }
    }


    /// <summary>
    /// Post
    /// </summary>
    public partial class Post : Item
    {

        /// <summary>
        /// Body
        /// </summary>
        [JsonProperty("Body", NullValueHandling = NullValueHandling.Ignore)]
        public ItemBody Body
        {
            get { return (ItemBody) this.propertyBag[PostObjectSchema.Body]; }
            set { this.propertyBag[PostObjectSchema.Body] = value; }
        }

        /// <summary>
        /// ReceivedDateTime
        /// </summary>
        [JsonProperty("ReceivedDateTime", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset ReceivedDateTime
        {
            get { return (DateTimeOffset) this.propertyBag[PostObjectSchema.ReceivedDateTime]; }
            set { this.propertyBag[PostObjectSchema.ReceivedDateTime] = value; }
        }

        /// <summary>
        /// HasAttachments
        /// </summary>
        [JsonProperty("HasAttachments", NullValueHandling = NullValueHandling.Ignore)]
        public bool HasAttachments
        {
            get { return (bool) this.propertyBag[PostObjectSchema.HasAttachments]; }
            set { this.propertyBag[PostObjectSchema.HasAttachments] = value; }
        }

        /// <summary>
        /// From
        /// </summary>
        [JsonProperty("From", NullValueHandling = NullValueHandling.Ignore)]
        public Recipient From
        {
            get { return (Recipient) this.propertyBag[PostObjectSchema.From]; }
            set { this.propertyBag[PostObjectSchema.From] = value; }
        }

        /// <summary>
        /// Sender
        /// </summary>
        [JsonProperty("Sender", NullValueHandling = NullValueHandling.Ignore)]
        public Recipient Sender
        {
            get { return (Recipient) this.propertyBag[PostObjectSchema.Sender]; }
            set { this.propertyBag[PostObjectSchema.Sender] = value; }
        }

        /// <summary>
        /// ConversationThreadId
        /// </summary>
        [JsonProperty("ConversationThreadId", NullValueHandling = NullValueHandling.Ignore)]
        public string ConversationThreadId
        {
            get { return (string) this.propertyBag[PostObjectSchema.ConversationThreadId]; }
            set { this.propertyBag[PostObjectSchema.ConversationThreadId] = value; }
        }

        /// <summary>
        /// NewParticipants
        /// </summary>
        [JsonProperty("NewParticipants", NullValueHandling = NullValueHandling.Ignore)]
        public IList<Recipient> NewParticipants
        {
            get { return (IList<Recipient>) this.propertyBag[PostObjectSchema.NewParticipants]; }
            set { this.propertyBag[PostObjectSchema.NewParticipants] = value; }
        }

        /// <summary>
        /// ConversationId
        /// </summary>
        [JsonProperty("ConversationId", NullValueHandling = NullValueHandling.Ignore)]
        public string ConversationId
        {
            get { return (string) this.propertyBag[PostObjectSchema.ConversationId]; }
            set { this.propertyBag[PostObjectSchema.ConversationId] = value; }
        }

        /// <summary>
        /// Importance
        /// </summary>
        [JsonProperty("Importance", NullValueHandling = NullValueHandling.Ignore)]
        public Importance Importance
        {
            get { return (Importance) this.propertyBag[PostObjectSchema.Importance]; }
            set { this.propertyBag[PostObjectSchema.Importance] = value; }
        }

        /// <summary>
        /// InReplyTo
        /// </summary>
        [JsonProperty("InReplyTo", NullValueHandling = NullValueHandling.Ignore)]
        public Post InReplyTo
        {
            get { return (Post) this.propertyBag[PostObjectSchema.InReplyTo]; }
            set { this.propertyBag[PostObjectSchema.InReplyTo] = value; }
        }

        /// <summary>
        /// SingleValueExtendedProperties
        /// </summary>
        [JsonProperty("SingleValueExtendedProperties", NullValueHandling = NullValueHandling.Ignore)]
        public IList<SingleValueLegacyExtendedProperty> SingleValueExtendedProperties
        {
            get { return (IList<SingleValueLegacyExtendedProperty>) this.propertyBag[PostObjectSchema.SingleValueExtendedProperties]; }
            set { this.propertyBag[PostObjectSchema.SingleValueExtendedProperties] = value; }
        }

        /// <summary>
        /// MultiValueExtendedProperties
        /// </summary>
        [JsonProperty("MultiValueExtendedProperties", NullValueHandling = NullValueHandling.Ignore)]
        public IList<MultiValueLegacyExtendedProperty> MultiValueExtendedProperties
        {
            get { return (IList<MultiValueLegacyExtendedProperty>) this.propertyBag[PostObjectSchema.MultiValueExtendedProperties]; }
            set { this.propertyBag[PostObjectSchema.MultiValueExtendedProperties] = value; }
        }

        /// <summary>
        /// Extensions
        /// </summary>
        [JsonProperty("Extensions", NullValueHandling = NullValueHandling.Ignore)]
        public IList<Extension> Extensions
        {
            get { return (IList<Extension>) this.propertyBag[PostObjectSchema.Extensions]; }
            set { this.propertyBag[PostObjectSchema.Extensions] = value; }
        }

        /// <summary>
        /// Attachments
        /// </summary>
        [JsonProperty("Attachments", NullValueHandling = NullValueHandling.Ignore)]
        public IList<Attachment> Attachments
        {
            get { return (IList<Attachment>) this.propertyBag[PostObjectSchema.Attachments]; }
            set { this.propertyBag[PostObjectSchema.Attachments] = value; }
        }

        /// <summary>
        /// Reply
        /// </summary>
        public void Reply()
        {
            Dictionary<string, object> additionalParameters = new Dictionary<string, object>();
            this.Service.Invoke(nameof(this.Reply), this, additionalParameters);
        }


        /// <summary>
        /// Forward
        /// </summary>
        public void Forward(string comment, IList<Recipient> toRecipients)
        {
            Dictionary<string, object> additionalParameters = new Dictionary<string, object>();
            additionalParameters.Add(nameof(comment), comment);
            additionalParameters.Add(nameof(toRecipients), toRecipients);
            this.Service.Invoke(nameof(this.Forward), this, additionalParameters);
        }

    }


    /// <summary>
    /// Extension
    /// </summary>
    public abstract partial class Extension : Entity
    {
    }


    /// <summary>
    /// OpenTypeExtension
    /// </summary>
    public partial class OpenTypeExtension : Extension
    {

        /// <summary>
        /// ExtensionName
        /// </summary>
        [JsonProperty("ExtensionName", NullValueHandling = NullValueHandling.Ignore)]
        public string ExtensionName
        {
            get { return (string) this.propertyBag[OpenTypeExtensionObjectSchema.ExtensionName]; }
            set { this.propertyBag[OpenTypeExtensionObjectSchema.ExtensionName] = value; }
        }
    }


    /// <summary>
    /// TimeZoneBase
    /// </summary>
    public partial class TimeZoneBase
    {

        /// <summary>
        /// Name
        /// </summary>
        [JsonProperty("Name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

    }


    /// <summary>
    /// CustomTimeZone
    /// </summary>
    public partial class CustomTimeZone : TimeZoneBase
    {

        /// <summary>
        /// Bias
        /// </summary>
        [JsonProperty("Bias", NullValueHandling = NullValueHandling.Ignore)]
        public int Bias { get; set; }


        /// <summary>
        /// StandardOffset
        /// </summary>
        [JsonProperty("StandardOffset", NullValueHandling = NullValueHandling.Ignore)]
        public StandardTimeZoneOffset StandardOffset { get; set; }


        /// <summary>
        /// DaylightOffset
        /// </summary>
        [JsonProperty("DaylightOffset", NullValueHandling = NullValueHandling.Ignore)]
        public DaylightTimeZoneOffset DaylightOffset { get; set; }

    }


    /// <summary>
    /// StandardTimeZoneOffset
    /// </summary>
    public partial class StandardTimeZoneOffset
    {

        /// <summary>
        /// Time
        /// </summary>
        [JsonProperty("Time", NullValueHandling = NullValueHandling.Ignore)]
        public TimeSpan Time { get; set; }


        /// <summary>
        /// DayOccurrence
        /// </summary>
        [JsonProperty("DayOccurrence", NullValueHandling = NullValueHandling.Ignore)]
        public int DayOccurrence { get; set; }


        /// <summary>
        /// DayOfWeek
        /// </summary>
        [JsonProperty("DayOfWeek", NullValueHandling = NullValueHandling.Ignore)]
        public DayOfWeek DayOfWeek { get; set; }


        /// <summary>
        /// Month
        /// </summary>
        [JsonProperty("Month", NullValueHandling = NullValueHandling.Ignore)]
        public int Month { get; set; }


        /// <summary>
        /// Year
        /// </summary>
        [JsonProperty("Year", NullValueHandling = NullValueHandling.Ignore)]
        public int Year { get; set; }

    }


    /// <summary>
    /// DaylightTimeZoneOffset
    /// </summary>
    public partial class DaylightTimeZoneOffset : StandardTimeZoneOffset
    {

        /// <summary>
        /// DaylightBias
        /// </summary>
        [JsonProperty("DaylightBias", NullValueHandling = NullValueHandling.Ignore)]
        public int DaylightBias { get; set; }

    }


    /// <summary>
    /// MailboxSettings
    /// </summary>
    public partial class MailboxSettings
    {

        /// <summary>
        /// AutomaticRepliesSetting
        /// </summary>
        [JsonProperty("AutomaticRepliesSetting", NullValueHandling = NullValueHandling.Ignore)]
        public AutomaticRepliesSetting AutomaticRepliesSetting { get; set; }


        /// <summary>
        /// ArchiveFolder
        /// </summary>
        [JsonProperty("ArchiveFolder", NullValueHandling = NullValueHandling.Ignore)]
        public string ArchiveFolder { get; set; }


        /// <summary>
        /// TimeZone
        /// </summary>
        [JsonProperty("TimeZone", NullValueHandling = NullValueHandling.Ignore)]
        public string TimeZone { get; set; }


        /// <summary>
        /// Language
        /// </summary>
        [JsonProperty("Language", NullValueHandling = NullValueHandling.Ignore)]
        public LocaleInfo Language { get; set; }


        /// <summary>
        /// WorkingHours
        /// </summary>
        [JsonProperty("WorkingHours", NullValueHandling = NullValueHandling.Ignore)]
        public WorkingHours WorkingHours { get; set; }

    }


    /// <summary>
    /// AutomaticRepliesSetting
    /// </summary>
    public partial class AutomaticRepliesSetting
    {

        /// <summary>
        /// Status
        /// </summary>
        [JsonProperty("Status", NullValueHandling = NullValueHandling.Ignore)]
        public AutomaticRepliesStatus Status { get; set; }


        /// <summary>
        /// ExternalAudience
        /// </summary>
        [JsonProperty("ExternalAudience", NullValueHandling = NullValueHandling.Ignore)]
        public ExternalAudienceScope ExternalAudience { get; set; }


        /// <summary>
        /// ScheduledStartDateTime
        /// </summary>
        [JsonProperty("ScheduledStartDateTime", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeTimeZone ScheduledStartDateTime { get; set; }


        /// <summary>
        /// ScheduledEndDateTime
        /// </summary>
        [JsonProperty("ScheduledEndDateTime", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeTimeZone ScheduledEndDateTime { get; set; }


        /// <summary>
        /// InternalReplyMessage
        /// </summary>
        [JsonProperty("InternalReplyMessage", NullValueHandling = NullValueHandling.Ignore)]
        public string InternalReplyMessage { get; set; }


        /// <summary>
        /// ExternalReplyMessage
        /// </summary>
        [JsonProperty("ExternalReplyMessage", NullValueHandling = NullValueHandling.Ignore)]
        public string ExternalReplyMessage { get; set; }

    }


    /// <summary>
    /// DateTimeTimeZone
    /// </summary>
    public partial class DateTimeTimeZone
    {

        /// <summary>
        /// DateTime
        /// </summary>
        [JsonProperty("DateTime", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime DateTime { get; set; }


        /// <summary>
        /// TimeZone
        /// </summary>
        [JsonProperty("TimeZone", NullValueHandling = NullValueHandling.Ignore)]
        public string TimeZone { get; set; }

    }


    /// <summary>
    /// LocaleInfo
    /// </summary>
    public partial class LocaleInfo
    {

        /// <summary>
        /// Locale
        /// </summary>
        [JsonProperty("Locale", NullValueHandling = NullValueHandling.Ignore)]
        public string Locale { get; set; }


        /// <summary>
        /// DisplayName
        /// </summary>
        [JsonProperty("DisplayName", NullValueHandling = NullValueHandling.Ignore)]
        public string DisplayName { get; set; }

    }


    /// <summary>
    /// WorkingHours
    /// </summary>
    public partial class WorkingHours
    {

        /// <summary>
        /// DaysOfWeek
        /// </summary>
        [JsonProperty("DaysOfWeek", NullValueHandling = NullValueHandling.Ignore)]
        public IList<DayOfWeek> DaysOfWeek { get; set; }


        /// <summary>
        /// StartTime
        /// </summary>
        [JsonProperty("StartTime", NullValueHandling = NullValueHandling.Ignore)]
        public TimeSpan StartTime { get; set; }


        /// <summary>
        /// EndTime
        /// </summary>
        [JsonProperty("EndTime", NullValueHandling = NullValueHandling.Ignore)]
        public TimeSpan EndTime { get; set; }


        /// <summary>
        /// TimeZone
        /// </summary>
        [JsonProperty("TimeZone", NullValueHandling = NullValueHandling.Ignore)]
        public TimeZoneBase TimeZone { get; set; }

    }


    /// <summary>
    /// Reminder
    /// </summary>
    public partial class Reminder
    {

        /// <summary>
        /// EventId
        /// </summary>
        [JsonProperty("EventId", NullValueHandling = NullValueHandling.Ignore)]
        public string EventId { get; set; }


        /// <summary>
        /// EventStartTime
        /// </summary>
        [JsonProperty("EventStartTime", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeTimeZone EventStartTime { get; set; }


        /// <summary>
        /// EventEndTime
        /// </summary>
        [JsonProperty("EventEndTime", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeTimeZone EventEndTime { get; set; }


        /// <summary>
        /// ChangeKey
        /// </summary>
        [JsonProperty("ChangeKey", NullValueHandling = NullValueHandling.Ignore)]
        public string ChangeKey { get; set; }


        /// <summary>
        /// EventSubject
        /// </summary>
        [JsonProperty("EventSubject", NullValueHandling = NullValueHandling.Ignore)]
        public string EventSubject { get; set; }


        /// <summary>
        /// EventLocation
        /// </summary>
        [JsonProperty("EventLocation", NullValueHandling = NullValueHandling.Ignore)]
        public Location EventLocation { get; set; }


        /// <summary>
        /// EventWebLink
        /// </summary>
        [JsonProperty("EventWebLink", NullValueHandling = NullValueHandling.Ignore)]
        public string EventWebLink { get; set; }


        /// <summary>
        /// ReminderFireTime
        /// </summary>
        [JsonProperty("ReminderFireTime", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeTimeZone ReminderFireTime { get; set; }

    }


    /// <summary>
    /// Location
    /// </summary>
    public partial class Location
    {

        /// <summary>
        /// DisplayName
        /// </summary>
        [JsonProperty("DisplayName", NullValueHandling = NullValueHandling.Ignore)]
        public string DisplayName { get; set; }


        /// <summary>
        /// LocationEmailAddress
        /// </summary>
        [JsonProperty("LocationEmailAddress", NullValueHandling = NullValueHandling.Ignore)]
        public string LocationEmailAddress { get; set; }


        /// <summary>
        /// Address
        /// </summary>
        [JsonProperty("Address", NullValueHandling = NullValueHandling.Ignore)]
        public PhysicalAddress Address { get; set; }


        /// <summary>
        /// Coordinates
        /// </summary>
        [JsonProperty("Coordinates", NullValueHandling = NullValueHandling.Ignore)]
        public GeoCoordinates Coordinates { get; set; }


        /// <summary>
        /// LocationUri
        /// </summary>
        [JsonProperty("LocationUri", NullValueHandling = NullValueHandling.Ignore)]
        public string LocationUri { get; set; }


        /// <summary>
        /// LocationType
        /// </summary>
        [JsonProperty("LocationType", NullValueHandling = NullValueHandling.Ignore)]
        public LocationType LocationType { get; set; }


        /// <summary>
        /// UniqueId
        /// </summary>
        [JsonProperty("UniqueId", NullValueHandling = NullValueHandling.Ignore)]
        public string UniqueId { get; set; }


        /// <summary>
        /// UniqueIdType
        /// </summary>
        [JsonProperty("UniqueIdType", NullValueHandling = NullValueHandling.Ignore)]
        public LocationUniqueIdType UniqueIdType { get; set; }

    }


    /// <summary>
    /// PhysicalAddress
    /// </summary>
    public partial class PhysicalAddress
    {

        /// <summary>
        /// Type
        /// </summary>
        [JsonProperty("Type", NullValueHandling = NullValueHandling.Ignore)]
        public PhysicalAddressType Type { get; set; }


        /// <summary>
        /// PostOfficeBox
        /// </summary>
        [JsonProperty("PostOfficeBox", NullValueHandling = NullValueHandling.Ignore)]
        public string PostOfficeBox { get; set; }


        /// <summary>
        /// Street
        /// </summary>
        [JsonProperty("Street", NullValueHandling = NullValueHandling.Ignore)]
        public string Street { get; set; }


        /// <summary>
        /// City
        /// </summary>
        [JsonProperty("City", NullValueHandling = NullValueHandling.Ignore)]
        public string City { get; set; }


        /// <summary>
        /// State
        /// </summary>
        [JsonProperty("State", NullValueHandling = NullValueHandling.Ignore)]
        public string State { get; set; }


        /// <summary>
        /// CountryOrRegion
        /// </summary>
        [JsonProperty("CountryOrRegion", NullValueHandling = NullValueHandling.Ignore)]
        public string CountryOrRegion { get; set; }


        /// <summary>
        /// PostalCode
        /// </summary>
        [JsonProperty("PostalCode", NullValueHandling = NullValueHandling.Ignore)]
        public string PostalCode { get; set; }

    }


    /// <summary>
    /// GeoCoordinates
    /// </summary>
    public partial class GeoCoordinates
    {

        /// <summary>
        /// Altitude
        /// </summary>
        [JsonProperty("Altitude", NullValueHandling = NullValueHandling.Ignore)]
        public Double Altitude { get; set; }


        /// <summary>
        /// Latitude
        /// </summary>
        [JsonProperty("Latitude", NullValueHandling = NullValueHandling.Ignore)]
        public Double Latitude { get; set; }


        /// <summary>
        /// Longitude
        /// </summary>
        [JsonProperty("Longitude", NullValueHandling = NullValueHandling.Ignore)]
        public Double Longitude { get; set; }


        /// <summary>
        /// Accuracy
        /// </summary>
        [JsonProperty("Accuracy", NullValueHandling = NullValueHandling.Ignore)]
        public Double Accuracy { get; set; }


        /// <summary>
        /// AltitudeAccuracy
        /// </summary>
        [JsonProperty("AltitudeAccuracy", NullValueHandling = NullValueHandling.Ignore)]
        public Double AltitudeAccuracy { get; set; }

    }


    /// <summary>
    /// MailTips
    /// </summary>
    public partial class MailTips
    {

        /// <summary>
        /// EmailAddress
        /// </summary>
        [JsonProperty("EmailAddress", NullValueHandling = NullValueHandling.Ignore)]
        public EmailAddress EmailAddress { get; set; }


        /// <summary>
        /// AutomaticReplies
        /// </summary>
        [JsonProperty("AutomaticReplies", NullValueHandling = NullValueHandling.Ignore)]
        public AutomaticRepliesMailTips AutomaticReplies { get; set; }


        /// <summary>
        /// MailboxFull
        /// </summary>
        [JsonProperty("MailboxFull", NullValueHandling = NullValueHandling.Ignore)]
        public bool MailboxFull { get; set; }


        /// <summary>
        /// CustomMailTip
        /// </summary>
        [JsonProperty("CustomMailTip", NullValueHandling = NullValueHandling.Ignore)]
        public string CustomMailTip { get; set; }


        /// <summary>
        /// ExternalMemberCount
        /// </summary>
        [JsonProperty("ExternalMemberCount", NullValueHandling = NullValueHandling.Ignore)]
        public int ExternalMemberCount { get; set; }


        /// <summary>
        /// TotalMemberCount
        /// </summary>
        [JsonProperty("TotalMemberCount", NullValueHandling = NullValueHandling.Ignore)]
        public int TotalMemberCount { get; set; }


        /// <summary>
        /// DeliveryRestricted
        /// </summary>
        [JsonProperty("DeliveryRestricted", NullValueHandling = NullValueHandling.Ignore)]
        public bool DeliveryRestricted { get; set; }


        /// <summary>
        /// IsModerated
        /// </summary>
        [JsonProperty("IsModerated", NullValueHandling = NullValueHandling.Ignore)]
        public bool IsModerated { get; set; }


        /// <summary>
        /// RecipientScope
        /// </summary>
        [JsonProperty("RecipientScope", NullValueHandling = NullValueHandling.Ignore)]
        public RecipientScopeType RecipientScope { get; set; }


        /// <summary>
        /// RecipientSuggestions
        /// </summary>
        [JsonProperty("RecipientSuggestions", NullValueHandling = NullValueHandling.Ignore)]
        public IList<Recipient> RecipientSuggestions { get; set; }


        /// <summary>
        /// MaxMessageSize
        /// </summary>
        [JsonProperty("MaxMessageSize", NullValueHandling = NullValueHandling.Ignore)]
        public int MaxMessageSize { get; set; }


        /// <summary>
        /// Error
        /// </summary>
        [JsonProperty("Error", NullValueHandling = NullValueHandling.Ignore)]
        public Error Error { get; set; }

    }


    /// <summary>
    /// EmailAddress
    /// </summary>
    public partial class EmailAddress
    {

        /// <summary>
        /// Name
        /// </summary>
        [JsonProperty("Name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }


        /// <summary>
        /// Address
        /// </summary>
        [JsonProperty("Address", NullValueHandling = NullValueHandling.Ignore)]
        public string Address { get; set; }

    }


    /// <summary>
    /// AutomaticRepliesMailTips
    /// </summary>
    public partial class AutomaticRepliesMailTips
    {

        /// <summary>
        /// Message
        /// </summary>
        [JsonProperty("Message", NullValueHandling = NullValueHandling.Ignore)]
        public string Message { get; set; }


        /// <summary>
        /// MessageLanguage
        /// </summary>
        [JsonProperty("MessageLanguage", NullValueHandling = NullValueHandling.Ignore)]
        public LocaleInfo MessageLanguage { get; set; }


        /// <summary>
        /// ScheduledStartTime
        /// </summary>
        [JsonProperty("ScheduledStartTime", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeTimeZone ScheduledStartTime { get; set; }


        /// <summary>
        /// ScheduledEndTime
        /// </summary>
        [JsonProperty("ScheduledEndTime", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeTimeZone ScheduledEndTime { get; set; }

    }


    /// <summary>
    /// Recipient
    /// </summary>
    public partial class Recipient
    {

        /// <summary>
        /// EmailAddress
        /// </summary>
        [JsonProperty("EmailAddress", NullValueHandling = NullValueHandling.Ignore)]
        public EmailAddress EmailAddress { get; set; }

    }


    /// <summary>
    /// Error
    /// </summary>
    public partial class Error
    {

        /// <summary>
        /// Message
        /// </summary>
        [JsonProperty("Message", NullValueHandling = NullValueHandling.Ignore)]
        public string Message { get; set; }


        /// <summary>
        /// Code
        /// </summary>
        [JsonProperty("Code", NullValueHandling = NullValueHandling.Ignore)]
        public string Code { get; set; }

    }


    /// <summary>
    /// TimeZoneInformation
    /// </summary>
    public partial class TimeZoneInformation
    {

        /// <summary>
        /// Alias
        /// </summary>
        [JsonProperty("Alias", NullValueHandling = NullValueHandling.Ignore)]
        public string Alias { get; set; }


        /// <summary>
        /// DisplayName
        /// </summary>
        [JsonProperty("DisplayName", NullValueHandling = NullValueHandling.Ignore)]
        public string DisplayName { get; set; }

    }


    /// <summary>
    /// AttendeeBase
    /// </summary>
    public partial class AttendeeBase : Recipient
    {

        /// <summary>
        /// Type
        /// </summary>
        [JsonProperty("Type", NullValueHandling = NullValueHandling.Ignore)]
        public AttendeeType Type { get; set; }

    }


    /// <summary>
    /// LocationConstraint
    /// </summary>
    public partial class LocationConstraint
    {

        /// <summary>
        /// IsRequired
        /// </summary>
        [JsonProperty("IsRequired", NullValueHandling = NullValueHandling.Ignore)]
        public bool IsRequired { get; set; }


        /// <summary>
        /// SuggestLocation
        /// </summary>
        [JsonProperty("SuggestLocation", NullValueHandling = NullValueHandling.Ignore)]
        public bool SuggestLocation { get; set; }


        /// <summary>
        /// Locations
        /// </summary>
        [JsonProperty("Locations", NullValueHandling = NullValueHandling.Ignore)]
        public IList<LocationConstraintItem> Locations { get; set; }

    }


    /// <summary>
    /// LocationConstraintItem
    /// </summary>
    public partial class LocationConstraintItem : Location
    {

        /// <summary>
        /// ResolveAvailability
        /// </summary>
        [JsonProperty("ResolveAvailability", NullValueHandling = NullValueHandling.Ignore)]
        public bool ResolveAvailability { get; set; }

    }


    /// <summary>
    /// TimeConstraint
    /// </summary>
    public partial class TimeConstraint
    {

        /// <summary>
        /// ActivityDomain
        /// </summary>
        [JsonProperty("ActivityDomain", NullValueHandling = NullValueHandling.Ignore)]
        public ActivityDomain ActivityDomain { get; set; }


        /// <summary>
        /// Timeslots
        /// </summary>
        [JsonProperty("Timeslots", NullValueHandling = NullValueHandling.Ignore)]
        public IList<TimeSlot> Timeslots { get; set; }

    }


    /// <summary>
    /// TimeSlot
    /// </summary>
    public partial class TimeSlot
    {

        /// <summary>
        /// Start
        /// </summary>
        [JsonProperty("Start", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeTimeZone Start { get; set; }


        /// <summary>
        /// End
        /// </summary>
        [JsonProperty("End", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeTimeZone End { get; set; }

    }


    /// <summary>
    /// MeetingTimeSuggestionsResult
    /// </summary>
    public partial class MeetingTimeSuggestionsResult
    {

        /// <summary>
        /// MeetingTimeSuggestions
        /// </summary>
        [JsonProperty("MeetingTimeSuggestions", NullValueHandling = NullValueHandling.Ignore)]
        public IList<MeetingTimeSuggestion> MeetingTimeSuggestions { get; set; }


        /// <summary>
        /// EmptySuggestionsReason
        /// </summary>
        [JsonProperty("EmptySuggestionsReason", NullValueHandling = NullValueHandling.Ignore)]
        public string EmptySuggestionsReason { get; set; }

    }


    /// <summary>
    /// MeetingTimeSuggestion
    /// </summary>
    public partial class MeetingTimeSuggestion
    {

        /// <summary>
        /// MeetingTimeSlot
        /// </summary>
        [JsonProperty("MeetingTimeSlot", NullValueHandling = NullValueHandling.Ignore)]
        public TimeSlot MeetingTimeSlot { get; set; }


        /// <summary>
        /// Confidence
        /// </summary>
        [JsonProperty("Confidence", NullValueHandling = NullValueHandling.Ignore)]
        public Double Confidence { get; set; }


        /// <summary>
        /// OrganizerAvailability
        /// </summary>
        [JsonProperty("OrganizerAvailability", NullValueHandling = NullValueHandling.Ignore)]
        public FreeBusyStatus OrganizerAvailability { get; set; }


        /// <summary>
        /// AttendeeAvailability
        /// </summary>
        [JsonProperty("AttendeeAvailability", NullValueHandling = NullValueHandling.Ignore)]
        public IList<AttendeeAvailability> AttendeeAvailability { get; set; }


        /// <summary>
        /// Locations
        /// </summary>
        [JsonProperty("Locations", NullValueHandling = NullValueHandling.Ignore)]
        public IList<Location> Locations { get; set; }


        /// <summary>
        /// SuggestionReason
        /// </summary>
        [JsonProperty("SuggestionReason", NullValueHandling = NullValueHandling.Ignore)]
        public string SuggestionReason { get; set; }

    }


    /// <summary>
    /// AttendeeAvailability
    /// </summary>
    public partial class AttendeeAvailability
    {

        /// <summary>
        /// Attendee
        /// </summary>
        [JsonProperty("Attendee", NullValueHandling = NullValueHandling.Ignore)]
        public AttendeeBase Attendee { get; set; }


        /// <summary>
        /// Availability
        /// </summary>
        [JsonProperty("Availability", NullValueHandling = NullValueHandling.Ignore)]
        public FreeBusyStatus Availability { get; set; }

    }


    /// <summary>
    /// ItemBody
    /// </summary>
    public partial class ItemBody
    {

        /// <summary>
        /// ContentType
        /// </summary>
        [JsonProperty("ContentType", NullValueHandling = NullValueHandling.Ignore)]
        public BodyType ContentType { get; set; }


        /// <summary>
        /// Content
        /// </summary>
        [JsonProperty("Content", NullValueHandling = NullValueHandling.Ignore)]
        public string Content { get; set; }

    }


    /// <summary>
    /// PatternedRecurrence
    /// </summary>
    public partial class PatternedRecurrence
    {

        /// <summary>
        /// Pattern
        /// </summary>
        [JsonProperty("Pattern", NullValueHandling = NullValueHandling.Ignore)]
        public RecurrencePattern Pattern { get; set; }


        /// <summary>
        /// Range
        /// </summary>
        [JsonProperty("Range", NullValueHandling = NullValueHandling.Ignore)]
        public RecurrenceRange Range { get; set; }

    }


    /// <summary>
    /// RecurrencePattern
    /// </summary>
    public partial class RecurrencePattern
    {

        /// <summary>
        /// Type
        /// </summary>
        [JsonProperty("Type", NullValueHandling = NullValueHandling.Ignore)]
        public RecurrencePatternType Type { get; set; }


        /// <summary>
        /// Interval
        /// </summary>
        [JsonProperty("Interval", NullValueHandling = NullValueHandling.Ignore)]
        public int Interval { get; set; }


        /// <summary>
        /// Month
        /// </summary>
        [JsonProperty("Month", NullValueHandling = NullValueHandling.Ignore)]
        public int Month { get; set; }


        /// <summary>
        /// DayOfMonth
        /// </summary>
        [JsonProperty("DayOfMonth", NullValueHandling = NullValueHandling.Ignore)]
        public int DayOfMonth { get; set; }


        /// <summary>
        /// DaysOfWeek
        /// </summary>
        [JsonProperty("DaysOfWeek", NullValueHandling = NullValueHandling.Ignore)]
        public IList<DayOfWeek> DaysOfWeek { get; set; }


        /// <summary>
        /// FirstDayOfWeek
        /// </summary>
        [JsonProperty("FirstDayOfWeek", NullValueHandling = NullValueHandling.Ignore)]
        public DayOfWeek FirstDayOfWeek { get; set; }


        /// <summary>
        /// Index
        /// </summary>
        [JsonProperty("Index", NullValueHandling = NullValueHandling.Ignore)]
        public WeekIndex Index { get; set; }

    }


    /// <summary>
    /// RecurrenceRange
    /// </summary>
    public partial class RecurrenceRange
    {

        /// <summary>
        /// Type
        /// </summary>
        [JsonProperty("Type", NullValueHandling = NullValueHandling.Ignore)]
        public RecurrenceRangeType Type { get; set; }


        /// <summary>
        /// StartDate
        /// </summary>
        [JsonProperty("StartDate", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime StartDate { get; set; }


        /// <summary>
        /// EndDate
        /// </summary>
        [JsonProperty("EndDate", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime EndDate { get; set; }


        /// <summary>
        /// RecurrenceTimeZone
        /// </summary>
        [JsonProperty("RecurrenceTimeZone", NullValueHandling = NullValueHandling.Ignore)]
        public string RecurrenceTimeZone { get; set; }


        /// <summary>
        /// NumberOfOccurrences
        /// </summary>
        [JsonProperty("NumberOfOccurrences", NullValueHandling = NullValueHandling.Ignore)]
        public int NumberOfOccurrences { get; set; }

    }


    /// <summary>
    /// InternetMessageHeader
    /// </summary>
    public partial class InternetMessageHeader
    {

        /// <summary>
        /// Name
        /// </summary>
        [JsonProperty("Name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }


        /// <summary>
        /// Value
        /// </summary>
        [JsonProperty("Value", NullValueHandling = NullValueHandling.Ignore)]
        public string Value { get; set; }

    }


    /// <summary>
    /// FollowupFlag
    /// </summary>
    public partial class FollowupFlag
    {

        /// <summary>
        /// CompletedDateTime
        /// </summary>
        [JsonProperty("CompletedDateTime", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeTimeZone CompletedDateTime { get; set; }


        /// <summary>
        /// DueDateTime
        /// </summary>
        [JsonProperty("DueDateTime", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeTimeZone DueDateTime { get; set; }


        /// <summary>
        /// StartDateTime
        /// </summary>
        [JsonProperty("StartDateTime", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeTimeZone StartDateTime { get; set; }


        /// <summary>
        /// FlagStatus
        /// </summary>
        [JsonProperty("FlagStatus", NullValueHandling = NullValueHandling.Ignore)]
        public FollowupFlagStatus FlagStatus { get; set; }

    }


    /// <summary>
    /// ResponseStatus
    /// </summary>
    public partial class ResponseStatus
    {

        /// <summary>
        /// Response
        /// </summary>
        [JsonProperty("Response", NullValueHandling = NullValueHandling.Ignore)]
        public ResponseType Response { get; set; }


        /// <summary>
        /// Time
        /// </summary>
        [JsonProperty("Time", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset Time { get; set; }

    }


    /// <summary>
    /// Attendee
    /// </summary>
    public partial class Attendee : AttendeeBase
    {

        /// <summary>
        /// Status
        /// </summary>
        [JsonProperty("Status", NullValueHandling = NullValueHandling.Ignore)]
        public ResponseStatus Status { get; set; }

    }


    /// <summary>
    /// ScoredEmailAddress
    /// </summary>
    public partial class ScoredEmailAddress
    {

        /// <summary>
        /// Address
        /// </summary>
        [JsonProperty("Address", NullValueHandling = NullValueHandling.Ignore)]
        public string Address { get; set; }


        /// <summary>
        /// RelevanceScore
        /// </summary>
        [JsonProperty("RelevanceScore", NullValueHandling = NullValueHandling.Ignore)]
        public Double RelevanceScore { get; set; }


        /// <summary>
        /// SelectionLikelihood
        /// </summary>
        [JsonProperty("SelectionLikelihood", NullValueHandling = NullValueHandling.Ignore)]
        public SelectionLikelihoodInfo SelectionLikelihood { get; set; }


        /// <summary>
        /// ItemId
        /// </summary>
        [JsonProperty("ItemId", NullValueHandling = NullValueHandling.Ignore)]
        public string ItemId { get; set; }

    }


    /// <summary>
    /// Phone
    /// </summary>
    public partial class Phone
    {

        /// <summary>
        /// Type
        /// </summary>
        [JsonProperty("Type", NullValueHandling = NullValueHandling.Ignore)]
        public PhoneType Type { get; set; }


        /// <summary>
        /// Number
        /// </summary>
        [JsonProperty("Number", NullValueHandling = NullValueHandling.Ignore)]
        public string Number { get; set; }


        /// <summary>
        /// Region
        /// </summary>
        [JsonProperty("Region", NullValueHandling = NullValueHandling.Ignore)]
        public string Region { get; set; }


        /// <summary>
        /// Language
        /// </summary>
        [JsonProperty("Language", NullValueHandling = NullValueHandling.Ignore)]
        public string Language { get; set; }

    }


    /// <summary>
    /// Website
    /// </summary>
    public partial class Website
    {

        /// <summary>
        /// Type
        /// </summary>
        [JsonProperty("Type", NullValueHandling = NullValueHandling.Ignore)]
        public WebsiteType Type { get; set; }


        /// <summary>
        /// Address
        /// </summary>
        [JsonProperty("Address", NullValueHandling = NullValueHandling.Ignore)]
        public string Address { get; set; }


        /// <summary>
        /// DisplayName
        /// </summary>
        [JsonProperty("DisplayName", NullValueHandling = NullValueHandling.Ignore)]
        public string DisplayName { get; set; }

    }


    /// <summary>
    /// PersonType
    /// </summary>
    public partial class PersonType
    {

        /// <summary>
        /// Class
        /// </summary>
        [JsonProperty("Class", NullValueHandling = NullValueHandling.Ignore)]
        public string Class { get; set; }


        /// <summary>
        /// Subclass
        /// </summary>
        [JsonProperty("Subclass", NullValueHandling = NullValueHandling.Ignore)]
        public string Subclass { get; set; }

    }


    /// <summary>
    /// MessageRulePredicates
    /// </summary>
    public partial class MessageRulePredicates
    {

        /// <summary>
        /// Categories
        /// </summary>
        [JsonProperty("Categories", NullValueHandling = NullValueHandling.Ignore)]
        public IList<String> Categories { get; set; }


        /// <summary>
        /// SubjectContains
        /// </summary>
        [JsonProperty("SubjectContains", NullValueHandling = NullValueHandling.Ignore)]
        public IList<String> SubjectContains { get; set; }


        /// <summary>
        /// BodyContains
        /// </summary>
        [JsonProperty("BodyContains", NullValueHandling = NullValueHandling.Ignore)]
        public IList<String> BodyContains { get; set; }


        /// <summary>
        /// BodyOrSubjectContains
        /// </summary>
        [JsonProperty("BodyOrSubjectContains", NullValueHandling = NullValueHandling.Ignore)]
        public IList<String> BodyOrSubjectContains { get; set; }


        /// <summary>
        /// SenderContains
        /// </summary>
        [JsonProperty("SenderContains", NullValueHandling = NullValueHandling.Ignore)]
        public IList<String> SenderContains { get; set; }


        /// <summary>
        /// RecipientContains
        /// </summary>
        [JsonProperty("RecipientContains", NullValueHandling = NullValueHandling.Ignore)]
        public IList<String> RecipientContains { get; set; }


        /// <summary>
        /// HeaderContains
        /// </summary>
        [JsonProperty("HeaderContains", NullValueHandling = NullValueHandling.Ignore)]
        public IList<String> HeaderContains { get; set; }


        /// <summary>
        /// MessageActionFlag
        /// </summary>
        [JsonProperty("MessageActionFlag", NullValueHandling = NullValueHandling.Ignore)]
        public MessageActionFlag MessageActionFlag { get; set; }


        /// <summary>
        /// Importance
        /// </summary>
        [JsonProperty("Importance", NullValueHandling = NullValueHandling.Ignore)]
        public Importance Importance { get; set; }


        /// <summary>
        /// Sensitivity
        /// </summary>
        [JsonProperty("Sensitivity", NullValueHandling = NullValueHandling.Ignore)]
        public Sensitivity Sensitivity { get; set; }


        /// <summary>
        /// FromAddresses
        /// </summary>
        [JsonProperty("FromAddresses", NullValueHandling = NullValueHandling.Ignore)]
        public IList<Recipient> FromAddresses { get; set; }


        /// <summary>
        /// SentToAddresses
        /// </summary>
        [JsonProperty("SentToAddresses", NullValueHandling = NullValueHandling.Ignore)]
        public IList<Recipient> SentToAddresses { get; set; }


        /// <summary>
        /// SentToMe
        /// </summary>
        [JsonProperty("SentToMe", NullValueHandling = NullValueHandling.Ignore)]
        public bool SentToMe { get; set; }


        /// <summary>
        /// SentOnlyToMe
        /// </summary>
        [JsonProperty("SentOnlyToMe", NullValueHandling = NullValueHandling.Ignore)]
        public bool SentOnlyToMe { get; set; }


        /// <summary>
        /// SentCcMe
        /// </summary>
        [JsonProperty("SentCcMe", NullValueHandling = NullValueHandling.Ignore)]
        public bool SentCcMe { get; set; }


        /// <summary>
        /// SentToOrCcMe
        /// </summary>
        [JsonProperty("SentToOrCcMe", NullValueHandling = NullValueHandling.Ignore)]
        public bool SentToOrCcMe { get; set; }


        /// <summary>
        /// NotSentToMe
        /// </summary>
        [JsonProperty("NotSentToMe", NullValueHandling = NullValueHandling.Ignore)]
        public bool NotSentToMe { get; set; }


        /// <summary>
        /// HasAttachments
        /// </summary>
        [JsonProperty("HasAttachments", NullValueHandling = NullValueHandling.Ignore)]
        public bool HasAttachments { get; set; }


        /// <summary>
        /// IsApprovalRequest
        /// </summary>
        [JsonProperty("IsApprovalRequest", NullValueHandling = NullValueHandling.Ignore)]
        public bool IsApprovalRequest { get; set; }


        /// <summary>
        /// IsAutomaticForward
        /// </summary>
        [JsonProperty("IsAutomaticForward", NullValueHandling = NullValueHandling.Ignore)]
        public bool IsAutomaticForward { get; set; }


        /// <summary>
        /// IsAutomaticReply
        /// </summary>
        [JsonProperty("IsAutomaticReply", NullValueHandling = NullValueHandling.Ignore)]
        public bool IsAutomaticReply { get; set; }


        /// <summary>
        /// IsEncrypted
        /// </summary>
        [JsonProperty("IsEncrypted", NullValueHandling = NullValueHandling.Ignore)]
        public bool IsEncrypted { get; set; }


        /// <summary>
        /// IsMeetingRequest
        /// </summary>
        [JsonProperty("IsMeetingRequest", NullValueHandling = NullValueHandling.Ignore)]
        public bool IsMeetingRequest { get; set; }


        /// <summary>
        /// IsMeetingResponse
        /// </summary>
        [JsonProperty("IsMeetingResponse", NullValueHandling = NullValueHandling.Ignore)]
        public bool IsMeetingResponse { get; set; }


        /// <summary>
        /// IsNonDeliveryReport
        /// </summary>
        [JsonProperty("IsNonDeliveryReport", NullValueHandling = NullValueHandling.Ignore)]
        public bool IsNonDeliveryReport { get; set; }


        /// <summary>
        /// IsPermissionControlled
        /// </summary>
        [JsonProperty("IsPermissionControlled", NullValueHandling = NullValueHandling.Ignore)]
        public bool IsPermissionControlled { get; set; }


        /// <summary>
        /// IsReadReceipt
        /// </summary>
        [JsonProperty("IsReadReceipt", NullValueHandling = NullValueHandling.Ignore)]
        public bool IsReadReceipt { get; set; }


        /// <summary>
        /// IsSigned
        /// </summary>
        [JsonProperty("IsSigned", NullValueHandling = NullValueHandling.Ignore)]
        public bool IsSigned { get; set; }


        /// <summary>
        /// IsVoicemail
        /// </summary>
        [JsonProperty("IsVoicemail", NullValueHandling = NullValueHandling.Ignore)]
        public bool IsVoicemail { get; set; }


        /// <summary>
        /// WithinSizeRange
        /// </summary>
        [JsonProperty("WithinSizeRange", NullValueHandling = NullValueHandling.Ignore)]
        public SizeRange WithinSizeRange { get; set; }

    }


    /// <summary>
    /// SizeRange
    /// </summary>
    public partial class SizeRange
    {

        /// <summary>
        /// MinimumSize
        /// </summary>
        [JsonProperty("MinimumSize", NullValueHandling = NullValueHandling.Ignore)]
        public int MinimumSize { get; set; }


        /// <summary>
        /// MaximumSize
        /// </summary>
        [JsonProperty("MaximumSize", NullValueHandling = NullValueHandling.Ignore)]
        public int MaximumSize { get; set; }

    }


    /// <summary>
    /// MessageRuleActions
    /// </summary>
    public partial class MessageRuleActions
    {

        /// <summary>
        /// MoveToFolder
        /// </summary>
        [JsonProperty("MoveToFolder", NullValueHandling = NullValueHandling.Ignore)]
        public string MoveToFolder { get; set; }


        /// <summary>
        /// CopyToFolder
        /// </summary>
        [JsonProperty("CopyToFolder", NullValueHandling = NullValueHandling.Ignore)]
        public string CopyToFolder { get; set; }


        /// <summary>
        /// Delete
        /// </summary>
        [JsonProperty("Delete", NullValueHandling = NullValueHandling.Ignore)]
        public bool Delete { get; set; }


        /// <summary>
        /// PermanentDelete
        /// </summary>
        [JsonProperty("PermanentDelete", NullValueHandling = NullValueHandling.Ignore)]
        public bool PermanentDelete { get; set; }


        /// <summary>
        /// MarkAsRead
        /// </summary>
        [JsonProperty("MarkAsRead", NullValueHandling = NullValueHandling.Ignore)]
        public bool MarkAsRead { get; set; }


        /// <summary>
        /// MarkImportance
        /// </summary>
        [JsonProperty("MarkImportance", NullValueHandling = NullValueHandling.Ignore)]
        public Importance MarkImportance { get; set; }


        /// <summary>
        /// ForwardTo
        /// </summary>
        [JsonProperty("ForwardTo", NullValueHandling = NullValueHandling.Ignore)]
        public IList<Recipient> ForwardTo { get; set; }


        /// <summary>
        /// ForwardAsAttachmentTo
        /// </summary>
        [JsonProperty("ForwardAsAttachmentTo", NullValueHandling = NullValueHandling.Ignore)]
        public IList<Recipient> ForwardAsAttachmentTo { get; set; }


        /// <summary>
        /// RedirectTo
        /// </summary>
        [JsonProperty("RedirectTo", NullValueHandling = NullValueHandling.Ignore)]
        public IList<Recipient> RedirectTo { get; set; }


        /// <summary>
        /// AssignCategories
        /// </summary>
        [JsonProperty("AssignCategories", NullValueHandling = NullValueHandling.Ignore)]
        public IList<String> AssignCategories { get; set; }


        /// <summary>
        /// StopProcessingRules
        /// </summary>
        [JsonProperty("StopProcessingRules", NullValueHandling = NullValueHandling.Ignore)]
        public bool StopProcessingRules { get; set; }

    }


    /// <summary>
    /// DayOfWeek
    /// </summary>
    public enum DayOfWeek
    {

        /// <summary>
        /// Sunday
        /// </summary>
        Sunday = 0,

        /// <summary>
        /// Monday
        /// </summary>
        Monday = 1,

        /// <summary>
        /// Tuesday
        /// </summary>
        Tuesday = 2,

        /// <summary>
        /// Wednesday
        /// </summary>
        Wednesday = 3,

        /// <summary>
        /// Thursday
        /// </summary>
        Thursday = 4,

        /// <summary>
        /// Friday
        /// </summary>
        Friday = 5,

        /// <summary>
        /// Saturday
        /// </summary>
        Saturday = 6,
    }


    /// <summary>
    /// AutomaticRepliesStatus
    /// </summary>
    public enum AutomaticRepliesStatus
    {

        /// <summary>
        /// Disabled
        /// </summary>
        Disabled = 0,

        /// <summary>
        /// AlwaysEnabled
        /// </summary>
        AlwaysEnabled = 1,

        /// <summary>
        /// Scheduled
        /// </summary>
        Scheduled = 2,
    }


    /// <summary>
    /// ExternalAudienceScope
    /// </summary>
    public enum ExternalAudienceScope
    {

        /// <summary>
        /// None
        /// </summary>
        None = 0,

        /// <summary>
        /// ContactsOnly
        /// </summary>
        ContactsOnly = 1,

        /// <summary>
        /// All
        /// </summary>
        All = 2,
    }


    /// <summary>
    /// PhysicalAddressType
    /// </summary>
    public enum PhysicalAddressType
    {

        /// <summary>
        /// Unknown
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// Home
        /// </summary>
        Home = 1,

        /// <summary>
        /// Business
        /// </summary>
        Business = 2,

        /// <summary>
        /// Other
        /// </summary>
        Other = 3,
    }


    /// <summary>
    /// LocationType
    /// </summary>
    public enum LocationType
    {

        /// <summary>
        /// Default
        /// </summary>
        Default = 0,

        /// <summary>
        /// ConferenceRoom
        /// </summary>
        ConferenceRoom = 1,

        /// <summary>
        /// HomeAddress
        /// </summary>
        HomeAddress = 2,

        /// <summary>
        /// BusinessAddress
        /// </summary>
        BusinessAddress = 3,

        /// <summary>
        /// GeoCoordinates
        /// </summary>
        GeoCoordinates = 4,

        /// <summary>
        /// StreetAddress
        /// </summary>
        StreetAddress = 5,

        /// <summary>
        /// Hotel
        /// </summary>
        Hotel = 6,

        /// <summary>
        /// Restaurant
        /// </summary>
        Restaurant = 7,

        /// <summary>
        /// LocalBusiness
        /// </summary>
        LocalBusiness = 8,

        /// <summary>
        /// PostalAddress
        /// </summary>
        PostalAddress = 9,
    }


    /// <summary>
    /// LocationUniqueIdType
    /// </summary>
    public enum LocationUniqueIdType
    {

        /// <summary>
        /// Unknown
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// LocationStore
        /// </summary>
        LocationStore = 1,

        /// <summary>
        /// Directory
        /// </summary>
        Directory = 2,

        /// <summary>
        /// Private
        /// </summary>
        Private = 3,

        /// <summary>
        /// Bing
        /// </summary>
        Bing = 4,
    }


    /// <summary>
    /// MailTipsType
    /// </summary>
    [Flags]
    public enum MailTipsType
    {

        /// <summary>
        /// AutomaticReplies
        /// </summary>
        AutomaticReplies = 1,

        /// <summary>
        /// MailboxFullStatus
        /// </summary>
        MailboxFullStatus = 2,

        /// <summary>
        /// CustomMailTip
        /// </summary>
        CustomMailTip = 4,

        /// <summary>
        /// ExternalMemberCount
        /// </summary>
        ExternalMemberCount = 8,

        /// <summary>
        /// TotalMemberCount
        /// </summary>
        TotalMemberCount = 16,

        /// <summary>
        /// MaxMessageSize
        /// </summary>
        MaxMessageSize = 32,

        /// <summary>
        /// DeliveryRestriction
        /// </summary>
        DeliveryRestriction = 64,

        /// <summary>
        /// ModerationStatus
        /// </summary>
        ModerationStatus = 128,

        /// <summary>
        /// RecipientScope
        /// </summary>
        RecipientScope = 256,

        /// <summary>
        /// RecipientSuggestions
        /// </summary>
        RecipientSuggestions = 512,
    }


    /// <summary>
    /// RecipientScopeType
    /// </summary>
    [Flags]
    public enum RecipientScopeType
    {

        /// <summary>
        /// None
        /// </summary>
        None = 0,

        /// <summary>
        /// Internal
        /// </summary>
        Internal = 1,

        /// <summary>
        /// External
        /// </summary>
        External = 2,

        /// <summary>
        /// ExternalPartner
        /// </summary>
        ExternalPartner = 4,

        /// <summary>
        /// ExternalNonPartner
        /// </summary>
        ExternalNonPartner = 8,
    }


    /// <summary>
    /// TimeZoneStandard
    /// </summary>
    public enum TimeZoneStandard
    {

        /// <summary>
        /// Windows
        /// </summary>
        Windows = 0,

        /// <summary>
        /// Iana
        /// </summary>
        Iana = 1,
    }


    /// <summary>
    /// AttendeeType
    /// </summary>
    public enum AttendeeType
    {

        /// <summary>
        /// Required
        /// </summary>
        Required = 0,

        /// <summary>
        /// Optional
        /// </summary>
        Optional = 1,

        /// <summary>
        /// Resource
        /// </summary>
        Resource = 2,
    }


    /// <summary>
    /// ActivityDomain
    /// </summary>
    public enum ActivityDomain
    {

        /// <summary>
        /// Unknown
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// Work
        /// </summary>
        Work = 1,

        /// <summary>
        /// Personal
        /// </summary>
        Personal = 2,

        /// <summary>
        /// Unrestricted
        /// </summary>
        Unrestricted = 3,
    }


    /// <summary>
    /// FreeBusyStatus
    /// </summary>
    public enum FreeBusyStatus
    {

        /// <summary>
        /// Free
        /// </summary>
        Free = 0,

        /// <summary>
        /// Tentative
        /// </summary>
        Tentative = 1,

        /// <summary>
        /// Busy
        /// </summary>
        Busy = 2,

        /// <summary>
        /// Oof
        /// </summary>
        Oof = 3,

        /// <summary>
        /// WorkingElsewhere
        /// </summary>
        WorkingElsewhere = 4,

        /// <summary>
        /// Unknown
        /// </summary>
        Unknown = -1,
    }


    /// <summary>
    /// GroupAccessType
    /// </summary>
    public enum GroupAccessType
    {

        /// <summary>
        /// None
        /// </summary>
        None = 0,

        /// <summary>
        /// Private
        /// </summary>
        Private = 1,

        /// <summary>
        /// Secret
        /// </summary>
        Secret = 2,

        /// <summary>
        /// Public
        /// </summary>
        Public = 3,
    }


    /// <summary>
    /// ChangeType
    /// </summary>
    [Flags]
    public enum ChangeType
    {

        /// <summary>
        /// Created
        /// </summary>
        Created = 1,

        /// <summary>
        /// Updated
        /// </summary>
        Updated = 2,

        /// <summary>
        /// Deleted
        /// </summary>
        Deleted = 4,

        /// <summary>
        /// Acknowledgment
        /// </summary>
        Acknowledgment = 8,

        /// <summary>
        /// Missed
        /// </summary>
        Missed = 16,

        /// <summary>
        /// SubscriptionRemoved
        /// </summary>
        SubscriptionRemoved = 32,
    }


    /// <summary>
    /// BodyType
    /// </summary>
    public enum BodyType
    {

        /// <summary>
        /// Text
        /// </summary>
        Text = 0,

        /// <summary>
        /// HTML
        /// </summary>
        HTML = 1,
    }


    /// <summary>
    /// Importance
    /// </summary>
    public enum Importance
    {

        /// <summary>
        /// Low
        /// </summary>
        Low = 0,

        /// <summary>
        /// Normal
        /// </summary>
        Normal = 1,

        /// <summary>
        /// High
        /// </summary>
        High = 2,
    }


    /// <summary>
    /// RecurrencePatternType
    /// </summary>
    public enum RecurrencePatternType
    {

        /// <summary>
        /// Daily
        /// </summary>
        Daily = 0,

        /// <summary>
        /// Weekly
        /// </summary>
        Weekly = 1,

        /// <summary>
        /// AbsoluteMonthly
        /// </summary>
        AbsoluteMonthly = 2,

        /// <summary>
        /// RelativeMonthly
        /// </summary>
        RelativeMonthly = 3,

        /// <summary>
        /// AbsoluteYearly
        /// </summary>
        AbsoluteYearly = 4,

        /// <summary>
        /// RelativeYearly
        /// </summary>
        RelativeYearly = 5,
    }


    /// <summary>
    /// WeekIndex
    /// </summary>
    public enum WeekIndex
    {

        /// <summary>
        /// First
        /// </summary>
        First = 0,

        /// <summary>
        /// Second
        /// </summary>
        Second = 1,

        /// <summary>
        /// Third
        /// </summary>
        Third = 2,

        /// <summary>
        /// Fourth
        /// </summary>
        Fourth = 3,

        /// <summary>
        /// Last
        /// </summary>
        Last = 4,
    }


    /// <summary>
    /// RecurrenceRangeType
    /// </summary>
    public enum RecurrenceRangeType
    {

        /// <summary>
        /// EndDate
        /// </summary>
        EndDate = 0,

        /// <summary>
        /// NoEnd
        /// </summary>
        NoEnd = 1,

        /// <summary>
        /// Numbered
        /// </summary>
        Numbered = 2,
    }


    /// <summary>
    /// Sensitivity
    /// </summary>
    public enum Sensitivity
    {

        /// <summary>
        /// Normal
        /// </summary>
        Normal = 0,

        /// <summary>
        /// Personal
        /// </summary>
        Personal = 1,

        /// <summary>
        /// Private
        /// </summary>
        Private = 2,

        /// <summary>
        /// Confidential
        /// </summary>
        Confidential = 3,
    }


    /// <summary>
    /// TaskStatus
    /// </summary>
    public enum TaskStatus
    {

        /// <summary>
        /// NotStarted
        /// </summary>
        NotStarted = 0,

        /// <summary>
        /// InProgress
        /// </summary>
        InProgress = 1,

        /// <summary>
        /// Completed
        /// </summary>
        Completed = 2,

        /// <summary>
        /// WaitingOnOthers
        /// </summary>
        WaitingOnOthers = 3,

        /// <summary>
        /// Deferred
        /// </summary>
        Deferred = 4,
    }


    /// <summary>
    /// InferenceClassificationType
    /// </summary>
    public enum InferenceClassificationType
    {

        /// <summary>
        /// Focused
        /// </summary>
        Focused = 0,

        /// <summary>
        /// Other
        /// </summary>
        Other = 1,
    }


    /// <summary>
    /// FollowupFlagStatus
    /// </summary>
    public enum FollowupFlagStatus
    {

        /// <summary>
        /// NotFlagged
        /// </summary>
        NotFlagged = 0,

        /// <summary>
        /// Complete
        /// </summary>
        Complete = 1,

        /// <summary>
        /// Flagged
        /// </summary>
        Flagged = 2,
    }


    /// <summary>
    /// MeetingMessageType
    /// </summary>
    public enum MeetingMessageType
    {

        /// <summary>
        /// None
        /// </summary>
        None = 0,

        /// <summary>
        /// MeetingRequest
        /// </summary>
        MeetingRequest = 1,

        /// <summary>
        /// MeetingCancelled
        /// </summary>
        MeetingCancelled = 2,

        /// <summary>
        /// MeetingAccepted
        /// </summary>
        MeetingAccepted = 3,

        /// <summary>
        /// MeetingTenativelyAccepted
        /// </summary>
        MeetingTenativelyAccepted = 4,

        /// <summary>
        /// MeetingDeclined
        /// </summary>
        MeetingDeclined = 5,
    }


    /// <summary>
    /// CalendarColor
    /// </summary>
    public enum CalendarColor
    {

        /// <summary>
        /// LightBlue
        /// </summary>
        LightBlue = 0,

        /// <summary>
        /// LightGreen
        /// </summary>
        LightGreen = 1,

        /// <summary>
        /// LightOrange
        /// </summary>
        LightOrange = 2,

        /// <summary>
        /// LightGray
        /// </summary>
        LightGray = 3,

        /// <summary>
        /// LightYellow
        /// </summary>
        LightYellow = 4,

        /// <summary>
        /// LightTeal
        /// </summary>
        LightTeal = 5,

        /// <summary>
        /// LightPink
        /// </summary>
        LightPink = 6,

        /// <summary>
        /// LightBrown
        /// </summary>
        LightBrown = 7,

        /// <summary>
        /// LightRed
        /// </summary>
        LightRed = 8,

        /// <summary>
        /// MaxColor
        /// </summary>
        MaxColor = 9,

        /// <summary>
        /// Auto
        /// </summary>
        Auto = -1,
    }


    /// <summary>
    /// ResponseType
    /// </summary>
    public enum ResponseType
    {

        /// <summary>
        /// None
        /// </summary>
        None = 0,

        /// <summary>
        /// Organizer
        /// </summary>
        Organizer = 1,

        /// <summary>
        /// TentativelyAccepted
        /// </summary>
        TentativelyAccepted = 2,

        /// <summary>
        /// Accepted
        /// </summary>
        Accepted = 3,

        /// <summary>
        /// Declined
        /// </summary>
        Declined = 4,

        /// <summary>
        /// NotResponded
        /// </summary>
        NotResponded = 5,
    }


    /// <summary>
    /// EventType
    /// </summary>
    public enum EventType
    {

        /// <summary>
        /// SingleInstance
        /// </summary>
        SingleInstance = 0,

        /// <summary>
        /// Occurrence
        /// </summary>
        Occurrence = 1,

        /// <summary>
        /// Exception
        /// </summary>
        Exception = 2,

        /// <summary>
        /// SeriesMaster
        /// </summary>
        SeriesMaster = 3,
    }


    /// <summary>
    /// SelectionLikelihoodInfo
    /// </summary>
    public enum SelectionLikelihoodInfo
    {

        /// <summary>
        /// NotSpecified
        /// </summary>
        NotSpecified = 0,

        /// <summary>
        /// High
        /// </summary>
        High = 1,
    }


    /// <summary>
    /// PhoneType
    /// </summary>
    public enum PhoneType
    {

        /// <summary>
        /// Home
        /// </summary>
        Home = 0,

        /// <summary>
        /// Business
        /// </summary>
        Business = 1,

        /// <summary>
        /// Mobile
        /// </summary>
        Mobile = 2,

        /// <summary>
        /// Other
        /// </summary>
        Other = 3,

        /// <summary>
        /// Assistant
        /// </summary>
        Assistant = 4,

        /// <summary>
        /// HomeFax
        /// </summary>
        HomeFax = 5,

        /// <summary>
        /// BusinessFax
        /// </summary>
        BusinessFax = 6,

        /// <summary>
        /// OtherFax
        /// </summary>
        OtherFax = 7,

        /// <summary>
        /// Pager
        /// </summary>
        Pager = 8,

        /// <summary>
        /// Radio
        /// </summary>
        Radio = 9,
    }


    /// <summary>
    /// WebsiteType
    /// </summary>
    public enum WebsiteType
    {

        /// <summary>
        /// Other
        /// </summary>
        Other = 0,

        /// <summary>
        /// Home
        /// </summary>
        Home = 1,

        /// <summary>
        /// Work
        /// </summary>
        Work = 2,

        /// <summary>
        /// Blog
        /// </summary>
        Blog = 3,

        /// <summary>
        /// Profile
        /// </summary>
        Profile = 4,
    }


    /// <summary>
    /// CategoryColor
    /// </summary>
    public enum CategoryColor
    {

        /// <summary>
        /// Preset0
        /// </summary>
        Preset0 = 0,

        /// <summary>
        /// Preset1
        /// </summary>
        Preset1 = 1,

        /// <summary>
        /// Preset2
        /// </summary>
        Preset2 = 2,

        /// <summary>
        /// Preset3
        /// </summary>
        Preset3 = 3,

        /// <summary>
        /// Preset4
        /// </summary>
        Preset4 = 4,

        /// <summary>
        /// Preset5
        /// </summary>
        Preset5 = 5,

        /// <summary>
        /// Preset6
        /// </summary>
        Preset6 = 6,

        /// <summary>
        /// Preset7
        /// </summary>
        Preset7 = 7,

        /// <summary>
        /// Preset8
        /// </summary>
        Preset8 = 8,

        /// <summary>
        /// Preset9
        /// </summary>
        Preset9 = 9,

        /// <summary>
        /// Preset10
        /// </summary>
        Preset10 = 10,

        /// <summary>
        /// Preset11
        /// </summary>
        Preset11 = 11,

        /// <summary>
        /// Preset12
        /// </summary>
        Preset12 = 12,

        /// <summary>
        /// Preset13
        /// </summary>
        Preset13 = 13,

        /// <summary>
        /// Preset14
        /// </summary>
        Preset14 = 14,

        /// <summary>
        /// Preset15
        /// </summary>
        Preset15 = 15,

        /// <summary>
        /// Preset16
        /// </summary>
        Preset16 = 16,

        /// <summary>
        /// Preset17
        /// </summary>
        Preset17 = 17,

        /// <summary>
        /// Preset18
        /// </summary>
        Preset18 = 18,

        /// <summary>
        /// Preset19
        /// </summary>
        Preset19 = 19,

        /// <summary>
        /// Preset20
        /// </summary>
        Preset20 = 20,

        /// <summary>
        /// Preset21
        /// </summary>
        Preset21 = 21,

        /// <summary>
        /// Preset22
        /// </summary>
        Preset22 = 22,

        /// <summary>
        /// Preset23
        /// </summary>
        Preset23 = 23,

        /// <summary>
        /// Preset24
        /// </summary>
        Preset24 = 24,

        /// <summary>
        /// None
        /// </summary>
        None = -1,
    }


    /// <summary>
    /// ReferenceAttachmentProvider
    /// </summary>
    public enum ReferenceAttachmentProvider
    {

        /// <summary>
        /// Other
        /// </summary>
        Other = 0,

        /// <summary>
        /// OneDriveBusiness
        /// </summary>
        OneDriveBusiness = 1,

        /// <summary>
        /// OneDriveConsumer
        /// </summary>
        OneDriveConsumer = 2,

        /// <summary>
        /// Dropbox
        /// </summary>
        Dropbox = 3,
    }


    /// <summary>
    /// ReferenceAttachmentPermission
    /// </summary>
    public enum ReferenceAttachmentPermission
    {

        /// <summary>
        /// Other
        /// </summary>
        Other = 0,

        /// <summary>
        /// View
        /// </summary>
        View = 1,

        /// <summary>
        /// Edit
        /// </summary>
        Edit = 2,

        /// <summary>
        /// AnonymousView
        /// </summary>
        AnonymousView = 3,

        /// <summary>
        /// AnonymousEdit
        /// </summary>
        AnonymousEdit = 4,

        /// <summary>
        /// OrganizationView
        /// </summary>
        OrganizationView = 5,

        /// <summary>
        /// OrganizationEdit
        /// </summary>
        OrganizationEdit = 6,
    }


    /// <summary>
    /// MessageActionFlag
    /// </summary>
    public enum MessageActionFlag
    {

        /// <summary>
        /// Any
        /// </summary>
        Any = 0,

        /// <summary>
        /// Call
        /// </summary>
        Call = 1,

        /// <summary>
        /// DoNotForward
        /// </summary>
        DoNotForward = 2,

        /// <summary>
        /// FollowUp
        /// </summary>
        FollowUp = 3,

        /// <summary>
        /// FYI
        /// </summary>
        FYI = 4,

        /// <summary>
        /// Forward
        /// </summary>
        Forward = 5,

        /// <summary>
        /// NoResponseNecessary
        /// </summary>
        NoResponseNecessary = 6,

        /// <summary>
        /// Read
        /// </summary>
        Read = 7,

        /// <summary>
        /// Reply
        /// </summary>
        Reply = 8,

        /// <summary>
        /// ReplyToAll
        /// </summary>
        ReplyToAll = 9,

        /// <summary>
        /// Review
        /// </summary>
        Review = 10,
    }

}
