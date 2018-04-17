using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Plantation.Repository;
using Plantation.Repository.Interface;
using Plantation.Models.DB;
using Plantation.Models;

namespace Plantation.Controllers
{
    public class TransporterGroupController : Controller
    {
        private TransporterGroupRepository ITG = new TransporterGroupRepository();
        ComboBoxContext context = new ComboBoxContext();
        // GET: TransporterGroup
        public ActionResult Index()
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            return View(ITG.GetAll());
        }

        public JsonResult GetControlJobList()
        {
            var users = context.GetControlJob();
            return Json(users, JsonRequestBehavior.AllowGet);
        }

        //
        // GET: /TransporterGroup/Details/5
        public ActionResult Details(int? id)
        {

            return View(ITG.Find(id));
        }

        //
        // GET: /TransporterGroup/Create
        public ActionResult Create(TransporterGroup transportergroup, string userid)
        {
            if (ModelState.IsValid)
            {
                ITG.Add(transportergroup, Session["userid"].ToString());
            }

            return View(transportergroup);
        }

        // POST: /TransporterGroup/Edit/5
        [HttpPost]
        public ActionResult Edit(TransporterGroup transportergroup, string userid)
        {
            if (ModelState.IsValid)
            {
                ITG.Update(transportergroup, Session["userid"].ToString());
            }
            return View(transportergroup);
        }

        //
        // GET: /TransporterGroup/Delete/5

        public ActionResult Delete(int id)
        {
            return View(ITG.Find(id));
        }

        //
        // POST: /TransporterGroup/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                ITG.Remove(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}