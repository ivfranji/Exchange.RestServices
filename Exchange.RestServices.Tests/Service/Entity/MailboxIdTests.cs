namespace Exchange.RestServices.Tests.Service.Entity
{
    using System;
    using Exchange.RestServices;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class MailboxIdTests
    {
        [TestMethod]
        public void TestMailboxIdProperties()
        {
            MailboxId mailboxId = new MailboxId("test@domain.com");
            
            Assert.AreEqual(
                mailboxId.Id, 
                "test@domain.com");

            Assert.IsTrue(mailboxId.IdInEmailAddressForm);
            Assert.IsFalse(mailboxId.IdInMeForm);
            Assert.AreEqual(
                mailboxId.Path, 
                "users/test@domain.com");

            Assert.IsTrue(mailboxId.Equals(new MailboxId("test@domain.com")));

            mailboxId = new MailboxId("me");
            Assert.IsTrue(mailboxId.IdInMeForm);
            Assert.IsFalse(mailboxId.IdInEmailAddressForm);

            Assert.AreEqual(
                mailboxId.Path, 
                "me");

            Assert.IsFalse(mailboxId.Equals(null));
            Assert.AreEqual(
                mailboxId.ToString(), 
                "me");

            string guidFormat = "35190d3b-a0c3-462f-873d-14739728ec13@fddcbad5-788b-4eb8-be11-21d569abe684";
            mailboxId = new MailboxId(guidFormat);

            Assert.AreEqual(
                mailboxId.Id, 
                guidFormat);

            Assert.AreEqual(
                mailboxId.Path,
                $"users/{guidFormat}");

            string invalidGuidFormat1 = "35190d3b-a0c3-462f873d-14739728ec13@fddcbad5-788b-4eb8-be11-21d569abe684";
            string invalidGuidFormat2 = "35190d3b-a0c3-462f-873d-14739728ec13@fddcbad5-788b-4eb8be11-21d569abe684";
            string invalidGuidFormat3 = "35190d3b-a0c3-462f-873d-14739728ec13@";
            string invalidFormat1 = "fsdsfsfds";
            string invalidFormat2 = "fsdfs@fsfsd@fs";

            Assert.ThrowsException<ArgumentException>(() =>
            {
                mailboxId = new MailboxId(invalidGuidFormat1);
            });

            Assert.ThrowsException<ArgumentException>(() =>
            {
                mailboxId = new MailboxId(invalidGuidFormat2);
            });

            Assert.ThrowsException<ArgumentException>(() =>
            {
                mailboxId = new MailboxId(invalidGuidFormat3);
            });

            Assert.ThrowsException<ArgumentException>(() =>
            {
                mailboxId = new MailboxId(invalidFormat1);
            });

            Assert.ThrowsException<ArgumentException>(() =>
            {
                mailboxId = new MailboxId(invalidFormat2);
            });
        }
    }
}
