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
    public class CustomerController : Controller
    {
        private CustomerRepository CTM = new CustomerRepository();
        ComboBoxContext context = new ComboBoxContext();
        // GET: Customer
        public ActionResult Index()
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            return View(CTM.GetAll());
        }

        public JsonResult GetCustomerGroupList()
        {
            var users = context.GetCustomerGroup();
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
        // GET: /Customer/Details/5
        public ActionResult Details(int? id)
        {

            return View(CTM.Find(id));
        }

        //
        // GET: /Customer/Create
        public ActionResult Create(Customer Customer, string userid)
        {
            if (ModelState.IsValid)
            {
                CTM.Add(Customer, Session["userid"].ToString());
            }

            return View(Customer);
        }

        //
        // GET: /Customer/Edit/5
        public ActionResult Edit(Customer Customer, string userid)
        {
            if (ModelState.IsValid)
            {
                CTM.Update(Customer, Session["userid"].ToString());
            }
            return View(Customer);
        }

        //
        // GET: /Customer/Delete/5
        public ActionResult Delete(int id)
        {
            return View(CTM.Find(id));
        }

        //
        // POST: /Customer/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                CTM.Remove(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}