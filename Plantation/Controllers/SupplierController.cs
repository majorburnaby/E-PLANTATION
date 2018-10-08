using Plantation.Models;
using Plantation.Models.DB;
using Plantation.Repository;
using System;
using System.Linq;
using System.Web.Mvc;

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
            return View(SPP.GetAllByCompanySite(int.Parse(Session["companysite"].ToString())));
        }

        //
        // GET: /Supplier/Details/5
        public ActionResult Details(int? id)
        {
            return View(SPP.Find(id));
        }

        //
        // GET: /Supplier/Create
        public ActionResult Create()
        {
            var model = new Supplier();
            model.ISACTIVE = true;
            model.GetSelectListSupplierGroup = GetSelectListSupplierGroup();
            model.GetSelectListControlJob = GetSelectListControlJob();
            model.GetSelectListBank = GetSelectListBank();
            model.GetSelectListCountry = GetSelectListCountry();
            model.GetSelectListProvince = GetSelectListProvince();
            model.GetSelectListCity = GetSelectListCity();
            return View(model);
        }

        //
        // POST: /Supplier/Create
        [HttpPost]
        public ActionResult Create(Supplier Supplier, string userid)
        {
            if (ModelState.IsValid)
            {
                if (Supplier.ISACTIVE)
                    Supplier.ISACTIVEDATE = DateTime.Now;

                Supplier.COMPANYSITE = int.Parse(Session["companysite"].ToString());
                SPP.Add(Supplier, Session["userid"].ToString());
                TempData["successmessage"] = "Saved successfully";
                return RedirectToAction("Create");
            }
            else
            {
                Supplier.GetSelectListSupplierGroup = GetSelectListSupplierGroup();
                Supplier.GetSelectListControlJob = GetSelectListControlJob();
                Supplier.GetSelectListBank = GetSelectListBank();
                Supplier.GetSelectListCountry = GetSelectListCountry();
                Supplier.GetSelectListProvince = GetSelectListProvince();
                Supplier.GetSelectListCity = GetSelectListCity();
                return View(Supplier);
            }
        }

        //
        // GET: /Supplier/Edit/5
        public ActionResult Edit(int id)
        {
            var model = SPP.Find(id);
            model.GetSelectListSupplierGroup = GetSelectListSupplierGroup(model.SUPPLIERGROUP);
            model.GetSelectListControlJob = GetSelectListControlJob(model.CONTROLJOB);
            model.GetSelectListBank = GetSelectListBank(model.BANK);
            model.GetSelectListCountry = GetSelectListCountry(model.COUNTRY);
            model.GetSelectListProvince = GetSelectListProvince(model.PROVINCE);
            model.GetSelectListCity = GetSelectListCity(model.CITY);
            return View(model);
        }

        //
        // POST: /Supplier/Edit/5
        [HttpPost]
        public ActionResult Edit(Supplier Supplier)
        {
            if (ModelState.IsValid)
            {
                Supplier.COMPANYSITE = int.Parse(Session["companysite"].ToString());
                Supplier.ISACTIVEDATE = DateTime.Now;
                Supplier.UPDATEDATE = DateTime.Now;
                SPP.Update(Supplier, Session["userid"].ToString());
                TempData["successmessage"] = "Saved successfully";
                return RedirectToAction("Index");
            }
            else
            {
                Supplier.GetSelectListSupplierGroup = GetSelectListSupplierGroup();
                Supplier.GetSelectListControlJob = GetSelectListControlJob();
                Supplier.GetSelectListBank = GetSelectListBank();
                Supplier.GetSelectListCountry = GetSelectListCountry();
                Supplier.GetSelectListProvince = GetSelectListProvince();
                Supplier.GetSelectListCity = GetSelectListCity();
                return View(Supplier);
            }
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
        public JsonResult Delete(int id, FormCollection collection)
        {
            try
            {
                SPP.Remove(id);
                return Json("success", JsonRequestBehavior.AllowGet);//RedirectToAction("Index");
            }
            catch
            {
                return Json("error", JsonRequestBehavior.AllowGet);//View();
            }
        }

        public JsonResult GetSupplierGroupList()
        {
            var users = context.GetSupplierGroup();
            return Json(users, JsonRequestBehavior.AllowGet);
        }

        private SelectList GetSelectListSupplierGroup(object selectedValue = null)
        {
            var model = context.GetSupplierGroup();
            var list = new SelectList(model.Select(x => new { x.SID, x.SUPPLIERGROUPNAME }), "SID", "SUPPLIERGROUPNAME", selectedValue).ToList();
            list.Insert(0, new SelectListItem { Value = "", Text = "--Select Supplier Group--" });
            return new SelectList(list, "Value", "Text");
        }

        public JsonResult GetControlJobList()
        {
            var users = context.GetControlJob();
            return Json(users, JsonRequestBehavior.AllowGet);
        }

        private SelectList GetSelectListControlJob(object selectedValue = null)
        {
            var model = context.GetControlJob();
            var list = new SelectList(model.Select(x => new { x.SID, x.ITEMDESCRIPTION }), "SID", "ITEMDESCRIPTION", selectedValue).ToList();
            list.Insert(0, new SelectListItem { Value = "", Text = "--Select Control Job--" });
            return new SelectList(list, "Value", "Text");
        }

        public JsonResult GetParameterValueBKList()
        {
            var users = context.GetParameterValueBK();
            return Json(users, JsonRequestBehavior.AllowGet);
        }

        private SelectList GetSelectListBank(object selectedValue = null)
        {
            var model = context.GetParameterValueBK();
            var list = new SelectList(model.Select(x => new { x.SID, x.PARAMETERVALUENAME }), "SID", "PARAMETERVALUENAME", selectedValue).ToList();
            list.Insert(0, new SelectListItem { Value = "", Text = "--Select Bank--" });
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