namespace Microsoft.RestServices.Tests.Service.QueryAndView
{
    using System;
    using Microsoft.RestServices.Exchange;
    using VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class SyncTokenTests
    {
        [TestMethod]
        public void TestSyncTokenProperties()
        {
            SyncToken syncToken = new SyncToken(
                "abcde", 
                SyncTokenType.DeltaToken);

            Assert.AreEqual(
                "$deltatoken=abcde", 
                syncToken.Query);
        }

        [TestMethod]
        public void TestSyncTokenParser()
        {
            Uri url = new Uri("https://graph.microsoft.com/beta/delta?$skiptoken=fasdlfjflds");
            ISyncToken token = null;
            
            Assert.IsTrue(
                SyncToken.TryParseFromUrl(
                    url, 
                    SyncTokenType.SkipToken, 
                    out token));

            Assert.AreEqual(
                "$skiptoken=fasdlfjflds", 
                token.Query);

            Assert.AreEqual(
                "fasdlfjflds",
                token.Value);

            Assert.IsFalse(
                SyncToken.TryParseFromUrl(
                    "abcd", 
                    SyncTokenType.DeltaToken, 
                    out token));

            Assert.IsFalse(
                SyncToken.TryParseFromUrl(
                    "",
                    SyncTokenType.DeltaToken,
                    out token));

            url = new Uri("https://graph.microsoft.com/beta/delta?$select=Something&$deltatoken=fasdlfjflds&$filter=A");

            Assert.IsTrue(
                SyncToken.TryParseFromUrl(
                    url,
                    SyncTokenType.DeltaToken,
                    out token));

            Assert.AreEqual(
                "$deltatoken=fasdlfjflds", 
                token.Query);
        }

        [TestMethod]
        public void TestSyncTokenSerializeDeserialize()
        {
            string serialized = "RGVsdGFUb2tlbnxhYmNkZWY=";
            SyncToken syncToken = SyncToken.Deserialize(serialized);

            Assert.AreEqual(
                SyncTokenType.DeltaToken, 
                syncToken.Type);

            Assert.AreEqual(
                "abcdef", 
                syncToken.Value);

            Assert.AreEqual(
                "$deltatoken=abcdef", 
                syncToken.Query);

            Assert.IsNull(SyncToken.Deserialize(null));
            Assert.IsNull(SyncToken.Deserialize("abc")); // this is incorrect (when it comes to formatting) base64 string
            Assert.IsNull(SyncToken.Deserialize("abcd")); // this is correct (when it comes to formatting) base64 string
            Assert.IsNull(SyncToken.Deserialize("bm9uZXhpc3Rpbmd0b2tlbnR5cGV8cGF5bG9hZA==")); // "nonexistingtokentype|payload" as base64

            string payload = "dG9rZW5wYXlsb2Fk";
            syncToken = new SyncToken(
                payload, 
                SyncTokenType.DeltaToken);

            serialized = syncToken.Serialize();

            Assert.IsTrue(syncToken.Equals(
                SyncToken.Deserialize(serialized)));

            Assert.IsFalse(
                syncToken.Equals(SyncToken.Deserialize("U2tpcFRva2VufHRva2VucGF5bG9hZA==")));

            Assert.IsFalse(
                syncToken.Equals("a"));
        }

        
    }
}
