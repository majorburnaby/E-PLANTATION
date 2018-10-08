using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Plantation.Models.DB
{
    public partial class BlockOrganization
    {
        public decimal SID { get; set; }
        public string IDBLOCKORGANIZATION { get; set; }
        public string BLOCKORGANIZATIONNAME { get; set; }
        public int COMPANYSITE { get; set; }
        public decimal INPUTBY { get; set; }
        public DateTime INPUTDATE { get; set; }
        public decimal UPDATEBY { get; set; }
        public DateTime UPDATEDATE { get; set; }
    }
}