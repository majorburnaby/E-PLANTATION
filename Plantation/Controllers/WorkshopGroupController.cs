using Plantation.Models.DB;
using Plantation.Repository;
using System.Web.Mvc;
using DataTables;
using Plantation.Utility;
using System;
using Dapper;

namespace Plantation.Controllers
{
    public class WorkshopGroupController : Controller
    {
        private WorkshopGroupRepository IWG = new WorkshopGroupRepository();

        public JsonResult Data()
        {
            var request = System.Web.HttpContext.Current.Request;

            using (var db = new Database("sqlserver", Constant.DatabaseConnection))
            {
                var response = new Editor(db, "WORKSHOPGROUP", "SID")
                    .Model<WorkshopGroup>()
                    .Field(new Field("IDWORKSHOPGROUP")
                        .Validator(Validation.NotEmpty())
                    )
                    .Field(new Field("WORKSHOPGROUPNAME")
                        .Validator(Validation.NotEmpty())
                    )
                    .Field(new Field("UPDATEBY").SetValue(Session["userid"].ToString()))
                    .Field(new Field("UPDATEDATE").SetValue(DateTime.Now))
                    .Process(request)
                    .Data();

                return Json(response, JsonRequestBehavior.AllowGet);
            }
        }

        // GET: WorkshopGroup
        public ActionResult Index()
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            return View(IWG.GetAll());
        }

        //
        // GET: /WorkshopGroup/Details/5
        public ActionResult Details(int? id)
        {

            return View(IWG.Find(id));
        }

        [HttpPost]
        public JsonResult Create(WorkshopGroup workshopgroup, string userid)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    IWG.Add(workshopgroup, Session["userid"].ToString());
                }
            }
            catch
            {
                return Json("error");
            }

            return Json("success");
        }

        [HttpPost]
        public JsonResult Edit(WorkshopGroup workshopgroup, string userid)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    IWG.Update(workshopgroup, Session["userid"].ToString());
                }
            }
            catch
            {
                return Json("error");
            }

            return Json("success");
        }

        //
        // GET: /WorkshopGroup/Delete/5

        public ActionResult Delete(int id)
        {
            return View(IWG.Find(id));
        }

        //
        // POST: /WorkshopGroup/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                IWG.Remove(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}