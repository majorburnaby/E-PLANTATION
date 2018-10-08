using Plantation.Models.DB;
using Plantation.Repository;
using System.Web.Mvc;
using DataTables;
using Plantation.Utility;
using System;

namespace Plantation.Controllers
{
    public class ParameterController : Controller
    {
        private ParameterRepository IPR = new ParameterRepository();

        #region PARAMETER

        public JsonResult Data()
        {
            var request = System.Web.HttpContext.Current.Request;

            using (var db = new Database("sqlserver", Constant.DatabaseConnection))
            {
                var response = new Editor(db, "PARAMETER", "SID")
                    .Model<Parameter>()
                    .Field(new Field("IDPARAMETER")
                        .Validator(Validation.NotEmpty())
                    )
                    .Field(new Field("PARAMETERNAME")
                        .Validator(Validation.NotEmpty())
                    )
                    .Field(new Field("UPDATEBY").SetValue(Session["userid"].ToString()))
                    .Field(new Field("UPDATEDATE").SetValue(DateTime.Now))
                    .Process(request)
                    .Data();

                return Json(response, JsonRequestBehavior.AllowGet);
            }
        }

        // GET: Parameter
        public ActionResult Index()
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            return View(IPR.GetAll());
        }

        //
        // GET: /Parameter/Details/5
        public ActionResult Details(int? id)
        {

            return View(IPR.Find(id));
        }

        [HttpPost]
        public JsonResult Create(Parameter parameter, string userid)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    IPR.Add(parameter, Session["userid"].ToString());
                }
            }
            catch
            {
                return Json("error");
            }

            return Json("success");
        }

        [HttpPost]
        public JsonResult Edit(Parameter parameter, string userid)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    IPR.Update(parameter, Session["userid"].ToString());
                }
            }
            catch
            {
                return Json("error");
            }

            return Json("success");
        }

        //
        // GET: /Parameter/Delete/5

        public ActionResult Delete(int id)
        {
            return View(IPR.Find(id));
        }

        //
        // POST: /Parameter/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                IPR.Remove(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        #endregion PARAMETER
    }
}