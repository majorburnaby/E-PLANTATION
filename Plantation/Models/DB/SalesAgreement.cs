using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Plantation.Models.DB
{
    public class SALESAGREEMENT
    {
        public long SID { get; set; }
        public string IDSALESAGREEMENT { get; set; }
        public System.DateTime CREATEDATE { get; set; }
        public int CUSTOMER { get; set; }
        public string CUSTOMERADDRESS { get; set; }
        public string NPWP { get; set; }
        public string BANKACCOUNT { get; set; }
        public string BANK { get; set; }
        public int PRODUCT { get; set; }
        public int UOM { get; set; }
        public decimal QUANTITY { get; set; }
        public int CURRENCYMASTER { get; set; }
        public decimal UNITPRICE { get; set; }
        public decimal AMOUNT { get; set; }
        public string VATCODE { get; set; }
        public int EXPORTORLOCAL { get; set; }
        public string EXPORTTAX { get; set; }
        public string PAYMENTCONDITION { get; set; }
        public decimal DOWNPAYMENT { get; set; }
        public string SALESTERMS { get; set; }
        public string TOLERANCE { get; set; }
        public Nullable<System.DateTime> DELIVERYDATE { get; set; }
        public string DELIVERYADDRESS { get; set; }
        public string WEIGHTBASIS { get; set; }
        public string REMARK { get; set; }
        public Nullable<int> COMPANY { get; set; }
        public Nullable<int> COMPANYSITE { get; set; }
        public int INPUTBY { get; set; }
        public System.DateTime INPUTDATE { get; set; }
        public int UPDATEBY { get; set; }
        public System.DateTime UPDATEDATE { get; set; }
    }
}