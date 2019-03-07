namespace Exchange.RestServices.Tests.Functional
{
    using System;
    using System.Diagnostics;
    using TestsDefinition;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Exchange.RestServices;
    using Microsoft.OutlookServices;
    using Task = System.Threading.Tasks.Task;

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

        [TestMethod]
        public async Task Test_CreateReadUpdateDeleteAsync()
        {
            await MailMessageTestDefinition.CreateReadUpdateDeleteMessageAsync(
                this.GetExchangeService(AppConfig.MailboxB));
        }

        #endregion

        #region InboxRule tests

        [TestMethod]
        public void Test_CreateReadUpdateDeleteInboxRule()
        {
            this.Run_TestCase_As_Mailbox_A(InboxRuleTestDefinition.CreateReadUpdateDeleteInboxRule);
        }

        [TestMethod]
        public async Task Test_CreateReadUpdateDeleteInboxRuleAsync()
        {
            await InboxRuleTestDefinition.CreateReadUpdateDeleteInboxRuleAsync(
                this.GetExchangeService(AppConfig.MailboxB));
        }

        #endregion

        #region Inference classification tests

        [TestMethod]
        public void Test_CreateReadUpdateDeleteInferenceClassificationOverride()
        {
            this.Run_TestCase_As_Mailbox_A(InferenceClassificationTestDefinition.CreateReadUpdateDeleteInferenceClassificationOverride);
        }

        [TestMethod]
        public async Task Test_CreateReadUpdateDeleteInferenceClassificationOverrideAsync()
        {
            await InferenceClassificationTestDefinition.CreateReadUpdateDeleteInferenceClassificationOverrideAsync(
                this.GetExchangeService(AppConfig.MailboxA));
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

        #region Tasks tests

        [TestMethod]
        public void Test_CreateUpdateReadDeleteTasks()
        {
            this.Run_TestCase_As_Mailbox_A(TasksTestDefinition.CreateUpdateReadDeleteTasks);
        }

        #endregion

        #region Contacts tests

        [TestMethod]
        public void Test_CreateReadUpdateDeleteContact()
        {
            this.Run_TestCase_As_Mailbox_A(ContactTestDefinition.CreateReadUpdateDeleteContact);
        }

        #endregion

        #region Multithread test

        [TestMethod]
        public async Task Test_MultiThreadTaskExecution()
        {
            ExchangeService exchangeServiceMailboxA = this.GetExchangeService(AppConfig.MailboxA);
            var itemsFromInbox1MailboxA = exchangeServiceMailboxA.FindItemsAsync(
                new FolderId(WellKnownFolderName.Inbox), 
                new MessageView(20));

            var itemsFromSentMailboxA = exchangeServiceMailboxA.FindItemsAsync(
                new FolderId(WellKnownFolderName.SentItems),
                new MessageView(20));

            var itemsFromInbox2MailboxA = exchangeServiceMailboxA.FindItemsAsync(
                new FolderId(WellKnownFolderName.Inbox),
                new MessageView(20));

            ExchangeService exchangeServiceMailboxB = this.GetExchangeService(AppConfig.MailboxB);
            var itemsFromInbox1MailboxB = exchangeServiceMailboxB.FindItemsAsync(
                new FolderId(WellKnownFolderName.Inbox),
                new MessageView(20));

            var itemsFromSentMailboxB = exchangeServiceMailboxB.FindItemsAsync(
                new FolderId(WellKnownFolderName.SentItems),
                new MessageView(20));

            var itemsFromInbox2MailboxB = exchangeServiceMailboxB.FindItemsAsync(
                new FolderId(WellKnownFolderName.Inbox),
                new MessageView(20));

            var createInferenceClassificationMailboxA = exchangeServiceMailboxA.CreateInferenceClassificationOverrideAsync(new InferenceClassificationOverride()
                {
                    ClassifyAs = InferenceClassificationType.Focused,
                    SenderEmailAddress = new EmailAddress()
                    {
                        Address = "a@b.com"
                    }
                });

            var createInferenceClassificationMailboxB = exchangeServiceMailboxA.CreateInferenceClassificationOverrideAsync(new InferenceClassificationOverride()
            {
                ClassifyAs = InferenceClassificationType.Focused,
                SenderEmailAddress = new EmailAddress()
                {
                    Address = "a@b.com"
                }
            });

            foreach (MailFolder folder in await exchangeServiceMailboxA.FindFoldersAsync(new FolderId(WellKnownFolderName.MsgFolderRoot),null, new FolderView(10) ))
            {
                Assert.AreEqual(
                    AppConfig.MailboxA,
                    folder.MailboxId.Id);
            }

            foreach (Item item in await itemsFromInbox1MailboxA)
            {
                if (item is Message msg)
                {
                    Assert.AreEqual(
                        AppConfig.MailboxA,
                        msg.MailboxId.Id);
                }
            }

            foreach (Item item in await itemsFromInbox1MailboxB)
            {
                if (item is Message msg)
                {
                    Assert.AreEqual(
                        AppConfig.MailboxB,
                        msg.MailboxId.Id);
                }
            }

            InferenceClassificationOverride inferenceClassificationOverrideA =
                await createInferenceClassificationMailboxA;

            inferenceClassificationOverrideA.SenderEmailAddress = new EmailAddress()
            {
                Address = "c@c.com"
            };

            await inferenceClassificationOverrideA.UpdateAsync();
            Assert.AreEqual(
                "c@c.com",
                inferenceClassificationOverrideA.SenderEmailAddress.Address);

            foreach (MailFolder folder in await exchangeServiceMailboxB.FindFoldersAsync(new FolderId(WellKnownFolderName.MsgFolderRoot), null, new FolderView(10)))
            {
                Assert.AreEqual(
                    AppConfig.MailboxB,
                    folder.MailboxId.Id);
            }

            foreach (Item item in await itemsFromInbox2MailboxA)
            {
                if (item is Message msg)
                {
                    Assert.AreEqual(
                        AppConfig.MailboxA,
                        msg.MailboxId.Id);
                }
            }

            InferenceClassificationOverride inferenceClassificationOverrideB =
                await createInferenceClassificationMailboxB;

            inferenceClassificationOverrideB.ClassifyAs = InferenceClassificationType.Other;
            await inferenceClassificationOverrideB.UpdateAsync();
            Assert.AreEqual(
                InferenceClassificationType.Other,
                inferenceClassificationOverrideB.ClassifyAs);

            foreach (Item item in await itemsFromSentMailboxB)
            {
                if (item is Message msg)
                {
                    Assert.AreEqual(
                        AppConfig.MailboxB,
                        msg.MailboxId.Id);
                }
            }

            foreach (Item item in await itemsFromSentMailboxA)
            {
                if (item is Message msg)
                {
                    Assert.AreEqual(
                        AppConfig.MailboxA,
                        msg.MailboxId.Id);
                }
            }

            foreach (Item item in await itemsFromInbox2MailboxB)
            {
                if (item is Message msg)
                {
                    Assert.AreEqual(
                        AppConfig.MailboxB,
                        msg.MailboxId.Id);
                }
            }
        }

        #endregion
    }
}
