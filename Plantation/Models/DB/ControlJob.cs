using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Plantation.Models.DB
{
    public class ControlJob
    {
        public int SID { get; set; }
        public string CONTROLSYSTEM { get; set; }
        public string ITEMCODE { get; set; }
        public string ITEMDESCRIPTION { get; set; }
        public string REMARKS { get; set; }
        public Nullable<int> ISACTIVE { get; set; }
        public Nullable<System.DateTime> ISACTIVEDATE { get; set; }
        public int COMPANY { get; set; }
        public int COMPANYSITE { get; set; }
        public int INPUTBY { get; set; }
        public System.DateTime INPUTDATE { get; set; }
        public int UPDATEBY { get; set; }
        public System.DateTime UPDATEDATE { get; set; }
    }
}