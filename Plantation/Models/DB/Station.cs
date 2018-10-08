﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Plantation.Models.DB
{
    public class Station
    {
        public int SID { get; set; }
        public string IDSTATION { get; set; }
        public string STATIONNAME { get; set; }
        public int ISACTIVE { get; set; }
        public int INPUTBY { get; set; }
        public System.DateTime INPUTDATE { get; set; }
        public int UPDATEBY { get; set; }
        public System.DateTime UPDATEDATE { get; set; }
    }
}