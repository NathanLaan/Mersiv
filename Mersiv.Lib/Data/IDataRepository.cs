using System;
using System.Collections.Generic;
using Mersiv.Lib.Entity;

namespace Mersiv.Lib.Data
{
    public interface IDataRepository
    {


        WebLink Add(WebLink webLink);

        List<WebLink> GetListForAccount(int accountID);


        #region Tag

        /// <summary>
        /// Get the list of all Tags in the system.
        /// </summary>
        /// <returns></returns>
        List<Tag> GetTagList();

        List<Entry> GetEntryListForTag(string tagName);

        /// <summary>
        /// Add the list of tags to the specified Entry. Checks if tags allready exist, and inserts new Tags as needed.
        /// </summary>
        /// <param name="entryID"></param>
        /// <param name="tagList"></param>
        void AddTagListForEntry(int entryID, List<Tag> tagList);

        #endregion Tag


        List<Entry> Search(string searchString, int page, int pageSize);


        /// <summary>
        /// Return the value passed in, with the ID filled in. Do I really need this, or just following the pattern???
        /// </summary>
        /// <param name="entryVote"></param>
        /// <returns></returns>
        EntryVote Add(EntryVote entryVote);

        /// <summary>
        /// Used to check if a user has already voted on an Entry.
        /// </summary>
        /// <param name="entryID"></param>
        /// <param name="accountID"></param>
        /// <returns></returns>
        EntryVote GetEntryVote(int entryID, int accountID);


        #region Entry

        Entry Add(Entry entry);

        Entry GetEntry(int id);


        /// <summary>
        /// All top-level entries (Entry with no parent).
        /// </summary>
        /// <returns></returns>
        List<Entry> GetTopLevelEntryList(int page, int pageSize);

        List<Entry> GetEntryListForParent(int parentID);
        List<Entry> GetEntryListForParentWithVotes(int parentID, int accountID);

        #endregion


        #region Account

        void Update(Account account);

        /// <summary>
        /// Creates the account, and populates the ID field.
        /// </summary>
        /// <param name="account">The account with populated ID field.</param>
        /// <returns></returns>
        Account Add(Account account);

        Account GetAccount(int id);

        Account GetAccount(string accountName);

        Account GetAccountForEmail(string email);

        List<Account> GetAccountList();

        List<Account> GetAccountListPaged(int page, int pageSize);

        #endregion


        //
        // Dashboard
        //

        List<Entry> GetLatestEntryList();
        List<Entry> GetPopularEntryList();
        List<Account> GetActiveAccountList();

        int GetTotalUsersCount();
        int GetTotalEntryCount();
        int GetTotalReplyCount();
        int GetTotalVotesCount();
        int GetTotalEntryCount(int accountID);
        int GetTotalReplyCount(int accountID);
        int GetTotalVotesCount(int accountID);

    }
}
