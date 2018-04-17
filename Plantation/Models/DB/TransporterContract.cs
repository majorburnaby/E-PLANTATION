using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Plantation.Models.DB
{
    public class TRANSPORTERCONTRACT
    {
        public long SID { get; set; }
        public string IDTRANSPORTERCONTRACT { get; set; }
        public Nullable<int> IDTRANSPORTER { get; set; }
        public System.DateTime TDATE { get; set; }
        public Nullable<int> LOADTYPE { get; set; }
        public Nullable<decimal> VOLUME { get; set; }
        public Nullable<int> UOM { get; set; }
        public Nullable<int> CUSTOMER { get; set; }
        public Nullable<decimal> UNITPRICE { get; set; }
        public Nullable<int> UOMPRICE { get; set; }
        public string TOLERANCE { get; set; }
        public string REMARK { get; set; }
        public Nullable<int> COMPANY { get; set; }
        public Nullable<int> COMPANYSITE { get; set; }
        public int INPUTBY { get; set; }
        public System.DateTime INPUTDATE { get; set; }
        public int UPDATEDBY { get; set; }
        public System.DateTime UPDATEDDATE { get; set; }
    }
}