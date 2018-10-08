using DataTables;
using Plantation.Models;
using Plantation.Models.DB;
using Plantation.Repository;
using Plantation.Utility;
using System;
using System.Web.Mvc;
namespace Plantation.Controllers
{
    public class UnitOfMeasureController : Controller
    {
        private UnitOfMeasureRepository UOM = new UnitOfMeasureRepository();

        public JsonResult GetRole()
        {
            return Json(UOM.GetRole(int.Parse(Session["IDMENU"].ToString()), Session["userid"].ToString()), JsonRequestBehavior.AllowGet);
        }

        // GET: Data For Editor Datatables
        public JsonResult Data()
        {
            var request = System.Web.HttpContext.Current.Request;

            using (var db = new Database("sqlserver", Constant.DatabaseConnection))
            {
                var response = new Editor(db, "UNITOFMEASURE", "SID")
                    .Model<UnitOfMeasure>()
                    .Field(new Field("IDUOM")
                        .Validator(Validation.NotEmpty())
                    )
                    .Field(new Field("UOMNAME")
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

        // GET: UnitOfMeasure
        public ActionResult Index(int IDMENU)
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            Session["IDMENU"] = IDMENU;
            return View(UOM.GetAll());
        }

        //
        // GET: /UnitOfMeasure/Details/5
        public ActionResult Details(int? id)
        {

            return View(UOM.Find(id));
        }
        
        [HttpPost]
        public JsonResult Create(UnitOfMeasure unitofmeasure, string userid)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UOM.Add(unitofmeasure, Session["userid"].ToString());
                }
            }
            catch
            {
                return Json("error");
            }

            return Json("success");
        }

        [HttpPost]
        public JsonResult Edit(UnitOfMeasure unitofmeasure, string userid)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UOM.Update(unitofmeasure, Session["userid"].ToString());
                }
            }
            catch
            {
                return Json("error");
            }

            return Json("success");
        }

        //
        // GET: /UnitOfMeasure/Delete/5

        public ActionResult Delete(int id)
        {
            return View(UOM.Find(id));
        }

        //
        // POST: /Position/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                UOM.Remove(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}