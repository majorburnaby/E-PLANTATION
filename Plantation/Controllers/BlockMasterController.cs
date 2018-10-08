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
    public class BlockMasterController : Controller
    {
        private BlockMasterRepository BM = new BlockMasterRepository();
        private ComboBoxContext context = new ComboBoxContext();

        // GET: BlockMaster
        public ActionResult Index()
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            
            return View(BM.GetAllByCompanySite(int.Parse(Session["companysite"].ToString())));
        }

        //
        // GET: /BlockMaster/Create
        public ActionResult Create()
        {
            var model = new BlockMaster();
            model.GetSelectListLandConcession = GetSelectListLandConcession();
            model.GetSelectListBlockOrganization = GetSelectListBlockOrganization();
            model.GetSelectListSoilType = GetSelectListSoilType();
            model.GetSelectListVegetation = GetSelectListVegetation();
            model.GetSelectListTopograph = GetSelectListTopograph();
            return View(model);
        }

        //
        // POST: /BlockMaster/Create
        [HttpPost]
        public ActionResult Create(BlockMaster blockmaster)
        {
            if (ModelState.IsValid)
            {
                blockmaster.COMPANYSITE = int.Parse(Session["companysite"].ToString());
                BM.Add(blockmaster, Session["userid"].ToString());
                TempData["successmessage"] = "Saved successfully";
                return RedirectToAction("Create");
            }
            else
            {
                blockmaster.GetSelectListLandConcession = GetSelectListLandConcession();
                blockmaster.GetSelectListBlockOrganization = GetSelectListBlockOrganization();
                blockmaster.GetSelectListSoilType = GetSelectListSoilType();
                blockmaster.GetSelectListVegetation = GetSelectListVegetation();
                blockmaster.GetSelectListTopograph = GetSelectListTopograph();
                return View(blockmaster);
            }
        }
        //
        // POST: /BlockMaster/CreateAndAddBlockUsage
        [HttpPost]
        public ActionResult CreateAndAddBlockUsage(BlockMaster blockmaster)
        {
            if (ModelState.IsValid)
            {
                blockmaster.COMPANYSITE = int.Parse(Session["companysite"].ToString());
                BlockMaster blo = BM.Add(blockmaster, Session["userid"].ToString());
                var model = BM.Find(blo.SID);
                Session["SID_BLOCKMASTER"] = blo.SID;
                model.GetSelectListLandConcession = GetSelectListLandConcession(model.LANDCONCESSION);
                model.GetSelectListBlockOrganization = GetSelectListBlockOrganization(model.BLOCKORGANIZATION);
                model.GetSelectListSoilType = GetSelectListSoilType(model.SOILTYPE);
                model.GetSelectListVegetation = GetSelectListVegetation(model.VEGETATION);
                model.GetSelectListTopograph = GetSelectListTopograph(model.TOPOGRAPH);
                return View("Edit", model);
            }
            else
            {
                blockmaster.GetSelectListLandConcession = GetSelectListLandConcession();
                blockmaster.GetSelectListBlockOrganization = GetSelectListBlockOrganization();
                blockmaster.GetSelectListSoilType = GetSelectListSoilType();
                blockmaster.GetSelectListVegetation = GetSelectListVegetation();
                blockmaster.GetSelectListTopograph = GetSelectListTopograph();
                return View(blockmaster);
            }
        }

        //
        // GET: /BlockMaster/Edit/5
        public ActionResult Edit(int id)
        {
            Session["SID_BLOCKMASTER"] = id;
            var model = BM.Find(id);
            model.GetSelectListLandConcession = GetSelectListLandConcession(model.LANDCONCESSION);
            model.GetSelectListBlockOrganization = GetSelectListBlockOrganization(model.BLOCKORGANIZATION);
            model.GetSelectListSoilType = GetSelectListSoilType(model.SOILTYPE);
            model.GetSelectListVegetation = GetSelectListVegetation(model.VEGETATION);
            model.GetSelectListTopograph = GetSelectListTopograph(model.TOPOGRAPH);
            return View(model);
        }

        // POST: /BlockMaster/Edit/5
        [HttpPost]
        public ActionResult Edit(BlockMaster blockmaster, int id)
        {
            if (ModelState.IsValid)
            {
                blockmaster.SID = id;
                blockmaster.COMPANYSITE = int.Parse(Session["companysite"].ToString());
                blockmaster.UPDATEBY = int.Parse(Session["userid"].ToString());
                blockmaster.UPDATEDATE = DateTime.Now;
                BM.Update(blockmaster);
                return View("Index", BM.GetAllByCompanySite(int.Parse(Session["companysite"].ToString())));
            }
            else
            {
                blockmaster.GetSelectListLandConcession = GetSelectListLandConcession(blockmaster.LANDCONCESSION);
                blockmaster.GetSelectListBlockOrganization = GetSelectListBlockOrganization(blockmaster.BLOCKORGANIZATION);
                blockmaster.GetSelectListSoilType = GetSelectListSoilType(blockmaster.SOILTYPE);
                blockmaster.GetSelectListVegetation = GetSelectListVegetation(blockmaster.VEGETATION);
                blockmaster.GetSelectListTopograph = GetSelectListTopograph(blockmaster.TOPOGRAPH);
                return View(blockmaster);
            }
        }

        // POST: /BlockMaster/Edit/5
        [HttpPost]
        public ActionResult EditHectarage(int SID, int Hectarage)
        {
            try
            {
                BM.UpdateHectarage(SID, Hectarage);
                return Json("success", JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json("error", JsonRequestBehavior.AllowGet);
            }
        }

        //
        // POST: /Company/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                if (BM.HasBlockUsage(id))
                    return Json("hasblockusage", JsonRequestBehavior.AllowGet);

                BM.Remove(id);
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

        private SelectList GetSelectListSoilType(object selectedValue = null)
        {
            var model = context.GetParameterValueSoilType();
            var list = new SelectList(model.Select(x => new { x.SID, x.PARAMETERVALUENAME }), "SID", "PARAMETERVALUENAME", selectedValue).ToList();
            list.Insert(0, new SelectListItem { Value = "", Text = "--Select Soil Type--" });
            return new SelectList(list, "Value", "Text");
        }

        private SelectList GetSelectListVegetation(object selectedValue = null)
        {
            var model = context.GetParameterValueVegetation();
            var list = new SelectList(model.Select(x => new { x.SID, x.PARAMETERVALUENAME }), "SID", "PARAMETERVALUENAME", selectedValue).ToList();
            list.Insert(0, new SelectListItem { Value = "", Text = "--Select Vegetation--" });
            return new SelectList(list, "Value", "Text");
        }

        private SelectList GetSelectListTopograph(object selectedValue = null)
        {
            var model = context.GetParameterValueTopograph();
            var list = new SelectList(model.Select(x => new { x.SID, x.PARAMETERVALUENAME }), "SID", "PARAMETERVALUENAME", selectedValue).ToList();
            list.Insert(0, new SelectListItem { Value = "", Text = "--Select Topograph--" });
            return new SelectList(list, "Value", "Text");
        }

        #endregion
    }
}