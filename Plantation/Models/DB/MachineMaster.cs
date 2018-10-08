using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Plantation.Models.DB
{
    public class MachineMaster
    {
        [Required]
        public int SID { get; set; }
        [Required]
        public string IDMACHINE { get; set; }
        [Required]
        public string MACHINENAME { get; set; }
        [Required]
        public int STATION { get; set; }
        [Required]
        public int OWNERSHIP { get; set; }
        public int FIXEDASSET { get; set; }
        public System.DateTime PURCHASEDATE { get; set; }
        public int PURCHASECOST { get; set; }
        public bool ISACTIVE { get; set; }
        public System.DateTime ISACTIVEDATE { get; set; }
        public int COMPANYSITE { get; set; }
        public int INPUTBY { get; set; }
        public System.DateTime INPUTDATE { get; set; }
        public int UPDATEBY { get; set; }
        public System.DateTime UPDATEDATE { get; set; }
        public string STATIONNAME { get; set; }
        public string PARAMETERVALUENAME { get; set; }
        public string FIXEDASSETNAME { get; set; }

        public SelectList GetSelectListStation { get; set; }
        public SelectList GetSelectListOwnership { get; set; }
        public SelectList GetSelectListFixedAsset { get; set; }
    }
}