﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Plantation.Models.DB
{
    public class CustomerGroup
    {
        public int SID { get; set; }
        public string IDCUSTOMERGROUP { get; set; }
        public string CUSTOMERGROUPNAME { get; set; }
        public int INPUTBY { get; set; }
        public System.DateTime INPUTDATE { get; set; }
        public int UPDATEBY { get; set; }
        public System.DateTime UPDATEDATE { get; set; }
    }
}