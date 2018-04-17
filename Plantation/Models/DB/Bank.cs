using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Plantation.Models.DB
{
    public class Bank
    {
        public int SID { get; set; }
        public string IDBANK { get; set; }
        public string BANKNAME { get; set; }
        public string BANKACCOUNTCODE { get; set; }
        public string ADDRESS { get; set; }
        public int COUNTRY { get; set; }
        public int PROVINCE { get; set; }
        public int CITY { get; set; }
        public string PHONE { get; set; }
        public string FAX { get; set; }
        public string DATEREGISTERED { get; set; }
        public int CURRENCY { get; set; }
        public int INACTIVE { get; set; }
        public DateTime INACTIVEDATE { get; set; }
        public string POSCODE { get; set; }
        public string COMPANY { get; set; }
        public string COMPANYSITE { get; set; }
        public Nullable<int> INPUTBY { get; set; }
        public Nullable<System.DateTime> INPUTDATE { get; set; }
        public Nullable<int> UPDATEBY { get; set; }
        public Nullable<System.DateTime> UPDATEDATE { get; set; }
        public string CITYNAME { get; set; }
        public string PROVINCENAME { get; set; }
        public string COUNTRYNAME { get; set; }
        public string CURRENCYNAME { get; set; }
    }
}