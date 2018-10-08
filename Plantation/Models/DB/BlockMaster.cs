using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Plantation.Models.DB
{
    public class BlockMaster
    {
        public int SID;
        [Required]
        public string IDBLOCKMASTER { get; set; }
        [Required]
        public string BLOCKMASTERNAME { get; set; }
        public int LANDCONCESSION { get; set; }
        public int BLOCKORGANIZATION { get; set; }
        public int SOILTYPE { get; set; }
        public int VEGETATION { get; set; }
        public int TOPOGRAPH { get; set; }
        public int HECTARAGE { get; set; }
        public int HECTARAGE_TEMP { get; set; }
        public int COMPANYSITE { get; set; }
        public decimal INPUTBY { get; set; }
        public DateTime INPUTDATE { get; set; }
        public decimal UPDATEBY { get; set; }
        public DateTime UPDATEDATE { get; set; }
        public string CONCESSIONNAME { get; set; }
        public string SOILTYPENAME { get; set; }
        public string VEGETATIONNAME { get; set; }
        public string SOILCATEGORYNAME { get; set; }
        public string TOPOGRAPHNAME { get; set; }
        public string BLOCKORGANIZATIONNAME { get; set; }

        public SelectList GetSelectListLandConcession { get; set; }
        public SelectList GetSelectListBlockOrganization { get; set; }
        public SelectList GetSelectListSoilType { get; set; }
        public SelectList GetSelectListVegetation { get; set; }
        public SelectList GetSelectListSoilCategory { get; set; }
        public SelectList GetSelectListTopograph { get; set; }

    }
}