//using System;
//using System.Collections.Generic;
//using System.Data.SqlClient;
//using Mersiv.Lib.Entity;

//namespace Mersiv.Lib.Data
//{
//    public class ADODataRepository: IDataRepository
//    {

//        private string connectionString;

//        public ADODataRepository(string connectionString)
//        {
//            this.connectionString = connectionString;
//        }

//        public Entry GetEntry(int id)
//        {
//            throw new NotImplementedException();
//        }

//        public Entry AddEntry(Entity.Entry em)
//        {
//            throw new NotImplementedException();
//        }


//        /// <summary>
//        /// All top-level entries (Entry with no parent).
//        /// </summary>
//        /// <returns></returns>
//        public List<Entry> GetTopLevelEntryList(int page, int pageSize)
//        {
//            List<Entry> entryList = new List<Entry>();
//            return entryList;
//        }

//        public List<Entry> GetEntryListForParent(int parentID)
//        {
//            List<Entry> entryList = new List<Entry>();
//            return entryList;
//        }



//        #region Account

//        private static readonly string SQL_ACCOUNT_INSERT = "INSERT INTO Account (Name,Email,Password,PasswordSalt) VALUES(@name,@email,@password,@passwordSalt); SELECT @@identity;";

//        public Account GetAccount(int id)
//        {
//            throw new NotImplementedException();
//        }
//        public Account GetAccount(string accountName)
//        {
//            throw new NotImplementedException();
//        }

//        public Account AddAccount(Account account)
//        {
//            try
//            {
//                using (SqlConnection sqlConnection = new SqlConnection(this.connectionString))
//                {
//                    sqlConnection.Open();
//                    SqlCommand sqlCommand = new SqlCommand(ADODataRepository.SQL_ACCOUNT_INSERT, sqlConnection);
//                    object returnValue = sqlCommand.ExecuteScalar();

//                    int id = int.Parse(returnValue.ToString());
//                    account.ID = id;
//                }

//                return account;
//            }
//            catch(Exception e)
//            {
//                return null;
//            }
//        }

//        public List<Account> GetAccountList()
//        {
//            return null;
//        }

//        #endregion




//        public EntryVote Add(EntryVote entryVote)
//        {

//            return entryVote;
//        }
//        public EntryVote Get(int entryID, int accountID)
//        {
//            return null;
//        }



//    }
//}
