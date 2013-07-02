using System;
using System.Collections.Generic;
using Mersiv.Lib.Entity;

namespace Mersiv.Web.Models
{
    public class EntryResponseModel
    {
        public Entry Entry { get; set; }

        public EntryResponseModel()
        {
            this.Entry = new Entry();
        }
    }
}