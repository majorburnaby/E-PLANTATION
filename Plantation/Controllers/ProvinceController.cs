using DataTables;
using Plantation.Models;
using Plantation.Models.DB;
using Plantation.Repository;
using Plantation.Utility;
using System;
using System.Web.Mvc;

namespace Plantation.Controllers
{
    public class ProvinceController : Controller
    {
        private ProvinceRepository IPV = new ProvinceRepository();
        ComboBoxContext context = new ComboBoxContext();

        // GET: Data For Editor Datatables
        public JsonResult Data()
        {
            var request = System.Web.HttpContext.Current.Request;

            using (var db = new Database("sqlserver", Constant.DatabaseConnection))
            {
                var response = new Editor(db, "PROVINCE", "PROVINCE.SID")
                    .Model<JoinModelProvince>("PROVINCE")
                    .Model<JoinModelCountry>("COUNTRY")
                    .Field(new Field("PROVINCE.IDPROVINCE")
                        .Validator(Validation.NotEmpty())
                    )
                    .Field(new Field("PROVINCE.PROVINCENAME")
                        .Validator(Validation.NotEmpty())
                    )
                    .Field(new Field("PROVINCE.UPDATEBY").SetValue(Session["userid"].ToString()))
                    .Field(new Field("PROVINCE.UPDATEDATE").SetValue(DateTime.Now))
                    .Field(new Field("PROVINCE.COUNTRY")
                        .Options(new Options()
                            .Table("COUNTRY")
                            .Value("SID")
                            .Label("COUNTRYNAME")
                        )
                        .Validator(Validation.DbValues(new ValidationOpts { Empty = false }))
                    )
                    .LeftJoin("COUNTRY", "COUNTRY.SID", "=", "PROVINCE.COUNTRY")
                    .Process(request)
                    .Data();
                
                return Json(response, JsonRequestBehavior.AllowGet);
            }
        }

        // GET: Province
        public ActionResult Index()
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            return View(IPV.GetAll());
        }

        public JsonResult GetCountryList()
        {
            var users = context.GetCountry();
            return Json(users, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Details(int? id)
        {

            return View(IPV.Find(id));
        }

        //
        // GET: /Province/Create
        //public ActionResult Create(Province province, string userid)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        IPV.Add(province, Session["userid"].ToString());
        //    }
        //    return View(province);
        //}
        [HttpPost]
        public JsonResult Create(Province province, string userid)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    IPV.Add(province, Session["userid"].ToString());
                }
            }
            catch
            {
                return Json("error");
            }

            return Json("success");
        }

        //
        // GET: /Province/Edit/5
        // POST: /Country/Edit/5
        //[HttpPost]
        //public ActionResult Edit(Province province, string userid)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        IPV.Update(province, Session["userid"].ToString());
        //    }
        //    return View(province);
        //}
        [HttpPost]
        public JsonResult Edit(Province province, string userid)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    IPV.Update(province, Session["userid"].ToString());
                }
            }
            catch
            {
                return Json("error");
            }

            return Json("success");
        }

        //
        // POST: /Province/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                IPV.Remove(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}