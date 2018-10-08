using DataTables;
using Plantation.Models;
using Plantation.Models.DB;
using Plantation.Repository;
using Plantation.Utility;
using System;
using System.Web.Mvc;

namespace Plantation.Controllers
{
    public class FixedAssetController : Controller
    {
        private FixedAssetRepository FA = new FixedAssetRepository();
        ComboBoxContext context = new ComboBoxContext();

        // GET: Data For Editor Datatables
        public JsonResult Data()
        {
            var request = System.Web.HttpContext.Current.Request;

            using (var db = new Database("sqlserver", Constant.DatabaseConnection))
            {
                var response = new Editor(db, "FIXEDASSET", "FIXEDASSET.SID")
                    .Model<JoinModelFixedAsset>("FIXEDASSET")
                    .Model<JoinModelFixedAssetGroup>("FIXEDASSETGROUP")
                    .Model<JoinModelUOM>("UNITOFMEASURE")
                    .Field(new Field("FIXEDASSET.IDFIXEDASSET")
                        .Validator(Validation.NotEmpty())
                    )
                    .Field(new Field("FIXEDASSET.FIXEDASSETNAME")
                        .Validator(Validation.NotEmpty())
                    )
                    .Field(new Field("FIXEDASSET.DESCRIPTION")
                        .Validator(Validation.NotEmpty())
                    )
                    .Field(new Field("FIXEDASSET.ISACTIVE"))
                    .Field(new Field("FIXEDASSET.UPDATEBY").SetValue(Session["userid"].ToString()))
                    .Field(new Field("FIXEDASSET.UPDATEDATE").SetValue(DateTime.Now))
                    .Field(new Field("FIXEDASSET.FIXEDASSETGROUP")
                        .Options(new Options()
                            .Table("FIXEDASSETGROUP")
                            .Value("SID")
                            .Label("FAGROUPNAME")
                        )
                        .Validator(Validation.DbValues(new ValidationOpts { Empty = false }))
                    )
                    .Field(new Field("FIXEDASSET.UOM")
                        .Options(new Options()
                            .Table("UNITOFMEASURE")
                            .Value("SID")
                            .Label("UOMNAME")
                        )
                        .Validator(Validation.DbValues(new ValidationOpts { Empty = false }))
                    )
                    .LeftJoin("FIXEDASSETGROUP", "FIXEDASSETGROUP.SID", "=", "FIXEDASSET.FIXEDASSETGROUP")
                    .LeftJoin("UNITOFMEASURE", "UNITOFMEASURE.SID", "=", "FIXEDASSET.UOM")
                    .Where("FIXEDASSET.COMPANYSITE", int.Parse(Session["companysite"].ToString()))
                    .Process(request)
                    .Data();

                return Json(response, JsonRequestBehavior.AllowGet);
            }
        }

        //
        // GET: /FixedAsset/
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
        
        [HttpPost]
        public JsonResult Create(FixedAsset fixedasset, string userid)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    fixedasset.COMPANYSITE = int.Parse(Session["companysite"].ToString());
                    FA.Add(fixedasset, Session["userid"].ToString());
                }
            }
            catch
            {
                return Json("error");
            }

            return Json("success");
        }
        
        [HttpPost]
        public JsonResult Edit(FixedAsset fixedasset, string userid)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    fixedasset.COMPANYSITE = int.Parse(Session["companysite"].ToString());
                    FA.Update(fixedasset, Session["userid"].ToString());
                }
            }
            catch
            {
                return Json("error");
            }

            return Json("success");
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