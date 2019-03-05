namespace Exchange.RestServices.Tests.Functional.TestsDefinition
{
    using System;
    using Exchange;
    using Microsoft.OutlookServices;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Define contact tests.
    /// </summary>
    public static class ContactTestDefinition
    {
        /// <summary>
        /// CRUD operations for task
        /// </summary>
        /// <param name="exchangeService"></param>
        public static void CreateReadUpdateDeleteContact(ExchangeService exchangeService)
        {
            string displayName = Guid.NewGuid().ToString();
            FolderId contactFolderId = new ContactFolderId("me");

            Contact contact = new Contact(exchangeService);
            contact.DisplayName = displayName;
            contact.EmailAddresses.Add(new EmailAddress()
            {
                Address = $"{displayName}@domain.com"
            });

            contact.Department = "Dept";
            contact.GivenName = "First Name";

            contact.Save(contactFolderId);

            SearchFilter searchFilter = new SearchFilter.IsEqualTo(
                ContactObjectSchema.DisplayName, 
                displayName);

            FindItemsResults<Item> contacts = exchangeService.FindItems(
                contactFolderId, 
                searchFilter,
                new ContactView(10));

            Assert.AreEqual(
                1, 
                contacts.TotalCount);
            

            contact.AssistantName = "Assistant";
            contact.Update();

            Assert.AreEqual(
                "Assistant",
                contact.AssistantName);

            contact.Delete();

            Assert.IsNull(contact.Id);
        }
    }
}
