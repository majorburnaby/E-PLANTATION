using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Plantation.Models.DB
{
    public class LoadType
    {
        public int SID { get; set; }
        public string IDLOADTYPE { get; set; }
        public string LOADTYPENAME { get; set; }
        public int UOM { get; set; }
        public int INPUTBY { get; set; }
        public System.DateTime INPUTDATE { get; set; }
        public int UPDATEBY { get; set; }
        public System.DateTime UPDATEDATE { get; set; }
        public string UOMNAME { get; set; }
    }
}