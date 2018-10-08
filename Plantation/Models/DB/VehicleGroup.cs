using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Plantation.Models.DB
{
    public class VehicleGroup
    {
        public int SID { get; set; }
        public string IDVEHICLEGROUP { get; set; }
        public string VEHICLEGROUPNAME { get; set; }
        public int INPUTBY { get; set; }
        public System.DateTime INPUTDATE { get; set; }
        public int UPDATEBY { get; set; }
        public System.DateTime UPDATEDATE { get; set; }
    }
}