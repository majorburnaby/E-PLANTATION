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
    public class StoreController : Controller
    {
        private StoreRepository STR = new StoreRepository();
        ComboBoxContext context = new ComboBoxContext();
        // GET: Store
        public ActionResult Index()
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            return View(STR.GetAll());
        }

        //
        // GET: /Store/Details/5
        public ActionResult Details(int? id)
        {

            return View(STR.Find(id));
        }

        //
        // GET: /Store/Create
        public ActionResult Create(Store Store, string userid)
        {
            if (ModelState.IsValid)
            {
                STR.Add(Store, Session["userid"].ToString());
            }

            return View(Store);
        }

        //
        // GET: /Store/Edit/5
        public ActionResult Edit(Store Store, string userid)
        {
            if (ModelState.IsValid)
            {
                STR.Update(Store, Session["userid"].ToString());
            }
            return View(Store);
        }

        //
        // GET: /Store/Delete/5
        public ActionResult Delete(int id)
        {
            return View(STR.Find(id));
        }

        //
        // POST: /Store/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                STR.Remove(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}