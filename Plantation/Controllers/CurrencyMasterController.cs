using Plantation.Models.DB;
using Plantation.Repository;
using System.Web.Mvc;
using DataTables;
using Plantation.Utility;
using System;

namespace Plantation.Controllers
{
    public class CurrencyMasterController : Controller
    {
        private CurrencyMasterRepository ICM = new CurrencyMasterRepository();

        public JsonResult Data()
        {
            var request = System.Web.HttpContext.Current.Request;

            using (var db = new Database("sqlserver", Constant.DatabaseConnection))
            {
                var response = new Editor(db, "CURRENCYMASTER", "SID")
                    .Model<CurrencyMaster>()
                    .Field(new Field("IDCURRENCY")
                        .Validator(Validation.NotEmpty())
                    )
                    .Field(new Field("CURRENCYNAME")
                        .Validator(Validation.NotEmpty())
                    )
                    .Field(new Field("UPDATEBY").SetValue(Session["userid"].ToString()))
                    .Field(new Field("UPDATEDATE").SetValue(DateTime.Now))
                    .Process(request)
                    .Data();

                return Json(response, JsonRequestBehavior.AllowGet);
            }
        }

        // GET: CurrencyMaster
        public ActionResult Index()
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            return View(ICM.GetAll());
        }

        //
        // GET: /CurrencyMaster/Details/5
        public ActionResult Details(int? id)
        {

            return View(ICM.Find(id));
        }

        [HttpPost]
        public JsonResult Create(CurrencyMaster currencymaster, string userid)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ICM.Add(currencymaster, Session["userid"].ToString());
                }
            }
            catch
            {
                return Json("error");
            }

            return Json("success");
        }

        [HttpPost]
        public JsonResult Edit(CurrencyMaster currencymaster, string userid)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ICM.Update(currencymaster, Session["userid"].ToString());
                }
            }
            catch
            {
                return Json("error");
            }

            return Json("success");
        }

        //
        // GET: /CurrencyMaster/Delete/5

        public ActionResult Delete(int id)
        {
            return View(ICM.Find(id));
        }

        //
        // POST: /CurrencyMaster/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                ICM.Remove(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}