using DataTables;
using Plantation.Models;
using Plantation.Models.DB;
using Plantation.Repository;
using Plantation.Utility;
using System;
using System.Web.Mvc;

namespace Plantation.Controllers
{
    public class FixedAssetLocationController : Controller
    {
        private FixedAssetLocationRepository FAL = new FixedAssetLocationRepository();
        ComboBoxContext context = new ComboBoxContext();

        // GET: Data For Editor Datatables
        public JsonResult Data()
        {
            var request = System.Web.HttpContext.Current.Request;

            using (var db = new Database("sqlserver", Constant.DatabaseConnection))
            {
                var response = new Editor(db, "FIXEDASSETLOCATION", "FIXEDASSETLOCATION.SID")
                    .Model<JoinModelFixedAssetLocation>("FIXEDASSETLOCATION")
                    .Model<JoinModelCostCenter>("COSTCENTER")
                    .Field(new Field("FIXEDASSETLOCATION.IDFIXEDASSETLOCATION")
                        .Validator(Validation.NotEmpty())
                    )
                    .Field(new Field("FIXEDASSETLOCATION.FIXEDASSETLOCATIONNAME")
                        .Validator(Validation.NotEmpty())
                    )
                    .Field(new Field("FIXEDASSETLOCATION.DESCRIPTION")
                        .Validator(Validation.NotEmpty())
                    )
                    .Field(new Field("FIXEDASSETLOCATION.ISACTIVE"))
                    .Field(new Field("FIXEDASSETLOCATION.UPDATEBY").SetValue(Session["userid"].ToString()))
                    .Field(new Field("FIXEDASSETLOCATION.UPDATEDATE").SetValue(DateTime.Now))
                    .Field(new Field("FIXEDASSETLOCATION.COSTCENTER")
                        .Options(new Options()
                            .Table("COSTCENTER")
                            .Value("SID")
                            .Label("COSTCENTERNAME")
                        )
                        .Validator(Validation.DbValues(new ValidationOpts { Empty = false }))
                    )
                    .LeftJoin("COSTCENTER", "COSTCENTER.SID", "=", "FIXEDASSETLOCATION.COSTCENTER")
                    .Where("FIXEDASSETLOCATION.COMPANYSITE", int.Parse(Session["companysite"].ToString()))
                    .Process(request)
                    .Data();

                return Json(response, JsonRequestBehavior.AllowGet);
            }
        }

        //
        // GET: /FixedAssetLocation/
        public ActionResult Index()
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            return View(FAL.GetAll());
        }
                
        public JsonResult GetCostCenterList()
        {
            var users = context.GetCostCenter();
            return Json(users, JsonRequestBehavior.AllowGet);
        }

        //
        // GET: /FixedAssetLocation/Details/5
        public ActionResult Details(int? id)
        {

            return View(FAL.Find(id));
        }
        
        [HttpPost]
        public JsonResult Create(FixedAssetLocation FixedAssetLocation, string userid)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    FixedAssetLocation.COMPANYSITE = int.Parse(Session["companysite"].ToString());
                    FAL.Add(FixedAssetLocation, Session["userid"].ToString());
                }
            }
            catch
            {
                return Json("error");
            }

            return Json("success");
        }
        
        [HttpPost]
        public JsonResult Edit(FixedAssetLocation FixedAssetLocation, string userid)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    FixedAssetLocation.COMPANYSITE = int.Parse(Session["companysite"].ToString());
                    FAL.Update(FixedAssetLocation, Session["userid"].ToString());
                }
            }
            catch
            {
                return Json("error");
            }

            return Json("success");
        }

        //
        // GET: /FixedAssetLocation/Delete/5

        public ActionResult Delete(int id)
        {
            return View(FAL.Find(id));
        }

        //
        // POST: /FixedAssetLocation/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                FAL.Remove(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}