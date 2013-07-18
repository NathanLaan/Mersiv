using System;
using System.Web;
using System.Web.Mvc;
using Mersiv.Web.Models;
using Mersiv.Lib.Data;
using System.Web.Security;

namespace Mersiv.Web.Controllers
{
    public class DefaultController : Controller
    {

        private IDataRepository dataRepository;
        public DefaultController()
        {
            //
            // TODO: Factory
            //
            string cs = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["KB-SQLiteDB"].ConnectionString;
            this.dataRepository = new SQLiteDataRepository(System.Web.HttpContext.Current.Server.MapPath(cs));
        }

        public ActionResult Index()
        {
            DashboardModel model = new DashboardModel();
            if (User.Identity.IsAuthenticated)
            {
                model.WebLinkList = this.dataRepository.GetListForAccount(GetFormsAuthenticationID());
            }
            else
            {
                //TODO
            }
            return View(model);
        }


        private int GetFormsAuthenticationID()
        {
            FormsIdentity id = (FormsIdentity)User.Identity;
            FormsAuthenticationTicket ticket = id.Ticket;
            return int.Parse(ticket.UserData);
        }

    }
}
