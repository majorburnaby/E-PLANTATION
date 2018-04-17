using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Plantation.Models.DB
{
    public class ParameterValue
    {
        public int SID { get; set; }
        public string IDPARAMETERVALUE { get; set; }
        public string PARAMETERVALUENAME { get; set; }
        public int PARAMETER { get; set; }
        public Nullable<int> ISACTIVE { get; set; }
        public int INPUTBY { get; set; }
        public System.DateTime INPUTDATE { get; set; }
        public int UPDATEBY { get; set; }
        public System.DateTime UPDATEDATE { get; set; }
        public string PARAMETERNAME { get; set; }
    }
}