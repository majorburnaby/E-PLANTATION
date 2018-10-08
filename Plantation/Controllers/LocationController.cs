using Plantation.Models.DB;
using Plantation.Repository;
using System.Web.Mvc;
using DataTables;
using Plantation.Utility;
using System;

namespace Plantation.Controllers
{
    public class LocationController : Controller
    {
        private LocationRepository ICL = new LocationRepository();

        public JsonResult Data()
        {
            var request = System.Web.HttpContext.Current.Request;

            using (var db = new Database("sqlserver", Constant.DatabaseConnection))
            {
                var response = new Editor(db, "LOCATION", "SID")
                    .Model<Location>()
                    .Field(new Field("IDLOCATION")
                        .Validator(Validation.NotEmpty())
                    )
                    .Field(new Field("LOCATIONNAME")
                        .Validator(Validation.NotEmpty())
                    )
                    .Field(new Field("UPDATEBY").SetValue(Session["userid"].ToString()))
                    .Field(new Field("UPDATEDATE").SetValue(DateTime.Now))
                    .Process(request)
                    .Data();

                return Json(response, JsonRequestBehavior.AllowGet);
            }
        }

        // GET: Location
        public ActionResult Index()
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            return View(ICL.GetAll());
        }

        //
        // GET: /Location/Details/5
        public ActionResult Details(int? id)
        {

            return View(ICL.Find(id));
        }

        [HttpPost]
        public JsonResult Create(Location location, string userid)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ICL.Add(location, Session["userid"].ToString());
                }
            }
            catch
            {
                return Json("error");
            }

            return Json("success");
        }

        [HttpPost]
        public JsonResult Edit(Location location, string userid)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ICL.Update(location, Session["userid"].ToString());
                }
            }
            catch
            {
                return Json("error");
            }

            return Json("success");
        }

        //
        // GET: /location/Delete/5

        public ActionResult Delete(int id)
        {
            return View(ICL.Find(id));
        }

        //
        // POST: /Location/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                ICL.Remove(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}