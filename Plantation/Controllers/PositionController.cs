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
    public class PositionController : Controller
    {
        private PositionRepository IPS = new PositionRepository();
        // GET: Position
        public ActionResult Index()
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            return View(IPS.GetAll());
        }

        //
        // GET: /Position/Details/5
        public ActionResult Details(int? id)
        {

            return View(IPS.Find(id));
        }

        //
        // GET: /Position/Create
        public ActionResult Create(Position position, string userid)
        {
            if (ModelState.IsValid)
            {
                IPS.Add(position, Session["userid"].ToString());
            }

            return View(position);
        }

        // POST: /Position/Edit/5
        [HttpPost]
        public ActionResult Edit(Position position, string userid)
        {
            if (ModelState.IsValid)
            {
                IPS.Update(position, Session["userid"].ToString());
            }
            return View(position);
        }

        //
        // GET: /Position/Delete/5

        public ActionResult Delete(int id)
        {
            return View(IPS.Find(id));
        }

        //
        // POST: /Position/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                IPS.Remove(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}