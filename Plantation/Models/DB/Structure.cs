using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Plantation.Models.DB
{
    public class Structure
    {
        public int SID { get; set; }
        public string IDSTRUCTURE { get; set; }
        public string STRUCTURENAME { get; set; }
        public int DEPARTMENT { get; set; }
        public int POSITION { get; set; }
        public int LOCATION { get; set; }
        public bool ISACTIVE { get; set; }
        public Nullable<int> COMPANY { get; set; }
        public Nullable<int> COMPANYSITE { get; set; }
        public int INPUTBY { get; set; }
        public System.DateTime INPUTDATE { get; set; }
        public int UPDATEDBY { get; set; }
        public System.DateTime UPDATEDDATE { get; set; }
    }
}