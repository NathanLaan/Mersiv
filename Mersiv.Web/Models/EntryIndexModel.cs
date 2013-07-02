using System;
using System.Collections.Generic;
using Mersiv.Lib.Entity;

namespace Mersiv.Web.Models
{
    public class EntryIndexModel
    {

        public int Page { get; set; }

        public int PageSize { get; set; }

        public List<Entry> List { get; set; }

        public EntryIndexModel()
        {
            this.List = new List<Entry>();
        }

        public int EntryMin
        {
            get
            {
                return (this.Page - 1) * this.PageSize + 1;
            }
        }

        public int EntryMax
        {
            get
            {
                return (this.Page) * this.PageSize;
            }
        }

    }
}