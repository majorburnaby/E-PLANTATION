using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Plantation.Models.DB
{
    public partial class Province
    {
        public decimal SID { get; set; }
        public string IDPROVINCE { get; set; }
        public string PROVINCENAME { get; set; }
        public decimal COUNTRY { get; set; }
        public decimal INPUTBY { get; set; }
        public System.DateTime INPUTDATE { get; set; }
        public decimal UPDATEBY { get; set; }
        public System.DateTime UPDATEDATE { get; set; }
        public string COUNTRYNAME { get; set; }
    }
}