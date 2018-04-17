using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Plantation.Models.DB
{
    public class FixedAssetLocation
    {
        public int SID { get; set; }
        public string IDFIXEDASSETLOCATION { get; set; }
        public string FIXEDASSETLOCATIONNAME { get; set; }       
        public string DESCRIPTION { get; set; }
        public int COSTCENTERID { get; set; }
        public string REMARK { get; set; }
        public int ACTIVE { get; set; }
        public string COMPANY { get; set; }
        public string COMPANYSITE { get; set; }
        public Nullable<int> INPUTBY { get; set; }
        public Nullable<System.DateTime> INPUTDATE { get; set; }
        public Nullable<int> UPDATEBY { get; set; }
        public Nullable<System.DateTime> UPDATEDATE { get; set; }      
    }
}