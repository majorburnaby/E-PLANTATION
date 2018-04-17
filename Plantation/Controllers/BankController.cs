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
    public class BankController : Controller
    {
        private BankRepository IBK = new BankRepository();
        ComboBoxContext context = new ComboBoxContext();
        // GET: Bank
        public ActionResult Index()
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            return View(IBK.GetAll());
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

        public JsonResult GetCurrencyMasterList()
        {
            var users = context.GetCurrencyMaster();
            return Json(users, JsonRequestBehavior.AllowGet);
        }

        //
        // GET: /Bank/Details/5
        public ActionResult Details(int? id)
        {

            return View(IBK.Find(id));
        }

        //
        // GET: /Bank/Create
        public ActionResult Create(Bank bank, string userid)
        {
            if (ModelState.IsValid)
            {
                IBK.Add(bank, Session["userid"].ToString());
            }

            return View(bank);
        }

        //
        // GET: /Bank/Edit/5
        public ActionResult Edit(Bank bank, string userid)
        {
            if (ModelState.IsValid)
            {
                IBK.Update(bank, Session["userid"].ToString());
            }
            return View(bank);
        }

        //
        // GET: /Bank/Delete/5
        public ActionResult Delete(int id)
        {
            return View(IBK.Find(id));
        }

        //
        // POST: /Bank/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                IBK.Remove(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}