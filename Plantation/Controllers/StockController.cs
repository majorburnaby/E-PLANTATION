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
    public class StockController : Controller
    {
        private StockRepository STK = new StockRepository();
        ComboBoxContext context = new ComboBoxContext();
        // GET: Stock
        public ActionResult Index()
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            return View(STK.GetAll());
        }

        public JsonResult GetStockGroupList()
        {
            var users = context.GetStockGroup();
            return Json(users, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetUnitOfMeasureList()
        {
            var users = context.GetUnitOfMeasure();
            return Json(users, JsonRequestBehavior.AllowGet);
        }

        //
        // GET: /Stock/Details/5
        public ActionResult Details(int? id)
        {

            return View(STK.Find(id));
        }

        //
        // GET: /Stock/Create
        public ActionResult Create(Stock stock, string userid)
        {
            if (ModelState.IsValid)
            {
                STK.Add(stock, Session["userid"].ToString());
            }

            return View(stock);
        }

        //
        // GET: /Stock/Edit/5
        public ActionResult Edit(Stock stock, string userid)
        {
            if (ModelState.IsValid)
            {
                STK.Update(stock, Session["userid"].ToString());
            }
            return View(stock);
        }

        //
        // GET: /Stock/Delete/5
        public ActionResult Delete(int id)
        {
            return View(STK.Find(id));
        }

        //
        // POST: /Stock/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                STK.Remove(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}