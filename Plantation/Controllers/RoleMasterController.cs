using Plantation.Models.DB;
using Plantation.Repository;
using System.Web.Mvc;
using DataTables;
using Plantation.Utility;
using System;
using Plantation.Models;

namespace Plantation.Controllers
{
    public class RoleMasterController : Controller
    {
        private RoleMasterRepository IRM = new RoleMasterRepository();
        ComboBoxContext context = new ComboBoxContext();

        public JsonResult Data()
        {
            var request = System.Web.HttpContext.Current.Request;

            using (var db = new Database("sqlserver", Constant.DatabaseConnection))
            {
                var response = new Editor(db, "ROLEMASTER", "SID")
                    .Model<RoleMaster>()
                    .Field(new Field("IDROLE"))
                    .Field(new Field("ROLENAME"))
                    .Field(new Field("ISACTIVE"))
                    .Process(request)
                    .Data();

                return Json(response, JsonRequestBehavior.AllowGet);
            }
        }

        // GET: RoleMaster
        public ActionResult Index()
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            return View(IRM.GetAll());
        }

        //
        // GET: /RoleMaster/Details/5
        public ActionResult Details(int? id)
        {
            return View(IRM.Find(id));
        }

        //
        // GET: /RoleMaster/Create
        public ActionResult Create(RoleMaster rolemaster, string userid)
        {
            if (ModelState.IsValid)
            {
                IRM.Add(rolemaster, Session["userid"].ToString());
            }

            return View(rolemaster);
        }

        // POST: /RoleMaster/Edit/5
        [HttpPost]
        public ActionResult Edit(RoleMaster rolemaster, string userid)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    IRM.Update(rolemaster, Session["userid"].ToString());
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }

            //if (ModelState.IsValid)
            //{
            //    IRM.Update(rolemaster, Session["userid"].ToString());
            //}
            //return View(rolemaster);
        }

        //
        // GET: /RoleMaster/Delete/5
        public ActionResult Delete(int id)
        {
            return View(IRM.Find(id));
        }

        //
        // POST: /RoleMaster/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                IRM.Remove(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}