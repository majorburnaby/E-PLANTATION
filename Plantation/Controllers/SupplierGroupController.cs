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
    public class SupplierGroupController : Controller
    {
        private SupplierGroupRepository SPG = new SupplierGroupRepository();
        ComboBoxContext context = new ComboBoxContext();
        // GET: SupplierGroup
        public ActionResult Index()
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            return View(SPG.GetAll());
        }

        public JsonResult GetControlJobList()
        {
            var users = context.GetControlJob();
            return Json(users, JsonRequestBehavior.AllowGet);
        }

        //
        // GET: /SupplierGroup/Details/5
        public ActionResult Details(int? id)
        {

            return View(SPG.Find(id));
        }

        //
        // GET: /SupplierGroup/Create
        public ActionResult Create(SupplierGroup Suppliergroup, string userid)
        {
            if (ModelState.IsValid)
            {
                SPG.Add(Suppliergroup, Session["userid"].ToString());
            }

            return View(Suppliergroup);
        }

        //
        // GET: /SupplierGroup/Edit/5
        public ActionResult Edit(SupplierGroup Suppliergroup, string userid)
        {
            if (ModelState.IsValid)
            {
                SPG.Update(Suppliergroup, Session["userid"].ToString());
            }
            return View(Suppliergroup);
        }

        //
        // GET: /SupplierGroup/Delete/5
        public ActionResult Delete(int id)
        {
            return View(SPG.Find(id));
        }

        //
        // POST: /SupplierGroup/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                SPG.Remove(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}