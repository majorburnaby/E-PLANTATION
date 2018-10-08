using Plantation.Models;
using Plantation.Models.DB;
using Plantation.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Plantation.Controllers
{
    public class EmployeeGangController : Controller
    {
        private EmployeeGangRepository EGH = new EmployeeGangRepository();
        private ComboBoxContext context = new ComboBoxContext();

        // GET: EmployeeGang
        public ActionResult Index()
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            
            return View(EGH.GetAllByCompanySite(int.Parse(Session["companysite"].ToString())));
        }

        //
        // GET: /EmployeeGang/Create
        public ActionResult Create()
        {
            var model = new EmployeeGang();
            model.GetSelectListForeman1 = GetSelectListForeman1();
            model.GetSelectListForeman = GetSelectListForeman();
            model.GetSelectListAdmin = GetSelectListAdmin();
            model.GetSelectListGangType = GetSelectListGangType();
            model.GetSelectListBlockOrganization = GetSelectListBlockOrganization();
            return View(model);
        }

        //
        // POST: /EmployeeGang/Create
        [HttpPost]
        public ActionResult Create(EmployeeGang EmployeeGang)
        {
            if (ModelState.IsValid)
            {
                EmployeeGang.COMPANYSITE = int.Parse(Session["companysite"].ToString());
                EGH.Add(EmployeeGang, Session["userid"].ToString());
                TempData["successmessage"] = "Saved successfully";
                return RedirectToAction("Create");
            }
            else
            {
                EmployeeGang.GetSelectListForeman1 = GetSelectListForeman1();
                EmployeeGang.GetSelectListForeman = GetSelectListForeman();
                EmployeeGang.GetSelectListAdmin = GetSelectListAdmin();
                EmployeeGang.GetSelectListGangType = GetSelectListGangType();
                EmployeeGang.GetSelectListBlockOrganization = GetSelectListBlockOrganization();
                return View(EmployeeGang);
            }
        }
        //
        // POST: /EmployeeGang/CreateAndAddBlockUsage
        //[HttpPost]
        //public ActionResult CreateAndAddBlockUsage(EmployeeGang EmployeeGang)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        EmployeeGang.COMPANYSITE = int.Parse(Session["companysite"].ToString());
        //        EmployeeGang blo = EGH.Add(EmployeeGang, Session["userid"].ToString());
        //        var model = EGH.Find(blo.SID);
        //        Session["SID_EmployeeGang"] = blo.SID;
        //        model.GetSelectListLandConcession = GetSelectListLandConcession(model.LANDCONCESSION);
        //        model.GetSelectListBlockOrganization = GetSelectListBlockOrganization(model.BLOCKORGANIZATION);
        //        model.GetSelectListSoilType = GetSelectListSoilType(model.SOILTYPE);
        //        model.GetSelectListVegetation = GetSelectListVegetation(model.VEGETATION);
        //        model.GetSelectListTopograph = GetSelectListTopograph(model.TOPOGRAPH);
        //        return View("Edit", model);
        //    }
        //    else
        //    {
        //        EmployeeGang.GetSelectListLandConcession = GetSelectListLandConcession();
        //        EmployeeGang.GetSelectListBlockOrganization = GetSelectListBlockOrganization();
        //        EmployeeGang.GetSelectListSoilType = GetSelectListSoilType();
        //        EmployeeGang.GetSelectListVegetation = GetSelectListVegetation();
        //        EmployeeGang.GetSelectListTopograph = GetSelectListTopograph();
        //        return View(EmployeeGang);
        //    }
        //}

        //
        // GET: /EmployeeGang/Edit/5
        public ActionResult Edit(int id)
        {
            Session["EmployeeGang"] = id;
            var model = EGH.Find(id);
            model.GetSelectListForeman1 = GetSelectListForeman1(model.FOREMAN1);
            model.GetSelectListForeman = GetSelectListForeman(model.FOREMAN);
            model.GetSelectListAdmin = GetSelectListAdmin(model.ADMIN);
            model.GetSelectListGangType = GetSelectListGangType(model.GANGTYPE);
            model.GetSelectListBlockOrganization = GetSelectListBlockOrganization(model.BLOCKORGANIZATION);
            return View(model);
        }

        // POST: /EmployeeGang/Edit/5
        [HttpPost]
        public ActionResult Edit(EmployeeGang EmployeeGang, int id)
        {
            if (ModelState.IsValid)
            {
                EmployeeGang.SID = id;
                EmployeeGang.COMPANYSITE = int.Parse(Session["companysite"].ToString());
                EmployeeGang.UPDATEBY = int.Parse(Session["userid"].ToString());
                EmployeeGang.UPDATEDATE = DateTime.Now;
                EGH.Update(EmployeeGang);
                return View("Index", EGH.GetAllByCompanySite(int.Parse(Session["companysite"].ToString())));
            }
            else
            {
                EmployeeGang.GetSelectListForeman1 = GetSelectListForeman1(EmployeeGang.FOREMAN1);
                EmployeeGang.GetSelectListForeman = GetSelectListForeman(EmployeeGang.FOREMAN);
                EmployeeGang.GetSelectListAdmin = GetSelectListAdmin(EmployeeGang.ADMIN);
                EmployeeGang.GetSelectListGangType = GetSelectListGangType(EmployeeGang.GANGTYPE);
                EmployeeGang.GetSelectListBlockOrganization = GetSelectListBlockOrganization(EmployeeGang.BLOCKORGANIZATION);
                return View(EmployeeGang);
            }
        }

        //
        // POST: /EmployeeGang/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                if (EGH.HasEmployeeGangDetail(id))
                    return Json("HasEmployeeGangDetail", JsonRequestBehavior.AllowGet);

                EGH.Remove(id);
                return Json("success", JsonRequestBehavior.AllowGet);//RedirectToAction("Index");
            }
            catch
            {
                return Json("error", JsonRequestBehavior.AllowGet);//View();
            }
        }

        #region Custom Method

        private SelectList GetSelectListLandConcession(object selectedValue = null)
        {
            var model = context.GetLandConcessionByCompanySite(int.Parse(Session["companysite"].ToString()));
            var list = new SelectList(model.Select(x => new { x.SID, x.CONCESSIONNAME }), "SID", "CONCESSIONNAME", selectedValue).ToList();
            list.Insert(0, new SelectListItem { Value = "", Text = "--Select Block Organization--" });
            return new SelectList(list, "Value", "Text");
        }

        private SelectList GetSelectListBlockOrganization(object selectedValue = null)
        {
            var model = context.GetBlockOrganizationByCompanySite(int.Parse(Session["companysite"].ToString()));
            var list = new SelectList(model.Select(x => new { x.SID, x.BLOCKORGANIZATIONNAME }), "SID", "BLOCKORGANIZATIONNAME", selectedValue).ToList();
            list.Insert(0, new SelectListItem { Value = "", Text = "--Select Block Organization--" });
            return new SelectList(list, "Value", "Text");
        }

        private SelectList GetSelectListForeman1(object selectedValue = null)
        {
            var model = context.GetForeman1(int.Parse(Session["companysite"].ToString()));
            var list = new SelectList(model.Select(x => new { x.SID, x.EMPLOYEENAME }), "SID", "EMPLOYEENAME", selectedValue).ToList();
            list.Insert(0, new SelectListItem { Value = "", Text = "--Select Foreman 1--" });
            return new SelectList(list, "Value", "Text");
        }

        private SelectList GetSelectListForeman(object selectedValue = null)
        {
            var model = context.GetForeman(int.Parse(Session["companysite"].ToString()));
            var list = new SelectList(model.Select(x => new { x.SID, x.EMPLOYEENAME }), "SID", "EMPLOYEENAME", selectedValue).ToList();
            list.Insert(0, new SelectListItem { Value = "", Text = "--Select Foreman--" });
            return new SelectList(list, "Value", "Text");
        }

        private SelectList GetSelectListAdmin(object selectedValue = null)
        {
            var model = context.GetAdmin(int.Parse(Session["companysite"].ToString()));
            var list = new SelectList(model.Select(x => new { x.SID, x.EMPLOYEENAME }), "SID", "EMPLOYEENAME", selectedValue).ToList();
            list.Insert(0, new SelectListItem { Value = "", Text = "--Select Admin--" });
            return new SelectList(list, "Value", "Text");
        }

        private SelectList GetSelectListGangType(object selectedValue = null)
        {
            var model = context.GetParameterValueGangType();
            var list = new SelectList(model.Select(x => new { x.SID, x.PARAMETERVALUENAME }), "SID", "PARAMETERVALUENAME", selectedValue).ToList();
            list.Insert(0, new SelectListItem { Value = "", Text = "--Select GangType--" });
            return new SelectList(list, "Value", "Text");
        }

        #endregion
    }
}