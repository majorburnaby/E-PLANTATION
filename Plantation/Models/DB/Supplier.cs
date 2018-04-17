using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Plantation.Models.DB
{
    public class Supplier
    {
        public int SID { get; set; }
        public string IDSUPPLIER { get; set; }
        public string SUPPLIERNAME { get; set; }
        public int SUPPLIERGROUP { get; set; }
        public Nullable<int> CONTROLJOB { get; set; }
        public string CONTACTNAME { get; set; }
        public string BANK { get; set; }
        public string BANKACCOUNT { get; set; }
        public string PHONE { get; set; }
        public string FAX { get; set; }
        public string EMAIL { get; set; }
        public string ADDRESS { get; set; }
        public int COUNTRY { get; set; }
        public int PROVINCE { get; set; }
        public int CITY { get; set; }
        public string POSCODE { get; set; }
        public string REMARKS { get; set; }
        public bool ISACTIVE { get; set; }
        public System.DateTime ISACTIVEDATE { get; set; }
        public int COMPANY { get; set; }
        public int COMPANYSITE { get; set; }
        public int INPUTBY { get; set; }
        public System.DateTime INPUTDATE { get; set; }
        public int UPDATEBY { get; set; }
        public System.DateTime UPDATEDATE { get; set; }
        public string ITEMDESCRIPTION { get; set; }
        public string SUPPLIERGROUPNAME { get; set; }
        public string PARAMETERVALUENAME { get; set; }
        public string COUNTRYNAME { get; set; }
        public string PROVINCENAME { get; set; }
        public string CITYNAME { get; set; }
    }
}