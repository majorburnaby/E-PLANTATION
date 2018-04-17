using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Plantation.Models.DB
{
    public class UnitOfMeasure
    {
        public int SID { get; set; }
        public string IDUOM { get; set; }
        public string UOMNAME { get; set; }
        public bool ISACTIVE { get; set; }
        public int INPUTBY { get; set; }
        public System.DateTime INPUTDATE { get; set; }
        public int UPDATEBY { get; set; }
        public System.DateTime UPDATEDATE { get; set; }
    }
}