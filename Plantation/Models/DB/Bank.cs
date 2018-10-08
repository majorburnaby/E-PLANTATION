using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Plantation.Models.DB
{
    public class Bank
    {
        [Required]
        public int SID { get; set; }
        [Required]
        public string IDBANK { get; set; }
        [Required]
        public string BANKNAME { get; set; }
        [Required]
        public string BANKACCOUNTCODE { get; set; }
        public string ADDRESS { get; set; }
        [Required]
        public int COUNTRY { get; set; }
        [Required]
        public int PROVINCE { get; set; }
        [Required]
        public int CITY { get; set; }
        public string PHONE { get; set; }
        public string FAX { get; set; }
        [Required]
        public int CURRENCY { get; set; }
        public bool ISACTIVE { get; set; }
        public System.DateTime ISACTIVEDATE { get; set; }
        public string POSCODE { get; set; }
        public int COMPANYSITE { get; set; }
        public int INPUTBY { get; set; }
        public System.DateTime INPUTDATE { get; set; }
        public int UPDATEBY { get; set; }
        public System.DateTime UPDATEDATE { get; set; }
        public string CITYNAME { get; set; }
        public string PROVINCENAME { get; set; }
        public string COUNTRYNAME { get; set; }
        public string CURRENCYNAME { get; set; }

        public SelectList GetSelectListCurrencyMaster { get; set; }
        public SelectList GetSelectListCountry { get; set; }
        public SelectList GetSelectListProvince { get; set; }
        public SelectList GetSelectListCity { get; set; }
    }
}