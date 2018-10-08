using Plantation.Models.DB;
using Plantation.Repository;
using System.Web.Mvc;
using DataTables;
using Plantation.Utility;
using System;

namespace Plantation.Controllers
{
    public class SupplierGroupController : Controller
    {
        private SupplierGroupRepository SPG = new SupplierGroupRepository();

        public JsonResult Data()
        {
            var request = System.Web.HttpContext.Current.Request;

            using (var db = new Database("sqlserver", Constant.DatabaseConnection))
            {
                var response = new Editor(db, "SUPPLIERGROUP", "SID")
                    .Model<SupplierGroup>()
                    .Field(new Field("IDSUPPLIERGROUP")
                        .Validator(Validation.NotEmpty())
                    )
                    .Field(new Field("SUPPLIERGROUPNAME")
                        .Validator(Validation.NotEmpty())
                    )
                    .Field(new Field("UPDATEBY").SetValue(Session["userid"].ToString()))
                    .Field(new Field("UPDATEDATE").SetValue(DateTime.Now))
                    .Process(request)
                    .Data();

                return Json(response, JsonRequestBehavior.AllowGet);
            }
        }

        // GET: SupplierGroup
        public ActionResult Index()
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            return View(SPG.GetAll());
        }

        //
        // GET: /SupplierGroup/Details/5
        public ActionResult Details(int? id)
        {

            return View(SPG.Find(id));
        }

        [HttpPost]
        public JsonResult Create(SupplierGroup suppliergroup, string userid)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    SPG.Add(suppliergroup, Session["userid"].ToString());
                }
            }
            catch
            {
                return Json("error");
            }

            return Json("success");
        }

        [HttpPost]
        public JsonResult Edit(SupplierGroup suppliergroup, string userid)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    SPG.Update(suppliergroup, Session["userid"].ToString());
                }
            }
            catch
            {
                return Json("error");
            }

            return Json("success");
        }

        //
        // GET: /SupplierGroup/Delete/5

        public ActionResult Delete(int id)
        {
            return View(SPG.Find(id));
        }

        //
        // POST: /SupplierGroup/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                SPG.Remove(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}