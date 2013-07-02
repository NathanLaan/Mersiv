using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Mersiv.Web.Models;
using Mersiv.Lib.Data;
using Mersiv.Lib.Entity;

namespace Mersiv.Web.Controllers
{
    public class EntryController : Controller
    {

        private IDataRepository dataRepository;
        public EntryController()
        {
            //
            // TODO: Factory
            //
            string cs = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["KB-SQLiteDB"].ConnectionString;
            this.dataRepository = new SQLiteDataRepository(System.Web.HttpContext.Current.Server.MapPath(cs));
        }


        public ActionResult EntryResponsePartial(int parentID, string parentTitle)
        {
            EntryResponseModel model = new EntryResponseModel();
            model.Entry = new Entry();
            model.Entry.ParentID = parentID;
            model.Entry.Title = parentTitle;
            return View(model);
        }
        [HttpPost]
        public ActionResult EntryResponsePartial(EntryResponseModel model)
        {
            if (ModelState.IsValid)
            {
                if (Request.IsAuthenticated)
                {
                    try
                    {
                        //
                        // TODO: Get Parent Title
                        //
                        model.Entry.AccountID = GetFormsAuthenticationID();
                        model.Entry.Timestamp = DateTime.Now;

                        if (model.Entry.ParentID != null)
                        {
                            Entry parentEntry = this.dataRepository.GetEntry(model.Entry.ParentID.Value);

                            //if (parentEntry != null)
                            {
                                //model.Entry.Title = "RE: " + parentEntry.Title;
                                model.Entry.Title = "RE: " + model.Entry.Title;
                                model.Entry = this.dataRepository.Add(model.Entry);
                                if (model.Entry != null && model.Entry.ID >= 0)
                                {
                                    return RedirectToAction("Details", "Entry", new { id = model.Entry.ParentID });
                                }
                                else
                                {
                                    ModelState.AddModelError("", "Unable to create entry");
                                }
                            }
                            //else
                            {
                                //
                                // TODO: Parent model does not exist!
                                //
                            }
                        }
                        else
                        {
                            //
                            // TODO: ParentID null!
                            //
                        }
                    }
                    catch
                    {
                        return View();
                    }
                }
            }
            return RedirectToAction("Details", "Entry", new { id = model.Entry.ParentID });
        }


        public ActionResult Index(int id = 1)
        {
            EntryIndexModel model = new EntryIndexModel();
            model.Page = id;
            model.PageSize = 10;
            model.List = this.dataRepository.GetTopLevelEntryList(model.Page, model.PageSize);
            return View(model);
        }
        public ActionResult Page(int id = 1)
        {
            EntryIndexModel model = new EntryIndexModel();
            model.Page = id;
            model.PageSize = 10;
            model.List = this.dataRepository.GetTopLevelEntryList(model.Page, model.PageSize);
            return View(model);
        }

        //
        // GET: /Entry/Details/5

        public ActionResult Details(int id)
        {
            EntryDetailsModel model = new EntryDetailsModel();
            model.Entry = this.dataRepository.GetEntry(id);
            model.ResponseList = this.dataRepository.GetEntryListForParentWithVotes(id, this.GetFormsAuthenticationID());
            return View(model);
        }

        private int GetFormsAuthenticationID()
        {
            try
            {
                FormsIdentity id = (FormsIdentity)User.Identity;
                FormsAuthenticationTicket ticket = id.Ticket;
                return int.Parse(ticket.UserData);
            }
            catch
            {
                return -999;
            }
        }

        //
        // GET: /Entry/Add
        [Authorize]
        public ActionResult Add(EntryModel model)
        {
            if (ModelState.IsValid)
            {
                if (Request.IsAuthenticated)
                {
                    try
                    {
                        // TODO: Add insert logic here
                        Entry entry = new Entry();
                        entry.ParentID = null;
                        entry.AccountID = GetFormsAuthenticationID();
                        entry.Title = model.Title;
                        entry.Contents = model.Contents;
                        entry.Timestamp = DateTime.Now;
                        entry = this.dataRepository.Add(entry);



                        if (entry != null && entry.ID >= 0)
                        {
                            #region Tags
                            string[] tags = model.TagListString.Split(new char[] { ' ', ',' });
                            List<Tag> tagList = new List<Tag>();
                            foreach (string tag in tags)
                            {
                                tagList.Add(new Tag(tag.ToLower()));
                            }
                            this.dataRepository.AddTagListForEntry(entry.ID, tagList);
                            #endregion

                            return RedirectToAction("Details", "Entry", new { id = entry.ID });
                        }
                        else
                        {
                            ModelState.AddModelError("", "Unable to create entry");
                        }

                        return RedirectToAction("Add", "Entry");
                    }
                    catch
                    {
                        return View();
                    }
                }
            }
            return View();
        }

        //
        // GET: /Entry/Edit/5
        [Authorize]
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Entry/Edit/5
        [Authorize]
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Entry/Delete/5
        [Authorize]
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Entry/Delete/5
        [Authorize]
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
