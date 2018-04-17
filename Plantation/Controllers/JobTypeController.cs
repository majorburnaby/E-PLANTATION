using Plantation.Models.DB;
using Plantation.Repository;
using System.Web.Mvc;
using DataTables;
using Plantation.Utility;
using System;

namespace Plantation.Controllers
{
    public class JobTypeController : Controller
    {
        private JobTypeRepository IJT = new JobTypeRepository();

        public JsonResult Data()
        {
            var request = System.Web.HttpContext.Current.Request;

            using (var db = new Database("sqlserver", Constant.DatabaseConnection))
            {
                var response = new Editor(db, "JOBTYPE", "SID")
                    .Model<JobType>()
                    .Field(new Field("IDJOBTYPE")
                        .Validator(Validation.NotEmpty())
                    )
                    .Field(new Field("JOBTYPENAME")
                        .Validator(Validation.NotEmpty())
                    )
                    .Field(new Field("UPDATEBY").SetValue(Session["userid"].ToString()))
                    .Field(new Field("UPDATEDATE").SetValue(DateTime.Now))
                    .Process(request)
                    .Data();

                return Json(response, JsonRequestBehavior.AllowGet);
            }
        }

        // GET: JobType
        public ActionResult Index()
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            return View(IJT.GetAll());
        }

        //
        // GET: /JobType/Details/5
        public ActionResult Details(int? id)
        {

            return View(IJT.Find(id));
        }

        [HttpPost]
        public JsonResult Create(JobType jobtype, string userid)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    IJT.Add(jobtype, Session["userid"].ToString());
                }
            }
            catch
            {
                return Json("error");
            }

            return Json("success");
        }

        [HttpPost]
        public JsonResult Edit(JobType jobtype, string userid)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    IJT.Update(jobtype, Session["userid"].ToString());
                }
            }
            catch
            {
                return Json("error");
            }

            return Json("success");
        }

        //
        // GET: /JobType/Delete/5

        public ActionResult Delete(int id)
        {
            return View(IJT.Find(id));
        }

        //
        // POST: /JobType/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                IJT.Remove(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}