using Plantation.Models;
using Plantation.Models.DB;
using Plantation.Repository;
using System;
using System.Linq;
using System.Web.Mvc;

namespace Plantation.Controllers
{
    public class ContractorController : Controller
    {
        private ContractorRepository CTR = new ContractorRepository();
        ComboBoxContext context = new ComboBoxContext();
        // GET: Contractor
        public ActionResult Index()
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            return View(CTR.GetAllByCompanySite(int.Parse(Session["companysite"].ToString())));
        }

        //
        // GET: /Contractor/Details/5
        public ActionResult Details(int? id)
        {
            return View(CTR.Find(id));
        }

        //
        // GET: /Contractor/Create
        public ActionResult Create()
        {
            var model = new Contractor();
            model.ISACTIVE = true;
            model.GetSelectListContractorGroup = GetSelectListContractorGroup();
            model.GetSelectListControlJob = GetSelectListControlJob();
            model.GetSelectListBank = GetSelectListBank();
            model.GetSelectListCountry = GetSelectListCountry();
            model.GetSelectListProvince = GetSelectListProvince();
            model.GetSelectListCity = GetSelectListCity();
            return View(model);
        }

        //
        // POST: /Contractor/Create
        [HttpPost]
        public ActionResult Create(Contractor Contractor, string userid)
        {
            if (ModelState.IsValid)
            {
                if (Contractor.ISACTIVE)
                    Contractor.ISACTIVEDATE = DateTime.Now;

                Contractor.COMPANYSITE = int.Parse(Session["companysite"].ToString());
                CTR.Add(Contractor, Session["userid"].ToString());
                TempData["successmessage"] = "Saved successfully";
                return RedirectToAction("Create");
            }
            else
            {
                Contractor.GetSelectListContractorGroup = GetSelectListContractorGroup();
                Contractor.GetSelectListControlJob = GetSelectListControlJob();
                Contractor.GetSelectListBank = GetSelectListBank();
                Contractor.GetSelectListCountry = GetSelectListCountry();
                Contractor.GetSelectListProvince = GetSelectListProvince();
                Contractor.GetSelectListCity = GetSelectListCity();
                return View(Contractor);
            }
        }

        //
        // GET: /Contractor/Edit/5
        public ActionResult Edit(int id)
        {
            var model = CTR.Find(id);
            model.GetSelectListContractorGroup = GetSelectListContractorGroup(model.CONTRACTORGROUP);
            model.GetSelectListControlJob = GetSelectListControlJob(model.CONTROLJOB);
            model.GetSelectListBank = GetSelectListBank(model.BANK);
            model.GetSelectListCountry = GetSelectListCountry(model.COUNTRY);
            model.GetSelectListProvince = GetSelectListProvince(model.PROVINCE);
            model.GetSelectListCity = GetSelectListCity(model.CITY);
            return View(model);
        }

        //
        // POST: /Contractor/Edit/5
        [HttpPost]
        public ActionResult Edit(Contractor Contractor)
        {
            if (ModelState.IsValid)
            {
                Contractor.COMPANYSITE = int.Parse(Session["companysite"].ToString());
                Contractor.ISACTIVEDATE = DateTime.Now;
                Contractor.UPDATEDATE = DateTime.Now;
                CTR.Update(Contractor, Session["userid"].ToString());
                TempData["successmessage"] = "Saved successfully";
                return RedirectToAction("Index");
            }
            else
            {
                Contractor.GetSelectListContractorGroup = GetSelectListContractorGroup();
                Contractor.GetSelectListControlJob = GetSelectListControlJob();
                Contractor.GetSelectListBank = GetSelectListBank();
                Contractor.GetSelectListCountry = GetSelectListCountry();
                Contractor.GetSelectListProvince = GetSelectListProvince();
                Contractor.GetSelectListCity = GetSelectListCity();
                return View(Contractor);
            }
        }

        //
        // GET: /Contractor/Delete/5
        public ActionResult Delete(int id)
        {
            return View(CTR.Find(id));
        }

        //
        // POST: /Contractor/Delete/5
        [HttpPost]
        public JsonResult Delete(int id, FormCollection collection)
        {
            try
            {
                CTR.Remove(id);
                return Json("success", JsonRequestBehavior.AllowGet);//RedirectToAction("Index");
            }
            catch
            {
                return Json("error", JsonRequestBehavior.AllowGet);//View();
            }
        }

        public JsonResult GetContractorGroupList()
        {
            var users = context.GetContractorGroup();
            return Json(users, JsonRequestBehavior.AllowGet);
        }

        private SelectList GetSelectListContractorGroup(object selectedValue = null)
        {
            var model = context.GetContractorGroup();
            var list = new SelectList(model.Select(x => new { x.SID, x.CONTRACTORGROUPNAME }), "SID", "CONTRACTORGROUPNAME", selectedValue).ToList();
            list.Insert(0, new SelectListItem { Value = "", Text = "--Select Contractor Group--" });
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