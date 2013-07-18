using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Mersiv.Web.Models;
using Mersiv.Lib.Data;
using Mersiv.Lib.Entity;
using Mersiv.Lib.Utility;
//AccountList:
using System.Web.Script.Serialization;

namespace Mersiv.Web.Controllers
{

    //
    // http://www.asp.net/mvc/tutorials/older-versions/security/authenticating-users-with-forms-authentication-cs
    //
    //
    public class AccountController : Controller
    {

        private IDataRepository dataRepository;

        public AccountController()
        {
            //
            // TODO: Factory
            //
            string cs = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["SQLiteDB"].ConnectionString;
            this.dataRepository = new SQLiteDataRepository(System.Web.HttpContext.Current.Server.MapPath(cs));
        }

        public ActionResult Index(int id = 1)
        {
            AccountIndexModel model = new AccountIndexModel();
            model.Page = id;
            model.PageSize = 10;
            model.List = this.dataRepository.GetAccountListPaged(model.Page, model.PageSize);
            return View(model);
        }
        public ActionResult Page(int id = 1)
        {
            AccountIndexModel model = new AccountIndexModel();
            model.Page = id;
            model.PageSize = 10;
            model.List = this.dataRepository.GetAccountListPaged(model.Page, model.PageSize);
            return View(model);
        }

        public ActionResult Details(int id)
        {
            AccountDetailsModel model = new AccountDetailsModel();
            model.Account = this.dataRepository.GetAccount(id);
            model.TotalEntryCount = this.dataRepository.GetTotalEntryCount(id);
            model.TotalReplyCount = this.dataRepository.GetTotalReplyCount(id);
            model.TotalVotesCount = this.dataRepository.GetTotalVotesCount(id);
            return View(model);
        }

        [Authorize]
        public ActionResult Manage()
        {
            int id = this.GetFormsAuthenticationID();
            Account account = this.dataRepository.GetAccount(id);
            AccountManageModel model = new AccountManageModel();
            model.Name = account.Name;
            model.Email = account.Email;
            model.SendEmailNotifications = account.SendEmailNotifications;
            return View(model);
        }
        [Authorize]
        [HttpPost]
        public ActionResult Manage(AccountManageModel model)
        {
            if (!string.IsNullOrEmpty(model.PasswordOld))
            {
                int id = this.GetFormsAuthenticationID();
                Account account = this.dataRepository.GetAccount(id);

                string encryptedPasswordNew = SecurityUtil.GenerateEncryptedPassword(model.PasswordOld, account.PasswordSalt).Password;

                if (account.Password == encryptedPasswordNew)
                {
                    if (!string.IsNullOrEmpty(model.PasswordNew) && model.PasswordNew == model.PasswordNewConfirm)
                    {
                        // create new encrypted password using the same SALT
                        account.Password = SecurityUtil.GenerateEncryptedPassword(model.PasswordNew, account.PasswordSalt).Password;
                    }
                    this.dataRepository.Update(account);

                }
                else
                {
                    //
                    // TODO: display error message
                    //
                }
            }
            else
            {
                //
                // TODO: display error message
                //
            }


            //
            // TODO: update account
            //
            return View(model);
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Default");
        }

        //
        // Not used
        //
        public ActionResult AccoutList(string sidx, string sord, int page, int rows)
        {
            List<Account> accountList = this.dataRepository.GetAccountList();
            var jsonData = new
            {
                total = 1,
                page = page,
                records = 3,
                rows = accountList
            };
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }


        public ActionResult Login(AccountLoginModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                Account account = this.dataRepository.GetAccount(model.Username);
                if (account != null)
                {

                    //
                    // TODO: check password
                    //
                    SecurityUtil.EncryptedPassword encryptedPassword = SecurityUtil.GenerateEncryptedPassword(model.Password, account.PasswordSalt);

                    if (encryptedPassword.Password == account.Password)
                    {

                        this.Login(account);
                        if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                            && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                        {
                            return Redirect(returnUrl);
                        }
                        else
                        {
                            return RedirectToAction("Index", "Default");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "The username or password is incorrect.");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "The username or password is incorrect.");
                }
            }
            else
            {
                ModelState.AddModelError("", "Invalid model state.");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }


        private void Login(Account account)
        {
            if (account != null && account.ID >= 0)
            {
                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1,
                    account.Name,
                    DateTime.Now,
                    DateTime.Now.AddDays(30),
                    true,
                    account.ID.ToString(),
                    FormsAuthentication.FormsCookiePath);

                // Encrypt the ticket.
                string encTicket = FormsAuthentication.Encrypt(ticket);

                // Create the cookie.
                Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encTicket));
            }
        }


        private int GetFormsAuthenticationID()
        {
            FormsIdentity id = (FormsIdentity)User.Identity;
            FormsAuthenticationTicket ticket = id.Ticket;
            return int.Parse(ticket.UserData);
        }


        public ActionResult Register(AccountRegisterModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    Account account = model.ToAccount();
                    SecurityUtil.EncryptedPassword encryptedPassword = SecurityUtil.GenerateEncryptedPassword(account.Password);
                    account.Password = encryptedPassword.Password;
                    account.PasswordSalt = encryptedPassword.PasswordSalt;
                    account.Timestamp = DateTime.Now;
                    account = this.dataRepository.Add(account);

                    if (account != null && account.ID >= 0)
                    {
                        //FormsAuthentication.SetAuthCookie(account.Name, true);
                        this.Login(account);

                        //todo: send email notification
                        MailUtil.SendMail(
                            Properties.Settings.Default.MailServer,
                            Properties.Settings.Default.MailPort,
                            Properties.Settings.Default.MailUsername,
                            Properties.Settings.Default.MailPassword,
                            Properties.Settings.Default.MailUsername,
                            "Mersiv",
                            account.Email,
                            account.Name,
                            "Mersiv Account Created",
                            "Mersiv Account Created: " + account.Name + " - " + account.Email,
                            true);
                        
                        return RedirectToAction("Index", "Default");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Unable to create account");
                    }
                }
                catch (Exception e)
                {
                    //
                    // TODO: TEMP!
                    //
                    ModelState.AddModelError("", e.ToString());
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }


        public ActionResult PasswordResetConfirm(AccountPasswordResetModel model)
        {
            return View(model);
        }

        public ActionResult PasswordReset()
        {
            return View();
        }

        [HttpPost]
        public ActionResult PasswordReset(AccountPasswordResetModel model)
        {
            //
            // TODO: Create new password, and email username and password to the user.
            // TODO: display confirmation message to the user
            //

            try
            {

                Account account = this.dataRepository.GetAccount(model.AccountNameOrEmail);

                if (account == null)
                {
                    account = this.dataRepository.GetAccountForEmail(model.AccountNameOrEmail);
                }

                if (account != null)
                {
                    // generate new password
                    string newPassword = SecurityUtil.GenerateRandomPassword(6);

                    SecurityUtil.EncryptedPassword encryptedPassword = SecurityUtil.GenerateEncryptedPassword(newPassword);
                    account.Password = encryptedPassword.Password;
                    account.PasswordSalt = encryptedPassword.PasswordSalt;

                    this.dataRepository.Update(account);

                    MailUtil.SendMail(
                        Properties.Settings.Default.MailServer,
                        Properties.Settings.Default.MailPort,
                        Properties.Settings.Default.MailUsername,
                        Properties.Settings.Default.MailPassword,
                        Properties.Settings.Default.MailUsername,
                        "Mersiv",
                        account.Email,
                        account.Name,
                        "Mersiv Account Password Reset",
                        "A password reset was requested for your account <b>" + account.Name + "</b>"
                            + "<br/><br/>Your password has been reset: " 
                            + newPassword 
                            + System.Environment.NewLine 
                            + System.Environment.NewLine 
                            + "<br/><br/>" + Properties.Settings.Default.BaseUrl + "/users/login",
                        true);

                    // TODO: redirect user to confirmation message


                    return RedirectToAction("PasswordResetConfirm", "Account", new { accountInfo = model.AccountNameOrEmail });
                }
                else
                {
                    //
                    // TODO: if account is still NULL then display error message to user.
                    //
                }

            }
            catch(Exception exception)
            {
                //
                // TODO: Logging? Recovery?
                //
            }

            return View();
        }

    }
}
