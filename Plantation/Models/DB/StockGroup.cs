﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Plantation.Models.DB
{
    public class StockGroup
    {
        public int SID { get; set; }
        public string IDSTOCKGROUP { get; set; }
        public string STOCKGROUPNAME { get; set; }
        public Nullable<int> CONTROLJOB { get; set; }
        public int INPUTBY { get; set; }
        public System.DateTime INPUTDATE { get; set; }
        public int UPDATEBY { get; set; }
        public System.DateTime UPDATEDATE { get; set; }
    }
}