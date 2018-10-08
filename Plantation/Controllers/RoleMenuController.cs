using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Plantation.Models.DB;
using Plantation.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Plantation.Controllers
{
    public class RoleMenuController : Controller
    {
        private RoleMenuRepository RMP = new RoleMenuRepository();

        // GET: RoleMenu
        public object GetRoleMenu(int IDROLE)
        {
            var rolemenu = RMP.GetAllByRole(IDROLE);
            //return Json(rolemenu, JsonRequestBehavior.AllowGet);


            JArray objectArray = new JArray();

            foreach (var obj in rolemenu)
            {
                var jsO = JsonConvert.SerializeObject(obj, Formatting.Indented);
                JObject oType = JObject.Parse(jsO);
                objectArray.Add(oType);
            }

            JObject jObj = new JObject();
            jObj.Add("data", objectArray);

            return jObj;
            //return Json(jObj, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public bool SaveRoleMenu(int IDROLE, List<ROLEMENU> RoleMenu)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("IDMENU", typeof(string)));
            dt.Columns.Add(new DataColumn("IDROLE", typeof(int)));
            dt.Columns.Add(new DataColumn("ISCANREAD", typeof(int)));
            dt.Columns.Add(new DataColumn("ISCANADD", typeof(int)));
            dt.Columns.Add(new DataColumn("ISCANEDIT", typeof(int)));
            dt.Columns.Add(new DataColumn("ISCANDELETE", typeof(int)));
            dt.Columns.Add(new DataColumn("INPUTBY", typeof(int)));
            dt.Columns.Add(new DataColumn("INPUTDATE", typeof(DateTime)));
            dt.Columns.Add(new DataColumn("UPDATEBY", typeof(int)));
            dt.Columns.Add(new DataColumn("UPDATEDATE", typeof(DateTime)));

            DateTime date = DateTime.Now;
            int userid = int.Parse(Session["userid"].ToString());
            foreach (var obj in RoleMenu)
            {
                dt.Rows.Add(obj.IDMENU, IDROLE, obj.ISCANREAD, obj.ISCANADD, obj.ISCANEDIT, obj.ISCANDELETE, userid, date, userid, date);
            }

            return RMP.BulkInsert(IDROLE, dt);
        }
    }
}