namespace Microsoft.RestServices.Tests.Service.FilterFormatter
{
    using System;
    using Exchange;
    using Microsoft.OutlookServices;
    using VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class FilterFormattersTests
    {
        [TestMethod]
        public void BoolFilterFormatterBehavior()
        {
            this.TestFilterFormatter(
                typeof(BoolFilterFormatter),
                typeof(bool),
                MessageObjectSchema.IsRead,
                true,
                FilterOperator.eq,
                "IsRead eq True");

            this.TestFilterFormatter(
                typeof(BoolFilterFormatter),
                typeof(bool),
                MessageObjectSchema.IsRead,
                "false",
                FilterOperator.eq,
                "IsRead eq false");
        }

        [TestMethod]
        public void DateTimeFilterFormatterBehavior()
        {
            PropertyDefinition propDef = new PropertyDefinition(
                "Blah", 
                typeof(DateTime));

            this.TestFilterFormatter(
                typeof(DateTimeFilterFormatter),
                typeof(DateTime),
                propDef,
                new DateTime(
                    2019, 
                    2, 
                    1, 
                    11, 
                    0,
                    0),
                FilterOperator.gt,
                "Blah gt 2019-02-01T12:00:00Z"); // Z - UTC

            this.TestFilterFormatter(
                typeof(DateTimeFilterFormatter),
                typeof(DateTime),
                propDef,
                "2019-01-01",
                FilterOperator.lt,
                "Blah lt 2019-01-01");
        }

        [TestMethod]
        public void DateTimeOffsetFilterFormatterBehavior()
        {
            this.TestFilterFormatter(
                typeof(DateTimeOffsetFilterFormatter),
                typeof(DateTimeOffset),
                EventObjectSchema.CreatedDateTime,
                new DateTimeOffset(
                    new DateTime(
                        2019,
                        2,
                        1,
                        11,
                        0,
                        0)), 
                FilterOperator.gt,
                "CreatedDateTime gt 2019-02-01T12:00:00Z"); // Z - UTC

            this.TestFilterFormatter(
                typeof(DateTimeOffsetFilterFormatter),
                typeof(DateTimeOffset),
                EventObjectSchema.CreatedDateTime,
                "2019-01-01",
                FilterOperator.gt,
                "CreatedDateTime gt 2019-01-01");
        }

        [TestMethod]
        public void IntFilterFormatterBehavior()
        {
            this.TestFilterFormatter(
                typeof(IntFilterFormatter),
                typeof(int),
                MailFolderObjectSchema.TotalItemCount,
                4,
                FilterOperator.lt,
                "TotalItemCount lt 4");

            this.TestFilterFormatter(
                typeof(IntFilterFormatter),
                typeof(int),
                MailFolderObjectSchema.TotalItemCount,
                "5",
                FilterOperator.eq,
                "TotalItemCount eq 5");
        }

        [TestMethod]
        public void RecipientFilterFormatterBehavior()
        {
            this.TestFilterFormatter(
                typeof(RecipientFilterFormatter),
                typeof(Recipient),
                MessageObjectSchema.From,
                new Recipient()
                {
                    EmailAddress = new EmailAddress()
                    {
                        Address = "t@t.com"
                    }
                }, 
                FilterOperator.eq,
                "From/EmailAddress/Address eq 't@t.com'");

            this.TestFilterFormatter(
                typeof(RecipientFilterFormatter),
                typeof(Recipient),
                MessageObjectSchema.From,
                new Recipient()
                {
                    EmailAddress = new EmailAddress()
                    {
                        Name = "T T"
                    }
                },
                FilterOperator.eq,
                "From/EmailAddress/Name eq 'T T'");

            this.TestFilterFormatter(
                typeof(RecipientFilterFormatter),
                typeof(Recipient),
                MessageObjectSchema.From,
                "t@t.com",
                FilterOperator.ne,
                "From/EmailAddress/Address ne 't@t.com'");
        }

        [TestMethod]
        public void StringFilterFormatterBehavior()
        {
            this.TestFilterFormatter(
                typeof(StringFilterFormatter),
                typeof(string),
                MessageObjectSchema.Subject,
                "aa",
                FilterOperator.ne,
                "Subject ne 'aa'");
        }

        private void TestFilterFormatter(
            Type type, 
            Type typeItHandle, 
            PropertyDefinition propertyDefinition, 
            object objectToFormat,
            FilterOperator filterOperator, 
            string expectedResult)
        {
            ArgumentValidator.ThrowIfNull(
                type, 
                nameof(type));

            if (!type.IsSubclassOf(typeof(BaseFilterFormatter)))
            {
                throw new ArgumentException("Please provide typeof(BaseFilterFormatter)");
            }

            if (type.IsAbstract)
            {
                throw new ArgumentException("Please provide non-abstract filter formatter type.");
            }

            IFilterFormatter formatter = (IFilterFormatter) Activator.CreateInstance(type);
            Assert.AreEqual(
                typeItHandle.FullName,
                formatter.Type);

            string formattedString = formatter.Format(
                objectToFormat, 
                filterOperator, 
                propertyDefinition);

            Assert.AreEqual(
                expectedResult,
                formattedString);

            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                formatter.Format(
                    null,
                    filterOperator,
                    propertyDefinition);
            });

            Assert.ThrowsException<ArgumentException>(() =>
            {
                formatter.Format(
                    new TempClass(), 
                    filterOperator,
                    propertyDefinition);
            });
        }

        private class TempClass
        {
        }
    }
}
