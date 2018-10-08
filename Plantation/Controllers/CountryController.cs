using Plantation.Models.DB;
using Plantation.Repository;
using System.Web.Mvc;
using DataTables;
using Plantation.Utility;
using System;

namespace Plantation.Controllers
{
    public class CountryController : Controller
    {
        private CountryRepository ICN = new CountryRepository();

        public JsonResult Data()
        {
            var request = System.Web.HttpContext.Current.Request;

            using (var db = new Database("sqlserver", Constant.DatabaseConnection))
            {
                var response = new Editor(db, "COUNTRY", "SID")
                    .Model<Country>()
                    .Field(new Field("IDCOUNTRY")
                        .Validator(Validation.NotEmpty())
                    )
                    .Field(new Field("COUNTRYNAME")
                        .Validator(Validation.NotEmpty())
                    )
                    .Field(new Field("UPDATEBY").SetValue(Session["userid"].ToString()))
                    .Field(new Field("UPDATEDATE").SetValue(DateTime.Now))
                    .Process(request)
                    .Data();

                return Json(response, JsonRequestBehavior.AllowGet);
            }
        }

        // GET: Country
        public ActionResult Index()
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            return View(ICN.GetAll());
        }

        //
        // GET: /Country/Details/5
        public ActionResult Details(int? id)
        {

            return View(ICN.Find(id));
        }

        [HttpPost]
        public JsonResult Create(Country country, string userid)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ICN.Add(country, Session["userid"].ToString());
                }
            }
            catch 
            {
                return Json("error");
            }

            return Json("success");
        }
        
        [HttpPost]
        public JsonResult Edit(Country country, string userid)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ICN.Update(country, Session["userid"].ToString());
                }
            }
            catch
            {
                return Json("error");
            }

            return Json("success");
        }

        //
        // GET: /Country/Delete/5

        public ActionResult Delete(int id)
        {
            return View(ICN.Find(id));
        }

        //
        // POST: /Country/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                ICN.Remove(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}