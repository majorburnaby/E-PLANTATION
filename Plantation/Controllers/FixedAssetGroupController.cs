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
    public class FixedAssetGroupController : Controller
    {
        private FixedAssetGroupRepository FAG = new FixedAssetGroupRepository();
        ComboBoxContext context = new ComboBoxContext();
        // GET: FixedAssetGroup
        public ActionResult Index()
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            return View(FAG.GetAll());
        }

        public JsonResult GetCostCenterList()
        {
            var users = context.GetCostCenter();
            return Json(users, JsonRequestBehavior.AllowGet);
        }

        //
        // GET: /FixedAssetGroup/Details/5
        public ActionResult Details(int? id)
        {

            return View(FAG.Find(id));
        }

        //
        // GET: /FixedAssetGroup/Create
        public ActionResult Create(FixedAssetGroup FixedAssetGroup, string userid)
        {
            if (ModelState.IsValid)
            {
                FAG.Add(FixedAssetGroup, Session["userid"].ToString());
            }

            return View(FixedAssetGroup);
        }

        //
        // GET: /FixedAssetGroup/Edit/5
        public ActionResult Edit(FixedAssetGroup FixedAssetGroup, string userid)
        {
            if (ModelState.IsValid)
            {
                FAG.Update(FixedAssetGroup, Session["userid"].ToString());
            }
            return View(FixedAssetGroup);
        }

        //
        // GET: /FixedAssetGroup/Delete/5
        public ActionResult Delete(int id)
        {
            return View(FAG.Find(id));
        }

        //
        // POST: /FixedAssetGroup/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                FAG.Remove(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}