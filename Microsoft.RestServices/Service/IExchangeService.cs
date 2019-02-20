namespace Microsoft.RestServices.Exchange
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Graph;

    /// <summary>
    /// Exchange rest service.
    /// </summary>
    public interface IExchangeService : IRestService
    {
        /// <summary>
        /// Base REST uri.
        /// </summary>
        Uri Url { get; }

        /// <summary>
        /// Trace enabled.
        /// </summary>
        bool TraceEnabled { get; set; }

        /// <summary>
        /// Trace flags.
        /// </summary>
        TraceFlags TraceFlags { get; set; }

        /// <summary>
        /// Trace listener.
        /// </summary>
        ITraceListener TraceListener { get; set; }

        /// <summary>
        /// Http response headers.
        /// </summary>
        IDictionary<string, string> HttpResponseHeaders { get; }

        /// <summary>
        /// MailboxId.
        /// </summary>
        MailboxId MailboxId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="traceFlags"></param>
        /// <param name="message"></param>
        void Trace(TraceFlags traceFlags, string message);

        /// <summary>
        /// Search for an items within folder.
        /// </summary>
        /// <returns></returns>
        FindItemsResults<OutlookItem> FindItems(FolderId parentFolderId, ViewBase itemView);

        /// <summary>
        /// Search for an items within folder.
        /// </summary>
        /// <returns></returns>
        FindItemsResults<OutlookItem> FindItems(FolderId parentFolderId, SearchFilter searchFilter, ViewBase itemView);

        /// <summary>
        /// Search for an items within folder.
        /// </summary>
        /// <returns></returns>
        FindItemsResults<OutlookItem> FindItems(WellKnownFolderName wellKnownFolderName, ViewBase itemView);

        /// <summary>
        /// Search for an items within folder.
        /// </summary>
        /// <returns></returns>
        FindItemsResults<OutlookItem> FindItems(WellKnownFolderName wellKnownFolderName, SearchFilter searchFilter, ViewBase itemView);

        /// <summary>
        /// Search for an items within folder.
        /// </summary>
        /// <returns></returns>
        FindFoldersResults FindFolders(FolderId parentFolderId, FolderView folderView);

        /// <summary>
        /// Search for an items within folder.
        /// </summary>
        /// <returns></returns>
        FindFoldersResults FindFolders(FolderId parentFolderId, SearchFilter searchFilter, FolderView folderView);

        /// <summary>
        /// Search for an items within folder.
        /// </summary>
        /// <returns></returns>
        FindFoldersResults FindFolders(WellKnownFolderName wellKnownFolderName, FolderView folderView);

        /// <summary>
        /// Search for an items within folder.
        /// </summary>
        /// <returns></returns>
        FindFoldersResults FindFolders(WellKnownFolderName wellKnownFolderName, SearchFilter searchFilter, FolderView folderView);
    }
}