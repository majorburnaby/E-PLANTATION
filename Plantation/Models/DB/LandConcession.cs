using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;


namespace Plantation.Models.DB
{
    public class LandConcession
    {
        [Required]
        public int SID { get; set; }
        [Required]
        public string IDCONCESSION { get; set; }
        [Required]
        public string CONCESSIONNAME { get; set; }
        [Required]
        public string PERMISSIONNO { get; set; }
        [Required]
        public int PERMISSIONTYPE { get; set; }
        public System.DateTime PERMISSIONDATE { get; set; }
        public System.DateTime STARTDATE { get; set; }
        public System.DateTime ENDDATE { get; set; }
        [Required]
        public int COUNTRY { get; set; }
        [Required]
        public int PROVINCE { get; set; }
        [Required]
        public int CITY { get; set; }
        [Required]
        public int HECTARAGE { get; set; }
        public int COMPANYSITE { get; set; }
        public int INPUTBY { get; set; }
        public System.DateTime INPUTDATE { get; set; }
        public int UPDATEBY { get; set; }
        public System.DateTime UPDATEDATE { get; set; }
        public string PARAMETERVALUENAME { get; set; }
        public string COUNTRYNAME { get; set; }
        public string PROVINCENAME { get; set; }
        public string CITYNAME { get; set; }

        public SelectList GetSelectListCountry { get; set; }
        public SelectList GetSelectListProvince { get; set; }
        public SelectList GetSelectListCity { get; set; }
        public SelectList GetSelectListPermissionType { get; set; }
    }
}