using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Plantation.Repository;
using Plantation.Repository.Interface;
using Plantation.Models.DB;

namespace Plantation.Controllers
{
    public class CostCenterController : Controller
    {
        private CostCenterRepository ICC = new CostCenterRepository();
        // GET: CostCenter

        public ActionResult Index()
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            return View(ICC.GetAll());
        }

        //
        // GET: /CostCenter/Details/5
        public ActionResult Details(int? id)
        {

            return View(ICC.Find(id));
        }

        //
        // GET: /CostCenter/Create
        public ActionResult Create(CostCenter CostCenter)
        {
            if (ModelState.IsValid)
            {
                ICC.Add(CostCenter, Session["userid"].ToString());
            }

            return View(CostCenter);
        }

        // POST: /CostCenter/Edit/5
        [HttpPost]
        public ActionResult Edit(CostCenter CostCenter)
        {
            if (ModelState.IsValid)
            {
                ICC.Update(CostCenter, Session["userid"].ToString());
            }
            return View(CostCenter);
        }

        //
        // GET: /CostCenter/Delete/5

        public ActionResult Delete(int id)
        {
            return View(ICC.Find(id));
        }

        //
        // POST: /CostCenter/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                ICC.Remove(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}