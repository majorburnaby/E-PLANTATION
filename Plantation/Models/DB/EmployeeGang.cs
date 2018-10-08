using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Plantation.Models.DB
{
    public class EmployeeGang
    {
        public int SID;
        [Required]
        public string IDEMPLOYEEGANG { get; set; }
        [Required]
        public string EMPLOYEEGANGNAME { get; set; }
        public int FOREMAN1 { get; set; }
        public int FOREMAN { get; set; }
        public int ADMIN { get; set; }
        public int GANGTYPE { get; set; }
        public int BLOCKORGANIZATION { get; set; }
        public int COMPANYSITE { get; set; }
        public decimal INPUTBY { get; set; }
        public DateTime INPUTDATE { get; set; }
        public decimal UPDATEBY { get; set; }
        public DateTime UPDATEDATE { get; set; }
        public string FOREMAN1NAME { get; set; }
        public string FOREMANNAME { get; set; }
        public string ADMINNAME { get; set; }
        public string GANGTYPENAME { get; set; }
        public string BLOCKORGANIZATIONNAME { get; set; }

        public SelectList GetSelectListForeman1 { get; set; }
        public SelectList GetSelectListForeman { get; set; }
        public SelectList GetSelectListAdmin { get; set; }
        public SelectList GetSelectListGangType { get; set; }
        public SelectList GetSelectListBlockOrganization { get; set; }

    }
}