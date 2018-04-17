using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Plantation.Models.DB
{
    public class Department
    {
        public int SID { get; set; }
        public string IDDEPARTMENT { get; set; }
        public string DEPARTMENTNAME { get; set; }
        public string DESCRIPTION { get; set; }
        public int INPUTBY { get; set; }
        public System.DateTime INPUTDATE { get; set; }
        public int UPDATEDBY { get; set; }
        public System.DateTime UPDATEDDATE { get; set; }
    }
}