using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Plantation.Models.DB
{
    public class SupplierGroup
    {
        public int SID { get; set; }
        public string IDSUPPLIERGROUP { get; set; }
        public string SUPPLIERGROUPNAME { get; set; }
        public int INPUTBY { get; set; }
        public System.DateTime INPUTDATE { get; set; }
        public int UPDATEBY { get; set; }
        public System.DateTime UPDATEDATE { get; set; }
    }
}