using Plantation.Models.DB;
using Plantation.Repository;
using System.Web.Mvc;
using DataTables;
using Plantation.Utility;
using System;

namespace Plantation.Controllers
{
    public class RegionController : Controller
    {
        private RegionRepository RG = new RegionRepository();

        public JsonResult Data()
        {
            var request = System.Web.HttpContext.Current.Request;

            using (var db = new Database("sqlserver", Constant.DatabaseConnection))
            {
                var response = new Editor(db, "REGION", "SID")
                    .Model<Region>()
                    .Field(new Field("IDREGION")
                        .Validator(Validation.NotEmpty())
                    )
                    .Field(new Field("REGIONNAME")
                        .Validator(Validation.NotEmpty())
                    )
                    .Field(new Field("UPDATEBY").SetValue(Session["userid"].ToString()))
                    .Field(new Field("UPDATEDATE").SetValue(DateTime.Now))
                    .Process(request)
                    .Data();

                return Json(response, JsonRequestBehavior.AllowGet);
            }
        }

        // GET: Region
        public ActionResult Index()
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            return View(RG.GetAll());
        }

        //
        // GET: /Region/Details/5
        public ActionResult Details(int? id)
        {

            return View(RG.Find(id));
        }

        [HttpPost]
        public JsonResult Create(Region region, string userid)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    RG.Add(region, Session["userid"].ToString());
                }
            }
            catch 
            {
                return Json("error");
            }

            return Json("success");
        }
        
        [HttpPost]
        public JsonResult Edit(Region region, string userid)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    RG.Update(region, Session["userid"].ToString());
                }
            }
            catch
            {
                return Json("error");
            }

            return Json("success");
        }

        //
        // GET: /Region/Delete/5

        public ActionResult Delete(int id)
        {
            return View(RG.Find(id));
        }

        //
        // POST: /Region/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                RG.Remove(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}