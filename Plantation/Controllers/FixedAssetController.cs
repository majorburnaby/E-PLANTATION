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
    public class FixedAssetController : Controller
    {
        private FixedAssetRepository FA = new FixedAssetRepository();
        ComboBoxContext context = new ComboBoxContext();
        // GET: FixedAsset
        public ActionResult Index()
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            return View(FA.GetAll());
        }

        public JsonResult GetFixedAssetGroupList()
        {
            var users = context.GetFixedAssetGroup();
            return Json(users, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetUnitOfMeasureList()
        {
            var users = context.GetUnitOfMeasure();
            return Json(users, JsonRequestBehavior.AllowGet);
        }

        //
        // GET: /FixedAsset/Details/5
        public ActionResult Details(int? id)
        {

            return View(FA.Find(id));
        }

        //
        // GET: /FixedAsset/Create
        public ActionResult Create(FixedAsset FixedAsset, string userid)
        {
            if (ModelState.IsValid)
            {
                FA.Add(FixedAsset, Session["userid"].ToString());
            }

            return View(FixedAsset);
        }

        //
        // GET: /FixedAsset/Edit/5
        public ActionResult Edit(FixedAsset FixedAsset, string userid)
        {
            if (ModelState.IsValid)
            {
                FA.Update(FixedAsset, Session["userid"].ToString());
            }
            return View(FixedAsset);
        }

        //
        // GET: /FixedAsset/Delete/5
        public ActionResult Delete(int id)
        {
            return View(FA.Find(id));
        }

        //
        // POST: /FixedAsset/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                FA.Remove(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}