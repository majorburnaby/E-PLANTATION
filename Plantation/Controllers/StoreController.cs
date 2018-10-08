using DataTables;
using Plantation.Models;
using Plantation.Models.DB;
using Plantation.Repository;
using Plantation.Utility;
using System;
using System.Web.Mvc;
namespace Plantation.Controllers
{
    public class StoreController : Controller
    {
        private StoreRepository STR = new StoreRepository();
        ComboBoxContext context = new ComboBoxContext();
        // GET: Data For Editor Datatables
        public JsonResult Data()
        {
            var request = System.Web.HttpContext.Current.Request;

            using (var db = new Database("sqlserver", Constant.DatabaseConnection))
            {
                var response = new Editor(db, "STORE", "STORE.SID")
                    .Model<JoinModelStore>("STORE")
                    .Model<JoinModelParameterValue>("PARAMETERVALUE")
                    .Field(new Field("STORE.IDSTORE")
                        .Validator(Validation.NotEmpty())
                    )
                    .Field(new Field("STORE.STORENAME")
                        .Validator(Validation.NotEmpty())
                    )
                    .Field(new Field("STORE.DESCRIPTION")
                        .Validator(Validation.NotEmpty())
                    )
                    .Field(new Field("STORE.ISACTIVE"))
                    .Field(new Field("STORE.UPDATEBY").SetValue(Session["userid"].ToString()))
                    .Field(new Field("STORE.UPDATEDATE").SetValue(DateTime.Now))
                    .Field(new Field("STORE.WAREHOUSETYPE")
                        .Options(new Options()
                            .Table("PARAMETERVALUE")
                            .Value("SID")
                            .Label("PARAMETERVALUENAME")
                        )
                        .Validator(Validation.DbValues(new ValidationOpts { Empty = false }))
                    )
                    .LeftJoin("PARAMETERVALUE", "PARAMETERVALUE.SID", "=", "STORE.WAREHOUSETYPE")
                    .Where("STORE.COMPANYSITE", int.Parse(Session["companysite"].ToString()))
                    .Process(request)
                    .Data();

                return Json(response, JsonRequestBehavior.AllowGet);
            }
        }

        // GET: Store
        public ActionResult Index()
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            return View(STR.GetAll());
        }

        public JsonResult GetParameterValueSTRList()
        {
            var users = context.GetParameterValueSTR();
            return Json(users, JsonRequestBehavior.AllowGet);
        }

        //
        // GET: /Store/Details/5
        public ActionResult Details(int? id)
        {

            return View(STR.Find(id));
        }

        [HttpPost]
        public JsonResult Create(Store store, string userid)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    store.COMPANYSITE = int.Parse(Session["companysite"].ToString());
                    STR.Add(store, Session["userid"].ToString());
                }
            }
            catch
            {
                return Json("error");
            }

            return Json("success");
        }

        [HttpPost]
        public JsonResult Edit(Store store, string userid)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    store.COMPANYSITE = int.Parse(Session["companysite"].ToString());
                    STR.Update(store, Session["userid"].ToString());
                }
            }
            catch
            {
                return Json("error");
            }

            return Json("success");
        }

        //
        // GET: /Store/Delete/5

        public ActionResult Delete(int id)
        {
            return View(STR.Find(id));
        }

        //
        // POST: /Store/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                STR.Remove(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}