﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Plantation.Models.DB
{
    public class Contractor
    {
        public int SID { get; set; }
        public string IDCONTRACTOR { get; set; }
        public string CONTRACTORNAME { get; set; }
        public int CONTRACTORGROUP { get; set; }
        public Nullable<int> CONTROLJOB { get; set; }
        public string CONTACTNAME { get; set; }
        public string BANK { get; set; }
        public string BANKACCOUNT { get; set; }
        public string ADDRESS { get; set; }
        public int COUNTRY { get; set; }
        public int PROVINCE { get; set; }
        public int CITY { get; set; }
        public string POSCODE { get; set; }
        public string PHONE { get; set; }
        public string FAX { get; set; }
        public string EMAIL { get; set; }
        public string NPWP { get; set; }
        public string NPKP { get; set; }
        public string REMARKS { get; set; }
        public Nullable<int> ISACTIVE { get; set; }
        public Nullable<System.DateTime> ISACTIVEDATE { get; set; }
        public int COMPANY { get; set; }
        public int COMPANYSITE { get; set; }
        public int INPUTBY { get; set; }
        public System.DateTime INPUTDATE { get; set; }
        public int UPDATEBY { get; set; }
        public System.DateTime UPDATEDATE { get; set; }
        public string ITEMDESCRIPTION { get; set; }
        public string CONTRACTORGROUPNAME { get; set; }
        public string COUNTRYNAME { get; set; }
        public string PROVINCENAME { get; set; }
        public string CITYNAME { get; set; }
    }
}