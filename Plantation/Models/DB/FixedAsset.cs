using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Plantation.Models.DB
{
    public class FixedAsset
    {
        public int SID { get; set; }
        public string IDFIXEDASSET { get; set; }
        public string FIXEDASSETNAME { get; set; }
        public int FIXEDASSETGROUP { get; set; }
        public int UOM { get; set; }
        public string DESCRIPTION { get; set; }
        public string REMARK { get; set; }
        public int ACTIVE { get; set; }
        public string COMPANY { get; set; }
        public string COMPANYSITE { get; set; }
        public Nullable<int> INPUTBY { get; set; }
        public Nullable<System.DateTime> INPUTDATE { get; set; }
        public Nullable<int> UPDATEBY { get; set; }
        public Nullable<System.DateTime> UPDATEDATE { get; set; }        
        public string UOMNAME { get; set; }
        public string FIXEDASSETGROUPNAME { get; set; }
    }
}