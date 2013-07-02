using System;
using System.Web;
using System.Web.Mvc;
using Mersiv.Web.Models;
using Mersiv.Lib.Data;
using Mersiv.Lib.Entity;

namespace Mersiv.Web.Controllers
{
    public class SearchController : Controller
    {

        private IDataRepository dataRepository;
        public SearchController()
        {
            string cs = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["KB-SQLiteDB"].ConnectionString;
            this.dataRepository = new SQLiteDataRepository(System.Web.HttpContext.Current.Server.MapPath(cs));
        }

        public ActionResult Index(string searchString = "", int page = 1)
        {
            if (string.IsNullOrEmpty(searchString))
            {
                return View();
            }
            else
            {
                SearchModel model = new SearchModel();
                model.SearchString = searchString;
                model.Page = page;
                model.PageSize = 10;
                model.List = this.dataRepository.Search(searchString, model.Page, model.PageSize);
                return View("Results", model);
            }
        }

    }
}
