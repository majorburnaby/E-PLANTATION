using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Plantation.Repository;
using Plantation.Repository.Interface;
using Plantation.Models;
using Plantation.Models.DB;

namespace Plantation.Controllers
{
    public class RoleMasterController : Controller
    {
        private RoleMasterRepository IRM = new RoleMasterRepository();
        ComboBoxContext context = new ComboBoxContext();
        // GET: RoleMaster
        public ActionResult Index()
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            return View(IRM.GetAll());
        }

        //
        // GET: /RoleMaster/Details/5
        public ActionResult Details(int? id)
        {
            return View(IRM.Find(id));
        }

        //
        // GET: /RoleMaster/Create
        public ActionResult Create(RoleMaster rolemaster, string userid)
        {
            if (ModelState.IsValid)
            {
                IRM.Add(rolemaster, Session["userid"].ToString());
            }

            return View(rolemaster);
        }

        // POST: /RoleMaster/Edit/5
        [HttpPost]
        public ActionResult Edit(RoleMaster rolemaster, string userid)
        {
            if (ModelState.IsValid)
            {
                IRM.Update(rolemaster, Session["userid"].ToString());
            }
            return View(rolemaster);
        }

        //
        // GET: /RoleMaster/Delete/5
        public ActionResult Delete(int id)
        {
            return View(IRM.Find(id));
        }

        //
        // POST: /RoleMaster/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                IRM.Remove(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}