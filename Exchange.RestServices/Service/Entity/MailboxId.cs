namespace Exchange.RestServices
{
    using System;

    /// <summary>
    /// Represents mailbox id. It can be in one of the following 'flavors':
    /// 'me', 'user@domain.com' or '84055e2f-c537-4f66-8ee1-f588abb3232b@d1e69a0e-c0f7-40cb-9973-bb9d5e0c3bcd'
    /// </summary>
    public class MailboxId
    {
        // 'me' identity.
        private const string MeKeyword = "me";

        /// <summary>
        /// Me instance.
        /// </summary>
        private static readonly MailboxId me = new MailboxId(MailboxId.MeKeyword);

        /// <summary>
        /// Create new instance of <see cref="MailboxId"/>.
        /// </summary>
        /// <param name="mailboxId">MailboxId id.</param>
        public MailboxId(string mailboxId)
        {
            if (string.IsNullOrEmpty(mailboxId) ||
                string.Equals(mailboxId, MailboxId.MeKeyword, StringComparison.OrdinalIgnoreCase))
            {
                this.Id = MailboxId.MeKeyword;
            }
            else
            {
                // user@domain.com
                if (EmailAddressValidator.IsValid(mailboxId))
                {
                    this.Id = mailboxId;
                }
                else
                {
                    // UserId                               OrgId
                    // 84055e2f-c537-4f66-8ee1-f588abb3232b@d1e69a0e-c0f7-40cb-9973-bb9d5e0c3bcd
                    if (mailboxId.IndexOf('@') > 0)
                    {
                        string[] parts = mailboxId.Split('@');
                        if (parts.Length != 2)
                        {
                            this.ThrowInvalidValue(mailboxId);
                        }

                        Guid id;
                        if (!Guid.TryParse(parts[0], out id))
                        {
                            this.ThrowInvalidValue(mailboxId);
                        }

                        if (!Guid.TryParse(parts[1], out id))
                        {
                            this.ThrowInvalidValue(mailboxId);
                        }
                    }
                    else
                    {
                        this.ThrowInvalidValue(mailboxId);
                    }

                    this.Id = mailboxId;
                }
            }
        }

        public static MailboxId Me
        {
            get { return MailboxId.me; }
        }

        /// <summary>
        /// Raw Id.
        /// </summary>
        public string Id { get; }

        /// <summary>
        /// Users or Groups. TODO: Implement groups.
        /// </summary>
        public string MailboxType
        {
            get { return "users"; }
        }

        /// <summary>
        /// Path.
        /// </summary>
        public string Path
        {
            get
            {
                if (this.IdInMeForm)
                {
                    return this.Id;
                }

                return $"{this.MailboxType}/{this.Id}";
            }
        }

        /// <summary>
        /// Indicate if Id is represented as email address.
        /// </summary>
        public bool IdInEmailAddressForm
        {
            get { return EmailAddressValidator.IsValid(this.Id); }
        }

        /// <summary>
        /// Indicate if Id is represented as 'me'.
        /// </summary>
        public bool IdInMeForm
        {
            get
            {
                return this.Id.Equals(
              MailboxId.MeKeyword,
              StringComparison.OrdinalIgnoreCase);
            }
        }

        /// <summary>
        /// Equals override.
        /// </summary>
        /// <param name="obj">Object to validate.</param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (!(obj is MailboxId mailboxId))
            {
                return false;
            }

            return this.Id.Equals(mailboxId.Id, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// ToString impl.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this.Id;
        }

        /// <summary>
        /// Throws if invalid value.
        /// </summary>
        /// <param name="value"></param>
        private void ThrowInvalidValue(string value)
        {
            throw new ArgumentException($"Invalid value provided for MailboxId: '{value}'.", "mailboxId");
        }
    }
}
