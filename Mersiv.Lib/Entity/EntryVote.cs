using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mersiv.Lib.Entity
{
    public class EntryVote : BaseEntity
    {
        public int EntryID { get; set; }
        public int AccountID { get; set; }
        public int Vote { get; set; }
    }
}