using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mersiv.Web.Models
{
    public abstract class PagedModel
    {
        public int Page { get; set; }
        public int PageSize { get; set; }

        public int Min
        {
            get
            {
                return (this.Page - 1) * this.PageSize + 1;
            }
        }

        public int Max
        {
            get
            {
                return (this.Page) * this.PageSize;
            }
        }

    }
}