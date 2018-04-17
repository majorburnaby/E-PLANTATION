using DataTables;
using Plantation.Models;
using Plantation.Models.DB;
using Plantation.Repository;
using Plantation.Utility;
using System;
using System.Web.Mvc;

namespace Plantation.Controllers
{
    public class ParameterValueController : Controller
    {
        private ParameterValueRepository IPV = new ParameterValueRepository();
        ComboBoxContext context = new ComboBoxContext();
        // GET: Data For Editor Datatables
        public JsonResult Data()
        {
            var request = System.Web.HttpContext.Current.Request;

            using (var db = new Database("sqlserver", Constant.DatabaseConnection))
            {
                var response = new Editor(db, "PARAMETERVALUE", "PARAMETERVALUE.SID")
                    .Model<JoinModelParameterValue>("PARAMETERVALUE")
                    .Model<JoinModelParameter>("PARAMETER")
                    .Field(new Field("PARAMETERVALUE.IDPARAMETERVALUE")
                        .Validator(Validation.NotEmpty())
                    )
                    .Field(new Field("PARAMETERVALUE.PARAMETERVALUENAME")
                        .Validator(Validation.NotEmpty())
                    )
                    .Field(new Field("PARAMETERVALUE.UPDATEBY").SetValue(Session["userid"].ToString()))
                    .Field(new Field("PARAMETERVALUE.UPDATEDATE").SetValue(DateTime.Now))
                    .Field(new Field("PARAMETERVALUE.PARAMETER")
                        .Options(new Options()
                            .Table("PARAMETER")
                            .Value("SID")
                            .Label("PARAMETERNAME")
                        )
                        .Validator(Validation.DbValues(new ValidationOpts { Empty = false }))
                    )
                    .LeftJoin("PARAMETER", "PARAMETER.SID", "=", "PARAMETERVALUE.PARAMETER")
                    .Process(request)
                    .Data();

                return Json(response, JsonRequestBehavior.AllowGet);
            }
        }

        // GET: ParameterValue
        public ActionResult Index()
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            return View(IPV.GetAll());
        }

        public JsonResult GetParameterList()
        {
            var users = context.GetParameter();
            return Json(users, JsonRequestBehavior.AllowGet);
        }

        //
        // GET: /ParameterValue/Details/5
        public ActionResult Details(int? id)
        {

            return View(IPV.Find(id));
        }

        //
        // GET: /ParameterValue/Create
        //public ActionResult Create(ParameterValue parametervalue, string userid)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        IPV.Add(parametervalue, Session["userid"].ToString());
        //    }

        //    return View(parametervalue);
        //}
        [HttpPost]
        public JsonResult Create(ParameterValue parametervalue, string userid)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    IPV.Add(parametervalue, Session["userid"].ToString());
                }
            }
            catch
            {
                return Json("error");
            }

            return Json("success");
        }

        // POST: /ParameterValue/Edit/5
        //[HttpPost]
        //public ActionResult Edit(ParameterValue parametervalue, string userid)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        IPV.Update(parametervalue, Session["userid"].ToString());
        //    }
        //    return View(parametervalue);
        //}
        [HttpPost]
        public JsonResult Edit(ParameterValue parametervalue, string userid)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    IPV.Update(parametervalue, Session["userid"].ToString());
                }
            }
            catch
            {
                return Json("error");
            }

            return Json("success");
        }

        //
        // GET: /ParameterValue/Delete/5

        public ActionResult Delete(int id)
        {
            return View(IPV.Find(id));
        }

        //
        // POST: /ParameterValue/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                IPV.Remove(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}