using DataTables;
using Plantation.Models;
using Plantation.Models.DB;
using Plantation.Repository;
using Plantation.Utility;
using System;
using System.Web.Mvc;

namespace Plantation.Controllers
{
    public class StockGroupController : Controller
    {
        private StockGroupRepository STG = new StockGroupRepository();
        ComboBoxContext context = new ComboBoxContext();

        // GET: Data For Editor Datatables
        public JsonResult Data()
        {
            var request = System.Web.HttpContext.Current.Request;

            using (var db = new Database("sqlserver", Constant.DatabaseConnection))
            {
                var response = new Editor(db, "STOCKGROUP", "STOCKGROUP.SID")
                    .Model<JoinModelStockGroup>("STOCKGROUP")
                    .Model<JoinModelControlJob>("CONTROLJOB")
                    .Field(new Field("STOCKGROUP.IDSTOCKGROUP")
                        .Validator(Validation.NotEmpty())
                    )
                    .Field(new Field("STOCKGROUP.STOCKGROUPNAME")
                        .Validator(Validation.NotEmpty())
                    )
                    .Field(new Field("STOCKGROUP.UPDATEBY").SetValue(Session["userid"].ToString()))
                    .Field(new Field("STOCKGROUP.UPDATEDATE").SetValue(DateTime.Now))
                    .Field(new Field("STOCKGROUP.CONTROLJOB")
                        .Options(new Options()
                            .Table("CONTROLJOB")
                            .Value("SID")
                            .Label("ITEMDESCRIPTION")
                        )
                        .Validator(Validation.DbValues(new ValidationOpts { Empty = false }))
                    )
                    .LeftJoin("CONTROLJOB", "CONTROLJOB.SID", "=", "STOCKGROUP.CONTROLJOB")
                    .Process(request)
                    .Data();

                return Json(response, JsonRequestBehavior.AllowGet);
            }
        }

        // GET: StockGroup
        public ActionResult Index()
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            return View(STG.GetAll());
        }

        public JsonResult GetControlJob()
        {
            var users = context.GetControlJob();
            return Json(users, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Details(int? id)
        {

            return View(STG.Find(id));
        }

        [HttpPost]
        public JsonResult Create(StockGroup stockgroup, string userid)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    STG.Add(stockgroup, Session["userid"].ToString());
                }
            }
            catch
            {
                return Json("error");
            }

            return Json("success");
        }

        [HttpPost]
        public JsonResult Edit(StockGroup stockgroup, string userid)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    STG.Update(stockgroup, Session["userid"].ToString());
                }
            }
            catch
            {
                return Json("error");
            }

            return Json("success");
        }

        //
        // POST: /StockGroup/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                STG.Remove(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}