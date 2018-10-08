using Plantation.Models;
using Plantation.Models.DB;
using Plantation.Repository;
using System;
using System.Linq;
using System.Web.Mvc;

namespace Plantation.Controllers
{
    public class FieldMasterController : Controller
    {
        private FieldMasterRepository FM = new FieldMasterRepository();
        ComboBoxContext context = new ComboBoxContext();
        // GET: Field
        public ActionResult Index()
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            return View(FM.GetAllByCompanySite(int.Parse(Session["companysite"].ToString())));
        }

        //
        // GET: /Field/Details/5
        public ActionResult Details(int? id)
        {
            return View(FM.Find(id));
        }

        //
        // GET: /Field/Create
        public ActionResult Create()
        {
            var model = new FieldMaster();
            model.ISACTIVE = true;
            model.GetSelectListBlockMaster = GetSelectListBlockMaster();
            model.GetSelectListCrop = GetSelectListCrop();
            model.GetSelectListPlantingMaterial = GetSelectListPlantingMaterial();
            model.GetSelectListPlantingDistance = GetSelectListPlantingDistance();
            model.GetSelectListFieldStatus = GetSelectListFieldStatus();
            model.GetSelectListOwnership2 = GetSelectListOwnership2();
            model.GetSelectListProgeny = GetSelectListProgeny();
            return View(model);
        }

        //
        // POST: /Field/Create
        [HttpPost]
        public ActionResult Create(FieldMaster Field, string userid)
        {
            if (ModelState.IsValid)
            {
                if (Field.ISACTIVE)
                    Field.ISACTIVEDATE = DateTime.Now;

                Field.COMPANYSITE = int.Parse(Session["companysite"].ToString());
                FM.Add(Field, Session["userid"].ToString());
                TempData["successmessage"] = "Saved successfully";
                return RedirectToAction("Create");
            }
            else
            {
                Field.GetSelectListBlockMaster = GetSelectListBlockMaster();
                Field.GetSelectListCrop = GetSelectListCrop();
                Field.GetSelectListPlantingMaterial = GetSelectListPlantingMaterial();
                Field.GetSelectListPlantingDistance = GetSelectListPlantingDistance();
                Field.GetSelectListFieldStatus = GetSelectListFieldStatus();
                Field.GetSelectListOwnership2 = GetSelectListOwnership2();
                Field.GetSelectListProgeny = GetSelectListProgeny();
                return View(Field);
            }
        }

        //
        // GET: /Field/Edit/5
        public ActionResult Edit(int id)
        {
            var model = FM.Find(id);
            model.GetSelectListBlockMaster = GetSelectListBlockMaster(model.BLOCKMASTER);
            model.GetSelectListCrop = GetSelectListCrop(model.CROPTYPE);
            model.GetSelectListPlantingMaterial = GetSelectListPlantingMaterial(model.PLANTINGMATERIAL);
            model.GetSelectListPlantingDistance = GetSelectListPlantingDistance(model.PLANTINGDISTANCE);
            model.GetSelectListFieldStatus = GetSelectListFieldStatus(model.FIELDSTATUS);
            model.GetSelectListOwnership2 = GetSelectListOwnership2(model.OWNERSHIP);
            model.GetSelectListProgeny = GetSelectListProgeny(model.PROGENY);
            return View(model);
        }

        //
        // POST: /Field/Edit/5
        [HttpPost]
        public ActionResult Edit(FieldMaster Field)
        {
            if (ModelState.IsValid)
            {
                Field.COMPANYSITE = int.Parse(Session["companysite"].ToString());
                Field.ISACTIVEDATE = DateTime.Now;
                Field.UPDATEDATE = DateTime.Now;
                FM.Update(Field, Session["userid"].ToString());
                TempData["successmessage"] = "Saved successfully";
                return RedirectToAction("Index");
            }
            else
            {
                Field.GetSelectListBlockMaster = GetSelectListBlockMaster();
                Field.GetSelectListCrop = GetSelectListCrop();
                Field.GetSelectListPlantingMaterial = GetSelectListPlantingMaterial();
                Field.GetSelectListPlantingDistance = GetSelectListPlantingDistance();
                Field.GetSelectListFieldStatus = GetSelectListFieldStatus();
                Field.GetSelectListOwnership2 = GetSelectListOwnership2();
                Field.GetSelectListProgeny = GetSelectListProgeny();
                return View(Field);
            }
        }

        //
        // GET: /Field/Delete/5
        public ActionResult Delete(int id)
        {
            return View(FM.Find(id));
        }

        //
        // POST: /Field/Delete/5
        [HttpPost]
        public JsonResult Delete(int id, FormCollection collection)
        {
            try
            {
                FM.Remove(id);
                return Json("success", JsonRequestBehavior.AllowGet);//RedirectToAction("Index");
            }
            catch
            {
                return Json("error", JsonRequestBehavior.AllowGet);//View();
            }
        }

        public JsonResult GetBlockMasterList()
        {
            var users = context.GetBlockMaster();
            return Json(users, JsonRequestBehavior.AllowGet);
        }

        private SelectList GetSelectListBlockMaster(object selectedValue = null)
        {
            var model = context.GetBlockMaster();
            var list = new SelectList(model.Select(x => new { x.SID, x.BLOCKMASTERNAME }), "SID", "BLOCKMASTERNAME", selectedValue).ToList();
            list.Insert(0, new SelectListItem { Value = "", Text = "--Select Block Master--" });
            return new SelectList(list, "Value", "Text");
        }

        public JsonResult GetCropList()
        {
            var users = context.GetCrop();
            return Json(users, JsonRequestBehavior.AllowGet);
        }

        private SelectList GetSelectListCrop(object selectedValue = null)
        {
            var model = context.GetCrop();
            var list = new SelectList(model.Select(x => new { x.SID, x.CROPNAME }), "SID", "CROPNAME", selectedValue).ToList();
            list.Insert(0, new SelectListItem { Value = "", Text = "--Select Crop--" });
            return new SelectList(list, "Value", "Text");
        }

        public JsonResult GetPlantingMaterialList()
        {
            var users = context.GetPlantingMaterial();
            return Json(users, JsonRequestBehavior.AllowGet);
        }

        private SelectList GetSelectListPlantingMaterial(object selectedValue = null)
        {
            var model = context.GetPlantingMaterial();
            var list = new SelectList(model.Select(x => new { x.SID, x.PARAMETERVALUENAME }), "SID", "PARAMETERVALUENAME", selectedValue).ToList();
            list.Insert(0, new SelectListItem { Value = "", Text = "--Select Planting Material--" });
            return new SelectList(list, "Value", "Text");
        }

        public JsonResult GetPlantingDistanceList()
        {
            var users = context.GetPlantingDistance();
            return Json(users, JsonRequestBehavior.AllowGet);
        }

        private SelectList GetSelectListPlantingDistance(object selectedValue = null)
        {
            var model = context.GetPlantingDistance();
            var list = new SelectList(model.Select(x => new { x.SID, x.PARAMETERVALUENAME }), "SID", "PARAMETERVALUENAME", selectedValue).ToList();
            list.Insert(0, new SelectListItem { Value = "", Text = "--Select Planting Distance--" });
            return new SelectList(list, "Value", "Text");
        }

        public JsonResult GetFieldStatusList()
        {
            var users = context.GetFieldStatus();
            return Json(users, JsonRequestBehavior.AllowGet);
        }

        private SelectList GetSelectListFieldStatus(object selectedValue = null)
        {
            var model = context.GetFieldStatus();
            var list = new SelectList(model.Select(x => new { x.SID, x.PARAMETERVALUENAME }), "SID", "PARAMETERVALUENAME", selectedValue).ToList();
            list.Insert(0, new SelectListItem { Value = "", Text = "--Select Field Status--" });
            return new SelectList(list, "Value", "Text");
        }

        public JsonResult GetOwnership2List()
        {
            var users = context.GetOwnership2();
            return Json(users, JsonRequestBehavior.AllowGet);
        }

        private SelectList GetSelectListOwnership2(object selectedValue = null)
        {
            var model = context.GetOwnership2();
            var list = new SelectList(model.Select(x => new { x.SID, x.PARAMETERVALUENAME }), "SID", "PARAMETERVALUENAME", selectedValue).ToList();
            list.Insert(0, new SelectListItem { Value = "", Text = "--Select Ownership--" });
            return new SelectList(list, "Value", "Text");
        }

        public JsonResult GetProgenyList()
        {
            var users = context.GetProgeny();
            return Json(users, JsonRequestBehavior.AllowGet);
        }

        private SelectList GetSelectListProgeny(object selectedValue = null)
        {
            var model = context.GetProgeny();
            var list = new SelectList(model.Select(x => new { x.SID, x.PARAMETERVALUENAME }), "SID", "PARAMETERVALUENAME", selectedValue).ToList();
            list.Insert(0, new SelectListItem { Value = "", Text = "--Select Progeny--" });
            return new SelectList(list, "Value", "Text");
        }
    }
}