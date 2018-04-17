using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Plantation.Models.DB
{
    public class ROLEMENU
    {
        public int SID { get; set; }
        public int IDROLE { get; set; }
        public int IDMENU { get; set; }
        public int ISPRIVILAGE { get; set; }
        public int ISCANREAD { get; set; }
        public int ISCANADD { get; set; }
        public int ISCANEDIT { get; set; }
        public int ISCANDELETE { get; set; }
        public int ISCANAPPROVE { get; set; }
        public int INPUTBY { get; set; }
        public System.DateTime INPUTDATE { get; set; }
        public int UPDATEBY { get; set; }
        public System.DateTime UPDATEDATE { get; set; }
    }
}