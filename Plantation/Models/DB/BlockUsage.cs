using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Plantation.Models.DB
{
    public class BlockUsage
    {
        public int SID { get; set; }
        public int BLOCKMASTER { get; set; }
        public int USAGE { get; set; }
        public string HECTARAGE { get; set; }
        public int COMPANYSITE { get; set; }
        public decimal INPUTBY { get; set; }
        public DateTime INPUTDATE { get; set; }
        public decimal UPDATEBY { get; set; }
        public DateTime UPDATEDATE { get; set; }
    }
}