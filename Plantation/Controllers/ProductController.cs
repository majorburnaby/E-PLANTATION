using DataTables;
using Plantation.Models;
using Plantation.Models.DB;
using Plantation.Repository;
using Plantation.Utility;
using System;
using System.Web.Mvc;

namespace Plantation.Controllers
{
    public class ProductController : Controller
    {
        private ProductRepository IPD = new ProductRepository();
        ComboBoxContext context = new ComboBoxContext();

        // GET: Data For Editor Datatables
        public JsonResult Data()
        {
            var request = System.Web.HttpContext.Current.Request;

            using (var db = new Database("sqlserver", Constant.DatabaseConnection))
            {
                var response = new Editor(db, "PRODUCT", "PRODUCT.SID")
                    .Model<JoinModelProduct>("PRODUCT")
                    .Model<JoinModelCrop>("CROP")
                    .Model<JoinModelLoadType>("LOADTYPE")
                    .Model<JoinModelUOM>("UNITOFMEASURE")
                    .Field(new Field("PRODUCT.IDPRODUCT")
                        .Validator(Validation.NotEmpty())
                    )
                    .Field(new Field("PRODUCT.PRODUCTNAME")
                        .Validator(Validation.NotEmpty())
                    )
                    .Field(new Field("PRODUCT.UPDATEBY").SetValue(Session["userid"].ToString()))
                    .Field(new Field("PRODUCT.UPDATEDATE").SetValue(DateTime.Now))
                    .Field(new Field("PRODUCT.CROP")
                        .Options(new Options()
                            .Table("CROP")
                            .Value("SID")
                            .Label("CROPNAME")
                        )
                        .Validator(Validation.DbValues(new ValidationOpts { Empty = false }))
                    )
                    .Field(new Field("PRODUCT.LOADTYPE")
                        .Options(new Options()
                            .Table("LOADTYPE")
                            .Value("SID")
                            .Label("LOADTYPENAME")
                        )
                        .Validator(Validation.DbValues(new ValidationOpts { Empty = false }))
                    )
                    .Field(new Field("PRODUCT.UOM")
                        .Options(new Options()
                            .Table("UNITOFMEASURE")
                            .Value("SID")
                            .Label("UOMNAME")
                        )
                        .Validator(Validation.DbValues(new ValidationOpts { Empty = false }))
                    )
                    .LeftJoin("CROP", "CROP.SID", "=", "PRODUCT.CROP")
                    .LeftJoin("LOADTYPE", "LOADTYPE.SID", "=", "PRODUCT.LOADTYPE")
                    .LeftJoin("UNITOFMEASURE", "UNITOFMEASURE.SID", "=", "PRODUCT.UOM")
                    .Process(request)
                    .Data();

                return Json(response, JsonRequestBehavior.AllowGet);
            }
        }

        //
        // GET: /Product/
        public ActionResult Index()
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            return View(IPD.GetAll());
        }

        public JsonResult GetCropList()
        {
            var users = context.GetCrop();
            return Json(users, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetLoadTypeList()
        {
            var users = context.GetLoadType();
            return Json(users, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetUnitOfMeasureList()
        {
            var users = context.GetUnitOfMeasure();
            return Json(users, JsonRequestBehavior.AllowGet);
        }

        //
        // GET: /Product/Details/5
        public ActionResult Details(int? id)
        {

            return View(IPD.Find(id));
        }

        [HttpPost]
        public JsonResult Create(Product product, string userid)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    IPD.Add(product, Session["userid"].ToString());
                }
            }
            catch
            {
                return Json("error");
            }

            return Json("success");
        }

        [HttpPost]
        public JsonResult Edit(Product product, string userid)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    IPD.Update(product, Session["userid"].ToString());
                }
            }
            catch
            {
                return Json("error");
            }

            return Json("success");
        }

        //
        // GET: /Product/Delete/5

        public ActionResult Delete(int id)
        {
            return View(IPD.Find(id));
        }

        //
        // POST: /Product/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                IPD.Remove(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}