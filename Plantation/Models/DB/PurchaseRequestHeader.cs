using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Plantation.Models.DB
{
    public class PurchaseRequestHeader
    {
        public int SID { get; set; }

        public string DOCNAME { get; set; }
        public string IDPURCHASEREQUEST { get; set; }
        [Required]
        public DateTime PURCHASEREQUESTDATE { get; set; }
        [Required]
        public int REQUESTORID { get; set; }
        [Required]
        public int PRIORITY { get; set; }
        public string REMARKS { get; set; }
        public int STATUS { get; set; }
        public int COMPANYSITE { get; set; }
        public decimal INPUTBY { get; set; }
        public DateTime INPUTDATE { get; set; }
        public decimal UPDATEBY { get; set; }
        public DateTime UPDATEDATE { get; set; }
        public string REQUESTORIDNAME { get; set; }
        public string PRIORITYNAME { get; set; }

        public SelectList GetSelectListRequestorID { get; set; }
        public SelectList GetSelectListPriority { get; set; }

    }
}