using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Plantation.Models.DB
{
    public class PurchaseRequestDetails
    {
        public int SID { get; set; }
        [Required]
        public string IDPURCHASEREQUEST { get; set; }
        [Required]
        public int ITEMCODE { get; set; }
        public int QUANTITY { get; set; }
        public int UOM { get; set; }
        public DateTime EXPECTDATE { get; set; }
        public int MANAGEBY { get; set; }
        public int ESTIMATEPRICE { get; set; }
        public string REMARKS { get; set; }
        public int APPROVEQUANTITY { get; set; }
        public int COMPANYSITE { get; set; }
        public decimal INPUTBY { get; set; }
        public DateTime INPUTDATE { get; set; }
        public decimal UPDATEBY { get; set; }
        public DateTime UPDATEDATE { get; set; }
        public string ITEMCODENAME { get; set; }

        public SelectList GetSelectListStock { get; set; }

    }
}