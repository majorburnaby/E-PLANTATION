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
    public class SupplierController : Controller
    {
        private SupplierRepository SPP = new SupplierRepository();
        ComboBoxContext context = new ComboBoxContext();
        // GET: Supplier
        public ActionResult Index()
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            return View(SPP.GetAll());
        }

        public JsonResult GetSupplierGroupList()
        {
            var users = context.GetSupplierGroup();
            return Json(users, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetControlJobList()
        {
            var users = context.GetControlJob();
            return Json(users, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetParameterValueBKList()
        {
            var users = context.GetParameterValueBK();
            return Json(users, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCountryList()
        {
            var users = context.GetCountry();
            return Json(users, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetProvinceList()
        {
            var users = context.GetProvince();
            return Json(users, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCityList()
        {
            var users = context.GetCity();
            return Json(users, JsonRequestBehavior.AllowGet);
        }

        //
        // GET: /Supplier/Details/5
        public ActionResult Details(int? id)
        {

            return View(SPP.Find(id));
        }

        //
        // GET: /Supplier/Create
        public ActionResult Create(Supplier Supplier, string userid)
        {
            if (ModelState.IsValid)
            {
                SPP.Add(Supplier, Session["userid"].ToString());
            }

            return View(Supplier);
        }

        //
        // GET: /Supplier/Edit/5
        public ActionResult Edit(Supplier Supplier, string userid)
        {
            if (ModelState.IsValid)
            {
                SPP.Update(Supplier, Session["userid"].ToString());
            }
            return View(Supplier);
        }

        //
        // GET: /Supplier/Delete/5
        public ActionResult Delete(int id)
        {
            return View(SPP.Find(id));
        }

        //
        // POST: /Supplier/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                SPP.Remove(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}