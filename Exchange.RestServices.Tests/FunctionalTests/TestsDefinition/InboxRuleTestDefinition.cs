namespace Exchange.RestServices.Tests.Functional.TestsDefinition
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using Exchange;
    using Microsoft.OutlookServices;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Contains test definition for inbox rules.
    /// </summary>
    internal static class InboxRuleTestDefinition
    {
        /// <summary>
        /// CRUD operations for Inbox rules.
        /// </summary>
        /// <param name="exchangeService">Exchange service.</param>
        public static void CreateReadUpdateDeleteInboxRule(ExchangeService exchangeService)
        {
            MessageRule messageRule = new MessageRule(exchangeService);
            messageRule.IsEnabled = true;
            messageRule.Sequence = 1;
            messageRule.Actions = new MessageRuleActions()
            {
                Delete = true,
                StopProcessingRules = true
            };

            messageRule.Conditions = new MessageRulePredicates()
            {
                FromAddresses = new List<Recipient>()
                {
                    new Recipient()
                    {
                        EmailAddress = new EmailAddress()
                        {
                            Address = "a@b.com"
                        }
                    }
                }
            };
            IList<string> s = new ObservableCollection<string>();
            messageRule.DisplayName = "Test rule";

            Assert.IsNull(messageRule.Id);
            messageRule.Save();
            Assert.IsNotNull(messageRule.Id);

            Assert.IsNotNull(messageRule.Id);

            MessageRule getMessageRule = exchangeService.GetInboxRule(messageRule.Id);
            Assert.IsNotNull(getMessageRule);

            getMessageRule.IsEnabled = false;
            getMessageRule.Update();
            Assert.IsFalse(getMessageRule.IsEnabled);


            List<MessageRule> rules = exchangeService.GetInboxRules();
            Assert.IsTrue(rules.Count == 1);

            getMessageRule.Delete();
            rules = exchangeService.GetInboxRules();
            Assert.IsTrue(rules.Count == 0);
        }
    }
}
