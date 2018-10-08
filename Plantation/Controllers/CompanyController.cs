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
    public class CompanyController : Controller
    {
        private CompanyRepository ICP = new CompanyRepository();
        private ComboBoxContext context = new ComboBoxContext();
        // GET: Company
        public ActionResult Index()
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            return View(ICP.GetAll());
        }

        //
        // GET: /Company/Details/5
        public ActionResult Details(int? id)
        {

            return View(ICP.Find(id));
        }

        //
        // GET: /Company/Create
        public ActionResult Create()
        {
            var model = new Company();
            model.GetSelectListCountry = GetSelectListCountry();
            model.GetSelectListProvince = GetSelectListProvince();
            model.GetSelectListCity = GetSelectListCity();
            return View(model);
        }

        //
        // POST: /Company/Create
        [HttpPost]
        public ActionResult Create([Bind(Include = "SID, IDCOMPANY, COMPANYNAME, ADDRESS1, ADDRESS2, COUNTRY, PROVINCE, CITY, TELEPHONE1, TELEPHONE2, FAX1, FAX2, EMAIL, WEBSITE, POSCODE, INPUTBY, INPUTDATE, UPDATEBY, UPDATEDATE")] Company company)
        {
            if (ModelState.IsValid)
            {
                ICP.Add(company);
                TempData["successmessage"] = "Saved successfully";
                return RedirectToAction("Create");
            }
            else
            {
                company.GetSelectListCountry = GetSelectListCountry();
                company.GetSelectListProvince = GetSelectListProvince();
                company.GetSelectListCity = GetSelectListCity();
                return View(company);
            }
        }

        [HttpPost]
        public ActionResult CreateAndAddCompanySite(Company company)
        {
            if (ModelState.IsValid)
            {
                Company comp = ICP.Add(company);
                //TempData["successmessage"] = "Saved successfully";
                //return RedirectToAction("Create");
                var model = ICP.Find(comp.SID);
                model.GetSelectListCountry = GetSelectListCountry(model.COUNTRY);
                model.GetSelectListProvince = GetSelectListProvince(model.PROVINCE);
                model.GetSelectListCity = GetSelectListCity(model.CITY);
                return View("Edit", model);
            }
            else
            {
                company.GetSelectListCountry = GetSelectListCountry();
                company.GetSelectListProvince = GetSelectListProvince();
                company.GetSelectListCity = GetSelectListCity();
                return View(company);
            }
        }

        //
        // GET: /Company/Edit/5
        public ActionResult Edit(int id)
        {
            var model = ICP.Find(id);
            model.GetSelectListCountry = GetSelectListCountry(model.COUNTRY);
            model.GetSelectListProvince = GetSelectListProvince(model.PROVINCE);
            model.GetSelectListCity = GetSelectListCity(model.CITY);
            return View(model);
        }

        // POST: /Company/Edit/5
        [HttpPost]
        public ActionResult Edit([Bind(Include = "SID, IDCOMPANY, COMPANYNAME, ADDRESS1, ADDRESS2, COUNTRY, PROVINCE, CITY, TELEPHONE1, TELEPHONE2, FAX1, FAX2, EMAIL, WEBSITE, POSCODE, INPUTBY, INPUTDATE, UPDATEBY, UPDATEDATE")] Company company, int id)
        {
            if (ModelState.IsValid)
            {
                company.UPDATEBY = int.Parse(Session["userid"].ToString());
                company.UPDATEDATE = DateTime.Now;
                ICP.Update(company);
            }
            return View("Index", ICP.GetAll());
        }

        //
        // GET: /Company/Delete/5

        public ActionResult Delete(int id)
        {
            return View(ICP.Find(id));
        }

        //
        // POST: /Company/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                if (ICP.HasCompanySite(id))
                    return Json("hascompanysite", JsonRequestBehavior.AllowGet);

                ICP.Remove(id);
                return Json("success", JsonRequestBehavior.AllowGet);//RedirectToAction("Index");
            }
            catch
            {
                return Json("error", JsonRequestBehavior.AllowGet);//View();
            }
        }

        #region Custom Method

        public JsonResult GetListCompanySite(int Company)
        {
            var companysite = ICP.GetAllCompanySiteByCompany(Company);
            return Json(companysite, JsonRequestBehavior.AllowGet);
        }

        private SelectList GetSelectListCountry(object selectedValue = null)
        {
            var model = context.GetCountry();
            var list = new SelectList(model.Select(x => new { x.SID, x.COUNTRYNAME }), "SID", "COUNTRYNAME", selectedValue).ToList();
            list.Insert(0, new SelectListItem { Value = "", Text = "--Select Country--" });
            return new SelectList(list, "Value", "Text");
        }

        private SelectList GetSelectListProvince(object selectedValue = null)
        {
            var model = context.GetProvince();
            var list = new SelectList(model.Select(x => new { x.SID, x.PROVINCENAME }), "SID", "PROVINCENAME", selectedValue).ToList();
            list.Insert(0, new SelectListItem { Value = "", Text = "--Select Province--" });
            return new SelectList(list, "Value", "Text");
        }

        private SelectList GetSelectListCity(object selectedValue = null)
        {
            var model = context.GetCity();
            var list = new SelectList(model.Select(x => new { x.SID, x.CITYNAME }), "SID", "CITYNAME", selectedValue).ToList();
            list.Insert(0, new SelectListItem { Value = "", Text = "--Select City--" });
            return new SelectList(list, "Value", "Text");
        }

        public JsonResult GetProvinceByCountry(int Country)
        {
            var users = context.GetProvinceByCountry(Country);
            return Json(users, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCityByProvince(int Province)
        {
            var users = context.GetCityByProvince(Province);
            return Json(users, JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}