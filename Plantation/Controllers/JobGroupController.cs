using Plantation.Models.DB;
using Plantation.Repository;
using System.Web.Mvc;
using DataTables;
using Plantation.Utility;
using System;
using Dapper;

namespace Plantation.Controllers
{
    public class JobGroupController : Controller
    {
        private JobGroupRepository IJG = new JobGroupRepository();

        public JsonResult Data()
        {
            var request = System.Web.HttpContext.Current.Request;

            using (var db = new Database("sqlserver", Constant.DatabaseConnection))
            {
                var response = new Editor(db, "JOBGROUP", "SID")
                    .Model<JobGroup>()
                    .Field(new Field("IDJOBGROUP")
                        .Validator(Validation.NotEmpty())
                    )
                    .Field(new Field("JOBGROUPNAME")
                        .Validator(Validation.NotEmpty())
                    )
                    .Field(new Field("UPDATEBY").SetValue(Session["userid"].ToString()))
                    .Field(new Field("UPDATEDATE").SetValue(DateTime.Now))
                    .Process(request)
                    .Data();

                return Json(response, JsonRequestBehavior.AllowGet);
            }
        }

        // GET: JobGroup
        public ActionResult Index()
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            return View(IJG.GetAll());
        }

        //
        // GET: /JobGroup/Details/5
        public ActionResult Details(int? id)
        {

            return View(IJG.Find(id));
        }

        [HttpPost]
        public JsonResult Create(JobGroup jobgroup, string userid)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    IJG.Add(jobgroup, Session["userid"].ToString());
                }
            }
            catch
            {
                return Json("error");
            }

            return Json("success");
        }

        [HttpPost]
        public JsonResult Edit(JobGroup jobgroup, string userid)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    IJG.Update(jobgroup, Session["userid"].ToString());
                }
            }
            catch
            {
                return Json("error");
            }

            return Json("success");
        }

        //
        // GET: /JobGroup/Delete/5

        public ActionResult Delete(int id)
        {
            return View(IJG.Find(id));
        }

        //
        // POST: /JobGroup/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                IJG.Remove(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}