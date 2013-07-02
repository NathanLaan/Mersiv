using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MarkdownSharp;

namespace Mersiv.Lib.Entity
{
    public class Entry : BaseEntity
    {
        public Nullable<int> ParentID { get; set; }
        //public int ParentID { get; set; } // IF null THEN Entry is top-level entry

        public int AccountID { get; set; }
        public string Title { get; set; }
        public string Contents { get; set; }
        public DateTime Timestamp { get; set; }
        public List<string> TagList { get; set; }



        // Populated field...
        public Account Author { get; set; }

        public int Vote { get; set; }
        public bool AuthorVote { get; set; }
        public bool AuthorVoteUp { get; set; }
        public bool AuthorVoteDown { get; set; }

        public string HtmlContents
        {
            get
            {
                return new Markdown().Transform(this.Contents);
            }
        }

    }
}