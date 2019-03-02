namespace Microsoft.OutlookServices
{
    using System;
    using System.Collections.Generic;
    using Microsoft.RestServices.Exchange;

    /// <summary>
    /// EntityObjectSchema
    /// </summary>
    public class EntityObjectSchema : ObjectSchema
    {

        /// <summary>
        /// Id
        /// </summary>
        public static PropertyDefinition Id = new PropertyDefinition(nameof(Id), typeof(string));

    }


    /// <summary>
    /// DirectoryObjectObjectSchema
    /// </summary>
    public class DirectoryObjectObjectSchema : EntityObjectSchema
    {
    }


    /// <summary>
    /// UserObjectSchema
    /// </summary>
    public class UserObjectSchema : DirectoryObjectObjectSchema
    {

        /// <summary>
        /// EmailAddress
        /// </summary>
        public static PropertyDefinition EmailAddress = new PropertyDefinition(nameof(EmailAddress), typeof(string));


        /// <summary>
        /// DisplayName
        /// </summary>
        public static PropertyDefinition DisplayName = new PropertyDefinition(nameof(DisplayName), typeof(string));


        /// <summary>
        /// Alias
        /// </summary>
        public static PropertyDefinition Alias = new PropertyDefinition(nameof(Alias), typeof(string));


        /// <summary>
        /// MailboxGuid
        /// </summary>
        public static PropertyDefinition MailboxGuid = new PropertyDefinition(nameof(MailboxGuid), typeof(Guid));


        /// <summary>
        /// MailboxSettings
        /// </summary>
        public static PropertyDefinition MailboxSettings = new PropertyDefinition(nameof(MailboxSettings), typeof(MailboxSettings));


        /// <summary>
        /// Subscriptions
        /// </summary>
        public static PropertyDefinition Subscriptions = new PropertyDefinition(nameof(Subscriptions), typeof(IList<Subscription>));


        /// <summary>
        /// TaskGroups
        /// </summary>
        public static PropertyDefinition TaskGroups = new PropertyDefinition(nameof(TaskGroups), typeof(IList<TaskGroup>));


        /// <summary>
        /// TaskFolders
        /// </summary>
        public static PropertyDefinition TaskFolders = new PropertyDefinition(nameof(TaskFolders), typeof(IList<TaskFolder>));


        /// <summary>
        /// Tasks
        /// </summary>
        public static PropertyDefinition Tasks = new PropertyDefinition(nameof(Tasks), typeof(IList<Task>));


        /// <summary>
        /// Messages
        /// </summary>
        public static PropertyDefinition Messages = new PropertyDefinition(nameof(Messages), typeof(IList<Message>));


        /// <summary>
        /// JoinedGroups
        /// </summary>
        public static PropertyDefinition JoinedGroups = new PropertyDefinition(nameof(JoinedGroups), typeof(IList<Group>));


        /// <summary>
        /// MailFolders
        /// </summary>
        public static PropertyDefinition MailFolders = new PropertyDefinition(nameof(MailFolders), typeof(IList<MailFolder>));


        /// <summary>
        /// Calendar
        /// </summary>
        public static PropertyDefinition Calendar = new PropertyDefinition(nameof(Calendar), typeof(Calendar));


        /// <summary>
        /// Calendars
        /// </summary>
        public static PropertyDefinition Calendars = new PropertyDefinition(nameof(Calendars), typeof(IList<Calendar>));


        /// <summary>
        /// CalendarGroups
        /// </summary>
        public static PropertyDefinition CalendarGroups = new PropertyDefinition(nameof(CalendarGroups), typeof(IList<CalendarGroup>));


        /// <summary>
        /// CalendarView
        /// </summary>
        public static PropertyDefinition CalendarView = new PropertyDefinition(nameof(CalendarView), typeof(IList<Event>));


        /// <summary>
        /// Events
        /// </summary>
        public static PropertyDefinition Events = new PropertyDefinition(nameof(Events), typeof(IList<Event>));


        /// <summary>
        /// People
        /// </summary>
        public static PropertyDefinition People = new PropertyDefinition(nameof(People), typeof(IList<Person>));


        /// <summary>
        /// Contacts
        /// </summary>
        public static PropertyDefinition Contacts = new PropertyDefinition(nameof(Contacts), typeof(IList<Contact>));


        /// <summary>
        /// ContactFolders
        /// </summary>
        public static PropertyDefinition ContactFolders = new PropertyDefinition(nameof(ContactFolders), typeof(IList<ContactFolder>));


        /// <summary>
        /// MasterCategories
        /// </summary>
        public static PropertyDefinition MasterCategories = new PropertyDefinition(nameof(MasterCategories), typeof(IList<OutlookCategory>));


        /// <summary>
        /// InferenceClassification
        /// </summary>
        public static PropertyDefinition InferenceClassification = new PropertyDefinition(nameof(InferenceClassification), typeof(InferenceClassification));


        /// <summary>
        /// Photo
        /// </summary>
        public static PropertyDefinition Photo = new PropertyDefinition(nameof(Photo), typeof(Photo));


        /// <summary>
        /// Photos
        /// </summary>
        public static PropertyDefinition Photos = new PropertyDefinition(nameof(Photos), typeof(IList<Photo>));

    }


    /// <summary>
    /// GroupObjectSchema
    /// </summary>
    public class GroupObjectSchema : DirectoryObjectObjectSchema
    {

        /// <summary>
        /// AccessType
        /// </summary>
        public static PropertyDefinition AccessType = new PropertyDefinition(nameof(AccessType), typeof(GroupAccessType));


        /// <summary>
        /// AllowExternalSenders
        /// </summary>
        public static PropertyDefinition AllowExternalSenders = new PropertyDefinition(nameof(AllowExternalSenders), typeof(bool));


        /// <summary>
        /// AutoSubscribeNewMembers
        /// </summary>
        public static PropertyDefinition AutoSubscribeNewMembers = new PropertyDefinition(nameof(AutoSubscribeNewMembers), typeof(bool));


        /// <summary>
        /// Description
        /// </summary>
        public static PropertyDefinition Description = new PropertyDefinition(nameof(Description), typeof(string));


        /// <summary>
        /// DisplayName
        /// </summary>
        public static PropertyDefinition DisplayName = new PropertyDefinition(nameof(DisplayName), typeof(string));


        /// <summary>
        /// EmailAddress
        /// </summary>
        public static PropertyDefinition EmailAddress = new PropertyDefinition(nameof(EmailAddress), typeof(string));


        /// <summary>
        /// Alias
        /// </summary>
        public static PropertyDefinition Alias = new PropertyDefinition(nameof(Alias), typeof(string));


        /// <summary>
        /// IsFavorite
        /// </summary>
        public static PropertyDefinition IsFavorite = new PropertyDefinition(nameof(IsFavorite), typeof(bool));


        /// <summary>
        /// IsMember
        /// </summary>
        public static PropertyDefinition IsMember = new PropertyDefinition(nameof(IsMember), typeof(bool));


        /// <summary>
        /// IsSubscribedByMail
        /// </summary>
        public static PropertyDefinition IsSubscribedByMail = new PropertyDefinition(nameof(IsSubscribedByMail), typeof(bool));


        /// <summary>
        /// LastVisitedDateTime
        /// </summary>
        public static PropertyDefinition LastVisitedDateTime = new PropertyDefinition(nameof(LastVisitedDateTime), typeof(DateTimeOffset));


        /// <summary>
        /// UnseenCount
        /// </summary>
        public static PropertyDefinition UnseenCount = new PropertyDefinition(nameof(UnseenCount), typeof(int));


        /// <summary>
        /// Threads
        /// </summary>
        public static PropertyDefinition Threads = new PropertyDefinition(nameof(Threads), typeof(IList<ConversationThread>));


        /// <summary>
        /// Calendar
        /// </summary>
        public static PropertyDefinition Calendar = new PropertyDefinition(nameof(Calendar), typeof(Calendar));


        /// <summary>
        /// CalendarView
        /// </summary>
        public static PropertyDefinition CalendarView = new PropertyDefinition(nameof(CalendarView), typeof(IList<Event>));


        /// <summary>
        /// Events
        /// </summary>
        public static PropertyDefinition Events = new PropertyDefinition(nameof(Events), typeof(IList<Event>));


        /// <summary>
        /// Conversations
        /// </summary>
        public static PropertyDefinition Conversations = new PropertyDefinition(nameof(Conversations), typeof(IList<Conversation>));


        /// <summary>
        /// Subscriptions
        /// </summary>
        public static PropertyDefinition Subscriptions = new PropertyDefinition(nameof(Subscriptions), typeof(IList<Subscription>));


        /// <summary>
        /// Photo
        /// </summary>
        public static PropertyDefinition Photo = new PropertyDefinition(nameof(Photo), typeof(Photo));


        /// <summary>
        /// Photos
        /// </summary>
        public static PropertyDefinition Photos = new PropertyDefinition(nameof(Photos), typeof(IList<Photo>));


        /// <summary>
        /// AcceptedSenders
        /// </summary>
        public static PropertyDefinition AcceptedSenders = new PropertyDefinition(nameof(AcceptedSenders), typeof(IList<DirectoryObject>));


        /// <summary>
        /// RejectedSenders
        /// </summary>
        public static PropertyDefinition RejectedSenders = new PropertyDefinition(nameof(RejectedSenders), typeof(IList<DirectoryObject>));

    }


    /// <summary>
    /// NotificationBaseObjectSchema
    /// </summary>
    public class NotificationBaseObjectSchema : EntityObjectSchema
    {
    }


    /// <summary>
    /// NotificationObjectSchema
    /// </summary>
    public class NotificationObjectSchema : NotificationBaseObjectSchema
    {

        /// <summary>
        /// SubscriptionId
        /// </summary>
        public static PropertyDefinition SubscriptionId = new PropertyDefinition(nameof(SubscriptionId), typeof(string));


        /// <summary>
        /// SubscriptionExpirationDateTime
        /// </summary>
        public static PropertyDefinition SubscriptionExpirationDateTime = new PropertyDefinition(nameof(SubscriptionExpirationDateTime), typeof(DateTimeOffset));


        /// <summary>
        /// SequenceNumber
        /// </summary>
        public static PropertyDefinition SequenceNumber = new PropertyDefinition(nameof(SequenceNumber), typeof(int));


        /// <summary>
        /// ChangeType
        /// </summary>
        public static PropertyDefinition ChangeType = new PropertyDefinition(nameof(ChangeType), typeof(ChangeType));


        /// <summary>
        /// Resource
        /// </summary>
        public static PropertyDefinition Resource = new PropertyDefinition(nameof(Resource), typeof(string));


        /// <summary>
        /// ResourceData
        /// </summary>
        public static PropertyDefinition ResourceData = new PropertyDefinition(nameof(ResourceData), typeof(Entity));

    }


    /// <summary>
    /// SubscriptionObjectSchema
    /// </summary>
    public class SubscriptionObjectSchema : EntityObjectSchema
    {

        /// <summary>
        /// Resource
        /// </summary>
        public static PropertyDefinition Resource = new PropertyDefinition(nameof(Resource), typeof(string));


        /// <summary>
        /// ChangeType
        /// </summary>
        public static PropertyDefinition ChangeType = new PropertyDefinition(nameof(ChangeType), typeof(ChangeType));

    }


    /// <summary>
    /// PushSubscriptionObjectSchema
    /// </summary>
    public class PushSubscriptionObjectSchema : SubscriptionObjectSchema
    {

        /// <summary>
        /// NotificationURL
        /// </summary>
        public static PropertyDefinition NotificationURL = new PropertyDefinition(nameof(NotificationURL), typeof(string));


        /// <summary>
        /// SubscriptionExpirationDateTime
        /// </summary>
        public static PropertyDefinition SubscriptionExpirationDateTime = new PropertyDefinition(nameof(SubscriptionExpirationDateTime), typeof(DateTimeOffset));


        /// <summary>
        /// ClientState
        /// </summary>
        public static PropertyDefinition ClientState = new PropertyDefinition(nameof(ClientState), typeof(string));

    }


    /// <summary>
    /// TaskGroupObjectSchema
    /// </summary>
    public class TaskGroupObjectSchema : EntityObjectSchema
    {

        /// <summary>
        /// ChangeKey
        /// </summary>
        public static PropertyDefinition ChangeKey = new PropertyDefinition(nameof(ChangeKey), typeof(string));


        /// <summary>
        /// IsDefaultGroup
        /// </summary>
        public static PropertyDefinition IsDefaultGroup = new PropertyDefinition(nameof(IsDefaultGroup), typeof(bool));


        /// <summary>
        /// Name
        /// </summary>
        public static PropertyDefinition Name = new PropertyDefinition(nameof(Name), typeof(string));


        /// <summary>
        /// GroupKey
        /// </summary>
        public static PropertyDefinition GroupKey = new PropertyDefinition(nameof(GroupKey), typeof(Guid));


        /// <summary>
        /// TaskFolders
        /// </summary>
        public static PropertyDefinition TaskFolders = new PropertyDefinition(nameof(TaskFolders), typeof(IList<TaskFolder>));

    }


    /// <summary>
    /// TaskFolderObjectSchema
    /// </summary>
    public class TaskFolderObjectSchema : EntityObjectSchema
    {

        /// <summary>
        /// ChangeKey
        /// </summary>
        public static PropertyDefinition ChangeKey = new PropertyDefinition(nameof(ChangeKey), typeof(string));


        /// <summary>
        /// Name
        /// </summary>
        public static PropertyDefinition Name = new PropertyDefinition(nameof(Name), typeof(string));


        /// <summary>
        /// IsDefaultFolder
        /// </summary>
        public static PropertyDefinition IsDefaultFolder = new PropertyDefinition(nameof(IsDefaultFolder), typeof(bool));


        /// <summary>
        /// ParentGroupKey
        /// </summary>
        public static PropertyDefinition ParentGroupKey = new PropertyDefinition(nameof(ParentGroupKey), typeof(Guid));


        /// <summary>
        /// Tasks
        /// </summary>
        public static PropertyDefinition Tasks = new PropertyDefinition(nameof(Tasks), typeof(IList<Task>));


        /// <summary>
        /// SingleValueExtendedProperties
        /// </summary>
        public static PropertyDefinition SingleValueExtendedProperties = new PropertyDefinition(nameof(SingleValueExtendedProperties), typeof(IList<SingleValueLegacyExtendedProperty>));


        /// <summary>
        /// MultiValueExtendedProperties
        /// </summary>
        public static PropertyDefinition MultiValueExtendedProperties = new PropertyDefinition(nameof(MultiValueExtendedProperties), typeof(IList<MultiValueLegacyExtendedProperty>));

    }


    /// <summary>
    /// SingleValueLegacyExtendedPropertyObjectSchema
    /// </summary>
    public class SingleValueLegacyExtendedPropertyObjectSchema : ObjectSchema
    {

        /// <summary>
        /// Value
        /// </summary>
        public static PropertyDefinition Value = new PropertyDefinition(nameof(Value), typeof(string));


        /// <summary>
        /// PropertyId
        /// </summary>
        public static PropertyDefinition PropertyId = new PropertyDefinition(nameof(PropertyId), typeof(string));

    }


    /// <summary>
    /// MultiValueLegacyExtendedPropertyObjectSchema
    /// </summary>
    public class MultiValueLegacyExtendedPropertyObjectSchema : ObjectSchema
    {

        /// <summary>
        /// Value
        /// </summary>
        public static PropertyDefinition Value = new PropertyDefinition(nameof(Value), typeof(IList<String>));


        /// <summary>
        /// PropertyId
        /// </summary>
        public static PropertyDefinition PropertyId = new PropertyDefinition(nameof(PropertyId), typeof(string));

    }


    /// <summary>
    /// ItemObjectSchema
    /// </summary>
    public class ItemObjectSchema : EntityObjectSchema
    {

        /// <summary>
        /// CreatedDateTime
        /// </summary>
        public static PropertyDefinition CreatedDateTime = new PropertyDefinition(nameof(CreatedDateTime), typeof(DateTimeOffset));


        /// <summary>
        /// LastModifiedDateTime
        /// </summary>
        public static PropertyDefinition LastModifiedDateTime = new PropertyDefinition(nameof(LastModifiedDateTime), typeof(DateTimeOffset));


        /// <summary>
        /// ChangeKey
        /// </summary>
        public static PropertyDefinition ChangeKey = new PropertyDefinition(nameof(ChangeKey), typeof(string));


        /// <summary>
        /// Categories
        /// </summary>
        public static PropertyDefinition Categories = new PropertyDefinition(nameof(Categories), typeof(IList<String>));

    }


    /// <summary>
    /// TaskObjectSchema
    /// </summary>
    public class TaskObjectSchema : ItemObjectSchema
    {

        /// <summary>
        /// AssignedTo
        /// </summary>
        public static PropertyDefinition AssignedTo = new PropertyDefinition(nameof(AssignedTo), typeof(string));


        /// <summary>
        /// Body
        /// </summary>
        public static PropertyDefinition Body = new PropertyDefinition(nameof(Body), typeof(ItemBody));


        /// <summary>
        /// CompletedDateTime
        /// </summary>
        public static PropertyDefinition CompletedDateTime = new PropertyDefinition(nameof(CompletedDateTime), typeof(DateTimeTimeZone));


        /// <summary>
        /// DueDateTime
        /// </summary>
        public static PropertyDefinition DueDateTime = new PropertyDefinition(nameof(DueDateTime), typeof(DateTimeTimeZone));


        /// <summary>
        /// HasAttachments
        /// </summary>
        public static PropertyDefinition HasAttachments = new PropertyDefinition(nameof(HasAttachments), typeof(bool));


        /// <summary>
        /// Importance
        /// </summary>
        public static PropertyDefinition Importance = new PropertyDefinition(nameof(Importance), typeof(Importance));


        /// <summary>
        /// IsReminderOn
        /// </summary>
        public static PropertyDefinition IsReminderOn = new PropertyDefinition(nameof(IsReminderOn), typeof(bool));


        /// <summary>
        /// Owner
        /// </summary>
        public static PropertyDefinition Owner = new PropertyDefinition(nameof(Owner), typeof(string));


        /// <summary>
        /// ParentFolderId
        /// </summary>
        public static PropertyDefinition ParentFolderId = new PropertyDefinition(nameof(ParentFolderId), typeof(string));


        /// <summary>
        /// Recurrence
        /// </summary>
        public static PropertyDefinition Recurrence = new PropertyDefinition(nameof(Recurrence), typeof(PatternedRecurrence));


        /// <summary>
        /// ReminderDateTime
        /// </summary>
        public static PropertyDefinition ReminderDateTime = new PropertyDefinition(nameof(ReminderDateTime), typeof(DateTimeTimeZone));


        /// <summary>
        /// Sensitivity
        /// </summary>
        public static PropertyDefinition Sensitivity = new PropertyDefinition(nameof(Sensitivity), typeof(Sensitivity));


        /// <summary>
        /// StartDateTime
        /// </summary>
        public static PropertyDefinition StartDateTime = new PropertyDefinition(nameof(StartDateTime), typeof(DateTimeTimeZone));


        /// <summary>
        /// Status
        /// </summary>
        public static PropertyDefinition Status = new PropertyDefinition(nameof(Status), typeof(TaskStatus));


        /// <summary>
        /// Subject
        /// </summary>
        public static PropertyDefinition Subject = new PropertyDefinition(nameof(Subject), typeof(string));


        /// <summary>
        /// SingleValueExtendedProperties
        /// </summary>
        public static PropertyDefinition SingleValueExtendedProperties = new PropertyDefinition(nameof(SingleValueExtendedProperties), typeof(IList<SingleValueLegacyExtendedProperty>));


        /// <summary>
        /// MultiValueExtendedProperties
        /// </summary>
        public static PropertyDefinition MultiValueExtendedProperties = new PropertyDefinition(nameof(MultiValueExtendedProperties), typeof(IList<MultiValueLegacyExtendedProperty>));


        /// <summary>
        /// Attachments
        /// </summary>
        public static PropertyDefinition Attachments = new PropertyDefinition(nameof(Attachments), typeof(IList<Attachment>));

    }


    /// <summary>
    /// MessageObjectSchema
    /// </summary>
    public class MessageObjectSchema : ItemObjectSchema
    {

        /// <summary>
        /// ReceivedDateTime
        /// </summary>
        public static PropertyDefinition ReceivedDateTime = new PropertyDefinition(nameof(ReceivedDateTime), typeof(DateTimeOffset));


        /// <summary>
        /// SentDateTime
        /// </summary>
        public static PropertyDefinition SentDateTime = new PropertyDefinition(nameof(SentDateTime), typeof(DateTimeOffset));


        /// <summary>
        /// HasAttachments
        /// </summary>
        public static PropertyDefinition HasAttachments = new PropertyDefinition(nameof(HasAttachments), typeof(bool));


        /// <summary>
        /// InternetMessageId
        /// </summary>
        public static PropertyDefinition InternetMessageId = new PropertyDefinition(nameof(InternetMessageId), typeof(string));


        /// <summary>
        /// InternetMessageHeaders
        /// </summary>
        public static PropertyDefinition InternetMessageHeaders = new PropertyDefinition(nameof(InternetMessageHeaders), typeof(IList<InternetMessageHeader>));


        /// <summary>
        /// Subject
        /// </summary>
        public static PropertyDefinition Subject = new PropertyDefinition(nameof(Subject), typeof(string));


        /// <summary>
        /// Body
        /// </summary>
        public static PropertyDefinition Body = new PropertyDefinition(nameof(Body), typeof(ItemBody));


        /// <summary>
        /// BodyPreview
        /// </summary>
        public static PropertyDefinition BodyPreview = new PropertyDefinition(nameof(BodyPreview), typeof(string));


        /// <summary>
        /// Importance
        /// </summary>
        public static PropertyDefinition Importance = new PropertyDefinition(nameof(Importance), typeof(Importance));


        /// <summary>
        /// ParentFolderId
        /// </summary>
        public static PropertyDefinition ParentFolderId = new PropertyDefinition(nameof(ParentFolderId), typeof(string));


        /// <summary>
        /// Sender
        /// </summary>
        public static PropertyDefinition Sender = new PropertyDefinition(nameof(Sender), typeof(Recipient));


        /// <summary>
        /// From
        /// </summary>
        public static PropertyDefinition From = new PropertyDefinition(nameof(From), typeof(Recipient));


        /// <summary>
        /// ToRecipients
        /// </summary>
        public static PropertyDefinition ToRecipients = new PropertyDefinition(nameof(ToRecipients), typeof(IList<Recipient>));


        /// <summary>
        /// CcRecipients
        /// </summary>
        public static PropertyDefinition CcRecipients = new PropertyDefinition(nameof(CcRecipients), typeof(IList<Recipient>));


        /// <summary>
        /// BccRecipients
        /// </summary>
        public static PropertyDefinition BccRecipients = new PropertyDefinition(nameof(BccRecipients), typeof(IList<Recipient>));


        /// <summary>
        /// ReplyTo
        /// </summary>
        public static PropertyDefinition ReplyTo = new PropertyDefinition(nameof(ReplyTo), typeof(IList<Recipient>));


        /// <summary>
        /// ConversationId
        /// </summary>
        public static PropertyDefinition ConversationId = new PropertyDefinition(nameof(ConversationId), typeof(string));


        /// <summary>
        /// UniqueBody
        /// </summary>
        public static PropertyDefinition UniqueBody = new PropertyDefinition(nameof(UniqueBody), typeof(ItemBody));


        /// <summary>
        /// IsDeliveryReceiptRequested
        /// </summary>
        public static PropertyDefinition IsDeliveryReceiptRequested = new PropertyDefinition(nameof(IsDeliveryReceiptRequested), typeof(bool));


        /// <summary>
        /// IsReadReceiptRequested
        /// </summary>
        public static PropertyDefinition IsReadReceiptRequested = new PropertyDefinition(nameof(IsReadReceiptRequested), typeof(bool));


        /// <summary>
        /// IsRead
        /// </summary>
        public static PropertyDefinition IsRead = new PropertyDefinition(nameof(IsRead), typeof(bool));


        /// <summary>
        /// IsDraft
        /// </summary>
        public static PropertyDefinition IsDraft = new PropertyDefinition(nameof(IsDraft), typeof(bool));


        /// <summary>
        /// WebLink
        /// </summary>
        public static PropertyDefinition WebLink = new PropertyDefinition(nameof(WebLink), typeof(string));


        /// <summary>
        /// InferenceClassification
        /// </summary>
        public static PropertyDefinition InferenceClassification = new PropertyDefinition(nameof(InferenceClassification), typeof(InferenceClassificationType));


        /// <summary>
        /// Flag
        /// </summary>
        public static PropertyDefinition Flag = new PropertyDefinition(nameof(Flag), typeof(FollowupFlag));


        /// <summary>
        /// SingleValueExtendedProperties
        /// </summary>
        public static PropertyDefinition SingleValueExtendedProperties = new PropertyDefinition(nameof(SingleValueExtendedProperties), typeof(IList<SingleValueLegacyExtendedProperty>));


        /// <summary>
        /// MultiValueExtendedProperties
        /// </summary>
        public static PropertyDefinition MultiValueExtendedProperties = new PropertyDefinition(nameof(MultiValueExtendedProperties), typeof(IList<MultiValueLegacyExtendedProperty>));


        /// <summary>
        /// Attachments
        /// </summary>
        public static PropertyDefinition Attachments = new PropertyDefinition(nameof(Attachments), typeof(IList<Attachment>));


        /// <summary>
        /// Extensions
        /// </summary>
        public static PropertyDefinition Extensions = new PropertyDefinition(nameof(Extensions), typeof(IList<Extension>));

    }


    /// <summary>
    /// EventMessageObjectSchema
    /// </summary>
    public class EventMessageObjectSchema : MessageObjectSchema
    {

        /// <summary>
        /// MeetingMessageType
        /// </summary>
        public static PropertyDefinition MeetingMessageType = new PropertyDefinition(nameof(MeetingMessageType), typeof(MeetingMessageType));


        /// <summary>
        /// Event
        /// </summary>
        public static PropertyDefinition Event = new PropertyDefinition(nameof(Event), typeof(Event));

    }


    /// <summary>
    /// MailFolderObjectSchema
    /// </summary>
    public class MailFolderObjectSchema : EntityObjectSchema
    {

        /// <summary>
        /// DisplayName
        /// </summary>
        public static PropertyDefinition DisplayName = new PropertyDefinition(nameof(DisplayName), typeof(string));


        /// <summary>
        /// ParentFolderId
        /// </summary>
        public static PropertyDefinition ParentFolderId = new PropertyDefinition(nameof(ParentFolderId), typeof(string));


        /// <summary>
        /// ChildFolderCount
        /// </summary>
        public static PropertyDefinition ChildFolderCount = new PropertyDefinition(nameof(ChildFolderCount), typeof(int));


        /// <summary>
        /// UnreadItemCount
        /// </summary>
        public static PropertyDefinition UnreadItemCount = new PropertyDefinition(nameof(UnreadItemCount), typeof(int));


        /// <summary>
        /// TotalItemCount
        /// </summary>
        public static PropertyDefinition TotalItemCount = new PropertyDefinition(nameof(TotalItemCount), typeof(int));


        /// <summary>
        /// SingleValueExtendedProperties
        /// </summary>
        public static PropertyDefinition SingleValueExtendedProperties = new PropertyDefinition(nameof(SingleValueExtendedProperties), typeof(IList<SingleValueLegacyExtendedProperty>));


        /// <summary>
        /// MultiValueExtendedProperties
        /// </summary>
        public static PropertyDefinition MultiValueExtendedProperties = new PropertyDefinition(nameof(MultiValueExtendedProperties), typeof(IList<MultiValueLegacyExtendedProperty>));


        /// <summary>
        /// Messages
        /// </summary>
        public static PropertyDefinition Messages = new PropertyDefinition(nameof(Messages), typeof(IList<Message>));


        /// <summary>
        /// MessageRules
        /// </summary>
        public static PropertyDefinition MessageRules = new PropertyDefinition(nameof(MessageRules), typeof(IList<MessageRule>));


        /// <summary>
        /// ChildFolders
        /// </summary>
        public static PropertyDefinition ChildFolders = new PropertyDefinition(nameof(ChildFolders), typeof(IList<MailFolder>));

    }


    /// <summary>
    /// CalendarObjectSchema
    /// </summary>
    public class CalendarObjectSchema : EntityObjectSchema
    {

        /// <summary>
        /// Name
        /// </summary>
        public static PropertyDefinition Name = new PropertyDefinition(nameof(Name), typeof(string));


        /// <summary>
        /// Color
        /// </summary>
        public static PropertyDefinition Color = new PropertyDefinition(nameof(Color), typeof(CalendarColor));


        /// <summary>
        /// ChangeKey
        /// </summary>
        public static PropertyDefinition ChangeKey = new PropertyDefinition(nameof(ChangeKey), typeof(string));


        /// <summary>
        /// CanShare
        /// </summary>
        public static PropertyDefinition CanShare = new PropertyDefinition(nameof(CanShare), typeof(bool));


        /// <summary>
        /// CanViewPrivateItems
        /// </summary>
        public static PropertyDefinition CanViewPrivateItems = new PropertyDefinition(nameof(CanViewPrivateItems), typeof(bool));


        /// <summary>
        /// CanEdit
        /// </summary>
        public static PropertyDefinition CanEdit = new PropertyDefinition(nameof(CanEdit), typeof(bool));


        /// <summary>
        /// Owner
        /// </summary>
        public static PropertyDefinition Owner = new PropertyDefinition(nameof(Owner), typeof(EmailAddress));


        /// <summary>
        /// SingleValueExtendedProperties
        /// </summary>
        public static PropertyDefinition SingleValueExtendedProperties = new PropertyDefinition(nameof(SingleValueExtendedProperties), typeof(IList<SingleValueLegacyExtendedProperty>));


        /// <summary>
        /// MultiValueExtendedProperties
        /// </summary>
        public static PropertyDefinition MultiValueExtendedProperties = new PropertyDefinition(nameof(MultiValueExtendedProperties), typeof(IList<MultiValueLegacyExtendedProperty>));


        /// <summary>
        /// Events
        /// </summary>
        public static PropertyDefinition Events = new PropertyDefinition(nameof(Events), typeof(IList<Event>));


        /// <summary>
        /// CalendarView
        /// </summary>
        public static PropertyDefinition CalendarView = new PropertyDefinition(nameof(CalendarView), typeof(IList<Event>));

    }


    /// <summary>
    /// CalendarGroupObjectSchema
    /// </summary>
    public class CalendarGroupObjectSchema : EntityObjectSchema
    {

        /// <summary>
        /// Name
        /// </summary>
        public static PropertyDefinition Name = new PropertyDefinition(nameof(Name), typeof(string));


        /// <summary>
        /// ClassId
        /// </summary>
        public static PropertyDefinition ClassId = new PropertyDefinition(nameof(ClassId), typeof(Guid));


        /// <summary>
        /// ChangeKey
        /// </summary>
        public static PropertyDefinition ChangeKey = new PropertyDefinition(nameof(ChangeKey), typeof(string));


        /// <summary>
        /// Calendars
        /// </summary>
        public static PropertyDefinition Calendars = new PropertyDefinition(nameof(Calendars), typeof(IList<Calendar>));

    }


    /// <summary>
    /// EventObjectSchema
    /// </summary>
    public class EventObjectSchema : ItemObjectSchema
    {

        /// <summary>
        /// OriginalStartTimeZone
        /// </summary>
        public static PropertyDefinition OriginalStartTimeZone = new PropertyDefinition(nameof(OriginalStartTimeZone), typeof(string));


        /// <summary>
        /// OriginalEndTimeZone
        /// </summary>
        public static PropertyDefinition OriginalEndTimeZone = new PropertyDefinition(nameof(OriginalEndTimeZone), typeof(string));


        /// <summary>
        /// ResponseStatus
        /// </summary>
        public static PropertyDefinition ResponseStatus = new PropertyDefinition(nameof(ResponseStatus), typeof(ResponseStatus));


        /// <summary>
        /// ICalUId
        /// </summary>
        public static PropertyDefinition ICalUId = new PropertyDefinition(nameof(ICalUId), typeof(string));


        /// <summary>
        /// ReminderMinutesBeforeStart
        /// </summary>
        public static PropertyDefinition ReminderMinutesBeforeStart = new PropertyDefinition(nameof(ReminderMinutesBeforeStart), typeof(int));


        /// <summary>
        /// IsReminderOn
        /// </summary>
        public static PropertyDefinition IsReminderOn = new PropertyDefinition(nameof(IsReminderOn), typeof(bool));


        /// <summary>
        /// HasAttachments
        /// </summary>
        public static PropertyDefinition HasAttachments = new PropertyDefinition(nameof(HasAttachments), typeof(bool));


        /// <summary>
        /// Subject
        /// </summary>
        public static PropertyDefinition Subject = new PropertyDefinition(nameof(Subject), typeof(string));


        /// <summary>
        /// Body
        /// </summary>
        public static PropertyDefinition Body = new PropertyDefinition(nameof(Body), typeof(ItemBody));


        /// <summary>
        /// BodyPreview
        /// </summary>
        public static PropertyDefinition BodyPreview = new PropertyDefinition(nameof(BodyPreview), typeof(string));


        /// <summary>
        /// Importance
        /// </summary>
        public static PropertyDefinition Importance = new PropertyDefinition(nameof(Importance), typeof(Importance));


        /// <summary>
        /// Sensitivity
        /// </summary>
        public static PropertyDefinition Sensitivity = new PropertyDefinition(nameof(Sensitivity), typeof(Sensitivity));


        /// <summary>
        /// Start
        /// </summary>
        public static PropertyDefinition Start = new PropertyDefinition(nameof(Start), typeof(DateTimeTimeZone));


        /// <summary>
        /// OriginalStart
        /// </summary>
        public static PropertyDefinition OriginalStart = new PropertyDefinition(nameof(OriginalStart), typeof(DateTimeOffset));


        /// <summary>
        /// End
        /// </summary>
        public static PropertyDefinition End = new PropertyDefinition(nameof(End), typeof(DateTimeTimeZone));


        /// <summary>
        /// Location
        /// </summary>
        public static PropertyDefinition Location = new PropertyDefinition(nameof(Location), typeof(Location));


        /// <summary>
        /// Locations
        /// </summary>
        public static PropertyDefinition Locations = new PropertyDefinition(nameof(Locations), typeof(IList<Location>));


        /// <summary>
        /// IsAllDay
        /// </summary>
        public static PropertyDefinition IsAllDay = new PropertyDefinition(nameof(IsAllDay), typeof(bool));


        /// <summary>
        /// IsCancelled
        /// </summary>
        public static PropertyDefinition IsCancelled = new PropertyDefinition(nameof(IsCancelled), typeof(bool));


        /// <summary>
        /// IsOrganizer
        /// </summary>
        public static PropertyDefinition IsOrganizer = new PropertyDefinition(nameof(IsOrganizer), typeof(bool));


        /// <summary>
        /// Recurrence
        /// </summary>
        public static PropertyDefinition Recurrence = new PropertyDefinition(nameof(Recurrence), typeof(PatternedRecurrence));


        /// <summary>
        /// ResponseRequested
        /// </summary>
        public static PropertyDefinition ResponseRequested = new PropertyDefinition(nameof(ResponseRequested), typeof(bool));


        /// <summary>
        /// SeriesMasterId
        /// </summary>
        public static PropertyDefinition SeriesMasterId = new PropertyDefinition(nameof(SeriesMasterId), typeof(string));


        /// <summary>
        /// ShowAs
        /// </summary>
        public static PropertyDefinition ShowAs = new PropertyDefinition(nameof(ShowAs), typeof(FreeBusyStatus));


        /// <summary>
        /// Type
        /// </summary>
        public static PropertyDefinition Type = new PropertyDefinition(nameof(Type), typeof(EventType));


        /// <summary>
        /// Attendees
        /// </summary>
        public static PropertyDefinition Attendees = new PropertyDefinition(nameof(Attendees), typeof(IList<Attendee>));


        /// <summary>
        /// Organizer
        /// </summary>
        public static PropertyDefinition Organizer = new PropertyDefinition(nameof(Organizer), typeof(Recipient));


        /// <summary>
        /// WebLink
        /// </summary>
        public static PropertyDefinition WebLink = new PropertyDefinition(nameof(WebLink), typeof(string));


        /// <summary>
        /// OnlineMeetingUrl
        /// </summary>
        public static PropertyDefinition OnlineMeetingUrl = new PropertyDefinition(nameof(OnlineMeetingUrl), typeof(string));


        /// <summary>
        /// Attachments
        /// </summary>
        public static PropertyDefinition Attachments = new PropertyDefinition(nameof(Attachments), typeof(IList<Attachment>));


        /// <summary>
        /// SingleValueExtendedProperties
        /// </summary>
        public static PropertyDefinition SingleValueExtendedProperties = new PropertyDefinition(nameof(SingleValueExtendedProperties), typeof(IList<SingleValueLegacyExtendedProperty>));


        /// <summary>
        /// MultiValueExtendedProperties
        /// </summary>
        public static PropertyDefinition MultiValueExtendedProperties = new PropertyDefinition(nameof(MultiValueExtendedProperties), typeof(IList<MultiValueLegacyExtendedProperty>));


        /// <summary>
        /// Calendar
        /// </summary>
        public static PropertyDefinition Calendar = new PropertyDefinition(nameof(Calendar), typeof(Calendar));


        /// <summary>
        /// Instances
        /// </summary>
        public static PropertyDefinition Instances = new PropertyDefinition(nameof(Instances), typeof(IList<Event>));


        /// <summary>
        /// Extensions
        /// </summary>
        public static PropertyDefinition Extensions = new PropertyDefinition(nameof(Extensions), typeof(IList<Extension>));

    }


    /// <summary>
    /// PersonObjectSchema
    /// </summary>
    public class PersonObjectSchema : EntityObjectSchema
    {

        /// <summary>
        /// DisplayName
        /// </summary>
        public static PropertyDefinition DisplayName = new PropertyDefinition(nameof(DisplayName), typeof(string));


        /// <summary>
        /// GivenName
        /// </summary>
        public static PropertyDefinition GivenName = new PropertyDefinition(nameof(GivenName), typeof(string));


        /// <summary>
        /// Surname
        /// </summary>
        public static PropertyDefinition Surname = new PropertyDefinition(nameof(Surname), typeof(string));


        /// <summary>
        /// Birthday
        /// </summary>
        public static PropertyDefinition Birthday = new PropertyDefinition(nameof(Birthday), typeof(string));


        /// <summary>
        /// PersonNotes
        /// </summary>
        public static PropertyDefinition PersonNotes = new PropertyDefinition(nameof(PersonNotes), typeof(string));


        /// <summary>
        /// IsFavorite
        /// </summary>
        public static PropertyDefinition IsFavorite = new PropertyDefinition(nameof(IsFavorite), typeof(bool));


        /// <summary>
        /// ScoredEmailAddresses
        /// </summary>
        public static PropertyDefinition ScoredEmailAddresses = new PropertyDefinition(nameof(ScoredEmailAddresses), typeof(IList<ScoredEmailAddress>));


        /// <summary>
        /// Phones
        /// </summary>
        public static PropertyDefinition Phones = new PropertyDefinition(nameof(Phones), typeof(IList<Phone>));


        /// <summary>
        /// PostalAddresses
        /// </summary>
        public static PropertyDefinition PostalAddresses = new PropertyDefinition(nameof(PostalAddresses), typeof(IList<Location>));


        /// <summary>
        /// Websites
        /// </summary>
        public static PropertyDefinition Websites = new PropertyDefinition(nameof(Websites), typeof(IList<Website>));


        /// <summary>
        /// JobTitle
        /// </summary>
        public static PropertyDefinition JobTitle = new PropertyDefinition(nameof(JobTitle), typeof(string));


        /// <summary>
        /// CompanyName
        /// </summary>
        public static PropertyDefinition CompanyName = new PropertyDefinition(nameof(CompanyName), typeof(string));


        /// <summary>
        /// YomiCompany
        /// </summary>
        public static PropertyDefinition YomiCompany = new PropertyDefinition(nameof(YomiCompany), typeof(string));


        /// <summary>
        /// Department
        /// </summary>
        public static PropertyDefinition Department = new PropertyDefinition(nameof(Department), typeof(string));


        /// <summary>
        /// OfficeLocation
        /// </summary>
        public static PropertyDefinition OfficeLocation = new PropertyDefinition(nameof(OfficeLocation), typeof(string));


        /// <summary>
        /// Profession
        /// </summary>
        public static PropertyDefinition Profession = new PropertyDefinition(nameof(Profession), typeof(string));


        /// <summary>
        /// PersonType
        /// </summary>
        public static PropertyDefinition PersonType = new PropertyDefinition(nameof(PersonType), typeof(PersonType));


        /// <summary>
        /// UserPrincipalName
        /// </summary>
        public static PropertyDefinition UserPrincipalName = new PropertyDefinition(nameof(UserPrincipalName), typeof(string));


        /// <summary>
        /// IMAddress
        /// </summary>
        public static PropertyDefinition IMAddress = new PropertyDefinition(nameof(IMAddress), typeof(string));

    }


    /// <summary>
    /// ContactObjectSchema
    /// </summary>
    public class ContactObjectSchema : ItemObjectSchema
    {

        /// <summary>
        /// ParentFolderId
        /// </summary>
        public static PropertyDefinition ParentFolderId = new PropertyDefinition(nameof(ParentFolderId), typeof(string));


        /// <summary>
        /// Birthday
        /// </summary>
        public static PropertyDefinition Birthday = new PropertyDefinition(nameof(Birthday), typeof(DateTimeOffset));


        /// <summary>
        /// FileAs
        /// </summary>
        public static PropertyDefinition FileAs = new PropertyDefinition(nameof(FileAs), typeof(string));


        /// <summary>
        /// DisplayName
        /// </summary>
        public static PropertyDefinition DisplayName = new PropertyDefinition(nameof(DisplayName), typeof(string));


        /// <summary>
        /// GivenName
        /// </summary>
        public static PropertyDefinition GivenName = new PropertyDefinition(nameof(GivenName), typeof(string));


        /// <summary>
        /// Initials
        /// </summary>
        public static PropertyDefinition Initials = new PropertyDefinition(nameof(Initials), typeof(string));


        /// <summary>
        /// MiddleName
        /// </summary>
        public static PropertyDefinition MiddleName = new PropertyDefinition(nameof(MiddleName), typeof(string));


        /// <summary>
        /// NickName
        /// </summary>
        public static PropertyDefinition NickName = new PropertyDefinition(nameof(NickName), typeof(string));


        /// <summary>
        /// Surname
        /// </summary>
        public static PropertyDefinition Surname = new PropertyDefinition(nameof(Surname), typeof(string));


        /// <summary>
        /// Title
        /// </summary>
        public static PropertyDefinition Title = new PropertyDefinition(nameof(Title), typeof(string));


        /// <summary>
        /// YomiGivenName
        /// </summary>
        public static PropertyDefinition YomiGivenName = new PropertyDefinition(nameof(YomiGivenName), typeof(string));


        /// <summary>
        /// YomiSurname
        /// </summary>
        public static PropertyDefinition YomiSurname = new PropertyDefinition(nameof(YomiSurname), typeof(string));


        /// <summary>
        /// YomiCompanyName
        /// </summary>
        public static PropertyDefinition YomiCompanyName = new PropertyDefinition(nameof(YomiCompanyName), typeof(string));


        /// <summary>
        /// Generation
        /// </summary>
        public static PropertyDefinition Generation = new PropertyDefinition(nameof(Generation), typeof(string));


        /// <summary>
        /// EmailAddresses
        /// </summary>
        public static PropertyDefinition EmailAddresses = new PropertyDefinition(nameof(EmailAddresses), typeof(IList<EmailAddress>));


        /// <summary>
        /// ImAddresses
        /// </summary>
        public static PropertyDefinition ImAddresses = new PropertyDefinition(nameof(ImAddresses), typeof(IList<String>));


        /// <summary>
        /// JobTitle
        /// </summary>
        public static PropertyDefinition JobTitle = new PropertyDefinition(nameof(JobTitle), typeof(string));


        /// <summary>
        /// CompanyName
        /// </summary>
        public static PropertyDefinition CompanyName = new PropertyDefinition(nameof(CompanyName), typeof(string));


        /// <summary>
        /// Department
        /// </summary>
        public static PropertyDefinition Department = new PropertyDefinition(nameof(Department), typeof(string));


        /// <summary>
        /// OfficeLocation
        /// </summary>
        public static PropertyDefinition OfficeLocation = new PropertyDefinition(nameof(OfficeLocation), typeof(string));


        /// <summary>
        /// Profession
        /// </summary>
        public static PropertyDefinition Profession = new PropertyDefinition(nameof(Profession), typeof(string));


        /// <summary>
        /// BusinessHomePage
        /// </summary>
        public static PropertyDefinition BusinessHomePage = new PropertyDefinition(nameof(BusinessHomePage), typeof(string));


        /// <summary>
        /// AssistantName
        /// </summary>
        public static PropertyDefinition AssistantName = new PropertyDefinition(nameof(AssistantName), typeof(string));


        /// <summary>
        /// Manager
        /// </summary>
        public static PropertyDefinition Manager = new PropertyDefinition(nameof(Manager), typeof(string));


        /// <summary>
        /// HomePhones
        /// </summary>
        public static PropertyDefinition HomePhones = new PropertyDefinition(nameof(HomePhones), typeof(IList<String>));


        /// <summary>
        /// MobilePhone1
        /// </summary>
        public static PropertyDefinition MobilePhone1 = new PropertyDefinition(nameof(MobilePhone1), typeof(string));


        /// <summary>
        /// BusinessPhones
        /// </summary>
        public static PropertyDefinition BusinessPhones = new PropertyDefinition(nameof(BusinessPhones), typeof(IList<String>));


        /// <summary>
        /// HomeAddress
        /// </summary>
        public static PropertyDefinition HomeAddress = new PropertyDefinition(nameof(HomeAddress), typeof(PhysicalAddress));


        /// <summary>
        /// BusinessAddress
        /// </summary>
        public static PropertyDefinition BusinessAddress = new PropertyDefinition(nameof(BusinessAddress), typeof(PhysicalAddress));


        /// <summary>
        /// OtherAddress
        /// </summary>
        public static PropertyDefinition OtherAddress = new PropertyDefinition(nameof(OtherAddress), typeof(PhysicalAddress));


        /// <summary>
        /// SpouseName
        /// </summary>
        public static PropertyDefinition SpouseName = new PropertyDefinition(nameof(SpouseName), typeof(string));


        /// <summary>
        /// PersonalNotes
        /// </summary>
        public static PropertyDefinition PersonalNotes = new PropertyDefinition(nameof(PersonalNotes), typeof(string));


        /// <summary>
        /// Children
        /// </summary>
        public static PropertyDefinition Children = new PropertyDefinition(nameof(Children), typeof(IList<String>));


        /// <summary>
        /// SingleValueExtendedProperties
        /// </summary>
        public static PropertyDefinition SingleValueExtendedProperties = new PropertyDefinition(nameof(SingleValueExtendedProperties), typeof(IList<SingleValueLegacyExtendedProperty>));


        /// <summary>
        /// MultiValueExtendedProperties
        /// </summary>
        public static PropertyDefinition MultiValueExtendedProperties = new PropertyDefinition(nameof(MultiValueExtendedProperties), typeof(IList<MultiValueLegacyExtendedProperty>));


        /// <summary>
        /// Photo
        /// </summary>
        public static PropertyDefinition Photo = new PropertyDefinition(nameof(Photo), typeof(Photo));


        /// <summary>
        /// Extensions
        /// </summary>
        public static PropertyDefinition Extensions = new PropertyDefinition(nameof(Extensions), typeof(IList<Extension>));

    }


    /// <summary>
    /// ContactFolderObjectSchema
    /// </summary>
    public class ContactFolderObjectSchema : EntityObjectSchema
    {

        /// <summary>
        /// ParentFolderId
        /// </summary>
        public static PropertyDefinition ParentFolderId = new PropertyDefinition(nameof(ParentFolderId), typeof(string));


        /// <summary>
        /// DisplayName
        /// </summary>
        public static PropertyDefinition DisplayName = new PropertyDefinition(nameof(DisplayName), typeof(string));


        /// <summary>
        /// SingleValueExtendedProperties
        /// </summary>
        public static PropertyDefinition SingleValueExtendedProperties = new PropertyDefinition(nameof(SingleValueExtendedProperties), typeof(IList<SingleValueLegacyExtendedProperty>));


        /// <summary>
        /// MultiValueExtendedProperties
        /// </summary>
        public static PropertyDefinition MultiValueExtendedProperties = new PropertyDefinition(nameof(MultiValueExtendedProperties), typeof(IList<MultiValueLegacyExtendedProperty>));


        /// <summary>
        /// Contacts
        /// </summary>
        public static PropertyDefinition Contacts = new PropertyDefinition(nameof(Contacts), typeof(IList<Contact>));


        /// <summary>
        /// ChildFolders
        /// </summary>
        public static PropertyDefinition ChildFolders = new PropertyDefinition(nameof(ChildFolders), typeof(IList<ContactFolder>));

    }


    /// <summary>
    /// OutlookCategoryObjectSchema
    /// </summary>
    public class OutlookCategoryObjectSchema : EntityObjectSchema
    {

        /// <summary>
        /// DisplayName
        /// </summary>
        public static PropertyDefinition DisplayName = new PropertyDefinition(nameof(DisplayName), typeof(string));


        /// <summary>
        /// Color
        /// </summary>
        public static PropertyDefinition Color = new PropertyDefinition(nameof(Color), typeof(CategoryColor));

    }


    /// <summary>
    /// InferenceClassificationObjectSchema
    /// </summary>
    public class InferenceClassificationObjectSchema : EntityObjectSchema
    {

        /// <summary>
        /// Overrides
        /// </summary>
        public static PropertyDefinition Overrides = new PropertyDefinition(nameof(Overrides), typeof(IList<InferenceClassificationOverride>));

    }


    /// <summary>
    /// PhotoObjectSchema
    /// </summary>
    public class PhotoObjectSchema : EntityObjectSchema
    {

        /// <summary>
        /// Height
        /// </summary>
        public static PropertyDefinition Height = new PropertyDefinition(nameof(Height), typeof(int));


        /// <summary>
        /// Width
        /// </summary>
        public static PropertyDefinition Width = new PropertyDefinition(nameof(Width), typeof(int));

    }


    /// <summary>
    /// ConversationThreadObjectSchema
    /// </summary>
    public class ConversationThreadObjectSchema : EntityObjectSchema
    {

        /// <summary>
        /// ToRecipients
        /// </summary>
        public static PropertyDefinition ToRecipients = new PropertyDefinition(nameof(ToRecipients), typeof(IList<Recipient>));


        /// <summary>
        /// Topic
        /// </summary>
        public static PropertyDefinition Topic = new PropertyDefinition(nameof(Topic), typeof(string));


        /// <summary>
        /// HasAttachments
        /// </summary>
        public static PropertyDefinition HasAttachments = new PropertyDefinition(nameof(HasAttachments), typeof(bool));


        /// <summary>
        /// LastDeliveredDateTime
        /// </summary>
        public static PropertyDefinition LastDeliveredDateTime = new PropertyDefinition(nameof(LastDeliveredDateTime), typeof(DateTimeOffset));


        /// <summary>
        /// UniqueSenders
        /// </summary>
        public static PropertyDefinition UniqueSenders = new PropertyDefinition(nameof(UniqueSenders), typeof(IList<String>));


        /// <summary>
        /// CcRecipients
        /// </summary>
        public static PropertyDefinition CcRecipients = new PropertyDefinition(nameof(CcRecipients), typeof(IList<Recipient>));


        /// <summary>
        /// Preview
        /// </summary>
        public static PropertyDefinition Preview = new PropertyDefinition(nameof(Preview), typeof(string));


        /// <summary>
        /// IsLocked
        /// </summary>
        public static PropertyDefinition IsLocked = new PropertyDefinition(nameof(IsLocked), typeof(bool));


        /// <summary>
        /// Posts
        /// </summary>
        public static PropertyDefinition Posts = new PropertyDefinition(nameof(Posts), typeof(IList<Post>));

    }


    /// <summary>
    /// ConversationObjectSchema
    /// </summary>
    public class ConversationObjectSchema : EntityObjectSchema
    {

        /// <summary>
        /// Topic
        /// </summary>
        public static PropertyDefinition Topic = new PropertyDefinition(nameof(Topic), typeof(string));


        /// <summary>
        /// HasAttachments
        /// </summary>
        public static PropertyDefinition HasAttachments = new PropertyDefinition(nameof(HasAttachments), typeof(bool));


        /// <summary>
        /// LastDeliveredDateTime
        /// </summary>
        public static PropertyDefinition LastDeliveredDateTime = new PropertyDefinition(nameof(LastDeliveredDateTime), typeof(DateTimeOffset));


        /// <summary>
        /// UniqueSenders
        /// </summary>
        public static PropertyDefinition UniqueSenders = new PropertyDefinition(nameof(UniqueSenders), typeof(IList<String>));


        /// <summary>
        /// Preview
        /// </summary>
        public static PropertyDefinition Preview = new PropertyDefinition(nameof(Preview), typeof(string));


        /// <summary>
        /// Threads
        /// </summary>
        public static PropertyDefinition Threads = new PropertyDefinition(nameof(Threads), typeof(IList<ConversationThread>));

    }


    /// <summary>
    /// AttachmentObjectSchema
    /// </summary>
    public class AttachmentObjectSchema : EntityObjectSchema
    {

        /// <summary>
        /// LastModifiedDateTime
        /// </summary>
        public static PropertyDefinition LastModifiedDateTime = new PropertyDefinition(nameof(LastModifiedDateTime), typeof(DateTimeOffset));


        /// <summary>
        /// Name
        /// </summary>
        public static PropertyDefinition Name = new PropertyDefinition(nameof(Name), typeof(string));


        /// <summary>
        /// ContentType
        /// </summary>
        public static PropertyDefinition ContentType = new PropertyDefinition(nameof(ContentType), typeof(string));


        /// <summary>
        /// Size
        /// </summary>
        public static PropertyDefinition Size = new PropertyDefinition(nameof(Size), typeof(int));


        /// <summary>
        /// IsInline
        /// </summary>
        public static PropertyDefinition IsInline = new PropertyDefinition(nameof(IsInline), typeof(bool));

    }


    /// <summary>
    /// FileAttachmentObjectSchema
    /// </summary>
    public class FileAttachmentObjectSchema : AttachmentObjectSchema
    {

        /// <summary>
        /// ContentId
        /// </summary>
        public static PropertyDefinition ContentId = new PropertyDefinition(nameof(ContentId), typeof(string));


        /// <summary>
        /// ContentLocation
        /// </summary>
        public static PropertyDefinition ContentLocation = new PropertyDefinition(nameof(ContentLocation), typeof(string));


        /// <summary>
        /// ContentBytes
        /// </summary>
        public static PropertyDefinition ContentBytes = new PropertyDefinition(nameof(ContentBytes), typeof(string));

    }


    /// <summary>
    /// ItemAttachmentObjectSchema
    /// </summary>
    public class ItemAttachmentObjectSchema : AttachmentObjectSchema
    {

        /// <summary>
        /// Item
        /// </summary>
        public static PropertyDefinition Item = new PropertyDefinition(nameof(Item), typeof(Item));

    }


    /// <summary>
    /// ReferenceAttachmentObjectSchema
    /// </summary>
    public class ReferenceAttachmentObjectSchema : AttachmentObjectSchema
    {

        /// <summary>
        /// SourceUrl
        /// </summary>
        public static PropertyDefinition SourceUrl = new PropertyDefinition(nameof(SourceUrl), typeof(string));


        /// <summary>
        /// ProviderType
        /// </summary>
        public static PropertyDefinition ProviderType = new PropertyDefinition(nameof(ProviderType), typeof(ReferenceAttachmentProvider));


        /// <summary>
        /// ThumbnailUrl
        /// </summary>
        public static PropertyDefinition ThumbnailUrl = new PropertyDefinition(nameof(ThumbnailUrl), typeof(string));


        /// <summary>
        /// PreviewUrl
        /// </summary>
        public static PropertyDefinition PreviewUrl = new PropertyDefinition(nameof(PreviewUrl), typeof(string));


        /// <summary>
        /// Permission
        /// </summary>
        public static PropertyDefinition Permission = new PropertyDefinition(nameof(Permission), typeof(ReferenceAttachmentPermission));


        /// <summary>
        /// IsFolder
        /// </summary>
        public static PropertyDefinition IsFolder = new PropertyDefinition(nameof(IsFolder), typeof(bool));

    }


    /// <summary>
    /// MessageRuleObjectSchema
    /// </summary>
    public class MessageRuleObjectSchema : EntityObjectSchema
    {

        /// <summary>
        /// DisplayName
        /// </summary>
        public static PropertyDefinition DisplayName = new PropertyDefinition(nameof(DisplayName), typeof(string));


        /// <summary>
        /// Sequence
        /// </summary>
        public static PropertyDefinition Sequence = new PropertyDefinition(nameof(Sequence), typeof(int));


        /// <summary>
        /// Conditions
        /// </summary>
        public static PropertyDefinition Conditions = new PropertyDefinition(nameof(Conditions), typeof(MessageRulePredicates));


        /// <summary>
        /// Actions
        /// </summary>
        public static PropertyDefinition Actions = new PropertyDefinition(nameof(Actions), typeof(MessageRuleActions));


        /// <summary>
        /// Exceptions
        /// </summary>
        public static PropertyDefinition Exceptions = new PropertyDefinition(nameof(Exceptions), typeof(MessageRulePredicates));


        /// <summary>
        /// IsEnabled
        /// </summary>
        public static PropertyDefinition IsEnabled = new PropertyDefinition(nameof(IsEnabled), typeof(bool));


        /// <summary>
        /// HasError
        /// </summary>
        public static PropertyDefinition HasError = new PropertyDefinition(nameof(HasError), typeof(bool));


        /// <summary>
        /// IsReadOnly
        /// </summary>
        public static PropertyDefinition IsReadOnly = new PropertyDefinition(nameof(IsReadOnly), typeof(bool));

    }


    /// <summary>
    /// InferenceClassificationOverrideObjectSchema
    /// </summary>
    public class InferenceClassificationOverrideObjectSchema : EntityObjectSchema
    {

        /// <summary>
        /// ClassifyAs
        /// </summary>
        public static PropertyDefinition ClassifyAs = new PropertyDefinition(nameof(ClassifyAs), typeof(InferenceClassificationType));


        /// <summary>
        /// SenderEmailAddress
        /// </summary>
        public static PropertyDefinition SenderEmailAddress = new PropertyDefinition(nameof(SenderEmailAddress), typeof(EmailAddress));

    }


    /// <summary>
    /// PostObjectSchema
    /// </summary>
    public class PostObjectSchema : ItemObjectSchema
    {

        /// <summary>
        /// Body
        /// </summary>
        public static PropertyDefinition Body = new PropertyDefinition(nameof(Body), typeof(ItemBody));


        /// <summary>
        /// ReceivedDateTime
        /// </summary>
        public static PropertyDefinition ReceivedDateTime = new PropertyDefinition(nameof(ReceivedDateTime), typeof(DateTimeOffset));


        /// <summary>
        /// HasAttachments
        /// </summary>
        public static PropertyDefinition HasAttachments = new PropertyDefinition(nameof(HasAttachments), typeof(bool));


        /// <summary>
        /// From
        /// </summary>
        public static PropertyDefinition From = new PropertyDefinition(nameof(From), typeof(Recipient));


        /// <summary>
        /// Sender
        /// </summary>
        public static PropertyDefinition Sender = new PropertyDefinition(nameof(Sender), typeof(Recipient));


        /// <summary>
        /// ConversationThreadId
        /// </summary>
        public static PropertyDefinition ConversationThreadId = new PropertyDefinition(nameof(ConversationThreadId), typeof(string));


        /// <summary>
        /// NewParticipants
        /// </summary>
        public static PropertyDefinition NewParticipants = new PropertyDefinition(nameof(NewParticipants), typeof(IList<Recipient>));


        /// <summary>
        /// ConversationId
        /// </summary>
        public static PropertyDefinition ConversationId = new PropertyDefinition(nameof(ConversationId), typeof(string));


        /// <summary>
        /// Importance
        /// </summary>
        public static PropertyDefinition Importance = new PropertyDefinition(nameof(Importance), typeof(Importance));


        /// <summary>
        /// InReplyTo
        /// </summary>
        public static PropertyDefinition InReplyTo = new PropertyDefinition(nameof(InReplyTo), typeof(Post));


        /// <summary>
        /// SingleValueExtendedProperties
        /// </summary>
        public static PropertyDefinition SingleValueExtendedProperties = new PropertyDefinition(nameof(SingleValueExtendedProperties), typeof(IList<SingleValueLegacyExtendedProperty>));


        /// <summary>
        /// MultiValueExtendedProperties
        /// </summary>
        public static PropertyDefinition MultiValueExtendedProperties = new PropertyDefinition(nameof(MultiValueExtendedProperties), typeof(IList<MultiValueLegacyExtendedProperty>));


        /// <summary>
        /// Extensions
        /// </summary>
        public static PropertyDefinition Extensions = new PropertyDefinition(nameof(Extensions), typeof(IList<Extension>));


        /// <summary>
        /// Attachments
        /// </summary>
        public static PropertyDefinition Attachments = new PropertyDefinition(nameof(Attachments), typeof(IList<Attachment>));

    }


    /// <summary>
    /// ExtensionObjectSchema
    /// </summary>
    public class ExtensionObjectSchema : EntityObjectSchema
    {
    }


    /// <summary>
    /// OpenTypeExtensionObjectSchema
    /// </summary>
    public class OpenTypeExtensionObjectSchema : ExtensionObjectSchema
    {

        /// <summary>
        /// ExtensionName
        /// </summary>
        public static PropertyDefinition ExtensionName = new PropertyDefinition(nameof(ExtensionName), typeof(string));

    }

}
