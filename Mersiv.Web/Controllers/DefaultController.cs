using System;
using System.Web;
using System.Web.Mvc;
using Mersiv.Web.Models;
using Mersiv.Lib.Data;

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
            model.TotalEntryCount = this.dataRepository.GetTotalEntryCount();
            model.TotalReplyCount = this.dataRepository.GetTotalReplyCount();
            model.TotalUsersCount = this.dataRepository.GetTotalUsersCount();
            model.TotalVotesCount = this.dataRepository.GetTotalVotesCount();
            model.LatestEntryList = this.dataRepository.GetLatestEntryList();
            model.PopularEntryList = this.dataRepository.GetPopularEntryList();
            model.ActiveUserList = this.dataRepository.GetActiveAccountList();
            return View(model);
        }

    }
}
