using Plantation.Models;
using Plantation.Models.DB;
using Plantation.Repository;
using System;
using System.Linq;
using System.Web.Mvc;

namespace Plantation.Controllers
{
    public class EmployeeController : Controller
    {
        private EmployeeRepository EM = new EmployeeRepository();
        ComboBoxContext context = new ComboBoxContext();
        // GET: Employee
        public ActionResult Index()
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            return View(EM.GetAllByCompanySite(int.Parse(Session["companysite"].ToString())));
        }

        //
        // GET: /Employee/Details/5
        public ActionResult Details(int? id)
        {
            return View(EM.Find(id));
        }

        //
        // GET: /Employee/Create
        public ActionResult Create()
        {
            var model = new Employee();
            model.TERMINATE = false;
            model.GetSelectListSex = GetSelectListSex();
            model.GetSelectListRace = GetSelectListRace();
            model.GetSelectListEducationLevel = GetSelectListEducationLevel();
            model.GetSelectListInstitution = GetSelectListInstitution();
            model.GetSelectListReligion = GetSelectListReligion();
            model.GetSelectListNationality = GetSelectListNationality();
            model.GetSelectListProgramOfStudy = GetSelectListProgramOfStudy();
            model.GetSelectListMaritalStatus = GetSelectListMaritalStatus();
            model.GetSelectListFamilyRiceStatus = GetSelectListFamilyRiceStatus();
            model.GetSelectListFamilyTaxStatus = GetSelectListFamilyTaxStatus();
            model.GetSelectListNatureType = GetSelectListNatureType();
            model.GetSelectListDepartment = GetSelectListDepartment();
            model.GetSelectListGrade = GetSelectListGrade();
            model.GetSelectListEmployeeType = GetSelectListEmployeeType();
            model.GetSelectListEmployeeSection = GetSelectListEmployeeSection();
            model.GetSelectListEmployeePosition = GetSelectListEmployeePosition();
            model.GetSelectListPaymentMethod = GetSelectListPaymentMethod();
            model.GetSelectListBank = GetSelectListBank();
            return View(model);
        }

        //
        // POST: /Employee/Create
        [HttpPost]
        public ActionResult Create(Employee Employee, string userid)
        {
            if (ModelState.IsValid)
            {
                if (Employee.TERMINATE)
                    Employee.DATETERMINATE = DateTime.Now;

                Employee.COMPANYSITE = int.Parse(Session["companysite"].ToString());
                var EMP = EM.Add(Employee, Session["userid"].ToString());
                EMP.SIDEMPLOYEE = EMP.SID;
                EM.AddHistory(EMP, Session["userid"].ToString());
                TempData["successmessage"] = "Saved successfully";
                return RedirectToAction("Create");
            }
            else
            {
                Employee.GetSelectListSex = GetSelectListSex();
                Employee.GetSelectListRace = GetSelectListRace();
                Employee.GetSelectListEducationLevel = GetSelectListEducationLevel();
                Employee.GetSelectListInstitution = GetSelectListInstitution();
                Employee.GetSelectListReligion = GetSelectListReligion();
                Employee.GetSelectListNationality = GetSelectListNationality();
                Employee.GetSelectListProgramOfStudy = GetSelectListProgramOfStudy();
                Employee.GetSelectListMaritalStatus = GetSelectListMaritalStatus();
                Employee.GetSelectListFamilyRiceStatus = GetSelectListFamilyRiceStatus();
                Employee.GetSelectListFamilyTaxStatus = GetSelectListFamilyTaxStatus();
                Employee.GetSelectListNatureType = GetSelectListNatureType();
                Employee.GetSelectListDepartment = GetSelectListDepartment();
                Employee.GetSelectListGrade = GetSelectListGrade();
                Employee.GetSelectListEmployeeType = GetSelectListEmployeeType();
                Employee.GetSelectListEmployeeSection = GetSelectListEmployeeSection();
                Employee.GetSelectListEmployeePosition = GetSelectListEmployeePosition();
                Employee.GetSelectListPaymentMethod = GetSelectListPaymentMethod();
                Employee.GetSelectListBank = GetSelectListBank();
                return View(Employee);
            }
        }

        //
        // GET: /Employee/Edit/5
        public ActionResult Edit(int id)
        {
            var model = EM.Find(id);
            model.GetSelectListSex = GetSelectListSex(model.SEX);
            model.GetSelectListRace = GetSelectListRace(model.RACE);
            model.GetSelectListEducationLevel = GetSelectListEducationLevel(model.EDUCATIONLEVEL);
            model.GetSelectListInstitution = GetSelectListInstitution(model.INSTITUTION);
            model.GetSelectListReligion = GetSelectListReligion(model.RELIGION);
            model.GetSelectListNationality = GetSelectListNationality(model.NATIONALITY);
            model.GetSelectListProgramOfStudy = GetSelectListProgramOfStudy(model.PROGRAMOFSTUDY);
            model.GetSelectListMaritalStatus = GetSelectListMaritalStatus(model.MARITALSTATUS);
            model.GetSelectListFamilyRiceStatus = GetSelectListFamilyRiceStatus(model.FAMILYRICESTATUS);
            model.GetSelectListFamilyTaxStatus = GetSelectListFamilyTaxStatus(model.FAMILYTAXSTATUS);
            model.GetSelectListNatureType = GetSelectListNatureType(model.NATURETYPE);
            model.GetSelectListDepartment = GetSelectListDepartment(model.DEPARTMENT);
            model.GetSelectListGrade = GetSelectListGrade(model.GRADE);
            model.GetSelectListEmployeeType = GetSelectListEmployeeType(model.EMPLOYEETYPE);
            model.GetSelectListEmployeeSection = GetSelectListEmployeeSection(model.EMPLOYEESECTION);
            model.GetSelectListEmployeePosition = GetSelectListEmployeePosition(model.EMPLOYEEPOSITION);
            model.GetSelectListPaymentMethod = GetSelectListPaymentMethod(model.PAYMENTMETHOD);
            model.GetSelectListBank = GetSelectListBank(model.BANK);
            return View(model);
        }

        //
        // POST: /Employee/Edit/5
        [HttpPost]
        public ActionResult Edit(Employee Employee)
        {
            if (ModelState.IsValid)
            {
                Employee.SIDEMPLOYEE = Employee.SID;
                Employee.COMPANYSITE = int.Parse(Session["companysite"].ToString());
                Employee.DATETERMINATE = DateTime.Now;
                Employee.UPDATEDATE = DateTime.Now;
                EM.Update(Employee, Session["userid"].ToString());
                EM.AddHistory(Employee, Session["userid"].ToString());
                TempData["successmessage"] = "Saved successfully";
                return RedirectToAction("Index");
            }
            else
            {
                Employee.GetSelectListSex = GetSelectListSex();
                Employee.GetSelectListRace = GetSelectListRace();
                Employee.GetSelectListEducationLevel = GetSelectListEducationLevel();
                Employee.GetSelectListInstitution = GetSelectListInstitution();
                Employee.GetSelectListReligion = GetSelectListReligion();
                Employee.GetSelectListNationality = GetSelectListNationality();
                Employee.GetSelectListProgramOfStudy = GetSelectListProgramOfStudy();
                Employee.GetSelectListMaritalStatus = GetSelectListMaritalStatus();
                Employee.GetSelectListFamilyRiceStatus = GetSelectListFamilyRiceStatus();
                Employee.GetSelectListFamilyTaxStatus = GetSelectListFamilyTaxStatus();
                Employee.GetSelectListNatureType = GetSelectListNatureType();
                Employee.GetSelectListDepartment = GetSelectListDepartment();
                Employee.GetSelectListGrade = GetSelectListGrade();
                Employee.GetSelectListEmployeeType = GetSelectListEmployeeType();
                Employee.GetSelectListEmployeeSection = GetSelectListEmployeeSection();
                Employee.GetSelectListEmployeePosition = GetSelectListEmployeePosition();
                Employee.GetSelectListPaymentMethod = GetSelectListPaymentMethod();
                Employee.GetSelectListBank = GetSelectListBank();
                return View(Employee);
            }
        }

        //
        // GET: /Employee/Delete/5
        public ActionResult Delete(int id)
        {
            return View(EM.Find(id));
        }

        //
        // POST: /Employee/Delete/5
        [HttpPost]
        public JsonResult Delete(int id, FormCollection collection)
        {
            try
            {
                EM.Remove(id);
                return Json("success", JsonRequestBehavior.AllowGet);//RedirectToAction("Index");
            }
            catch
            {
                return Json("error", JsonRequestBehavior.AllowGet);//View();
            }
        }

        public JsonResult GetSexList()
        {
            var users = context.GetSex();
            return Json(users, JsonRequestBehavior.AllowGet);
        }

        private SelectList GetSelectListSex(object selectedValue = null)
        {
            var model = context.GetSex();
            var list = new SelectList(model.Select(x => new { x.SID, x.PARAMETERVALUENAME }), "SID", "PARAMETERVALUENAME", selectedValue).ToList();
            list.Insert(0, new SelectListItem { Value = "", Text = "--Select Gender--" });
            return new SelectList(list, "Value", "Text");
        }

        public JsonResult GetRaceList()
        {
            var users = context.GetRace();
            return Json(users, JsonRequestBehavior.AllowGet);
        }

        private SelectList GetSelectListRace(object selectedValue = null)
        {
            var model = context.GetRace();
            var list = new SelectList(model.Select(x => new { x.SID, x.PARAMETERVALUENAME }), "SID", "PARAMETERVALUENAME", selectedValue).ToList();
            list.Insert(0, new SelectListItem { Value = "", Text = "--Select Race--" });
            return new SelectList(list, "Value", "Text");
        }

        public JsonResult GetParameterValueBKList()
        {
            var users = context.GetParameterValueBK();
            return Json(users, JsonRequestBehavior.AllowGet);
        }

        private SelectList GetSelectListBank(object selectedValue = null)
        {
            var model = context.GetParameterValueBK();
            var list = new SelectList(model.Select(x => new { x.SID, x.PARAMETERVALUENAME }), "SID", "PARAMETERVALUENAME", selectedValue).ToList();
            list.Insert(0, new SelectListItem { Value = "", Text = "--Select Bank--" });
            return new SelectList(list, "Value", "Text");
        }

        public JsonResult GetCountryList()
        {
            var users = context.GetCountry();
            return Json(users, JsonRequestBehavior.AllowGet);
        }

        private SelectList GetSelectListNationality(object selectedValue = null)
        {
            var model = context.GetCountry();
            var list = new SelectList(model.Select(x => new { x.SID, x.COUNTRYNAME }), "SID", "COUNTRYNAME", selectedValue).ToList();
            list.Insert(0, new SelectListItem { Value = "", Text = "--Select Nationality--" });
            return new SelectList(list, "Value", "Text");
        }

        public JsonResult GetEducationLevelList()
        {
            var users = context.GetEducationLevel();
            return Json(users, JsonRequestBehavior.AllowGet);
        }

        private SelectList GetSelectListEducationLevel(object selectedValue = null)
        {
            var model = context.GetEducationLevel();
            var list = new SelectList(model.Select(x => new { x.SID, x.PARAMETERVALUENAME }), "SID", "PARAMETERVALUENAME", selectedValue).ToList();
            list.Insert(0, new SelectListItem { Value = "", Text = "--Select Education Level--" });
            return new SelectList(list, "Value", "Text");
        }

        public JsonResult GetInstitutionList()
        {
            var users = context.GetInstitution();
            return Json(users, JsonRequestBehavior.AllowGet);
        }

        private SelectList GetSelectListInstitution(object selectedValue = null)
        {
            var model = context.GetInstitution();
            var list = new SelectList(model.Select(x => new { x.SID, x.PARAMETERVALUENAME }), "SID", "PARAMETERVALUENAME", selectedValue).ToList();
            list.Insert(0, new SelectListItem { Value = "", Text = "--Select Institution--" });
            return new SelectList(list, "Value", "Text");
        }

        public JsonResult GetReligionList()
        {
            var users = context.GetReligion();
            return Json(users, JsonRequestBehavior.AllowGet);
        }

        private SelectList GetSelectListReligion(object selectedValue = null)
        {
            var model = context.GetReligion();
            var list = new SelectList(model.Select(x => new { x.SID, x.PARAMETERVALUENAME }), "SID", "PARAMETERVALUENAME", selectedValue).ToList();
            list.Insert(0, new SelectListItem { Value = "", Text = "--Select Religion--" });
            return new SelectList(list, "Value", "Text");
        }

        public JsonResult GetProgramOfStudyList()
        {
            var users = context.GetProgramOfStudy();
            return Json(users, JsonRequestBehavior.AllowGet);
        }

        private SelectList GetSelectListProgramOfStudy(object selectedValue = null)
        {
            var model = context.GetProgramOfStudy();
            var list = new SelectList(model.Select(x => new { x.SID, x.PARAMETERVALUENAME }), "SID", "PARAMETERVALUENAME", selectedValue).ToList();
            list.Insert(0, new SelectListItem { Value = "", Text = "--Select Program Of Study--" });
            return new SelectList(list, "Value", "Text");
        }

        public JsonResult GetMaritalStatusList()
        {
            var users = context.GetMaritalStatus();
            return Json(users, JsonRequestBehavior.AllowGet);
        }

        private SelectList GetSelectListMaritalStatus(object selectedValue = null)
        {
            var model = context.GetMaritalStatus();
            var list = new SelectList(model.Select(x => new { x.SID, x.PARAMETERVALUENAME }), "SID", "PARAMETERVALUENAME", selectedValue).ToList();
            list.Insert(0, new SelectListItem { Value = "", Text = "--Select Marital Status--" });
            return new SelectList(list, "Value", "Text");
        }

        public JsonResult GetFamilyRiceStatusList()
        {
            var users = context.GetFamilyRiceStatus();
            return Json(users, JsonRequestBehavior.AllowGet);
        }

        private SelectList GetSelectListFamilyRiceStatus(object selectedValue = null)
        {
            var model = context.GetFamilyRiceStatus();
            var list = new SelectList(model.Select(x => new { x.SID, x.PARAMETERVALUENAME }), "SID", "PARAMETERVALUENAME", selectedValue).ToList();
            list.Insert(0, new SelectListItem { Value = "", Text = "--Select Family Rice Status--" });
            return new SelectList(list, "Value", "Text");
        }

        public JsonResult GetFamilyTaxStatusList()
        {
            var users = context.GetFamilyTaxStatus();
            return Json(users, JsonRequestBehavior.AllowGet);
        }

        private SelectList GetSelectListFamilyTaxStatus(object selectedValue = null)
        {
            var model = context.GetFamilyTaxStatus();
            var list = new SelectList(model.Select(x => new { x.SID, x.PARAMETERVALUENAME }), "SID", "PARAMETERVALUENAME", selectedValue).ToList();
            list.Insert(0, new SelectListItem { Value = "", Text = "--Select Family Tax Status--" });
            return new SelectList(list, "Value", "Text");
        }

        public JsonResult GetNatureTypeList()
        {
            var users = context.GetNatureType();
            return Json(users, JsonRequestBehavior.AllowGet);
        }

        private SelectList GetSelectListNatureType(object selectedValue = null)
        {
            var model = context.GetNatureType();
            var list = new SelectList(model.Select(x => new { x.SID, x.PARAMETERVALUENAME }), "SID", "PARAMETERVALUENAME", selectedValue).ToList();
            list.Insert(0, new SelectListItem { Value = "", Text = "--Select Nature Type--" });
            return new SelectList(list, "Value", "Text");
        }

        public JsonResult GetDepartmentList()
        {
            var users = context.GetDepartment();
            return Json(users, JsonRequestBehavior.AllowGet);
        }

        private SelectList GetSelectListDepartment(object selectedValue = null)
        {
            var model = context.GetDepartment();
            var list = new SelectList(model.Select(x => new { x.SID, x.DEPARTMENTNAME }), "SID", "DEPARTMENTNAME", selectedValue).ToList();
            list.Insert(0, new SelectListItem { Value = "", Text = "--Select Deparment--" });
            return new SelectList(list, "Value", "Text");
        }

        public JsonResult GetGradeList()
        {
            var users = context.GetGrade();
            return Json(users, JsonRequestBehavior.AllowGet);
        }

        private SelectList GetSelectListGrade(object selectedValue = null)
        {
            var model = context.GetGrade();
            var list = new SelectList(model.Select(x => new { x.SID, x.PARAMETERVALUENAME }), "SID", "PARAMETERVALUENAME", selectedValue).ToList();
            list.Insert(0, new SelectListItem { Value = "", Text = "--Select Grade--" });
            return new SelectList(list, "Value", "Text");
        }

        public JsonResult GetEmployeeTypeList()
        {
            var users = context.GetEmployeeType();
            return Json(users, JsonRequestBehavior.AllowGet);
        }

        private SelectList GetSelectListEmployeeType(object selectedValue = null)
        {
            var model = context.GetEmployeeType();
            var list = new SelectList(model.Select(x => new { x.SID, x.PARAMETERVALUENAME }), "SID", "PARAMETERVALUENAME", selectedValue).ToList();
            list.Insert(0, new SelectListItem { Value = "", Text = "--Select Employee Type--" });
            return new SelectList(list, "Value", "Text");
        }

        public JsonResult GetEmployeeSectionList()
        {
            var users = context.GetEmployeeSection();
            return Json(users, JsonRequestBehavior.AllowGet);
        }

        private SelectList GetSelectListEmployeeSection(object selectedValue = null)
        {
            var model = context.GetEmployeeSection();
            var list = new SelectList(model.Select(x => new { x.SID, x.PARAMETERVALUENAME }), "SID", "PARAMETERVALUENAME", selectedValue).ToList();
            list.Insert(0, new SelectListItem { Value = "", Text = "--Select Employee Section--" });
            return new SelectList(list, "Value", "Text");
        }

        public JsonResult GetPaymentMethodList()
        {
            var users = context.GetPaymentMethod();
            return Json(users, JsonRequestBehavior.AllowGet);
        }

        private SelectList GetSelectListPaymentMethod(object selectedValue = null)
        {
            var model = context.GetPaymentMethod();
            var list = new SelectList(model.Select(x => new { x.SID, x.PARAMETERVALUENAME }), "SID", "PARAMETERVALUENAME", selectedValue).ToList();
            list.Insert(0, new SelectListItem { Value = "", Text = "--Select Payment Method--" });
            return new SelectList(list, "Value", "Text");
        }

        public JsonResult GetEmployeePositionList()
        {
            var users = context.GetEmployeePosition();
            return Json(users, JsonRequestBehavior.AllowGet);
        }

        private SelectList GetSelectListEmployeePosition(object selectedValue = null)
        {
            var model = context.GetEmployeePosition();
            var list = new SelectList(model.Select(x => new { x.SID, x.PARAMETERVALUENAME }), "SID", "PARAMETERVALUENAME", selectedValue).ToList();
            list.Insert(0, new SelectListItem { Value = "", Text = "--Select Position--" });
            return new SelectList(list, "Value", "Text");
        }
    }
}