using Plantation.Models.DB;
using Plantation.Repository;
using System.Web.Mvc;
using DataTables;
using Plantation.Utility;
using System;
using Plantation.Models;

namespace Plantation.Controllers
{
    public class BlockUsageController : Controller
    {
        private BlockUsageRepository BU = new BlockUsageRepository();
        private ComboBoxContext context = new ComboBoxContext();

        public JsonResult Data()
        {
            var request = System.Web.HttpContext.Current.Request;

            using (var db = new Database("sqlserver", Constant.DatabaseConnection))
            {
                var response = new Editor(db, "BLOCKUSAGE", "SID")
                    .Model<BlockUsage>("BLOCKUSAGE")
                    .Model<JoinModelParameterValue>("PARAMETERVALUE")
                    .Field(new Field("BLOCKUSAGE.BLOCKMASTER"))
                    .Field(new Field("BLOCKUSAGE.USAGE").Options(new Options()
                            .Table("PARAMETERVALUE")
                            .Value("SID")
                            .Label("PARAMETERVALUENAME")
                        ))
                    .Field(new Field("BLOCKUSAGE.HECTARAGE"))
                    .LeftJoin("PARAMETERVALUE", "PARAMETERVALUE.SID", "=", "BLOCKUSAGE.USAGE")
                    .Where("BLOCKUSAGE.BLOCKMASTER", int.Parse(Session["SID_BLOCKMASTER"].ToString()), "=")
                    .Process(request)
                    .Data();

                return Json(response, JsonRequestBehavior.AllowGet);
            }
        }

        // GET: BlockUsage
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Create(BlockUsage blockusage)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    blockusage.COMPANYSITE = int.Parse(Session["companysite"].ToString());
                    blockusage.INPUTBY = int.Parse(Session["userid"].ToString());
                    blockusage.INPUTDATE = DateTime.Now;
                    blockusage.UPDATEBY = int.Parse(Session["userid"].ToString());
                    blockusage.UPDATEDATE = DateTime.Now;
                    BU.Add(blockusage);
                }
            }
            catch (Exception ex)
            {
                return Json("error");
            }

            return Json("success");
        }

        [HttpPost]
        public JsonResult Edit(BlockUsage blockusage)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    blockusage.UPDATEBY = int.Parse(Session["userid"].ToString());
                    blockusage.UPDATEDATE = DateTime.Now;
                    BU.Update(blockusage);
                }
            }
            catch
            {
                return Json("error");
            }

            return Json("success");
        }

        //
        // POST: /BlockUsage/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                BU.Remove(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public JsonResult GetUsageList()
        {
            var users = context.GetParameterValueBlockUsage();
            return Json(users, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetUnusedUsageList(int BlockMaster)
        {
            var users = context.GetUnusedParameterValueBlockUsage(BlockMaster);
            return Json(users, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetTotalBlockUsage(int BlockMaster)
        {
            var total = BU.GetTotalBlockUsage(BlockMaster);
            return Json(total, JsonRequestBehavior.AllowGet);
        }
    }
}