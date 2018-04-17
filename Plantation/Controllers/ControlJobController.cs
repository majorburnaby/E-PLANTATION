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
    public class ControlJobController : Controller
    {
        private ControlJobRepository ICJ = new ControlJobRepository();
        // GET: ControlJob

        public ActionResult Index()
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            return View(ICJ.GetAll());
        }

        //
        // GET: /ControlJob/Details/5
        public ActionResult Details(int? id)
        {

            return View(ICJ.Find(id));
        }

        //
        // GET: /ControlJob/Create
        public ActionResult Create(ControlJob controljob)
        {
            if (ModelState.IsValid)
            {
                ICJ.Add(controljob, Session["userid"].ToString());
            }

            return View(controljob);
        }

        // POST: /ControlJob/Edit/5
        [HttpPost]
        public ActionResult Edit(ControlJob controljob)
        {
            if (ModelState.IsValid)
            {
                ICJ.Update(controljob, Session["userid"].ToString());
            }
            return View(controljob);
        }

        //
        // GET: /ControlJob/Delete/5

        public ActionResult Delete(int id)
        {
            return View(ICJ.Find(id));
        }

        //
        // POST: /ControlJob/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                ICJ.Remove(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}