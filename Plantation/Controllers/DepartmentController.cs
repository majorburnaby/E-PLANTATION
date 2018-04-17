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
    public class DepartmentController : Controller
    {
        private DepartmentRepository IPD = new DepartmentRepository();
        // GET: Department
        public ActionResult Index()
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            return View(IPD.GetAll());
        }

        //
        // GET: /Department/Details/5
        public ActionResult Details(int? id)
        {

            return View(IPD.Find(id));
        }

        //
        // GET: /Department/Create
        public ActionResult Create(Department department, string userid)
        {
            if (ModelState.IsValid)
            {
                IPD.Add(department, Session["userid"].ToString());
            }

            return View(department);
        }

        // POST: /Department/Edit/5
        [HttpPost]
        public ActionResult Edit(Department department, string userid)
        {
            if (ModelState.IsValid)
            {
                IPD.Update(department, Session["userid"].ToString());
            }
            return View(department);
        }

        //
        // GET: /Department/Delete/5

        public ActionResult Delete(int id)
        {
            return View(IPD.Find(id));
        }

        //
        // POST: /Department/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                IPD.Remove(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}