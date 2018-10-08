using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Plantation.Models.DB
{
    public class EmployeeGangDetail
    {
        public int SID;
        [Required]
        public string EMPLOYEEGANG { get; set; }
        public int EMPLOYEE { get; set; }
        public int COMPANYSITE { get; set; }
        public decimal INPUTBY { get; set; }
        public DateTime INPUTDATE { get; set; }
        public decimal UPDATEBY { get; set; }
        public DateTime UPDATEDATE { get; set; }
        public string EMPLOYEENAME { get; set; }
        public string EMPLOYEEGANGNAME { get; set; }

        public SelectList GetSelectListEmployee { get; set; }
        public SelectList GetSelectListEmployeeGang { get; set; }

    }
}