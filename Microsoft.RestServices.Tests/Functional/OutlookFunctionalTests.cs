namespace Microsoft.RestServices.Tests.Functional
{
    using System;
    using Exchange;
    using TestsDefinition;
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
                RestEnvironment.OutlookBeta);
        }

        /// <summary>
        /// Runs test case as mailbox A.
        /// </summary>
        /// <param name="testCase">test case.</param>
        protected void Run_TestCase_As_Mailbox_A(Action<ExchangeService> testCase)
        {
            ExchangeService exchangeService = this.GetExchangeService(AppConfig.MailboxA);
            this.RunFunctionalTestCase(
                exchangeService,
                testCase);
        }

        /// <summary>
        /// Runs test case as mailbox B.
        /// </summary>
        /// <param name="testCase">Test case.</param>
        protected void Run_TestCase_As_Mailbox_B(Action<ExchangeService> testCase)
        {
            ExchangeService exchangeService = this.GetExchangeService(AppConfig.MailboxB);
            this.RunFunctionalTestCase(
                exchangeService,
                testCase);
        }

        /// <summary>
        /// Run functional test case.
        /// </summary>
        /// <param name="exchangeService">Exchange service.</param>
        /// <param name="testCase">Test case.</param>
        private void RunFunctionalTestCase(ExchangeService exchangeService, Action<ExchangeService> testCase)
        {
            FunctionalTestRunner.RunTestCase(
                testCase,
                exchangeService);
        }
    }

    [TestClass]
    public class OutlookProdTestCases : OutlookFunctionalTestsBase
    {
        #region MailFolder tests

        /// <summary>
        /// Sync mail folders.
        /// </summary>
        [TestMethod]
        public void Test_SyncMailFolders()
        {
            this.Run_TestCase_As_Mailbox_A(MailFolderTestDefinition.SyncMailFolders);
        }

        /// <summary>
        /// Get mail folders.
        /// </summary>
        [TestMethod]
        public void Test_GetMailFolders()
        {
            this.Run_TestCase_As_Mailbox_A(MailFolderTestDefinition.GetMailFolders);
        }

        [TestMethod]
        public void Test_CreateReadUpdateDeleteMailFolder()
        {
            this.Run_TestCase_As_Mailbox_A(MailFolderTestDefinition.CreateReadUpdateDeleteMailFolder);
        }

        #endregion

        #region MailMessage tests

        [TestMethod]
        public void Test_GetMailMessageWithSingleExtendedProperties()
        {
            this.Run_TestCase_As_Mailbox_B(MailMessageTestDefinition.GetMailMessageWithSingleExtendedProperties);
        }

        [TestMethod]
        public void Test_SendMessageFromMailboxAToMailboxB()
        {
            this.Run_TestCase_As_Mailbox_A(MailMessageTestDefinition.SendMessageFromMailboxAToMailboxB);
        }

        [TestMethod]
        public void Test_CreateReadUpdateDeleteExtendedProperties()
        {
            this.Run_TestCase_As_Mailbox_A(MailMessageTestDefinition.CreateReadUpdateDeleteExtendedProperties);
        }

        [TestMethod]
        public void Test_FindMessage()
        {
            this.Run_TestCase_As_Mailbox_A((MailMessageTestDefinition.FindMessage));
        }

        [TestMethod]
        public void Test_SyncMessages()
        {
            this.Run_TestCase_As_Mailbox_A(MailMessageTestDefinition.SyncMessages);
        }

        #endregion

        #region InboxRule tests

        [TestMethod]
        public void Test_CreateReadUpdateDeleteInboxRule()
        {
            this.Run_TestCase_As_Mailbox_A(InboxRuleTestDefinition.CreateReadUpdateDeleteInboxRule);
        }

        #endregion

        #region Inference classification tests

        [TestMethod]
        public void Test_CreateReadUpdateDeleteInferenceClassificationOverride()
        {
            this.Run_TestCase_As_Mailbox_A(InferenceClassificationTestDefinition.CreateReadUpdateDeleteInferenceClassificationOverride);
        }

        #endregion

        #region Calendar Event tests

        [TestMethod]
        public void Test_CreateReadUpdateDeleteEvents()
        {
            this.Run_TestCase_As_Mailbox_B(EventTestDefinition.CreateReadUpdateDeleteEvents);
        }

        #endregion

        #region Attachment tests

        [TestMethod]
        public void Test_CreateReadUpdateDeleteFileAttachment()
        {
            this.Run_TestCase_As_Mailbox_A(AttachmentTestDefinition.CreateReadUpdateDeleteFileAttachment);
        }

        [TestMethod]
        public void Test_CreateReadUpdateDeleteItemAttachment()
        {
            this.Run_TestCase_As_Mailbox_A(AttachmentTestDefinition.CreateReadUpdateDeleteItemAttachment);
        }

        #endregion
    }
}
