using Plantation.Models.DB;
using Plantation.Repository;
using System.Web.Mvc;
using DataTables;
using Plantation.Utility;
using System;

namespace Plantation.Controllers
{
    public class TransporterGroupController : Controller
    {
        private TransporterGroupRepository ITG = new TransporterGroupRepository();

        public JsonResult Data()
        {
            var request = System.Web.HttpContext.Current.Request;

            using (var db = new Database("sqlserver", Constant.DatabaseConnection))
            {
                var response = new Editor(db, "TRANSPORTERGROUP", "SID")
                    .Model<TransporterGroup>()
                    .Field(new Field("IDTRANSPORTERGROUP")
                        .Validator(Validation.NotEmpty())
                    )
                    .Field(new Field("TRANSPORTERGROUPNAME")
                        .Validator(Validation.NotEmpty())
                    )
                    .Field(new Field("UPDATEBY").SetValue(Session["userid"].ToString()))
                    .Field(new Field("UPDATEDATE").SetValue(DateTime.Now))
                    .Process(request)
                    .Data();

                return Json(response, JsonRequestBehavior.AllowGet);
            }
        }

        // GET: TransporterGroup
        public ActionResult Index()
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            return View(ITG.GetAll());
        }

        //
        // GET: /TransporterGroup/Details/5
        public ActionResult Details(int? id)
        {

            return View(ITG.Find(id));
        }

        [HttpPost]
        public JsonResult Create(TransporterGroup transportergroup, string userid)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ITG.Add(transportergroup, Session["userid"].ToString());
                }
            }
            catch
            {
                return Json("error");
            }

            return Json("success");
        }

        [HttpPost]
        public JsonResult Edit(TransporterGroup transportergroup, string userid)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ITG.Update(transportergroup, Session["userid"].ToString());
                }
            }
            catch
            {
                return Json("error");
            }

            return Json("success");
        }

        //
        // GET: /TransporterGroup/Delete/5

        public ActionResult Delete(int id)
        {
            return View(ITG.Find(id));
        }

        //
        // POST: /TransporterGroup/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                ITG.Remove(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}