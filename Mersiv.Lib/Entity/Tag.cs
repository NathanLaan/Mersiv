using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mersiv.Lib.Entity
{
    public class Tag : BaseEntity
    {

        public string Name { get; set; }

        public Tag(string name)
        {
            this.Name = name;
        }

    }
}
