using DataTables;
using Plantation.Models;
using Plantation.Models.DB;
using Plantation.Repository;
using Plantation.Utility;
using System;
using System.Web.Mvc;

namespace Plantation.Controllers
{
    public class WorkshopController : Controller
    {
        private WorkshopRepository WS = new WorkshopRepository();
        ComboBoxContext context = new ComboBoxContext();

        // GET: Data For Editor Datatables
        public JsonResult Data()
        {
            var request = System.Web.HttpContext.Current.Request;

            using (var db = new Database("sqlserver", Constant.DatabaseConnection))
            {
                var response = new Editor(db, "WORKSHOP", "WORKSHOP.SID")
                    .Model<JoinModelWorkshop>("WORKSHOP")
                    .Model<JoinModelWorkshopGroup>("WORKSHOPGROUP")
                    .Field(new Field("WORKSHOP.IDWORKSHOP")
                        .Validator(Validation.NotEmpty())
                    )
                    .Field(new Field("WORKSHOP.WORKSHOPNAME")
                        .Validator(Validation.NotEmpty())
                    )
                    .Field(new Field("WORKSHOP.ISACTIVE"))
                    .Field(new Field("WORKSHOP.UPDATEBY").SetValue(Session["userid"].ToString()))
                    .Field(new Field("WORKSHOP.UPDATEDATE").SetValue(DateTime.Now))
                    .Field(new Field("WORKSHOP.WORKSHOPGROUP")
                        .Options(new Options()
                            .Table("WORKSHOPGROUP")
                            .Value("SID")
                            .Label("WORKSHOPGROUPNAME")
                        )
                        .Validator(Validation.DbValues(new ValidationOpts { Empty = false }))
                    )
                    .LeftJoin("WORKSHOPGROUP", "WORKSHOPGROUP.SID", "=", "WORKSHOP.WORKSHOPGROUP")
                    .Where("WORKSHOP.COMPANYSITE", int.Parse(Session["companysite"].ToString()))
                    .Process(request)
                    .Data();

                return Json(response, JsonRequestBehavior.AllowGet);
            }
        }

        //
        // GET: /Workshop/
        public ActionResult Index()
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            return View(WS.GetAll());
        }

        public JsonResult GetWorkshopGroupList()
        {
            var users = context.GetWorkshopGroup();
            return Json(users, JsonRequestBehavior.AllowGet);
        }

        //
        // GET: /Workshop/Details/5
        public ActionResult Details(int? id)
        {

            return View(WS.Find(id));
        }
        
        [HttpPost]
        public JsonResult Create(Workshop workshop, string userid)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    workshop.COMPANYSITE = int.Parse(Session["companysite"].ToString());
                    WS.Add(workshop, Session["userid"].ToString());
                }
            }
            catch
            {
                return Json("error");
            }

            return Json("success");
        }
        
        [HttpPost]
        public JsonResult Edit(Workshop workshop, string userid)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    workshop.COMPANYSITE = int.Parse(Session["companysite"].ToString());
                    WS.Update(workshop, Session["userid"].ToString());
                }
            }
            catch
            {
                return Json("error");
            }

            return Json("success");
        }

        //
        // GET: /Workshop/Delete/5

        public ActionResult Delete(int id)
        {
            return View(WS.Find(id));
        }

        //
        // POST: /Workshop/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                WS.Remove(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}