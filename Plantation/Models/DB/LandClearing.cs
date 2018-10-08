using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Plantation.Models.DB
{
    public class LandClearing
    {
        [Required]
        public int SID { get; set; }
        [Required]
        public string DOCUMENTNO { get; set; }
        [Required]
        public System.DateTime DATE { get; set; }
        [Required]
        public int HECTARAGE { get; set; }
        [Required]
        public int HECTARAGEPLANTING { get; set; }
        public int HECTARAGECLEARED { get; set; }
        public int COMPANYSITE { get; set; }
        public int INPUTBY { get; set; }
        public System.DateTime INPUTDATE { get; set; }
        public int UPDATEBY { get; set; }
        public System.DateTime UPDATEDATE { get; set; }
        public string VEHICLEGROUPNAME { get; set; }
        public string PARAMETERVALUENAME { get; set; }
        public string FIXEDASSETNAME { get; set; }

        public SelectList GetSelectListVehicleGroup { get; set; }
        public SelectList GetSelectListOwnership { get; set; }
        public SelectList GetSelectListFixedAsset { get; set; }
    }
}