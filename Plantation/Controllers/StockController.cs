using DataTables;
using Plantation.Models;
using Plantation.Models.DB;
using Plantation.Repository;
using Plantation.Utility;
using System;
using System.Linq;
using System.Web.Mvc;

namespace Plantation.Controllers
{
    public class StockController : Controller
    {
        private StockRepository STK = new StockRepository();
        ComboBoxContext context = new ComboBoxContext();
        // GET: Stock
        public ActionResult Index()
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            return View(STK.GetAll());
        }

        //
        // GET: /Stock/Details/5
        public ActionResult Details(int? id)
        {

            return View(STK.Find(id));
        }

        //
        // GET: /Stock/Create
        public ActionResult Create()
        {
            var model = new Stock();
            model.GetSelectListStockGroup = GetSelectListStockGroup();
            model.GetSelectListUnitOfMeasure = GetSelectListUnitOfMeasure();
            model.GetSelectListItemType = GetSelectListItemType();
            return View(model);
        }

        //
        // POST: /Stock/Create
        [HttpPost]
        public ActionResult Create([Bind(Include = "SID, IDSTOCK, STOCKNAME, STOCKGROUP, PARTNUMBER, UOM, ITEMTYPE, MINIMUMSTOCK, MAXIMUMSTOCK, DESCRIPTION, POSCODE, INPUTBY, INPUTDATE, UPDATEBY, UPDATEDATE")] Stock Stock)
        {
            if (ModelState.IsValid)
            {                
                STK.Add(Stock, Session["userid"].ToString());
                TempData["successmessage"] = "Saved successfully";
                return RedirectToAction("Create");
            }
            else
            {
                Stock.GetSelectListStockGroup = GetSelectListStockGroup();
                Stock.GetSelectListUnitOfMeasure = GetSelectListUnitOfMeasure();
                Stock.GetSelectListItemType = GetSelectListItemType();
                return View(Stock);
            }
        }

        //
        // GET: /Stock/Edit/5
        public ActionResult Edit(int id)
        {
            var model = STK.Find(id);
            model.GetSelectListStockGroup = GetSelectListStockGroup(model.STOCKGROUP);
            model.GetSelectListUnitOfMeasure = GetSelectListUnitOfMeasure(model.UOM);
            model.GetSelectListItemType = GetSelectListItemType(model.ITEMTYPE);
            return View(model);
        }

        // POST: /Stock/Edit/5
        [HttpPost]
        public ActionResult Edit([Bind(Include = "SID, IDSTOCK, STOCKNAME, STOCKGROUP, PARTNUMBER, UOM, ITEMTYPE, MINIMUMSTOCK, MAXIMUMSTOCK, DESCRIPTION, POSCODE, INPUTBY, INPUTDATE, UPDATEBY, UPDATEDATE")] Stock Stock, int id)
        {
            if (ModelState.IsValid)
            {
                Stock.UPDATEBY = int.Parse(Session["userid"].ToString());
                Stock.UPDATEDATE = DateTime.Now;
                STK.Update(Stock, Session["userid"].ToString());
                TempData["successmessage"] = "Saved successfully";
                return RedirectToAction("Index");
            }
            else
            {
                Stock.GetSelectListStockGroup = GetSelectListStockGroup();
                Stock.GetSelectListUnitOfMeasure = GetSelectListUnitOfMeasure();
                Stock.GetSelectListItemType = GetSelectListItemType();
                return View(Stock);
            }
        }

        //
        // GET: /Stock/Delete/5

        public ActionResult Delete(int id)
        {
            return View(STK.Find(id));
        }

        //
        // POST: /Stock/Delete/5

        [HttpPost]
        public JsonResult Delete(int id, FormCollection collection)
        {
            try
            {
                STK.Remove(id);
                return Json("success", JsonRequestBehavior.AllowGet);//RedirectToAction("Index");
            }
            catch
            {
                return Json("error", JsonRequestBehavior.AllowGet);//View();
            }
        }

        #region Custom Method
        
        private SelectList GetSelectListStockGroup(object selectedValue = null)
        {
            var model = context.GetStockGroup();
            var list = new SelectList(model.Select(x => new { x.SID, x.STOCKGROUPNAME }), "SID", "STOCKGROUPNAME", selectedValue).ToList();
            list.Insert(0, new SelectListItem { Value = "", Text = "--Select Stock Group--" });
            return new SelectList(list, "Value", "Text");
        }

        private SelectList GetSelectListUnitOfMeasure(object selectedValue = null)
        {
            var model = context.GetUnitOfMeasure();
            var list = new SelectList(model.Select(x => new { x.SID, x.UOMNAME }), "SID", "UOMNAME", selectedValue).ToList();
            list.Insert(0, new SelectListItem { Value = "", Text = "--Select UOM--" });
            return new SelectList(list, "Value", "Text");
        }

        private SelectList GetSelectListItemType(object selectedValue = null)
        {
            var model = context.GetItemType();
            var list = new SelectList(model.Select(x => new { x.SID, x.PARAMETERVALUENAME }), "SID", "PARAMETERVALUENAME", selectedValue).ToList();
            list.Insert(0, new SelectListItem { Value = "", Text = "--Select Item Type--" });
            return new SelectList(list, "Value", "Text");
        }
        #endregion
    }
}