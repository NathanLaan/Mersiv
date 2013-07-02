using System;
using System.Collections.Generic;
using System.Linq;
using Mersiv.Lib.Entity;

namespace Mersiv.Web.Models
{
    public class DashboardModel
    {


        public int TotalUsersCount { get; set; }
        public int TotalEntryCount { get; set; }
        public int TotalReplyCount { get; set; }
        public int TotalVotesCount { get; set; }

        public List<Entry> LatestEntryList { get; set; }
        public List<Entry> PopularEntryList { get; set; }
        public List<Account> ActiveUserList { get; set; }

        public DashboardModel()
        {
            this.LatestEntryList = new List<Entry>();
            this.PopularEntryList = new List<Entry>();
            this.ActiveUserList = new List<Account>();
        }

    }
}