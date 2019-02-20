namespace Microsoft.RestServices.Tests.Service.QueryAndView
{
    using System;
    using Microsoft.RestServices.Exchange;
    using VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class PageQueryTests
    {
        [TestMethod]
        public void TestPageQueryProperties()
        {
            PageQuery pageQuery = new PageQuery(12, 2);
            
            Assert.AreEqual(
                12, 
                pageQuery.Offset);

            Assert.AreEqual(
                2, 
                pageQuery.PageSize);

            Assert.AreEqual(
                "$top=2&$skip=12", 
                pageQuery.Query);

            Assert.ThrowsException<ArgumentOutOfRangeException>(() => {
                new  PageQuery(
                    -1, 
                    10);
            });

            Assert.ThrowsException<ArgumentOutOfRangeException>(() => {
                new PageQuery(
                    0,
                    51);
            });

            Assert.ThrowsException<ArgumentOutOfRangeException>(() => {
                new PageQuery(
                    0,
                    0);
            });

            Assert.AreEqual(
                5, 
                (new PageQuery()).PageSize);
        }
    }
}
