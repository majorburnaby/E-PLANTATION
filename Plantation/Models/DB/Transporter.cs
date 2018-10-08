using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Plantation.Models.DB
{
    public class Transporter
    {
        [Required]
        public int SID { get; set; }
        [Required]
        public string IDTRANSPORTER { get; set; }
        [Required]
        public string TRANSPORTERNAME { get; set; }
        [Required]
        public int TRANSPORTERGROUP { get; set; }
        [Required]
        public Nullable<int> CONTROLJOB { get; set; }
        public string CONTACTNAME { get; set; }
        public string BANK { get; set; }
        public string BANKACCOUNT { get; set; }
        public string PHONE { get; set; }
        public string FAX { get; set; }
        public string EMAIL { get; set; }
        [Required]
        public string ADDRESS { get; set; }
        [Required]
        public int COUNTRY { get; set; }
        [Required]
        public int PROVINCE { get; set; }
        [Required]
        public int CITY { get; set; }
        public string POSCODE { get; set; }
        public string REMARKS { get; set; }
        public bool ISACTIVE { get; set; }
        public Nullable<System.DateTime> ISACTIVEDATE { get; set; }
        public int COMPANY { get; set; }
        public int COMPANYSITE { get; set; }
        public int INPUTBY { get; set; }
        public System.DateTime INPUTDATE { get; set; }
        public int UPDATEBY { get; set; }
        public System.DateTime UPDATEDATE { get; set; }
        public string ITEMDESCRIPTION { get; set; }
        public string TRANSPORTERGROUPNAME { get; set; }
        public string PARAMETERVALUENAME { get; set; }
        public string COUNTRYNAME { get; set; }
        public string PROVINCENAME { get; set; }
        public string CITYNAME { get; set; }

        public SelectList GetSelectListTransporterGroup { get; set; }
        public SelectList GetSelectListControlJob { get; set; }
        public SelectList GetSelectListCountry { get; set; }
        public SelectList GetSelectListProvince { get; set; }
        public SelectList GetSelectListCity { get; set; }
        public SelectList GetSelectListBank { get; set; }
    }
}