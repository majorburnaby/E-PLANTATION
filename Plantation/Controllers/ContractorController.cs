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
            return View(CTR.GetAll());
        }

        public JsonResult GetContractorGroupList()
        {
            var users = context.GetContractorGroup();
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
        // GET: /Contractor/Details/5
        public ActionResult Details(int? id)
        {

            return View(CTR.Find(id));
        }

        //
        // GET: /Contractor/Create
        public ActionResult Create(Contractor Contractor, string userid)
        {
            if (ModelState.IsValid)
            {
                CTR.Add(Contractor, Session["userid"].ToString());
            }

            return View(Contractor);
        }

        //
        // GET: /Contractor/Edit/5
        public ActionResult Edit(Contractor Contractor, string userid)
        {
            if (ModelState.IsValid)
            {
                CTR.Update(Contractor, Session["userid"].ToString());
            }
            return View(Contractor);
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
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                CTR.Remove(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}