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
    public class UnitOfMeasureController : Controller
    {
        private UnitOfMeasureRepository UOM = new UnitOfMeasureRepository();
        // GET: UnitOfMeasure
        public ActionResult Index()
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            return View(UOM.GetAll());
        }

        //
        // GET: /UnitOfMeasure/Details/5
        public ActionResult Details(int? id)
        {

            return View(UOM.Find(id));
        }

        //
        // POST: /UnitOfMeasure/Create
        [HttpPost]
        public ActionResult Create(UnitOfMeasure unitofmeasure, string userid)
        {
            if (ModelState.IsValid)
            {
                UOM.Add(unitofmeasure, Session["userid"].ToString());
            }
            return View(unitofmeasure);
        }
        
        // POST: /UnitOfMeasure/Edit/5
        [HttpPost]
        public ActionResult Edit(UnitOfMeasure unitofmeasure, string userid)
        {
            if (ModelState.IsValid)
            {
                UOM.Update(unitofmeasure, Session["userid"].ToString());
            }
            return View(unitofmeasure);
        }

        //
        // GET: /UnitOfMeasure/Delete/5

        public ActionResult Delete(int id)
        {
            return View(UOM.Find(id));
        }

        //
        // POST: /UnitOfMeasure/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                UOM.Remove(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}