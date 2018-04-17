﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Plantation.Models.DB
{
    public class TRANSPORTER
    {
        public int SID { get; set; }
        public string IDTRANSPORTER { get; set; }
        public string TRANSPORTERNAME { get; set; }
        public int TRANSPORTERGROUP { get; set; }
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
        public bool ISACTIVE { get; set; }
        public Nullable<System.DateTime> ISACTIVEDATE { get; set; }
        public int COMPANY { get; set; }
        public int COMPANYSITE { get; set; }
        public int INPUTBY { get; set; }
        public System.DateTime INPUTDATE { get; set; }
        public int UPDATEDBY { get; set; }
        public System.DateTime UPDATEDDATE { get; set; }
    }
}