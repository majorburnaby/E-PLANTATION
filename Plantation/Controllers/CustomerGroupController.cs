using Plantation.Models.DB;
using Plantation.Repository;
using System.Web.Mvc;
using DataTables;
using Plantation.Utility;
using System;

namespace Plantation.Controllers
{
    public class CustomerGroupController : Controller
    {
        private CustomerGroupRepository CSG = new CustomerGroupRepository();

        public JsonResult Data()
        {
            var request = System.Web.HttpContext.Current.Request;

            using (var db = new Database("sqlserver", Constant.DatabaseConnection))
            {
                var response = new Editor(db, "CUSTOMERGROUP", "SID")
                    .Model<CustomerGroup>()
                    .Field(new Field("IDCUSTOMERGROUP")
                        .Validator(Validation.NotEmpty())
                    )
                    .Field(new Field("CUSTOMERGROUPNAME")
                        .Validator(Validation.NotEmpty())
                    )
                    .Field(new Field("UPDATEBY").SetValue(Session["userid"].ToString()))
                    .Field(new Field("UPDATEDATE").SetValue(DateTime.Now))
                    .Process(request)
                    .Data();

                return Json(response, JsonRequestBehavior.AllowGet);
            }
        }

        // GET: CustomerGroup
        public ActionResult Index()
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            return View(CSG.GetAll());
        }

        //
        // GET: /CustomerGroup/Details/5
        public ActionResult Details(int? id)
        {

            return View(CSG.Find(id));
        }

        [HttpPost]
        public JsonResult Create(CustomerGroup customergroup, string userid)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    CSG.Add(customergroup, Session["userid"].ToString());
                }
            }
            catch
            {
                return Json("error");
            }

            return Json("success");
        }

        [HttpPost]
        public JsonResult Edit(CustomerGroup customergroup, string userid)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    CSG.Update(customergroup, Session["userid"].ToString());
                }
            }
            catch
            {
                return Json("error");
            }

            return Json("success");
        }

        //
        // GET: /CustomerGroup/Delete/5

        public ActionResult Delete(int id)
        {
            return View(CSG.Find(id));
        }

        //
        // POST: /CustomerGroup/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                CSG.Remove(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}