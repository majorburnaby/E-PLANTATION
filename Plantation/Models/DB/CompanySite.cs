using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Plantation.Models.DB
{
    public class COMPANYSITE
    {
        public int SID { get; set; }
        public string IDCOMPANYSITE { get; set; }
        public string COMPANYSITENAME { get; set; }
        public int COMPANY { get; set; }
        public int LOCATION { get; set; }
        public string ADDRESS1 { get; set; }
        public string ADDRESS2 { get; set; }
        public int COUNTRY { get; set; }
        public int PROVINCE { get; set; }
        public int CITY { get; set; }
        public string POSCODE { get; set; }
        public string TELEPHONE1 { get; set; }
        public string TELEPHONE2 { get; set; }
        public string FAX1 { get; set; }
        public string FAX2 { get; set; }
        public string EMAIL { get; set; }
        public string WEBSITE { get; set; }
        public Nullable<int> INPUTBY { get; set; }
        public Nullable<System.DateTime> INPUTDATE { get; set; }
        public Nullable<int> UPDATEBY { get; set; }
        public Nullable<System.DateTime> UPDATEDATE { get; set; }
    }
}