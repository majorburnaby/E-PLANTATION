using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Plantation.Models.DB
{
    public class Rainfall
    {
        public int SID { get; set; }
        public System.DateTime RAINDATE { get; set; }
        public int BLOCKORGANIZATION { get; set; }
        public System.DateTime RAINSTART { get; set; }
        public System.DateTime RAINEND { get; set; }
        public int RAINQUANTITY { get; set; }
        public string REMARK { get; set; }
        public int COMPANYSITE { get; set; }
        public Nullable<int> INPUTBY { get; set; }
        public Nullable<System.DateTime> INPUTDATE { get; set; }
        public Nullable<int> UPDATEBY { get; set; }
        public Nullable<System.DateTime> UPDATEDATE { get; set; }
        public string BLOCKORGANIZATIONNAME { get; set; }

        public SelectList GetSelectListBlockOrganization { get; set; }
    }
}