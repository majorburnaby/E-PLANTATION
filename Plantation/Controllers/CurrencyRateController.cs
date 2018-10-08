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
    public class CurrencyRateController : Controller
    {
        private CurrencyRateRepository ICR = new CurrencyRateRepository();
        // GET: CurrencyRate
        public ActionResult Index()
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            return View(ICR.GetAll());
        }

        //
        // GET: /CurrencyRate/Details/5
        public ActionResult Details(int? id)
        {

            return View(ICR.Find(id));
        }

        //
        // POST: /CurrencyRate/Create
        [HttpPost]
        public ActionResult Create(CurrencyRate currencyrate, string userid)
        {
            if (ModelState.IsValid)
            {
                ICR.Add(currencyrate, Session["userid"].ToString());
            }
            return View(currencyrate);
        }

        // POST: /CurrencyRate/Edit/5
        [HttpPost]
        public ActionResult Edit(CurrencyRate currencyrate, string userid)
        {
            if (ModelState.IsValid)
            {
                ICR.Update(currencyrate, Session["userid"].ToString());
            }
            return View(currencyrate);
        }

        //
        // GET: /CurrencyRate/Delete/5

        public ActionResult Delete(int id)
        {
            return View(ICR.Find(id));
        }

        //
        // POST: /CurrencyRate/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                ICR.Remove(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}