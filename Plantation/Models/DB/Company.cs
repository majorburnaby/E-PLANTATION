using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Plantation.Models.DB
{
    public class Company
    {
        public int SID { get; set; }
        public string IDCOMPANY { get; set; }
        public string COMPANYNAME { get; set; }
        [DataType(DataType.MultilineText)]
        public string ADDRESS1 { get; set; }
        [DataType(DataType.MultilineText)]
        public string ADDRESS2 { get; set; }
        public int COUNTRY { get; set; }
        public int PROVINCE { get; set; }
        public int CITY { get; set; }
        public string TELEPHONE1 { get; set; }
        public string TELEPHONE2 { get; set; }
        public string FAX1 { get; set; }
        public string FAX2 { get; set; }
        public string EMAIL { get; set; }
        public string WEBSITE { get; set; }
        public string POSCODE { get; set; }
        public Nullable<int> INPUTBY { get; set; }
        public Nullable<System.DateTime> INPUTDATE { get; set; }
        public Nullable<int> UPDATEBY { get; set; }
        public Nullable<System.DateTime> UPDATEDATE { get; set; }
        public string CITYNAME { get; set; }
        public string PROVINCENAME { get; set; }
        public string COUNTRYNAME { get; set; }
    }
}