using DataTables;
using Plantation.Models;
using Plantation.Models.DB;
using Plantation.Repository;
using Plantation.Utility;
using System;
using System.Web.Mvc;

namespace Plantation.Controllers
{
    public class JobController : Controller
    {
        private JobRepository IJO = new JobRepository();
        ComboBoxContext context = new ComboBoxContext();

        // GET: Data For Editor Datatables
        public JsonResult Data()
        {
            var request = System.Web.HttpContext.Current.Request;

            using (var db = new Database("sqlserver", Constant.DatabaseConnection))
            {
                var response = new Editor(db, "JOB", "JOB.SID")
                    .Model<JoinModelJob>("JOB")
                    .Model<JoinModelJobType>("JOBTYPE")
                    .Model<JoinModelJobGroup>("JOBGROUP")
                    .Field(new Field("JOB.IDJOB")
                        .Validator(Validation.NotEmpty())
                    )
                    .Field(new Field("JOB.JOBNAME")
                        .Validator(Validation.NotEmpty())
                    )
                    .Field(new Field("JOB.UPDATEBY").SetValue(Session["userid"].ToString()))
                    .Field(new Field("JOB.UPDATEDATE").SetValue(DateTime.Now))
                    .Field(new Field("JOB.JOBGROUP")
                        .Options(new Options()
                            .Table("JOBGROUP")
                            .Value("SID")
                            .Label("JOBGROUPNAME")
                        )
                        .Validator(Validation.DbValues(new ValidationOpts { Empty = false }))
                    )
                    .Field(new Field("JOB.JOBTYPE")
                        .Options(new Options()
                            .Table("JOBTYPE")
                            .Value("SID")
                            .Label("JOBTYPENAME")
                        )
                        .Validator(Validation.DbValues(new ValidationOpts { Empty = false }))
                    )
                    .LeftJoin("JOBGROUP", "JOBGROUP.SID", "=", "JOB.JOBGROUP")
                    .LeftJoin("JOBTYPE", "JOBTYPE.SID", "=", "JOB.JOBTYPE")
                    .Process(request)
                    .Data();

                return Json(response, JsonRequestBehavior.AllowGet);
            }
        }

        //
        // GET: /Job/
        public ActionResult Index()
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            return View(IJO.GetAll());
        }

        public JsonResult GetJobTypeList()
        {
            var users = context.GetJobType();
            return Json(users, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetJobGroupList()
        {
            var users = context.GetJobGroup();
            return Json(users, JsonRequestBehavior.AllowGet);
        }

        //
        // GET: /Job/Details/5
        public ActionResult Details(int? id)
        {
            return View(IJO.Find(id));
        }

        //
        // POST: /Job/Create
        //[HttpPost]
        //public ActionResult Create(Job job, string userid)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        IJO.Add(job, Session["userid"].ToString());
        //    }

        //    return View(job);
        //}
        [HttpPost]
        public JsonResult Create(Job job, string userid)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    IJO.Add(job, Session["userid"].ToString());
                }
            }
            catch
            {
                return Json("error");
            }

            return Json("success");
        }

        //
        // POST: /Job/Edit/5
        //[HttpPost]
        //public ActionResult Edit(Job job, string userid)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        IJO.Update(job, Session["userid"].ToString());
        //    }
        //    return View(job);
        //}
        [HttpPost]
        public JsonResult Edit(Job job, string userid)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    IJO.Update(job, Session["userid"].ToString());
                }
            }
            catch
            {
                return Json("error");
            }

            return Json("success");
        }

        //
        // GET: /Job/Delete/5

        public ActionResult Delete(int id)
        {
            return View(IJO.Find(id));
        }

        //
        // POST: /Job/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                IJO.Remove(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}