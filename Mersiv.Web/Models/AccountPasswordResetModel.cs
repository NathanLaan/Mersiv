using System;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Mersiv.Web.Models
{
    public class AccountPasswordResetModel
    {

        [Required]
        [Display(Name = "Account Name or Email Address")]
        public string AccountNameOrEmail { get; set; }

    }
}