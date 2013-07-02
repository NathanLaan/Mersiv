using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mersiv.Lib.Entity
{
    class WebLink
    {
        public int ID { get; set; }
        public int AccountID { get; set; }
        public string Title { get; set; }
        public string URL { get; set; }
        public string IconURL { get; set; }
        public int ClickCount { get; set; }
        public int OtherUserClickCount { get; set; }
    }
}
