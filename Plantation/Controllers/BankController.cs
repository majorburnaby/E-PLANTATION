using Plantation.Models;
using Plantation.Models.DB;
using Plantation.Repository;
using System;
using System.Linq;
using System.Web.Mvc;

namespace Plantation.Controllers
{
    public class BankController : Controller
    {
        private BankRepository BK = new BankRepository();
        ComboBoxContext context = new ComboBoxContext();
        // GET: Bank
        public ActionResult Index()
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            return View(BK.GetAllByCompanySite(int.Parse(Session["companysite"].ToString())));
        }

        //
        // GET: /Bank/Details/5
        public ActionResult Details(int? id)
        {
            return View(BK.Find(id));
        }

        //
        // GET: /Bank/Create
        public ActionResult Create()
        {
            var model = new Bank();
            model.ISACTIVE = true;
            model.GetSelectListCurrencyMaster = GetSelectListCurrencyMaster();
            model.GetSelectListCountry = GetSelectListCountry();
            model.GetSelectListProvince = GetSelectListProvince();
            model.GetSelectListCity = GetSelectListCity();
            return View(model);
        }

        //
        // POST: /Bank/Create
        [HttpPost]
        public ActionResult Create(Bank bank, string userid)
        {
            if (ModelState.IsValid)
            {
                if (bank.ISACTIVE)
                    bank.ISACTIVEDATE = DateTime.Now;

                bank.COMPANYSITE = int.Parse(Session["companysite"].ToString());
                BK.Add(bank, Session["userid"].ToString());
                TempData["successmessage"] = "Saved successfully";
                return RedirectToAction("Create");
            }
            else
            {
                bank.GetSelectListCurrencyMaster = GetSelectListCurrencyMaster();
                bank.GetSelectListCountry = GetSelectListCountry();
                bank.GetSelectListProvince = GetSelectListProvince();
                bank.GetSelectListCity = GetSelectListCity();
                return View(bank);
            }
        }

        //
        // GET: /Bank/Edit/5
        public ActionResult Edit(int id)
        {
            var model = BK.Find(id);
            model.GetSelectListCurrencyMaster = GetSelectListCurrencyMaster(model.CURRENCY);
            model.GetSelectListCountry = GetSelectListCountry(model.COUNTRY);
            model.GetSelectListProvince = GetSelectListProvince(model.PROVINCE);
            model.GetSelectListCity = GetSelectListCity(model.CITY);
            return View(model);
        }

        //
        // POST: /Bank/Edit/5
        [HttpPost]
        public ActionResult Edit(Bank Bank)
        {
            if (ModelState.IsValid)
            {
                Bank.COMPANYSITE = int.Parse(Session["companysite"].ToString());
                Bank.ISACTIVEDATE = DateTime.Now;
                Bank.UPDATEDATE = DateTime.Now;
                BK.Update(Bank, Session["userid"].ToString());
                TempData["successmessage"] = "Saved successfully";
                return RedirectToAction("Index");
            }
            else
            {
                Bank.GetSelectListCurrencyMaster = GetSelectListCurrencyMaster();
                Bank.GetSelectListCountry = GetSelectListCountry();
                Bank.GetSelectListProvince = GetSelectListProvince();
                Bank.GetSelectListCity = GetSelectListCity();
                return View(Bank);
            }
        }

        //
        // GET: /Bank/Delete/5
        public ActionResult Delete(int id)
        {
            return View(BK.Find(id));
        }

        //
        // POST: /Bank/Delete/5
        [HttpPost]
        public JsonResult Delete(int id, FormCollection collection)
        {
            try
            {
                BK.Remove(id);
                return Json("success", JsonRequestBehavior.AllowGet);//RedirectToAction("Index");
            }
            catch
            {
                return Json("error", JsonRequestBehavior.AllowGet);//View();
            }
        }

        public JsonResult GetCurrencyMasterList()
        {
            var users = context.GetCurrencyMaster();
            return Json(users, JsonRequestBehavior.AllowGet);
        }

        private SelectList GetSelectListCurrencyMaster(object selectedValue = null)
        {
            var model = context.GetCurrencyMaster();
            var list = new SelectList(model.Select(x => new { x.SID, x.CURRENCYNAME }), "SID", "CURRENCYNAME", selectedValue).ToList();
            list.Insert(0, new SelectListItem { Value = "", Text = "--Select Currency--" });
            return new SelectList(list, "Value", "Text");
        }

        public JsonResult GetCountryList()
        {
            var users = context.GetCountry();
            return Json(users, JsonRequestBehavior.AllowGet);
        }

        private SelectList GetSelectListCountry(object selectedValue = null)
        {
            var model = context.GetCountry();
            var list = new SelectList(model.Select(x => new { x.SID, x.COUNTRYNAME }), "SID", "COUNTRYNAME", selectedValue).ToList();
            list.Insert(0, new SelectListItem { Value = "", Text = "--Select Country--" });
            return new SelectList(list, "Value", "Text");
        }

        public JsonResult GetProvinceList()
        {
            var users = context.GetProvince();
            return Json(users, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetProvinceByCountry(int Country)
        {
            var users = context.GetProvinceByCountry(Country);
            return Json(users, JsonRequestBehavior.AllowGet);
        }

        private SelectList GetSelectListProvince(object selectedValue = null)
        {
            var model = context.GetProvince();
            var list = new SelectList(model.Select(x => new { x.SID, x.PROVINCENAME }), "SID", "PROVINCENAME", selectedValue).ToList();
            list.Insert(0, new SelectListItem { Value = "", Text = "--Select Province--" });
            return new SelectList(list, "Value", "Text");
        }

        public JsonResult GetCityList()
        {
            var users = context.GetCity();
            return Json(users, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCityByProvince(int Province)
        {
            var users = context.GetCityByProvince(Province);
            return Json(users, JsonRequestBehavior.AllowGet);
        }

        private SelectList GetSelectListCity(object selectedValue = null)
        {
            var model = context.GetCity();
            var list = new SelectList(model.Select(x => new { x.SID, x.CITYNAME }), "SID", "CITYNAME", selectedValue).ToList();
            list.Insert(0, new SelectListItem { Value = "", Text = "--Select City--" });
            return new SelectList(list, "Value", "Text");
        }
    }
}