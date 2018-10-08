using DataTables;
using Plantation.Models;
using Plantation.Models.DB;
using Plantation.Repository;
using Plantation.Utility;
using System;
using System.Web.Mvc;
namespace Plantation.Controllers
{
    public class PositionController : Controller
    {
        private PositionRepository IPS = new PositionRepository();

        // GET: Data For Editor Datatables
        public JsonResult Data()
        {
            var request = System.Web.HttpContext.Current.Request;

            using (var db = new Database("sqlserver", Constant.DatabaseConnection))
            {
                var response = new Editor(db, "POSITION", "SID")
                    .Model<Position>()
                    .Field(new Field("IDPOSITION")
                        .Validator(Validation.NotEmpty())
                    )
                    .Field(new Field("POSITIONNAME")
                        .Validator(Validation.NotEmpty())
                    )
                    .Field(new Field("DESCRIPTION")
                        .Validator(Validation.NotEmpty())
                    )
                    .Field(new Field("STATUS"))
                    .Field(new Field("UPDATEBY").SetValue(Session["userid"].ToString()))
                    .Field(new Field("UPDATEDATE").SetValue(DateTime.Now))
                    .Process(request)
                    .Data();

                return Json(response, JsonRequestBehavior.AllowGet);
            }
        }

        // GET: Position
        public ActionResult Index()
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            return View(IPS.GetAll());
        }

        //
        // GET: /Position/Details/5
        public ActionResult Details(int? id)
        {

            return View(IPS.Find(id));
        }
        
        [HttpPost]
        public JsonResult Create(Position position, string userid)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    IPS.Add(position, Session["userid"].ToString());
                }
            }
            catch
            {
                return Json("error");
            }

            return Json("success");
        }

        [HttpPost]
        public JsonResult Edit(Position position, string userid)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    IPS.Update(position, Session["userid"].ToString());
                }
            }
            catch
            {
                return Json("error");
            }

            return Json("success");
        }

        //
        // GET: /Position/Delete/5

        public ActionResult Delete(int id)
        {
            return View(IPS.Find(id));
        }

        //
        // POST: /Position/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                IPS.Remove(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}