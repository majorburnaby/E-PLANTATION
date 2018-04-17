using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Plantation.Models.DB
{
    public class CostCenter
    {
        public int SID { get; set; }
        public string IDCOSTCENTER { get; set; }
        public string COSTCENTERNAME { get; set; }
        public string PARENTCOSTCENTER { get; set; }
        public string COSTCENTERTYPE { get; set; }
        public string ALIASNAME { get; set; }
        public string ALLOCATIONTYPE { get; set; }
        public string BUDGETTYPE { get; set; }
        public string TRANSACTIONS { get; set; }
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