using System;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using Mersiv.Lib.Entity;

namespace Mersiv.Web.Models
{
    public class AccountManageModel
    {

        /// <summary>
        /// For display purposes only.
        /// </summary>

        public string Name { get; set; }

        public string Email { get; set; }

        [Required]
        [Display(Name = "Current Password")]
        public string PasswordOld { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "New Password")]
        [StringLength(100, ErrorMessage = "The password must be at least {2} characters long.", MinimumLength = 6)]
        public string PasswordNew { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm your new password")]
        [Compare("PasswordNew", ErrorMessage = "The password and confirm password must be the same.")]
        public string PasswordNewConfirm { get; set; }

        public bool SendEmailNotifications { get; set; }

    }
}