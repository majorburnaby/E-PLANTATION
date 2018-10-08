using DataTables;
using Plantation.Models;
using Plantation.Models.DB;
using Plantation.Repository;
using Plantation.Utility;
using System;
using System.Web.Mvc;

namespace Plantation.Controllers
{
    public class ControlJobController : Controller
    {
        private ControlJobRepository ICJ = new ControlJobRepository();
        ComboBoxContext context = new ComboBoxContext();

        // GET: Data For Editor Datatables
        public JsonResult Data()
        {
            var request = System.Web.HttpContext.Current.Request;

            using (var db = new Database("sqlserver", Constant.DatabaseConnection))
            {
                var response = new Editor(db, "CONTROLJOB", "CONTROLJOB.SID")
                    .Model<JoinModelControlJob>("CONTROLJOB")
                    .Model<JoinModelJob>("JOB")
                    .Field(new Field("CONTROLJOB.ITEMCODE")
                        .Validator(Validation.NotEmpty())
                    )
                    .Field(new Field("CONTROLJOB.ITEMDESCRIPTION")
                        .Validator(Validation.NotEmpty())
                    )
                    .Field(new Field("CONTROLJOB.CONTROLSYSTEM")
                        .Validator(Validation.NotEmpty())
                    )
                    .Field(new Field("CONTROLJOB.ISACTIVE"))
                    .Field(new Field("CONTROLJOB.UPDATEBY").SetValue(Session["userid"].ToString()))
                    .Field(new Field("CONTROLJOB.UPDATEDATE").SetValue(DateTime.Now))
                    .Field(new Field("CONTROLJOB.JOB")
                        .Options(new Options()
                            .Table("JOB")
                            .Value("SID")
                            .Label("JOBNAME")
                        )
                        .Validator(Validation.DbValues(new ValidationOpts { Empty = false }))
                    )
                    .LeftJoin("JOB", "JOB.SID", "=", "CONTROLJOB.JOB")
                    .Process(request)
                    .Data();

                return Json(response, JsonRequestBehavior.AllowGet);
            }
        }

        //
        // GET: /ControlJob/
        public ActionResult Index()
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            return View(ICJ.GetAll());
        }

        public JsonResult GetJobList()
        {
            var users = context.GetJob();
            return Json(users, JsonRequestBehavior.AllowGet);
        }
        
        //
        // GET: /ControlJob/Details/5
        public ActionResult Details(int? id)
        {

            return View(ICJ.Find(id));
        }
        
        [HttpPost]
        public JsonResult Create(ControlJob controljob, string userid)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ICJ.Add(controljob, Session["userid"].ToString());
                }
            }
            catch
            {
                return Json("error");
            }

            return Json("success");
        }
        
        [HttpPost]
        public JsonResult Edit(ControlJob controljob, string userid)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ICJ.Update(controljob, Session["userid"].ToString());
                }
            }
            catch
            {
                return Json("error");
            }

            return Json("success");
        }

        //
        // GET: /ControlJob/Delete/5

        public ActionResult Delete(int id)
        {
            return View(ICJ.Find(id));
        }

        //
        // POST: /ControlJob/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                ICJ.Remove(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}