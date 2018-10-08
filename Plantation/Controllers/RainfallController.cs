using Plantation.Models;
using Plantation.Models.DB;
using Plantation.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Plantation.Controllers
{
    public class RainfallController : Controller
    {
        private RainfallRepository RF = new RainfallRepository();
        ComboBoxContext context = new ComboBoxContext();

        // GET: Rainfall
        public ActionResult Index()
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            return View(RF.GetAllByCompanySite(int.Parse(Session["companysite"].ToString())));
        }

        //
        // GET: /Rainfall/Details/5
        public ActionResult Details(int? id)
        {
            return View(RF.Find(id));
        }

        //
        // GET: /Rainfall/Create
        public ActionResult Create()
        {
            var model = new Rainfall();
            model.GetSelectListBlockOrganization = GetSelectListBlockOrganization();
            return View(model);
        }

        //
        // POST: /Rainfall/Create
        [HttpPost]
        public ActionResult Create(Rainfall Rainfall, string userid)
        {
            if (ModelState.IsValid)
            {
                Rainfall.COMPANYSITE = int.Parse(Session["companysite"].ToString());
                RF.Add(Rainfall, Session["userid"].ToString());
                TempData["successmessage"] = "Saved successfully";
                return RedirectToAction("Create");
            }
            else
            {
                Rainfall.GetSelectListBlockOrganization = GetSelectListBlockOrganization();
                return View(Rainfall);
            }
        }
        //
        // GET: /Rainfall/Edit/5
        public ActionResult Edit(int id)
        {
            var model = RF.Find(id);
            model.GetSelectListBlockOrganization = GetSelectListBlockOrganization(model.BLOCKORGANIZATION);
            return View(model);
        }

        //
        // POST: /Rainfall/Edit/5
        [HttpPost]
        public ActionResult Edit(Rainfall Rainfall)
        {
            if (ModelState.IsValid)
            {
                Rainfall.COMPANYSITE = int.Parse(Session["companysite"].ToString());
                Rainfall.UPDATEDATE = DateTime.Now;
                RF.Update(Rainfall, Session["userid"].ToString());
                TempData["successmessage"] = "Saved successfully";
                return RedirectToAction("Index");
            }
            else
            {
                Rainfall.GetSelectListBlockOrganization = GetSelectListBlockOrganization();
                return View(Rainfall);
            }
        }

        //
        // GET: /Rainfall/Delete/5
        public ActionResult Delete(int id)
        {
            return View(RF.Find(id));
        }

        //
        // POST: /Rainfall/Delete/5
        [HttpPost]
        public JsonResult Delete(int id, FormCollection collection)
        {
            try
            {
                RF.Remove(id);
                return Json("success", JsonRequestBehavior.AllowGet);//RedirectToAction("Index");
            }
            catch
            {
                return Json("error", JsonRequestBehavior.AllowGet);//View();
            }
        }

        private SelectList GetSelectListBlockOrganization(object selectedValue = null)
        {
            var model = context.GetBlockOrganizationByCompanySite(int.Parse(Session["companysite"].ToString()));
            var list = new SelectList(model.Select(x => new { x.SID, x.BLOCKORGANIZATIONNAME }), "SID", "BLOCKORGANIZATIONNAME", selectedValue).ToList();
            list.Insert(0, new SelectListItem { Value = "", Text = "--Select Location--" });
            return new SelectList(list, "Value", "Text");
        }
    }
}