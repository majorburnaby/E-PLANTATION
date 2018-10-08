using Plantation.Models;
using Plantation.Models.DB;
using Plantation.Repository;
using System;
using System.Linq;
using System.Web.Mvc;

namespace Plantation.Controllers
{
    public class MachineMasterController : Controller
    {
        private MachineMasterRepository VM = new MachineMasterRepository();
        ComboBoxContext context = new ComboBoxContext();
        // GET: Machine
        public ActionResult Index()
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            return View(VM.GetAllByCompanySite(int.Parse(Session["companysite"].ToString())));
        }

        //
        // GET: /Machine/Details/5
        public ActionResult Details(int? id)
        {
            return View(VM.Find(id));
        }

        //
        // GET: /Machine/Create
        public ActionResult Create()
        {
            var model = new MachineMaster();
            model.ISACTIVE = true;
            model.GetSelectListStation = GetSelectListStation();
            model.GetSelectListFixedAsset = GetSelectListFixedAsset();
            model.GetSelectListOwnership = GetSelectListOwnership();
            return View(model);
        }

        //
        // POST: /Machine/Create
        [HttpPost]
        public ActionResult Create(MachineMaster Machine, string userid)
        {
            if (ModelState.IsValid)
            {
                if (Machine.ISACTIVE)
                    Machine.ISACTIVEDATE = DateTime.Now;

                Machine.COMPANYSITE = int.Parse(Session["companysite"].ToString());
                VM.Add(Machine, Session["userid"].ToString());
                TempData["successmessage"] = "Saved successfully";
                return RedirectToAction("Create");
            }
            else
            {
                Machine.GetSelectListStation = GetSelectListStation();
                Machine.GetSelectListFixedAsset = GetSelectListFixedAsset();
                Machine.GetSelectListOwnership = GetSelectListOwnership();
                return View(Machine);
            }
        }

        //
        // GET: /Machine/Edit/5
        public ActionResult Edit(int id)
        {
            var model = VM.Find(id);
            model.GetSelectListStation = GetSelectListStation(model.STATION);
            model.GetSelectListFixedAsset = GetSelectListFixedAsset(model.FIXEDASSET);
            model.GetSelectListOwnership = GetSelectListOwnership(model.OWNERSHIP);
            return View(model);
        }

        //
        // POST: /Machine/Edit/5
        [HttpPost]
        public ActionResult Edit(MachineMaster Machine)
        {
            if (ModelState.IsValid)
            {
                Machine.COMPANYSITE = int.Parse(Session["companysite"].ToString());
                Machine.ISACTIVEDATE = DateTime.Now;
                Machine.UPDATEDATE = DateTime.Now;
                VM.Update(Machine, Session["userid"].ToString());
                TempData["successmessage"] = "Saved successfully";
                return RedirectToAction("Index");
            }
            else
            {
                Machine.GetSelectListStation = GetSelectListStation();
                Machine.GetSelectListFixedAsset = GetSelectListFixedAsset();
                Machine.GetSelectListOwnership = GetSelectListOwnership();
                return View(Machine);
            }
        }

        //
        // GET: /Machine/Delete/5
        public ActionResult Delete(int id)
        {
            return View(VM.Find(id));
        }

        //
        // POST: /Machine/Delete/5
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

        public JsonResult GetStationList()
        {
            var users = context.GetStation();
            return Json(users, JsonRequestBehavior.AllowGet);
        }

        private SelectList GetSelectListStation(object selectedValue = null)
        {
            var model = context.GetStation();
            var list = new SelectList(model.Select(x => new { x.SID, x.STATIONNAME }), "SID", "STATIONNAME", selectedValue).ToList();
            list.Insert(0, new SelectListItem { Value = "", Text = "--Select Machine Group--" });
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