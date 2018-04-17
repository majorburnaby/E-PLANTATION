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
    public class StockGroupController : Controller
    {
        private StockGroupRepository STG = new StockGroupRepository();
        ComboBoxContext context = new ComboBoxContext();
        // GET: StockGroup
        public ActionResult Index()
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            return View(STG.GetAll());
        }

        public JsonResult GetControlJobList()
        {
            var users = context.GetControlJob();
            return Json(users, JsonRequestBehavior.AllowGet);
        }

        //
        // GET: /StockGroup/Details/5
        public ActionResult Details(int? id)
        {

            return View(STG.Find(id));
        }

        //
        // GET: /StockGroup/Create
        public ActionResult Create(StockGroup stockgroup, string userid)
        {
            if (ModelState.IsValid)
            {
                STG.Add(stockgroup, Session["userid"].ToString());
            }

            return View(stockgroup);
        }

        //
        // GET: /StockGroup/Edit/5
        public ActionResult Edit(StockGroup stockgroup, string userid)
        {
            if (ModelState.IsValid)
            {
                STG.Update(stockgroup, Session["userid"].ToString());
            }
            return View(stockgroup);
        }

        //
        // GET: /StockGroup/Delete/5
        public ActionResult Delete(int id)
        {
            return View(STG.Find(id));
        }

        //
        // POST: /StockGroup/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                STG.Remove(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}