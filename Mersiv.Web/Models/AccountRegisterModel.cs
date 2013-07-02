using System;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using Mersiv.Lib.Entity;

namespace Mersiv.Web.Models
{
    public class AccountRegisterModel
    {

        public static readonly int MinPasswordLength = System.Web.Security.Membership.MinRequiredPasswordLength;

        [Required]
        [Display(Name = "User name")]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email address")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The password must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirm password must be the same.")]
        public string ConfirmPassword { get; set; }

        public Account ToAccount()
        {
            Account account = new Account();
            account.Name = this.Username;
            account.Email = this.Email;
            account.Password = this.Password;
            return account;
        }

    }
}