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
    public class CustomerGroupController : Controller
    {
        private CustomerGroupRepository CSG = new CustomerGroupRepository();
        ComboBoxContext context = new ComboBoxContext();
        // GET: CustomerGroup
        public ActionResult Index()
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            return View(CSG.GetAll());
        }

        public JsonResult GetControlJobList()
        {
            var users = context.GetControlJob();
            return Json(users, JsonRequestBehavior.AllowGet);
        }

        //
        // GET: /CustomerGroup/Details/5
        public ActionResult Details(int? id)
        {

            return View(CSG.Find(id));
        }

        //
        // GET: /CustomerGroup/Create
        public ActionResult Create(CustomerGroup Customergroup, string userid)
        {
            if (ModelState.IsValid)
            {
                CSG.Add(Customergroup, Session["userid"].ToString());
            }

            return View(Customergroup);
        }

        //
        // GET: /CustomerGroup/Edit/5
        public ActionResult Edit(CustomerGroup Customergroup, string userid)
        {
            if (ModelState.IsValid)
            {
                CSG.Update(Customergroup, Session["userid"].ToString());
            }
            return View(Customergroup);
        }

        //
        // GET: /CustomerGroup/Delete/5
        public ActionResult Delete(int id)
        {
            return View(CSG.Find(id));
        }

        //
        // POST: /CustomerGroup/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                CSG.Remove(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}