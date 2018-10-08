using Plantation.Models.DB;
using Plantation.Repository;
using System.Web.Mvc;
using DataTables;
using Plantation.Utility;
using System;

namespace Plantation.Controllers
{
    public class DepartmentController : Controller
    {
        private DepartmentRepository IPD = new DepartmentRepository();

        public JsonResult Data()
        {
            var request = System.Web.HttpContext.Current.Request;

            using (var db = new Database("sqlserver", Constant.DatabaseConnection))
            {
                var response = new Editor(db, "Department", "SID")
                    .Model<Department>()
                    .Field(new Field("IDDEPARTMENT")
                        .Validator(Validation.NotEmpty())
                    )
                    .Field(new Field("DEPARTMENTNAME")
                        .Validator(Validation.NotEmpty())
                    )
                    .Field(new Field("DESCRIPTION")
                        .Validator(Validation.NotEmpty())
                    )
                    .Field(new Field("UPDATEBY").SetValue(Session["userid"].ToString()))
                    .Field(new Field("UPDATEDATE").SetValue(DateTime.Now))
                    .Process(request)
                    .Data();

                return Json(response, JsonRequestBehavior.AllowGet);
            }
        }

        // GET: Department
        public ActionResult Index()
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            return View(IPD.GetAll());
        }

        //
        // GET: /Department/Details/5
        public ActionResult Details(int? id)
        {

            return View(IPD.Find(id));
        }

        [HttpPost]
        public JsonResult Create(Department department, string userid)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    IPD.Add(department, Session["userid"].ToString());
                }
            }
            catch
            {
                return Json("error");
            }

            return Json("success");
        }

        [HttpPost]
        public JsonResult Edit(Department department, string userid)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    IPD.Update(department, Session["userid"].ToString());
                }
            }
            catch
            {
                return Json("error");
            }

            return Json("success");
        }

        //
        // GET: /Department/Delete/5

        public ActionResult Delete(int id)
        {
            return View(IPD.Find(id));
        }

        //
        // POST: /Department/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                IPD.Remove(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}