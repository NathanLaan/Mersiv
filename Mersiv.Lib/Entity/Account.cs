using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mersiv.Lib.Entity
{
    public class Account: BaseEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PasswordSalt { get; set; }
        public DateTime Timestamp { get; set; }
        public bool SendEmailNotifications { get; set; }
    }
}
