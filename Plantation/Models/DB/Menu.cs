using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Plantation.Models.DB
{
    public class MENU
    {
        public decimal IDMENU { get; set; }
        public string MENUNAME { get; set; }
        public decimal IDMENUPARENT { get; set; }
        public string MENUPATH { get; set; }
        public int ISACTIVE { get; set; }
        public string DESCRIPTION { get; set; }
        public int INPUTBY { get; set; }
        public System.DateTime INPUTDATE { get; set; }
        public int UPDATEBY { get; set; }
        public System.DateTime UPDATEDATE { get; set; }
    }
}