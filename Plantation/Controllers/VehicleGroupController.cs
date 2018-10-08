using Plantation.Models.DB;
using Plantation.Repository;
using System.Web.Mvc;
using DataTables;
using Plantation.Utility;
using System;
using Dapper;

namespace Plantation.Controllers
{
    public class VehicleGroupController : Controller
    {
        private VehicleGroupRepository IVG = new VehicleGroupRepository();

        public JsonResult Data()
        {
            var request = System.Web.HttpContext.Current.Request;

            using (var db = new Database("sqlserver", Constant.DatabaseConnection))
            {
                var response = new Editor(db, "VEHICLEGROUP", "SID")
                    .Model<VehicleGroup>()
                    .Field(new Field("IDVEHICLEGROUP")
                        .Validator(Validation.NotEmpty())
                    )
                    .Field(new Field("VEHICLEGROUPNAME")
                        .Validator(Validation.NotEmpty())
                    )
                    .Field(new Field("UPDATEBY").SetValue(Session["userid"].ToString()))
                    .Field(new Field("UPDATEDATE").SetValue(DateTime.Now))
                    .Process(request)
                    .Data();

                return Json(response, JsonRequestBehavior.AllowGet);
            }
        }

        // GET: VehicleGroup
        public ActionResult Index()
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            return View(IVG.GetAll());
        }

        //
        // GET: /VehicleGroup/Details/5
        public ActionResult Details(int? id)
        {

            return View(IVG.Find(id));
        }

        [HttpPost]
        public JsonResult Create(VehicleGroup vehiclegroup, string userid)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    IVG.Add(vehiclegroup, Session["userid"].ToString());
                }
            }
            catch
            {
                return Json("error");
            }

            return Json("success");
        }

        [HttpPost]
        public JsonResult Edit(VehicleGroup vehiclegroup, string userid)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    IVG.Update(vehiclegroup, Session["userid"].ToString());
                }
            }
            catch
            {
                return Json("error");
            }

            return Json("success");
        }

        //
        // GET: /VehicleGroup/Delete/5

        public ActionResult Delete(int id)
        {
            return View(IVG.Find(id));
        }

        //
        // POST: /VehicleGroup/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                IVG.Remove(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}