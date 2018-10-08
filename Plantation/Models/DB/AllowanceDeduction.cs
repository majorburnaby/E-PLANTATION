using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Plantation.Models.DB
{
    public partial class AllowanceDeduction
    {
        public decimal SID { get; set; }
        public string IDALLOWANCEDEDUCTION { get; set; }
        public string ALLOWANCEDEDUCTIONNAME { get; set; }
        public int ALLOWANCEDEDUCTIONTYPE { get; set; }
        public decimal INPUTBY { get; set; }
        public System.DateTime INPUTDATE { get; set; }
        public decimal UPDATEBY { get; set; }
        public System.DateTime UPDATEDATE { get; set; }
        public string PARAMETERVALUENAME { get; set; }
    }
}