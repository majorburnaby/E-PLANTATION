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
        public int COSTCENTER { get; set; }
        public int ISACTIVE { get; set; }
        public int COMPANYSITE { get; set; }
        public Nullable<int> INPUTBY { get; set; }
        public Nullable<System.DateTime> INPUTDATE { get; set; }
        public Nullable<int> UPDATEBY { get; set; }
        public Nullable<System.DateTime> UPDATEDATE { get; set; }      
    }
}