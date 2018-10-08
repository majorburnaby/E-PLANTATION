using Plantation.Models.DB;
using Plantation.Repository;
using System.Web.Mvc;
using DataTables;
using Plantation.Utility;
using System;

namespace Plantation.Controllers
{
    public class BlockOrganizationController : Controller
    {
        private BlockOrganizationRepository BLO = new BlockOrganizationRepository();

        public JsonResult Data()
        {
            var request = System.Web.HttpContext.Current.Request;

            using (var db = new Database("sqlserver", Constant.DatabaseConnection))
            {
                var response = new Editor(db, "BLOCKORGANIZATION", "SID")
                    .Model<BlockOrganization>()
                    .Field(new Field("IDBLOCKORGANIZATION")
                        .Validator(Validation.NotEmpty())
                    )
                    .Field(new Field("BLOCKORGANIZATIONNAME")
                        .Validator(Validation.NotEmpty())
                    )
                    .Field(new Field("UPDATEBY").SetValue(Session["userid"].ToString()))
                    .Field(new Field("UPDATEDATE").SetValue(DateTime.Now))
                    .Where("COMPANYSITE", int.Parse(Session["companysite"].ToString()))
                    .Process(request)
                    .Data();

                return Json(response, JsonRequestBehavior.AllowGet);
            }
        }

        // GET: BlockOrganization
        public ActionResult Index()
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            return View(BLO.GetAll());
        }

        //
        // GET: /BlockOrganization/Details/5
        public ActionResult Details(int? id)
        {

            return View(BLO.Find(id));
        }

        [HttpPost]
        public JsonResult Create(BlockOrganization blockorganization, string userid)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    blockorganization.COMPANYSITE = int.Parse(Session["companysite"].ToString());
                    BLO.Add(blockorganization, Session["userid"].ToString());
                }
            }
            catch 
            {
                return Json("error");
            }

            return Json("success");
        }
        
        [HttpPost]
        public JsonResult Edit(BlockOrganization blockorganization, string userid)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    blockorganization.COMPANYSITE = int.Parse(Session["companysite"].ToString());
                    BLO.Update(blockorganization, Session["userid"].ToString());
                }
            }
            catch
            {
                return Json("error");
            }

            return Json("success");
        }

        //
        // GET: /BlockOrganization/Delete/5

        public ActionResult Delete(int id)
        {
            return View(BLO.Find(id));
        }

        //
        // POST: /BlockOrganization/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                BLO.Remove(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}