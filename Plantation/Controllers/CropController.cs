using Plantation.Models.DB;
using Plantation.Repository;
using System.Web.Mvc;
using DataTables;
using Plantation.Utility;
using System;

namespace Plantation.Controllers
{
    public class CropController : Controller
    {
        private CropRepository ICR = new CropRepository();

        #region CROP

        public JsonResult Data()
        {
            var request = System.Web.HttpContext.Current.Request;

            using (var db = new Database("sqlserver", Constant.DatabaseConnection))
            {
                var response = new Editor(db, "CROP", "SID")
                    .Model<Crop>()
                    .Field(new Field("IDCROP")
                        .Validator(Validation.NotEmpty())
                    )
                    .Field(new Field("CROPNAME")
                        .Validator(Validation.NotEmpty())
                    )
                    .Field(new Field("UPDATEBY").SetValue(Session["userid"].ToString()))
                    .Field(new Field("UPDATEDATE").SetValue(DateTime.Now))
                    .Process(request)
                    .Data();

                return Json(response, JsonRequestBehavior.AllowGet);
            }
        }

        // GET: Crop
        public ActionResult Index()
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            return View(ICR.GetAll());
        }

        //
        // GET: /Crop/Details/5
        public ActionResult Details(int? id)
        {

            return View(ICR.Find(id));
        }

        [HttpPost]
        public JsonResult Create(Crop crop, string userid)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ICR.Add(crop, Session["userid"].ToString());
                }
            }
            catch
            {
                return Json("error");
            }

            return Json("success");
        }

        [HttpPost]
        public JsonResult Edit(Crop crop, string userid)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ICR.Update(crop, Session["userid"].ToString());
                }
            }
            catch
            {
                return Json("error");
            }

            return Json("success");
        }

        //
        // GET: /Crop/Delete/5

        public ActionResult Delete(int id)
        {
            return View(ICR.Find(id));
        }

        //
        // POST: /Crop/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                ICR.Remove(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        #endregion CROP
    }
}