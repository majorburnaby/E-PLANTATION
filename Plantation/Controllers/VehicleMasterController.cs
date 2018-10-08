using Plantation.Models;
using Plantation.Models.DB;
using Plantation.Repository;
using System;
using System.Linq;
using System.Web.Mvc;

namespace Plantation.Controllers
{
    public class VehicleMasterController : Controller
    {
        private VehicleMasterRepository VM = new VehicleMasterRepository();
        ComboBoxContext context = new ComboBoxContext();
        // GET: Vehicle
        public ActionResult Index()
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            return View(VM.GetAllByCompanySite(int.Parse(Session["companysite"].ToString())));
        }

        //
        // GET: /Vehicle/Details/5
        public ActionResult Details(int? id)
        {
            return View(VM.Find(id));
        }

        //
        // GET: /Vehicle/Create
        public ActionResult Create()
        {
            var model = new VehicleMaster();
            model.ISACTIVE = true;
            model.GetSelectListVehicleGroup = GetSelectListVehicleGroup();
            model.GetSelectListFixedAsset = GetSelectListFixedAsset();
            model.GetSelectListOwnership = GetSelectListOwnership();
            return View(model);
        }

        //
        // POST: /Vehicle/Create
        [HttpPost]
        public ActionResult Create(VehicleMaster vehicle, string userid)
        {
            if (ModelState.IsValid)
            {
                if (vehicle.ISACTIVE)
                    vehicle.ISACTIVEDATE = DateTime.Now;

                vehicle.COMPANYSITE = int.Parse(Session["companysite"].ToString());
                VM.Add(vehicle, Session["userid"].ToString());
                TempData["successmessage"] = "Saved successfully";
                return RedirectToAction("Create");
            }
            else
            {
                vehicle.GetSelectListVehicleGroup = GetSelectListVehicleGroup();
                vehicle.GetSelectListFixedAsset = GetSelectListFixedAsset();
                vehicle.GetSelectListOwnership = GetSelectListOwnership();
                return View(vehicle);
            }
        }

        //
        // GET: /Vehicle/Edit/5
        public ActionResult Edit(int id)
        {
            var model = VM.Find(id);
            model.GetSelectListVehicleGroup = GetSelectListVehicleGroup(model.VEHICLEGROUP);
            model.GetSelectListFixedAsset = GetSelectListFixedAsset(model.FIXEDASSET);
            model.GetSelectListOwnership = GetSelectListOwnership(model.OWNERSHIP);
            return View(model);
        }

        //
        // POST: /Vehicle/Edit/5
        [HttpPost]
        public ActionResult Edit(VehicleMaster vehicle)
        {
            if (ModelState.IsValid)
            {
                vehicle.COMPANYSITE = int.Parse(Session["companysite"].ToString());
                vehicle.ISACTIVEDATE = DateTime.Now;
                vehicle.UPDATEDATE = DateTime.Now;
                VM.Update(vehicle, Session["userid"].ToString());
                TempData["successmessage"] = "Saved successfully";
                return RedirectToAction("Index");
            }
            else
            {
                vehicle.GetSelectListVehicleGroup = GetSelectListVehicleGroup();
                vehicle.GetSelectListFixedAsset = GetSelectListFixedAsset();
                vehicle.GetSelectListOwnership = GetSelectListOwnership();
                return View(vehicle);
            }
        }

        //
        // GET: /Vehicle/Delete/5
        public ActionResult Delete(int id)
        {
            return View(VM.Find(id));
        }

        //
        // POST: /Vehicle/Delete/5
        [HttpPost]
        public JsonResult Delete(int id, FormCollection collection)
        {
            try
            {
                VM.Remove(id);
                return Json("success", JsonRequestBehavior.AllowGet);//RedirectToAction("Index");
            }
            catch
            {
                return Json("error", JsonRequestBehavior.AllowGet);//View();
            }
        }

        public JsonResult GetVehicleGroupList()
        {
            var users = context.GetVehicleGroup();
            return Json(users, JsonRequestBehavior.AllowGet);
        }

        private SelectList GetSelectListVehicleGroup(object selectedValue = null)
        {
            var model = context.GetVehicleGroup();
            var list = new SelectList(model.Select(x => new { x.SID, x.VEHICLEGROUPNAME }), "SID", "VEHICLEGROUPNAME", selectedValue).ToList();
            list.Insert(0, new SelectListItem { Value = "", Text = "--Select Vehicle Group--" });
            return new SelectList(list, "Value", "Text");
        }

        public JsonResult GetFixedAssetList()
        {
            var users = context.GetFixedAsset();
            return Json(users, JsonRequestBehavior.AllowGet);
        }

        private SelectList GetSelectListFixedAsset(object selectedValue = null)
        {
            var model = context.GetFixedAsset();
            var list = new SelectList(model.Select(x => new { x.SID, x.FIXEDASSETNAME }), "SID", "FIXEDASSETNAME", selectedValue).ToList();
            list.Insert(0, new SelectListItem { Value = "", Text = "--Select Fixed Asset--" });
            return new SelectList(list, "Value", "Text");
        }

        public JsonResult GetOwnershipList()
        {
            var users = context.GetOwnership();
            return Json(users, JsonRequestBehavior.AllowGet);
        }

        private SelectList GetSelectListOwnership(object selectedValue = null)
        {
            var model = context.GetOwnership();
            var list = new SelectList(model.Select(x => new { x.SID, x.PARAMETERVALUENAME }), "SID", "PARAMETERVALUENAME", selectedValue).ToList();
            list.Insert(0, new SelectListItem { Value = "", Text = "--Select Ownership--" });
            return new SelectList(list, "Value", "Text");
        }        
    }
}