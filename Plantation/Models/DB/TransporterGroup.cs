using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Plantation.Models.DB
{
    public class TransporterGroup
    {
        public int SID { get; set; }
        public string IDTRANSPORTERGROUP { get; set; }
        public string TRANSPORTERGROUPNAME { get; set; }
        public Nullable<int> CONTROLJOB { get; set; }
        public int INPUTBY { get; set; }
        public System.DateTime INPUTDATE { get; set; }
        public int UPDATEDBY { get; set; }
        public System.DateTime UPDATEDDATE { get; set; }
        public string CONTROLSYSTEM { get; set; }
    }
}