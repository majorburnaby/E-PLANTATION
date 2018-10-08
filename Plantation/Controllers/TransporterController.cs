using Plantation.Models;
using Plantation.Models.DB;
using Plantation.Repository;
using System;
using System.Linq;
using System.Web.Mvc;

namespace Plantation.Controllers
{
    public class TransporterController : Controller
    {
        private TransporterRepository TSP = new TransporterRepository();
        ComboBoxContext context = new ComboBoxContext();
        // GET: Transporter
        public ActionResult Index()
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            return View(TSP.GetAllByCompanySite(int.Parse(Session["companysite"].ToString())));
        }

        //
        // GET: /Transporter/Details/5
        public ActionResult Details(int? id)
        {
            return View(TSP.Find(id));
        }

        //
        // GET: /Transporter/Create
        public ActionResult Create()
        {
            var model = new Transporter();
            model.ISACTIVE = true;
            model.GetSelectListTransporterGroup = GetSelectListTransporterGroup();
            model.GetSelectListControlJob = GetSelectListControlJob();
            model.GetSelectListBank = GetSelectListBank();
            model.GetSelectListCountry = GetSelectListCountry();
            model.GetSelectListProvince = GetSelectListProvince();
            model.GetSelectListCity = GetSelectListCity();
            return View(model);
        }

        //
        // POST: /Transporter/Create
        [HttpPost]
        public ActionResult Create(Transporter Transporter, string userid)
        {
            if (ModelState.IsValid)
            {
                if (Transporter.ISACTIVE)
                    Transporter.ISACTIVEDATE = DateTime.Now;

                Transporter.COMPANYSITE = int.Parse(Session["companysite"].ToString());
                TSP.Add(Transporter, Session["userid"].ToString());
                TempData["successmessage"] = "Saved successfully";
                return RedirectToAction("Create");
            }
            else
            {
                Transporter.GetSelectListTransporterGroup = GetSelectListTransporterGroup();
                Transporter.GetSelectListControlJob = GetSelectListControlJob();
                Transporter.GetSelectListBank = GetSelectListBank();
                Transporter.GetSelectListCountry = GetSelectListCountry();
                Transporter.GetSelectListProvince = GetSelectListProvince();
                Transporter.GetSelectListCity = GetSelectListCity();
                return View(Transporter);
            }
        }

        //
        // GET: /Transporter/Edit/5
        public ActionResult Edit(int id)
        {
            var model = TSP.Find(id);
            model.GetSelectListTransporterGroup = GetSelectListTransporterGroup(model.TRANSPORTERGROUP);
            model.GetSelectListControlJob = GetSelectListControlJob(model.CONTROLJOB);
            model.GetSelectListBank = GetSelectListBank(model.BANK);
            model.GetSelectListCountry = GetSelectListCountry(model.COUNTRY);
            model.GetSelectListProvince = GetSelectListProvince(model.PROVINCE);
            model.GetSelectListCity = GetSelectListCity(model.CITY);
            return View(model);
        }

        //
        // POST: /Transporter/Edit/5
        [HttpPost]
        public ActionResult Edit(Transporter Transporter)
        {
            if (ModelState.IsValid)
            {
                Transporter.COMPANYSITE = int.Parse(Session["companysite"].ToString());
                Transporter.ISACTIVEDATE = DateTime.Now;
                Transporter.UPDATEDATE = DateTime.Now;
                TSP.Update(Transporter, Session["userid"].ToString());
                TempData["successmessage"] = "Saved successfully";
                return RedirectToAction("Index");
            }
            else
            {
                Transporter.GetSelectListTransporterGroup = GetSelectListTransporterGroup();
                Transporter.GetSelectListControlJob = GetSelectListControlJob();
                Transporter.GetSelectListBank = GetSelectListBank();
                Transporter.GetSelectListCountry = GetSelectListCountry();
                Transporter.GetSelectListProvince = GetSelectListProvince();
                Transporter.GetSelectListCity = GetSelectListCity();
                return View(Transporter);
            }
        }

        //
        // GET: /Transporter/Delete/5
        public ActionResult Delete(int id)
        {
            return View(TSP.Find(id));
        }

        //
        // POST: /Transporter/Delete/5
        [HttpPost]
        public JsonResult Delete(int id, FormCollection collection)
        {
            try
            {
                TSP.Remove(id);
                return Json("success", JsonRequestBehavior.AllowGet);//RedirectToAction("Index");
            }
            catch
            {
                return Json("error", JsonRequestBehavior.AllowGet);//View();
            }
        }

        public JsonResult GetTransporterGroupList()
        {
            var users = context.GetTransporterGroup();
            return Json(users, JsonRequestBehavior.AllowGet);
        }

        private SelectList GetSelectListTransporterGroup(object selectedValue = null)
        {
            var model = context.GetTransporterGroup();
            var list = new SelectList(model.Select(x => new { x.SID, x.TRANSPORTERGROUPNAME }), "SID", "TRANSPORTERGROUPNAME", selectedValue).ToList();
            list.Insert(0, new SelectListItem { Value = "", Text = "--Select Transporter Group--" });
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