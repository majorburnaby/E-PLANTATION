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
    public class ContractorGroupController : Controller
    {
        private ContractorGroupRepository CTG = new ContractorGroupRepository();
        ComboBoxContext context = new ComboBoxContext();
        // GET: ContractorGroup
        public ActionResult Index()
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            return View(CTG.GetAll());
        }

        public JsonResult GetControlJobList()
        {
            var users = context.GetControlJob();
            return Json(users, JsonRequestBehavior.AllowGet);
        }

        //
        // GET: /ContractorGroup/Details/5
        public ActionResult Details(int? id)
        {

            return View(CTG.Find(id));
        }

        //
        // GET: /ContractorGroup/Create
        public ActionResult Create(ContractorGroup Contractorgroup, string userid)
        {
            if (ModelState.IsValid)
            {
                CTG.Add(Contractorgroup, Session["userid"].ToString());
            }

            return View(Contractorgroup);
        }

        //
        // GET: /ContractorGroup/Edit/5
        public ActionResult Edit(ContractorGroup Contractorgroup, string userid)
        {
            if (ModelState.IsValid)
            {
                CTG.Update(Contractorgroup, Session["userid"].ToString());
            }
            return View(Contractorgroup);
        }

        //
        // GET: /ContractorGroup/Delete/5
        public ActionResult Delete(int id)
        {
            return View(CTG.Find(id));
        }

        //
        // POST: /ContractorGroup/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                CTG.Remove(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}