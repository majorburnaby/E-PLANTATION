using DataTables;
using Newtonsoft.Json.Linq;
using Plantation.Models;
using Plantation.Models.DB;
using Plantation.Repository;
using Plantation.Utility;
using System;
using System.Web.Mvc;

namespace Plantation.Controllers
{
    public class PurchaseRequestDetailsController : Controller
    {
        private PurchaseRequestDetailsRepository PRD = new PurchaseRequestDetailsRepository();
        private ComboBoxContext context = new ComboBoxContext();

        public object GetPurchaseDetail(int IDPURCHASEREQUEST)
        {
            var data = PRD.GetPurchaseDetail(IDPURCHASEREQUEST);

            JArray jArray = new JArray();
            foreach (ViewModelPurchaseDetail vm in data)
            {
                JObject jObj = new JObject(
                            new JProperty("SID", vm.SID),
                            new JProperty("IDPURCHASEREQUEST", vm.IDPURCHASEREQUEST),
                            new JProperty("ITEMCODE", vm.ITEMCODE),
                            new JProperty("IDSTOCK", vm.IDSTOCK),
                            new JProperty("STOCKNAME", vm.STOCKNAME),
                            new JProperty("QUANTITY", vm.QUANTITY),
                            new JProperty("UOM", vm.UOM),
                            new JProperty("IDUOM", vm.IDUOM),
                            new JProperty("UOMNAME", vm.UOMNAME),
                            new JProperty("EXPECTDATE", vm.EXPECTDATE),
                            new JProperty("MANAGEBY", vm.MANAGEBY),
                            new JProperty("IDMANAGEBY", vm.IDMANAGEBY),
                            new JProperty("MANAGEBYNAME", vm.MANAGEBYNAME),
                            new JProperty("ESTIMATEPRICE", vm.ESTIMATEPRICE),
                            new JProperty("REMARK", vm.REMARK),
                            new JProperty("APPROVEQUANTITY", vm.APPROVEQUANTITY),
                            new JProperty("COMPANYSITE", vm.COMPANYSITE)
                            );

                jArray.Add(jObj);
            }
            
            return new JObject(new JProperty("data", jArray)).ToString(); //Json(jArray.ToString(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult Data()
        {
            var request = System.Web.HttpContext.Current.Request;

            using (var db = new Database("sqlserver", Constant.DatabaseConnection))
            {
                var response = new Editor(db, "PURCHASEREQUESTDETAILS", "SID")
                    .Model<PurchaseRequestDetails>("PURCHASEREQUESTDETAILS")
                    .Model<JoinModelStock>("STOCK")
                    .Model<JoinModelUnitOfMeasure>("UNITOFMEASURE")
                    .Model<JoinModelParameterValue>("PARAMETERVALUE")
                    .Field(new Field("PURCHASEREQUESTDETAILS.IDPURCHASEREQUEST"))
                    .Field(new Field("PURCHASEREQUESTDETAILS.ITEMCODE").Options(new Options()
                            .Table("STOCK")
                            .Value("SID")
                            .Label("STOCKNAME")
                        ))
                    .Field(new Field("PURCHASEREQUESTDETAILS.QUANTITY"))
                    .Field(new Field("PURCHASEREQUESTDETAILS.UOM").Options(new Options()
                            .Table("UNITOFMEASURE")
                            .Value("SID")
                            .Label("UOMNAME")
                        ))
                    .Field(new Field("PURCHASEREQUESTDETAILS.EXPECTDATE"))
                    .Field(new Field("PURCHASEREQUESTDETAILS.MANAGEBY").Options(new Options()
                            .Table("PARAMETERVALUE")
                            .Value("SID")
                            .Label("PARAMETERVALUENAME")
                        ))
                    .Field(new Field("PURCHASEREQUESTDETAILS.ESTIMATEPRICE"))
                    .Field(new Field("PURCHASEREQUESTDETAILS.REMARK"))
                    .Field(new Field("PURCHASEREQUESTDETAILS.APPROVEQUANTITY"))
                    .LeftJoin("STOCK", "STOCK.SID", "=", "PURCHASEREQUESTDETAILS.ITEMCODE")
                    .LeftJoin("PARAMETERVALUE", "PARAMETERVALUE.SID", "=", "PURCHASEREQUESTDETAILS.MANAGEBY")
                    .Process(request)
                    .Data();

                return Json(response, JsonRequestBehavior.AllowGet);
            }
        }

        // GET: PurchaseRequestDetails
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Create(PurchaseRequestDetails PurchaseRequestDetails)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    PurchaseRequestDetails.COMPANYSITE = int.Parse(Session["companysite"].ToString());
                    PurchaseRequestDetails.INPUTBY = int.Parse(Session["userid"].ToString());
                    PurchaseRequestDetails.INPUTDATE = DateTime.Now;
                    PurchaseRequestDetails.UPDATEBY = int.Parse(Session["userid"].ToString());
                    PurchaseRequestDetails.UPDATEDATE = DateTime.Now;
                    PRD.Add(PurchaseRequestDetails);
                }
            }
            catch (Exception ex)
            {
                return Json("error");
            }

            return Json("success");
        }

        [HttpPost]
        public JsonResult Edit(PurchaseRequestDetails PurchaseRequestDetails)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    PurchaseRequestDetails.UPDATEBY = int.Parse(Session["userid"].ToString());
                    PurchaseRequestDetails.UPDATEDATE = DateTime.Now;
                    PRD.Update(PurchaseRequestDetails);
                }
            }
            catch(Exception ex)
            {
                return Json("error");
            }

            return Json("success");
        }

        //
        // POST: /PurchaseRequestDetails/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                PRD.Remove(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public JsonResult GetStockList(int CompanySite)
        {
            var users = context.GetStockByCompanySite(CompanySite);
            return Json(users, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetUnitOfMeasureList()
        {
            var users = context.GetUnitOfMeasure();
            return Json(users, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetParameterValueManageByList()
        {
            var users = context.GetParameterValueManageBy();
            return Json(users, JsonRequestBehavior.AllowGet);
        }
    }
}