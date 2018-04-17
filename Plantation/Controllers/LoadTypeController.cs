using DataTables;
using Plantation.Models;
using Plantation.Models.DB;
using Plantation.Repository;
using Plantation.Utility;
using System;
using System.Web.Mvc;

namespace Plantation.Controllers
{
    public class LoadTypeController : Controller
    {
        private LoadTypeRepository LTR = new LoadTypeRepository();
        ComboBoxContext context = new ComboBoxContext();

        // GET: Data For Editor Datatables
        public JsonResult Data()
        {
            var request = System.Web.HttpContext.Current.Request;

            using (var db = new Database("sqlserver", Constant.DatabaseConnection))
            {
                var response = new Editor(db, "LOADTYPE", "LOADTYPE.SID")
                    .Model<JoinModelLoadType>("LOADTYPE")
                    .Model<JoinModelUOM>("UNITOFMEASURE")
                    .Field(new Field("LOADTYPE.IDLOADTYPE")
                        .Validator(Validation.NotEmpty())
                    )
                    .Field(new Field("LOADTYPE.LOADTYPENAME")
                        .Validator(Validation.NotEmpty())
                    )
                    .Field(new Field("LOADTYPE.UPDATEBY").SetValue(Session["userid"].ToString()))
                    .Field(new Field("LOADTYPE.UPDATEDATE").SetValue(DateTime.Now))
                    .Field(new Field("LOADTYPE.UOM")
                        .Options(new Options()
                            .Table("UNITOFMEASURE")
                            .Value("SID")
                            .Label("UOMNAME")
                        )
                        .Validator(Validation.DbValues(new ValidationOpts { Empty = false }))
                    )
                    .LeftJoin("UNITOFMEASURE", "UNITOFMEASURE.SID", "=", "LOADTYPE.UOM")
                    .Process(request)
                    .Data();

                return Json(response, JsonRequestBehavior.AllowGet);
            }
        }

        // GET: LoadType
        public ActionResult Index()
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            return View(LTR.GetAll());
        }

        public JsonResult GetUnitOfMeasureList()
        {
            var users = context.GetUnitOfMeasure();
            return Json(users, JsonRequestBehavior.AllowGet);
        }

        //
        // GET: /LoadType/Details/5
        public ActionResult Details(int? id)
        {

            return View(LTR.Find(id));
        }

        //
        // POST: /LoadType/Create
        //[HttpPost]
        //public ActionResult Create(LoadType loadtype, string userid)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        LTR.Add(loadtype, Session["userid"].ToString());
        //    }

        //    return View(loadtype);
        //}
        [HttpPost]
        public JsonResult Create(LoadType loadtype, string userid)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    LTR.Add(loadtype, Session["userid"].ToString());
                }
            }
            catch
            {
                return Json("error");
            }

            return Json("success");
        }

        // POST: /LoadType/Edit/5
        //[HttpPost]
        //public ActionResult Edit(LoadType loadtype, string userid)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        LTR.Update(loadtype, Session["userid"].ToString());
        //    }
        //    return View(loadtype);
        //}
        [HttpPost]
        public JsonResult Edit(LoadType loadtype, string userid)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    LTR.Update(loadtype, Session["userid"].ToString());
                }
            }
            catch
            {
                return Json("error");
            }

            return Json("success");
        }

        //
        // GET: /LoadType/Delete/5

        public ActionResult Delete(int id)
        {
            return View(LTR.Find(id));
        }

        //
        // POST: /LoadType/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                LTR.Remove(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}