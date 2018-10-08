using DataTables;
using Plantation.Models;
using Plantation.Models.DB;
using Plantation.Repository;
using Plantation.Utility;
using System;
using System.Web.Mvc;

namespace Plantation.Controllers
{
    public class CityController : Controller
    {
        private CityRepository ICR = new CityRepository();
        ComboBoxContext context = new ComboBoxContext();
               

        // GET: Data For Editor Datatables
        public JsonResult Data()
        {
            var request = System.Web.HttpContext.Current.Request;

            using (var db = new Database("sqlserver", Constant.DatabaseConnection))
            {
                var response = new Editor(db, "CITY", "CITY.SID")
                    .Model<JoinModelCity>("CITY")
                    .Model<JoinModelProvince>("PROVINCE")
                    .Model<JoinModelCountry>("COUNTRY")
                    .Field(new Field("CITY.IDCITY")
                        .Validator(Validation.NotEmpty())
                    )
                    .Field(new Field("CITY.CITYNAME")
                        .Validator(Validation.NotEmpty())
                    )
                    .Field(new Field("CITY.UPDATEBY").SetValue(Session["userid"].ToString()))
                    .Field(new Field("CITY.UPDATEDATE").SetValue(DateTime.Now))
                    .Field(new Field("CITY.PROVINCE")
                        .Options(new Options()
                            .Table("PROVINCE")
                            .Value("SID")
                            .Label("PROVINCENAME")
                        )
                        .Validator(Validation.DbValues(new ValidationOpts { Empty = false }))
                    )
                    .Field(new Field("CITY.COUNTRY")
                        .Options(new Options()
                            .Table("COUNTRY")
                            .Value("SID")
                            .Label("COUNTRYNAME")
                        )
                        .Validator(Validation.DbValues(new ValidationOpts { Empty = false }))
                    )
                    .LeftJoin("PROVINCE", "PROVINCE.SID", "=", "CITY.PROVINCE")
                    .LeftJoin("COUNTRY", "COUNTRY.SID", "=", "CITY.COUNTRY")
                    .Process(request)
                    .Data();

                return Json(response, JsonRequestBehavior.AllowGet);
            }
        }

        //
        // GET: /City/
        public ActionResult Index()
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            return View(ICR.GetAll());
        }

        public JsonResult GetCountryList()
        {
            var users = context.GetCountry();
            return Json(users, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetProvinceList()
        {
            var users = context.GetProvince();
            return Json(users, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetProvinceByCountry(int Country)
        {
            var users = context.GetProvinceByCountry(Country);
            return Json(users, JsonRequestBehavior.AllowGet);
        }

        //
        // GET: /City/Details/5
        public ActionResult Details(int? id)
        {
            return View(ICR.Find(id));
        }
        
        [HttpPost]
        public JsonResult Create(City city, string userid)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ICR.Add(city, Session["userid"].ToString());
                }
            }
            catch
            {
                return Json("error");
            }

            return Json("success");
        }
        
        [HttpPost]
        public JsonResult Edit(City city, string userid)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ICR.Update(city, Session["userid"].ToString());
                }
            }
            catch
            {
                return Json("error");
            }

            return Json("success");
        }

        //
        // GET: /City/Delete/5

        public ActionResult Delete(int id)
        {
            return View(ICR.Find(id));
        }

        //
        // POST: /City/Delete/5

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
    }
}