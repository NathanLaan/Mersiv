using System;
using System.Collections.Generic;
using System.Linq;
using Mersiv.Lib.Entity;

namespace Mersiv.Web.Models
{
    public class DashboardModel
    {

        public List<WebLink> WebLinkList { get; set; }

        public DashboardModel()
        {
            this.WebLinkList = new List<WebLink>();
        }

    }
}