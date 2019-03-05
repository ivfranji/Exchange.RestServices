namespace Exchange.RestServices.Tests.Service.QueryAndView
{
    using System;
    using Exchange.RestServices;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

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
                folderView.PropertySet.Add("Non-ExistingProperty");
            });

            folderView.PropertySet.Add("DisplayName");
            Assert.AreEqual(
                "$top=10&$skip=0&$select=Id,ChildFolderCount,DisplayName,TotalItemCount",
                folderView.ViewQuery.Query);

            // Adding same property shouldn't change select
            folderView.PropertySet.Add("DisplayName");
            Assert.AreEqual(
                "$top=10&$skip=0&$select=Id,ChildFolderCount,DisplayName,TotalItemCount",
                folderView.ViewQuery.Query);

            folderView.PropertySet.Add(new[] { "ChildFolderCount", "ParentFolderId" });
            Assert.AreEqual(
                "$top=10&$skip=0&$select=Id,ChildFolderCount,DisplayName,TotalItemCount,ParentFolderId",
                folderView.ViewQuery.Query);

            // Adding small letters to collection shouldn't change it
            folderView.PropertySet.Add(new[] { "childFoldercount", "parentFolderId" });
            Assert.AreEqual(
                "$top=10&$skip=0&$select=Id,ChildFolderCount,DisplayName,TotalItemCount,ParentFolderId",
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
                "$top=44&$skip=12&$select=Id,ChildFolderCount,DisplayName,TotalItemCount,ParentFolderId",
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
