using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Plantation.Models.DB
{
    public class Stock
    {
        public int SID { get; set; }
        [Required]
        public string IDSTOCK { get; set; }
        [Required]
        public string STOCKNAME { get; set; }
        [Required]
        public int STOCKGROUP { get; set; }
        public string PARTNUMBER { get; set; }
        [Required]
        public int UOM { get; set; }
        [Required]
        public int ITEMTYPE { get; set; }
        public int MINIMUMSTOCK { get; set; }
        public int MAXIMUMSTOCK { get; set; }
        public string DESCRIPTION { get; set; } 
        public Nullable<int> INPUTBY { get; set; }
        public Nullable<System.DateTime> INPUTDATE { get; set; }
        public Nullable<int> UPDATEBY { get; set; }
        public Nullable<System.DateTime> UPDATEDATE { get; set; }
        public string STOCKGROUPNAME { get; set; }
        public string UOMNAME { get; set; }
        public string ITEMTYPENAME { get; set; }

        public SelectList GetSelectListStockGroup { get; set; }
        public SelectList GetSelectListUnitOfMeasure { get; set; }
        public SelectList GetSelectListItemType { get; set; }
    }
}