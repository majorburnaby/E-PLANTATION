using Plantation.Models;
using Plantation.Models.DB;
using Plantation.Repository;
using System;
using System.Linq;
using System.Web.Mvc;

namespace Plantation.Controllers
{
    public class LandConcessionController : Controller
    {
        private LandConcessionRepository LCC = new LandConcessionRepository();
        ComboBoxContext context = new ComboBoxContext();
        // GET: LandConcession
        public ActionResult Index()
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            return View(LCC.GetAllByCompanySite(int.Parse(Session["companysite"].ToString())));
        }

        //
        // GET: /LandConcession/Details/5
        public ActionResult Details(int? id)
        {
            return View(LCC.Find(id));
        }

        //
        // GET: /LandConcession/Create
        public ActionResult Create()
        {
            var model = new LandConcession();
            model.GetSelectListPermissionType = GetSelectListPermissionType();
            model.GetSelectListCountry = GetSelectListCountry();
            model.GetSelectListProvince = GetSelectListProvince();
            model.GetSelectListCity = GetSelectListCity();
            return View(model);
        }

        //
        // POST: /LandConcession/Create
        [HttpPost]
        public ActionResult Create(LandConcession LandConcession, string userid)
        {
            if (ModelState.IsValid)
            {
                LandConcession.COMPANYSITE = int.Parse(Session["companysite"].ToString());
                LCC.Add(LandConcession, Session["userid"].ToString());
                TempData["successmessage"] = "Saved successfully";
                return RedirectToAction("Create");
            }
            else
            {
                LandConcession.GetSelectListPermissionType = GetSelectListPermissionType();
                LandConcession.GetSelectListCountry = GetSelectListCountry();
                LandConcession.GetSelectListProvince = GetSelectListProvince();
                LandConcession.GetSelectListCity = GetSelectListCity();
                return View(LandConcession);
            }
        }

        //
        // GET: /LandConcession/Edit/5
        public ActionResult Edit(int id)
        {
            var model = LCC.Find(id);
            model.GetSelectListPermissionType = GetSelectListPermissionType(model.PERMISSIONTYPE);
            model.GetSelectListCountry = GetSelectListCountry(model.COUNTRY);
            model.GetSelectListProvince = GetSelectListProvince(model.PROVINCE);
            model.GetSelectListCity = GetSelectListCity(model.CITY);
            return View(model);
        }

        //
        // POST: /LandConcession/Edit/5
        [HttpPost]
        public ActionResult Edit(LandConcession LandConcession)
        {
            if (ModelState.IsValid)
            {
                LandConcession.COMPANYSITE = int.Parse(Session["companysite"].ToString());
                LCC.Update(LandConcession, Session["userid"].ToString());
                TempData["successmessage"] = "Saved successfully";
                return RedirectToAction("Index");
            }
            else
            {
                LandConcession.GetSelectListPermissionType = GetSelectListPermissionType();
                LandConcession.GetSelectListCountry = GetSelectListCountry();
                LandConcession.GetSelectListProvince = GetSelectListProvince();
                LandConcession.GetSelectListCity = GetSelectListCity();
                return View(LandConcession);
            }
        }

        //
        // GET: /LandConcession/Delete/5
        public ActionResult Delete(int id)
        {
            return View(LCC.Find(id));
        }

        //
        // POST: /LandConcession/Delete/5
        [HttpPost]
        public JsonResult Delete(int id, FormCollection collection)
        {
            try
            {
                LCC.Remove(id);
                return Json("success", JsonRequestBehavior.AllowGet);//RedirectToAction("Index");
            }
            catch
            {
                return Json("error", JsonRequestBehavior.AllowGet);//View();
            }
        }

        public JsonResult GetPermissionTypeList()
        {
            var users = context.GetPermissionType();
            return Json(users, JsonRequestBehavior.AllowGet);
        }

        private SelectList GetSelectListPermissionType(object selectedValue = null)
        {
            var model = context.GetPermissionType();
            var list = new SelectList(model.Select(x => new { x.SID, x.PARAMETERVALUENAME }), "SID", "PARAMETERVALUENAME", selectedValue).ToList();
            list.Insert(0, new SelectListItem { Value = "", Text = "--Select Permission Type--" });
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