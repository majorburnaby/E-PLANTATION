using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Plantation.Models.DB
{
    public class Product
    {
        public int SID { get; set; }
        public string IDPRODUCT { get; set; }
        public string PRODUCTNAME { get; set; }
        public int CROP { get; set; }
        public int UOM { get; set; }
        public int LOADTYPE { get; set; }
        public int INPUTBY { get; set; }
        public System.DateTime INPUTDATE { get; set; }
        public int UPDATEBY { get; set; }
        public System.DateTime UPDATEDATE { get; set; }
        public string CROPNAME { get; set; }
        public string LOADTYPENAME { get; set; }
        public string UOMNAME { get; set; }
    }
}