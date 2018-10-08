using DataTables;
using Plantation.Models;
using Plantation.Models.DB;
using Plantation.Repository;
using Plantation.Utility;
using System;
using System.Web.Mvc;

namespace Plantation.Controllers
{
    public class AllowanceDeductionController : Controller
    {
        private AllowanceDeductionRepository AD = new AllowanceDeductionRepository();
        ComboBoxContext context = new ComboBoxContext();

        // GET: Data For Editor Datatables
        public JsonResult Data()
        {
            var request = System.Web.HttpContext.Current.Request;

            using (var db = new Database("sqlserver", Constant.DatabaseConnection))
            {
                var response = new Editor(db, "ALLOWANCEDEDUCTION", "ALLOWANCEDEDUCTION.SID")
                    .Model<JoinModelAllowanceDeduction>("ALLOWANCEDEDUCTION")
                    .Model<JoinModelParameterValue>("PARAMETERVALUE")
                    .Field(new Field("ALLOWANCEDEDUCTION.IDALLOWANCEDEDUCTION")
                        .Validator(Validation.NotEmpty())
                    )
                    .Field(new Field("ALLOWANCEDEDUCTION.ALLOWANCEDEDUCTIONNAME")
                        .Validator(Validation.NotEmpty())
                    )
                    .Field(new Field("ALLOWANCEDEDUCTION.UPDATEBY").SetValue(Session["userid"].ToString()))
                    .Field(new Field("ALLOWANCEDEDUCTION.UPDATEDATE").SetValue(DateTime.Now))
                    .Field(new Field("ALLOWANCEDEDUCTION.ALLOWANCEDEDUCTIONTYPE")
                        .Options(new Options()
                            .Table("PARAMETERVALUE")
                            .Value("SID")
                            .Label("PARAMETERVALUENAME")
                        )
                        .Validator(Validation.DbValues(new ValidationOpts { Empty = false }))
                    )
                    .LeftJoin("PARAMETERVALUE", "PARAMETERVALUE.SID", "=", "ALLOWANCEDEDUCTION.ALLOWANCEDEDUCTIONTYPE")
                    .Process(request)
                    .Data();
                
                return Json(response, JsonRequestBehavior.AllowGet);
            }
        }

        // GET: AllowanceDeduction
        public ActionResult Index()
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            return View(AD.GetAll());
        }

        public JsonResult GetAllowanceDeductionList()
        {
            var users = context.GetAllowanceDeduction();
            return Json(users, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Details(int? id)
        {

            return View(AD.Find(id));
        }
        
        [HttpPost]
        public JsonResult Create(AllowanceDeduction AllowanceDeduction, string userid)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    AD.Add(AllowanceDeduction, Session["userid"].ToString());
                }
            }
            catch
            {
                return Json("error");
            }

            return Json("success");
        }
        
        [HttpPost]
        public JsonResult Edit(AllowanceDeduction AllowanceDeduction, string userid)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    AD.Update(AllowanceDeduction, Session["userid"].ToString());
                }
            }
            catch
            {
                return Json("error");
            }

            return Json("success");
        }

        //
        // POST: /AllowanceDeduction/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                AD.Remove(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}