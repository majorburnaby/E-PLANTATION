using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Plantation.Models.DB
{
    public partial class City
    {
        public decimal SID { get; set; }
        public string IDCITY { get; set; }
        public string CITYNAME { get; set; }
        public decimal COUNTRY { get; set; }        
        public decimal PROVINCE { get; set; }
        public decimal INPUTBY { get; set; }
        public DateTime INPUTDATE { get; set; }
        public decimal UPDATEBY { get; set; }
        public DateTime UPDATEDATE { get; set; }
        public string COUNTRYNAME { get; set; }
        public string PROVINCENAME { get; set; }


    }
}