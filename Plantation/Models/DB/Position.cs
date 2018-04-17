using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Plantation.Models.DB
{
    public class Position
    {
        public int SID { get; set; }
        public string IDPOSITION { get; set; }
        public string POSITIONNAME { get; set; }
        public string DESCRIPTION { get; set; }
        public int STATUS { get; set; }
        public int INPUTBY { get; set; }
        public System.DateTime INPUTDATE { get; set; }
        public int UPDATEBY { get; set; }
        public System.DateTime UPDATEDATE { get; set; }
    }
}