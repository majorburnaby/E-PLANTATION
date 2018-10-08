﻿using Plantation.Models;
using Plantation.Models.DB;
using Plantation.Repository;
using System;
using System.Linq;
using System.Web.Mvc;

namespace Plantation.Controllers
{
    public class CustomerController : Controller
    {
        private CustomerRepository CTM = new CustomerRepository();
        ComboBoxContext context = new ComboBoxContext();
        // GET: Customer
        public ActionResult Index()
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            return View(CTM.GetAllByCompanySite(int.Parse(Session["companysite"].ToString())));
        }

        //
        // GET: /Customer/Details/5
        public ActionResult Details(int? id)
        {
            return View(CTM.Find(id));
        }

        //
        // GET: /Customer/Create
        public ActionResult Create()
        {
            var model = new Customer();
            model.ISACTIVE = true;
            model.GetSelectListCustomerGroup = GetSelectListCustomerGroup();
            model.GetSelectListControlJob = GetSelectListControlJob();
            model.GetSelectListBank = GetSelectListBank();
            model.GetSelectListCountry = GetSelectListCountry();
            model.GetSelectListProvince = GetSelectListProvince();
            model.GetSelectListCity = GetSelectListCity();
            return View(model);
        }

        //
        // POST: /Customer/Create
        [HttpPost]
        public ActionResult Create(Customer Customer, string userid)
        {
            if (ModelState.IsValid)
            {
                if (Customer.ISACTIVE)
                    Customer.ISACTIVEDATE = DateTime.Now;

                Customer.COMPANYSITE = int.Parse(Session["companysite"].ToString());
                CTM.Add(Customer, Session["userid"].ToString());
                TempData["successmessage"] = "Saved successfully";
                return RedirectToAction("Create");
            }
            else
            {
                Customer.GetSelectListCustomerGroup = GetSelectListCustomerGroup();
                Customer.GetSelectListControlJob = GetSelectListControlJob();
                Customer.GetSelectListBank = GetSelectListBank();
                Customer.GetSelectListCountry = GetSelectListCountry();
                Customer.GetSelectListProvince = GetSelectListProvince();
                Customer.GetSelectListCity = GetSelectListCity();
                return View(Customer);
            }
        }

        //
        // GET: /Customer/Edit/5
        public ActionResult Edit(int id)
        {
            var model = CTM.Find(id);
            model.GetSelectListCustomerGroup = GetSelectListCustomerGroup(model.CUSTOMERGROUP);
            model.GetSelectListControlJob = GetSelectListControlJob(model.CONTROLJOB);
            model.GetSelectListBank = GetSelectListBank(model.BANK);
            model.GetSelectListCountry = GetSelectListCountry(model.COUNTRY);
            model.GetSelectListProvince = GetSelectListProvince(model.PROVINCE);
            model.GetSelectListCity = GetSelectListCity(model.CITY);
            return View(model);
        }

        //
        // POST: /Customer/Edit/5
        [HttpPost]
        public ActionResult Edit(Customer Customer)
        {
            if (ModelState.IsValid)
            {
                Customer.COMPANYSITE = int.Parse(Session["companysite"].ToString());
                Customer.ISACTIVEDATE = DateTime.Now;
                Customer.UPDATEDATE = DateTime.Now;
                CTM.Update(Customer, Session["userid"].ToString());
                TempData["successmessage"] = "Saved successfully";
                return RedirectToAction("Index");
            }
            else
            {
                Customer.GetSelectListCustomerGroup = GetSelectListCustomerGroup();
                Customer.GetSelectListControlJob = GetSelectListControlJob();
                Customer.GetSelectListBank = GetSelectListBank();
                Customer.GetSelectListCountry = GetSelectListCountry();
                Customer.GetSelectListProvince = GetSelectListProvince();
                Customer.GetSelectListCity = GetSelectListCity();
                return View(Customer);
            }
        }

        //
        // GET: /Customer/Delete/5
        public ActionResult Delete(int id)
        {
            return View(CTM.Find(id));
        }

        //
        // POST: /Customer/Delete/5
        [HttpPost]
        public JsonResult Delete(int id, FormCollection collection)
        {
            try
            {
                CTM.Remove(id);
                return Json("success", JsonRequestBehavior.AllowGet);//RedirectToAction("Index");
            }
            catch
            {
                return Json("error", JsonRequestBehavior.AllowGet);//View();
            }
        }

        public JsonResult GetCustomerGroupList()
        {
            var users = context.GetCustomerGroup();
            return Json(users, JsonRequestBehavior.AllowGet);
        }

        private SelectList GetSelectListCustomerGroup(object selectedValue = null)
        {
            var model = context.GetCustomerGroup();
            var list = new SelectList(model.Select(x => new { x.SID, x.CUSTOMERGROUPNAME }), "SID", "CUSTOMERGROUPNAME", selectedValue).ToList();
            list.Insert(0, new SelectListItem { Value = "", Text = "--Select Customer Group--" });
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