﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Plantation.Models.DB
{
    public class Workshop
    {
        public int SID { get; set; }
        public string IDWORKSHOP { get; set; }
        public string WORKSHOPNAME { get; set; }
        public int WORKSHOPGROUP { get; set; }
        public int ISACTIVE { get; set; }
        public int COMPANYSITE { get; set; }
        public int INPUTBY { get; set; }
        public System.DateTime INPUTDATE { get; set; }
        public int UPDATEBY { get; set; }
        public System.DateTime UPDATEDATE { get; set; }
    }
}