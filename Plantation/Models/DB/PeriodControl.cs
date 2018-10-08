using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Plantation.Models.DB
{
    public partial class PeriodControl
    {
        public string SYSTEM { get; set; }
        public System.DateTime LASTPERIODPOSTED { get; set; }
        public System.DateTime CURRENTDATE { get; set; }
        public int POSTINGPOSITION { get; set; }
        public int CURRENTACCOUNTINGYEAR { get; set; }
        public int CURRENTACCOUNTINGPERIOD { get; set; }
        public int CLOSEACCOUNTINGYEAR { get; set; }
        public int CLOSEACCOUNTINGPERIOD { get; set; }
        public int NEXTACCOUNTINGYEAR { get; set; }
        public int NEXTACCOUNTINGPERIOD { get; set; }
        public int COMPANYSITE { get; set; }
        public int INPUTBY { get; set; }
        public System.DateTime INPUTDATE { get; set; }
        public int UPDATEBY { get; set; }
        public System.DateTime UPDATEDATE { get; set; }
    }
}