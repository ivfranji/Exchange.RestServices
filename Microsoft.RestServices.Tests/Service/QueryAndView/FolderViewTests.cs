namespace Microsoft.RestServices.Tests.Service.QueryAndView
{
    using System;
    using Microsoft.RestServices.Exchange;
    using VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class FolderViewTests
    {
        [TestMethod]
        public void TestFolderViewProperties()
        {
            FolderView folderView = new FolderView(10);
            Assert.AreEqual(
                "$top=10&$skip=0", 
                folderView.ViewQuery.Query);

            Assert.ThrowsException<ArgumentException>(() =>
            {
                folderView.SelectProperty("Non-ExistingProperty");
            });

            folderView.SelectProperty("DisplayName");
            Assert.AreEqual(
                "$top=10&$skip=0&$select=DisplayName",
                folderView.ViewQuery.Query);

            // Adding same property shouldn't change select
            folderView.SelectProperty("DisplayName");
            Assert.AreEqual(
                "$top=10&$skip=0&$select=DisplayName",
                folderView.ViewQuery.Query);

            folderView.SelectProperty(new []{ "ChildFolderCount", "ParentFolderId" });
            Assert.AreEqual(
                "$top=10&$skip=0&$select=DisplayName,ChildFolderCount,ParentFolderId",
                folderView.ViewQuery.Query);

            // Adding small letters to collection shouldn't change it
            folderView.SelectProperty(new[] { "childFoldercount", "parentFolderId" });
            Assert.AreEqual(
                "$top=10&$skip=0&$select=DisplayName,ChildFolderCount,ParentFolderId",
                folderView.ViewQuery.Query);

            Assert.AreEqual(
                10, 
                folderView.PageSize);

            Assert.AreEqual(
                0, 
                folderView.Offset);

            folderView.Offset = 12;
            folderView.PageSize = 44;

            Assert.AreEqual(
                "$top=44&$skip=12&$select=DisplayName,ChildFolderCount,ParentFolderId",
                folderView.ViewQuery.Query);

            folderView = new FolderView(
                16, 
                13);

            Assert.AreEqual(
                "$top=16&$skip=13",
                folderView.ViewQuery.Query);
        }
    }
}
