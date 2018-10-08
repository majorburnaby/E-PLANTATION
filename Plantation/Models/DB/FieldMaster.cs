using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;


namespace Plantation.Models.DB
{
    public class FieldMaster
    {
        [Required]
        public int SID { get; set; }
        [Required]
        public string IDFIELDMASTER { get; set; }
        [Required]
        public string FIELDMASTERNAME { get; set; }
        [Required]
        public int BLOCKMASTER { get; set; }
        [Required]
        public int CROPTYPE { get; set; }
        [Required]
        public int PLANTINGMATERIAL { get; set; }
        public int PROGENY { get; set; }
        [Required]
        public int HECTPLANTED { get; set; }
        [Required]
        public int PLANTINGPOINTNUM { get; set; }
        [Required]
        public int PLANTINGDISTANCE { get; set; }
        [Required]
        public int TOTALSTAND { get; set; }
        [Required]
        public System.DateTime PLANTINGDATE { get; set; }
        [Required]
        public int STANDPERHECT { get; set; }
        public int EMPTYHOLE { get; set; }
        [Required]
        public int FIELDSTATUS { get; set; }
        public System.DateTime HARVCOMMDATE { get; set; }
        public int HECTHARV { get; set; }
        [Required]
        public int OWNERSHIP { get; set; }
        public int CARRYDISTANCE { get; set; }
        [Required]
        public bool ISACTIVE { get; set; }
        public System.DateTime ISACTIVEDATE { get; set; }
        public int COMPANYSITE { get; set; }
        public int INPUTBY { get; set; }
        public System.DateTime INPUTDATE { get; set; }
        public int UPDATEBY { get; set; }
        public System.DateTime UPDATEDATE { get; set; }
        public string BLOCKMASTERNAME { get; set; }
        public string CROPNAME { get; set; }
        public string PLANTINGMATERIALNAME { get; set; }
        public string FIELDSTATUSNAME { get; set; }
        public string OWNERSHIPNAME { get; set; }
        public string PROGENYNAME { get; set; }

        public SelectList GetSelectListBlockMaster { get; set; }
        public SelectList GetSelectListCrop { get; set; }
        public SelectList GetSelectListPlantingMaterial { get; set; }
        public SelectList GetSelectListPlantingDistance { get; set; }
        public SelectList GetSelectListFieldStatus { get; set; }
        public SelectList GetSelectListOwnership2 { get; set; }
        public SelectList GetSelectListProgeny { get; set; }
    }
}