using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Plantation.Models.DB
{
    public class Store
    {
        public int SID { get; set; }
        public string IDSTORE { get; set; }
        public string STORENAME { get; set; }
        public Nullable<int> WAREHOUSETYPE { get; set; }
        public string REMARK { get; set; }
        public int INACTIVE { get; set; }
        public DateTime INACTIVEDATE { get; set; }
        public string COMPANY { get; set; }
        public string COMPANYSITE { get; set; }
        public int INPUTBY { get; set; }
        public System.DateTime INPUTDATE { get; set; }
        public int UPDATEBY { get; set; }
        public System.DateTime UPDATEDATE { get; set; }
    }
}