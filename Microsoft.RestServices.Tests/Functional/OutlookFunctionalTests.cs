namespace Microsoft.RestServices.Tests.Functional
{
    using Exchange;
    using VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public abstract class OutlookFunctionalTestsBase
    {
        /// <summary>
        /// Create new <see cref="ExchangeService"/>
        /// </summary>
        /// <param name="mailboxId">Mailbox id.</param>
        /// <returns></returns>
        protected ExchangeService GetExchangeService(string mailboxId)
        {
            return new ExchangeService(
                new TestAuthenticationProvider(AppConfig.OutlookResourceUri),
                mailboxId,
                RestEnvironment.OutlookProd);
        }
    }

    [TestClass]
    public class OutlookTestCases : OutlookFunctionalTestsBase
    {
        /// <summary>
        /// Sync mail folders.
        /// </summary>
        [TestMethod]
        public void Test_SyncMailFolders()
        {
            FunctionalTestRunner.RunTestCase(
                MailFolderTestDefinition.SyncMailFolders,
                this.GetExchangeService(AppConfig.MailboxA));
        }

        /// <summary>
        /// Get mail folders.
        /// </summary>
        [TestMethod]
        public void Test_GetMailFolders()
        {
            FunctionalTestRunner.RunTestCase(
                MailFolderTestDefinition.GetMailFolders,
                this.GetExchangeService(AppConfig.MailboxA));
        }

        [TestMethod]
        public void Test_CreateReadUpdateDeleteMailFolder()
        {
            FunctionalTestRunner.RunTestCase(
                MailFolderTestDefinition.CreateReadUpdateDeleteMailFolder,
                this.GetExchangeService(AppConfig.MailboxA));
        }

        [TestMethod]
        public void Test_GetMailMessageWithSingleExtendedProperties()
        {
            FunctionalTestRunner.RunTestCase(
                MailMessageTestDefinition.GetMailMessageWithSingleExtendedProperties,
                this.GetExchangeService(AppConfig.MailboxA));
        }

        [TestMethod]
        public void Test_SendMessageFromMailboxAToMailboxB()
        {
            FunctionalTestRunner.RunTestCase(
                MailMessageTestDefinition.SendMessageFromMailboxAToMailboxB,
                this.GetExchangeService(AppConfig.MailboxA));
        }
    }
}
