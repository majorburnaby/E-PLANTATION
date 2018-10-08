using DataTables;
using Plantation.Models;
using Plantation.Models.DB;
using Plantation.Repository;
using Plantation.Utility;
using System;
using System.Web.Mvc;

namespace Plantation.Controllers
{
    public class FixedAssetGroupController : Controller
    {
        private FixedAssetGroupRepository FAG = new FixedAssetGroupRepository();
        ComboBoxContext context = new ComboBoxContext();

        // GET: Data For Editor Datatables
        public JsonResult Data()
        {
            var request = System.Web.HttpContext.Current.Request;

            using (var db = new Database("sqlserver", Constant.DatabaseConnection))
            {
                var response = new Editor(db, "FIXEDASSETGROUP", "FIXEDASSETGROUP.SID")
                    .Model<JoinModelFixedAssetGroup>("FIXEDASSETGROUP")
                    .Model<JoinModelCostCenter>("COSTCENTER")
                    .Field(new Field("FIXEDASSETGROUP.IDFAGROUP")
                        .Validator(Validation.NotEmpty())
                    )
                    .Field(new Field("FIXEDASSETGROUP.FAGROUPNAME")
                        .Validator(Validation.NotEmpty())
                    )
                    .Field(new Field("FIXEDASSETGROUP.ESTIMATELIFEYEAR")
                        .Validator(Validation.NotEmpty())
                    )
                    .Field(new Field("FIXEDASSETGROUP.UPDATEBY").SetValue(Session["userid"].ToString()))
                    .Field(new Field("FIXEDASSETGROUP.UPDATEDATE").SetValue(DateTime.Now))
                    .Field(new Field("FIXEDASSETGROUP.COSTCENTER")
                        .Options(new Options()
                            .Table("COSTCENTER")
                            .Value("SID")
                            .Label("COSTCENTERNAME")
                        )
                        .Validator(Validation.DbValues(new ValidationOpts { Empty = false }))
                    )
                    .LeftJoin("COSTCENTER", "COSTCENTER.SID", "=", "FIXEDASSETGROUP.COSTCENTER")
                    .Process(request)
                    .Data();

                return Json(response, JsonRequestBehavior.AllowGet);
            }
        }

        // GET: FixedAssetGroup
        public ActionResult Index()
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            return View(FAG.GetAll());
        }

        public JsonResult GetCostCenter()
        {
            var users = context.GetCostCenter();
            return Json(users, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Details(int? id)
        {

            return View(FAG.Find(id));
        }

        [HttpPost]
        public JsonResult Create(FixedAssetGroup fixedassetgroup, string userid)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    FAG.Add(fixedassetgroup, Session["userid"].ToString());
                }
            }
            catch
            {
                return Json("error");
            }

            return Json("success");
        }

        [HttpPost]
        public JsonResult Edit(FixedAssetGroup fixedassetgroup, string userid)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    FAG.Update(fixedassetgroup, Session["userid"].ToString());
                }
            }
            catch
            {
                return Json("error");
            }

            return Json("success");
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