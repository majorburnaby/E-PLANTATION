using DataTables;
using Plantation.Models;
using Plantation.Models.DB;
using Plantation.Repository;
using Plantation.Utility;
using System;
using System.Web.Mvc;
namespace Plantation.Controllers
{
    public class StationController : Controller
    {
        private StationRepository ST = new StationRepository();

        // GET: Data For Editor Datatables
        public JsonResult Data()
        {
            var request = System.Web.HttpContext.Current.Request;

            using (var db = new Database("sqlserver", Constant.DatabaseConnection))
            {
                var response = new Editor(db, "STATION", "SID")
                    .Model<Station>()
                    .Field(new Field("IDSTATION")
                        .Validator(Validation.NotEmpty())
                    )
                    .Field(new Field("STATIONNAME")
                        .Validator(Validation.NotEmpty())
                    )
                    .Field(new Field("ISACTIVE"))
                    .Field(new Field("UPDATEBY").SetValue(Session["userid"].ToString()))
                    .Field(new Field("UPDATEDATE").SetValue(DateTime.Now))
                    .Process(request)
                    .Data();

                return Json(response, JsonRequestBehavior.AllowGet);
            }
        }

        // GET: Station
        public ActionResult Index()
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            return View(ST.GetAll());
        }

        //
        // GET: /Station/Details/5
        public ActionResult Details(int? id)
        {

            return View(ST.Find(id));
        }
        
        [HttpPost]
        public JsonResult Create(Station station, string userid)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ST.Add(station, Session["userid"].ToString());
                }
            }
            catch
            {
                return Json("error");
            }

            return Json("success");
        }

        [HttpPost]
        public JsonResult Edit(Station station, string userid)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ST.Update(station, Session["userid"].ToString());
                }
            }
            catch
            {
                return Json("error");
            }

            return Json("success");
        }

        //
        // GET: /Station/Delete/5

        public ActionResult Delete(int id)
        {
            return View(ST.Find(id));
        }

        //
        // POST: /Station/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                ST.Remove(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}