using Plantation.Models;
using Plantation.Models.DB;
using Plantation.Repository;
using System;
using System.Linq;
using System.Web.Mvc;

namespace Plantation.Controllers
{
    public class CompanySiteController : Controller
    {
        private CompanyRepository CP = new CompanyRepository();
        private CompanySiteRepository CS = new CompanySiteRepository();
        private ComboBoxContext context = new ComboBoxContext();

        // GET: CompanySite
        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /CompanySite/Create
        public ActionResult Create(int Company)
        {
            //ViewData["Company"] = Company;
            var comp = CP.Find(Company);
            //ViewData["CompanyName"] = comp.COMPANYNAME;

            var model = new CompanySite();
            model.COMPANY = Company;
            model.COMPANYNAME = comp.COMPANYNAME;
            model.GetSelectListRegion = GetSelectListRegion();
            model.GetSelectListLocation = GetSelectListLocation();
            model.GetSelectListCountry = GetSelectListCountry();
            model.GetSelectListProvince = GetSelectListProvince();
            model.GetSelectListCity = GetSelectListCity();
            return View(model);
        }

        //
        // POST: /Company/Create
        [HttpPost]
        public ActionResult Create(CompanySite companysite)
        {
            if (ModelState.IsValid)
            {
                companysite.INPUTBY = int.Parse(Session["userid"].ToString());
                companysite.INPUTDATE = DateTime.Now;
                companysite.REGION = 1;
                CS.Add(companysite);
                return RedirectToAction("Edit", "Company", new { id = companysite.COMPANY });
            }
            else
            {
                companysite.GetSelectListRegion = GetSelectListRegion();
                companysite.GetSelectListLocation = GetSelectListLocation();
                companysite.GetSelectListCountry = GetSelectListCountry();
                companysite.GetSelectListProvince = GetSelectListProvince();
                companysite.GetSelectListCity = GetSelectListCity();
                return View(companysite);
            }
        }

        //
        // GET: /CompanySite/Edit/5
        public ActionResult Edit(int id)
        {
            var model = CS.Find(id);
            model.GetSelectListRegion = GetSelectListRegion(model.REGION);
            model.GetSelectListLocation = GetSelectListLocation(model.LOCATION);
            model.GetSelectListCountry = GetSelectListCountry(model.COUNTRY);
            model.GetSelectListProvince = GetSelectListProvince(model.PROVINCE);
            model.GetSelectListCity = GetSelectListCity(model.CITY);
            return View(model);
        }

        // POST: /CompanySite/Edit/5
        [HttpPost]
        public ActionResult Edit(CompanySite companysite, int id)
        {
            if (ModelState.IsValid)
            {
                companysite.UPDATEBY = int.Parse(Session["userid"].ToString());
                companysite.UPDATEDATE = DateTime.Now;
                CS.Update(companysite);
            }
            return RedirectToAction("Edit", "Company", new { id = companysite.COMPANY });
        }

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                CS.Remove(id);
                return Json("success", JsonRequestBehavior.AllowGet);//RedirectToAction("Index");
            }
            catch
            {
                return Json("error", JsonRequestBehavior.AllowGet);//View();
            }
        }


        #region Custom Method

        private SelectList GetSelectListRegion(object selectedValue = null)
        {
            var model = context.GetRegion();
            var list = new SelectList(model.Select(x => new { x.SID, x.REGIONNAME }), "SID", "REGIONNAME", selectedValue).ToList();
            list.Insert(0, new SelectListItem { Value = "", Text = "--Select Region--" });
            return new SelectList(list, "Value", "Text");
        }

        private SelectList GetSelectListLocation(object selectedValue = null)
        {
            var model = context.GetLocation();
            var list = new SelectList(model.Select(x => new { x.SID, x.LOCATIONNAME }), "SID", "LOCATIONNAME", selectedValue).ToList();
            list.Insert(0, new SelectListItem { Value = "", Text = "--Select Location--" });
            return new SelectList(list, "Value", "Text");
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

        #endregion
    }
}