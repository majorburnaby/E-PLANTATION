using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Plantation.Models.DB
{
    public class DELIVERYORDER
    {
        public long SID { get; set; }
        public string IDDELIVERYORDER { get; set; }
        public System.DateTime DELIVERYDATE { get; set; }
        public long SALESAGREEMENT { get; set; }
        public int CUSTOMER { get; set; }
        public int PRODUCT { get; set; }
        public decimal CONTRACTQUANTITY { get; set; }
        public int UOMCONTRACT { get; set; }
        public System.DateTime STARTDATE { get; set; }
        public System.DateTime ENDDATE { get; set; }
        public string QUANTITY { get; set; }
        public int UOM { get; set; }
        public Nullable<long> TRANSPORTERCONTRACT { get; set; }
        public Nullable<int> TRANSPORTER { get; set; }
        public string TRANSPORTCOST { get; set; }
        public string TOLERANCE { get; set; }
        public string REMARK { get; set; }
        public int COMPANY { get; set; }
        public int COMPANYSITE { get; set; }
        public int INPUTBY { get; set; }
        public System.DateTime INPUTDATE { get; set; }
        public int UPDATEBY { get; set; }
        public System.DateTime UPDATEDATE { get; set; }
    }
}