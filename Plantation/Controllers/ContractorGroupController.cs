using Plantation.Models.DB;
using Plantation.Repository;
using System.Web.Mvc;
using DataTables;
using Plantation.Utility;
using System;

namespace Plantation.Controllers
{
    public class ContractorGroupController : Controller
    {
        private ContractorGroupRepository CTG = new ContractorGroupRepository();

        public JsonResult Data()
        {
            var request = System.Web.HttpContext.Current.Request;

            using (var db = new Database("sqlserver", Constant.DatabaseConnection))
            {
                var response = new Editor(db, "CONTRACTORGROUP", "SID")
                    .Model<ContractorGroup>()
                    .Field(new Field("IDCONTRACTORGROUP")
                        .Validator(Validation.NotEmpty())
                    )
                    .Field(new Field("CONTRACTORGROUPNAME")
                        .Validator(Validation.NotEmpty())
                    )
                    .Field(new Field("UPDATEBY").SetValue(Session["userid"].ToString()))
                    .Field(new Field("UPDATEDATE").SetValue(DateTime.Now))
                    .Process(request)
                    .Data();

                return Json(response, JsonRequestBehavior.AllowGet);
            }
        }

        // GET: ContractorGroup
        public ActionResult Index()
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            return View(CTG.GetAll());
        }

        //
        // GET: /ContractorGroup/Details/5
        public ActionResult Details(int? id)
        {

            return View(CTG.Find(id));
        }

        [HttpPost]
        public JsonResult Create(ContractorGroup contractorgroup, string userid)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    CTG.Add(contractorgroup, Session["userid"].ToString());
                }
            }
            catch
            {
                return Json("error");
            }

            return Json("success");
        }

        [HttpPost]
        public JsonResult Edit(ContractorGroup contractorgroup, string userid)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    CTG.Update(contractorgroup, Session["userid"].ToString());
                }
            }
            catch
            {
                return Json("error");
            }

            return Json("success");
        }

        //
        // GET: /ContractorGroup/Delete/5

        public ActionResult Delete(int id)
        {
            return View(CTG.Find(id));
        }

        //
        // POST: /ContractorGroup/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                CTG.Remove(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}