using Plantation.Models.DB;
using Plantation.Repository;
using System.Web.Mvc;
using DataTables;
using Plantation.Utility;
using System;
using Plantation.Models;
using System.Linq;

namespace Plantation.Controllers
{
    public class EmployeeGangDetailController : Controller
    {
        private EmployeeGangDetailRepository EGD = new EmployeeGangDetailRepository();
        private ComboBoxContext context = new ComboBoxContext();

        public JsonResult Data()
        {
            var request = System.Web.HttpContext.Current.Request;

            using (var db = new Database("sqlserver", Constant.DatabaseConnection))
            {
                var response = new Editor(db, "EMPLOYEEGANGDETAIL", "SID")
                    .Model<EmployeeGangDetail>("EMPLOYEEGANGDETAIL")
                    .Model<JoinModelEmployee>("EMPLOYEE")
                    .Field(new Field("EMPLOYEEGANGDETAIL.EMPLOYEEGANG"))
                    .Field(new Field("EMPLOYEEGANGDETAIL.EMPLOYEE").Options(new Options()
                            .Table("EMPLOYEE")
                            .Value("SID")
                            .Label("EMPLOYEENAME")
                        ))
                    .LeftJoin("EMPLOYEE", "EMPLOYEE.SID", "=", "EMPLOYEEGANGDETAIL.EMPLOYEE")
                    .Where("EMPLOYEEGANGDETAIL.EMPLOYEE", int.Parse(Session["SID_EMPLOYEE"].ToString()), "=")
                    .Process(request)
                    .Data();

                return Json(response, JsonRequestBehavior.AllowGet);
            }
        }

        // GET: EmployeeGangDetail
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Create(EmployeeGangDetail EmployeeGangDetail)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    EmployeeGangDetail.INPUTBY = int.Parse(Session["userid"].ToString());
                    EmployeeGangDetail.INPUTDATE = DateTime.Now;
                    EmployeeGangDetail.UPDATEBY = int.Parse(Session["userid"].ToString());
                    EmployeeGangDetail.UPDATEDATE = DateTime.Now;
                    EGD.Add(EmployeeGangDetail);
                }
            }
            catch (Exception ex)
            {
                return Json("error");
            }

            return Json("success");
        }

        [HttpPost]
        public JsonResult Edit(EmployeeGangDetail EmployeeGangDetail)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    EmployeeGangDetail.UPDATEBY = int.Parse(Session["userid"].ToString());
                    EmployeeGangDetail.UPDATEDATE = DateTime.Now;
                    EGD.Update(EmployeeGangDetail);
                }
            }
            catch
            {
                return Json("error");
            }

            return Json("success");
        }

        //
        // POST: /EmployeeGangDetail/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                EGD.Remove(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        private SelectList GetSelectListEmployee(object selectedValue = null)
        {
            var model = context.GetEmployee(int.Parse(Session["companysite"].ToString()));
            var list = new SelectList(model.Select(x => new { x.SID, x.EMPLOYEENAME }), "SID", "EMPLOYEENAME", selectedValue).ToList();
            list.Insert(0, new SelectListItem { Value = "", Text = "--Select Employee--" });
            return new SelectList(list, "Value", "Text");
        }
    }
}