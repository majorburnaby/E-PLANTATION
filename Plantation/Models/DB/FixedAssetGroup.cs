using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Plantation.Models.DB
{
    public class FixedAssetGroup
    {
        public int SID { get; set; }
        public string IDFAGROUP { get; set; }
        public string FAGROUPNAME { get; set; }
        public Nullable<int> COSTCENTERID { get; set; }
        public int INPUTBY { get; set; }
        public System.DateTime INPUTDATE { get; set; }
        public int UPDATEBY { get; set; }
        public System.DateTime UPDATEDATE { get; set; }
        public string COSTCENTERNAME { get; set; }
    }
}