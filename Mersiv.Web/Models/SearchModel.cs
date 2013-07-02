using System;
using System.Collections.Generic;
using Mersiv.Lib.Entity;

namespace Mersiv.Web.Models
{

    public class SearchModel : PagedModel
    {
        public string SearchString { get; set; }
        public List<Entry> List { get; set; }
        public SearchModel()
        {
            this.List = new List<Entry>();
        }
    }

}