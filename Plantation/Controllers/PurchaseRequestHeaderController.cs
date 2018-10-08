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
    public class PurchaseRequestHeaderController : Controller
    {
        private PurchaseRequestHeaderRepository PRH = new PurchaseRequestHeaderRepository();
        private ComboBoxContext context = new ComboBoxContext();

        // GET: PurchaseRequestHeader
        public ActionResult Index()
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            
            return View(PRH.GetAllByCompanySite(int.Parse(Session["companysite"].ToString())));
        }

        //
        // GET: /PurchaseRequestHeader/Create
        public ActionResult Create()
        {
            var model = new PurchaseRequestHeader();
            model.GetSelectListRequestorID = GetSelectListRequestorID();
            model.GetSelectListPriority = GetSelectListPriority();
            return View(model);
        }

        //
        // POST: /PurchaseRequestHeader/Create
        [HttpPost]
        public ActionResult Create(PurchaseRequestHeader PurchaseRequestHeader)
        {
            if (ModelState.IsValid)
            {
                PurchaseRequestHeader.COMPANYSITE = int.Parse(Session["companysite"].ToString());
                PRH.Add(PurchaseRequestHeader, Session["userid"].ToString());
                TempData["successmessage"] = "Saved successfully";
                return RedirectToAction("Create");
            }
            else
            {
                PurchaseRequestHeader.GetSelectListRequestorID = GetSelectListRequestorID();
                PurchaseRequestHeader.GetSelectListPriority = GetSelectListPriority();
                return View(PurchaseRequestHeader);
            }
        }
        //
        // POST: /PurchaseRequestHeader/CreateAndAddPurchaseRequestDetails
        [HttpPost]
        public ActionResult CreateAndAddPurchaseRequestDetails(PurchaseRequestHeader PurchaseRequestHeader)
        {
            if (ModelState.IsValid)
            {
                PurchaseRequestHeader.COMPANYSITE = int.Parse(Session["companysite"].ToString());
                PurchaseRequestHeader blo = PRH.Add(PurchaseRequestHeader, Session["userid"].ToString());
                var model = PRH.Find(blo.SID);
                Session["SID_PURCHASEREQUESTHEADER"] = blo.SID;
                model.GetSelectListRequestorID = GetSelectListRequestorID(model.REQUESTORID);
                model.GetSelectListPriority = GetSelectListPriority(model.PRIORITY);

                return RedirectToAction("Edit", new { id = blo.SID });
                //return View("Edit", model);
            }
            else
            {
                PurchaseRequestHeader.GetSelectListRequestorID = GetSelectListRequestorID();
                PurchaseRequestHeader.GetSelectListPriority = GetSelectListPriority();
                return View(PurchaseRequestHeader);
            }
        }

        //
        // GET: /PurchaseRequestHeader/Edit/5
        public ActionResult Edit(int id)
        {
            Session["SID_PURCHASEREQUESTHEADER"] = id;
            var model = PRH.Find(id);
            model.GetSelectListRequestorID = GetSelectListRequestorID(model.REQUESTORID);
            model.GetSelectListPriority = GetSelectListPriority(model.PRIORITY);
            return View(model);
        }

        // POST: /PurchaseRequestHeader/Edit/5
        [HttpPost]
        public ActionResult Edit(PurchaseRequestHeader PurchaseRequestHeader, int id)
        {
            if (ModelState.IsValid)
            {
                PurchaseRequestHeader.SID = id;
                PurchaseRequestHeader.COMPANYSITE = int.Parse(Session["companysite"].ToString());
                PurchaseRequestHeader.UPDATEBY = int.Parse(Session["userid"].ToString());
                PurchaseRequestHeader.UPDATEDATE = DateTime.Now;
                PRH.Update(PurchaseRequestHeader);
                return RedirectToAction("Index", PRH.GetAllByCompanySite(int.Parse(Session["companysite"].ToString())));
                //return View("Index", PRH.GetAllByCompanySite(int.Parse(Session["companysite"].ToString())));
            }
            else
            {
                PurchaseRequestHeader.GetSelectListRequestorID = GetSelectListRequestorID(PurchaseRequestHeader.REQUESTORID);
                PurchaseRequestHeader.GetSelectListPriority = GetSelectListPriority(PurchaseRequestHeader.PRIORITY);
                return View(PurchaseRequestHeader);
            }
        }

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                if (PRH.HasPurchaseRequestDetails(id))
                    return Json("HasPurchaseRequestDetails", JsonRequestBehavior.AllowGet);

                PRH.Remove(id);
                return Json("success", JsonRequestBehavior.AllowGet);//RedirectToAction("Index");
            }
            catch
            {
                return Json("error", JsonRequestBehavior.AllowGet);//View();
            }
        }

        #region Custom Method

        private SelectList GetSelectListRequestorID(object selectedValue = null)
        {
            var model = context.GetRequestorID(int.Parse(Session["companysite"].ToString()));
            var list = new SelectList(model.Select(x => new { x.SID, x.STORENAME }), "SID", "STORENAME", selectedValue).ToList();
            list.Insert(0, new SelectListItem { Value = "", Text = "--Select Requestor--" });
            return new SelectList(list, "Value", "Text");
        }

        private SelectList GetSelectListPriority(object selectedValue = null)
        {
            var model = context.GetPriority();
            var list = new SelectList(model.Select(x => new { x.SID, x.PARAMETERVALUENAME }), "SID", "PARAMETERVALUENAME", selectedValue).ToList();
            list.Insert(0, new SelectListItem { Value = "", Text = "--Select Priority--" });
            return new SelectList(list, "Value", "Text");
        }

        #endregion
    }
}