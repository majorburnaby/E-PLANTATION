using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Plantation.Models.DB
{
    public class UserCompanySite
    {
        public int SID { get; set; }
        public int IDUSER { get; set; }
        public int COMPANY { get; set; }
        public int COMPANYSITE { get; set; }
        public bool ISDEFAULT { get; set; }
        public int INPUTBY { get; set; }
        public System.DateTime INPUTDATE { get; set; }
        public int UPDATEDBY { get; set; }
        public System.DateTime UPDATEDDATE { get; set; }
       
    }
}