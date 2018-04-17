using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Plantation.Models.DB
{
    public class Job
    {
        public int SID { get; set; }
        public string IDJOB { get; set; }
        public string JOBNAME { get; set; }
        public string ACCOUNT { get; set; }
        public int JOBTYPE { get; set; }
        public int JOBGROUP { get; set; }
        public int COMPANY { get; set; }
        public int COMPANYSITE { get; set; }
        public int INPUTBY { get; set; }
        public System.DateTime INPUTDATE { get; set; }
        public int UPDATEDBY { get; set; }
        public System.DateTime UPDATEDDATE { get; set; }
        public string JOBGROUPNAME { get; set; }
        public string JOBTYPENAME { get; set; }
    }
}