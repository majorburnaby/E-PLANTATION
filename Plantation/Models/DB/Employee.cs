using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Plantation.Models.DB
{
    public class Employee
    {
        [Required]
        public int SID { get; set; }
        [Required]
        public int SIDEMPLOYEE { get; set; }
        [Required]
        public string IDEMPLOYEE { get; set; }
        [Required]
        public string EMPLOYEENAME { get; set; }
        public string OTHERNAME { get; set; }
        [Required]
        public string IDCARD { get; set; }
        public string IDFINGERPRINT { get; set; }
        [Required]
        public string BIRTHPLACE { get; set; }
        [Required]
        public System.DateTime DOB { get; set; }
        [Required]
        public int SEX { get; set; }
        public int RACE { get; set; }
        [Required]
        public string ADDRESS { get; set; }
        public string HOMEPHONE { get; set; }
        public string MOBILEPHONE { get; set; }
        public string EMAIL { get; set; }
        [Required]
        public int RELIGION { get; set; }
        [Required]
        public int NATIONALITY { get; set; }
        public string ZIPCODE { get; set; }
        [Required]
        public int EDUCATIONLEVEL { get; set; }
        public int INSTITUTION { get; set; }
        public int PROGRAMOFSTUDY { get; set; }
        [Required]
        public int MARITALSTATUS { get; set; }
        public string SPOUSENAME { get; set; }
        public int NUMBEROFCHILD { get; set; }
        [Required]
        public int FAMILYRICESTATUS { get; set; }
        [Required]
        public int NATURETYPE { get; set; }
        [Required]
        public System.DateTime DATEJOIN { get; set; }
        [Required]
        public int DEPARTMENT { get; set; }
        [Required]
        public int GRADE { get; set; }
        [Required]
        public int EMPLOYEETYPE { get; set; }
        [Required]
        public int EMPLOYEESECTION { get; set; }
        [Required]
        public int EMPLOYEEPOSITION { get; set; }
        [Required]
        public int BASICSALARY { get; set; }
        [Required]
        public int PAYMENTMETHOD { get; set; }
        public int BANK { get; set; }
        public string OWNERSHIP { get; set; }
        public string BANKACCOUNT { get; set; }
        [Required]
        public int FAMILYTAXSTATUS { get; set; }
        public string NPWPNO { get; set; }
        public string JAMSOSTEKNO { get; set; }
        public bool TERMINATE { get; set; }
        public System.DateTime DATETERMINATE { get; set; }
        public string REMARK { get; set; }
        public int COMPANYSITE { get; set; }
        public int INPUTBY { get; set; }
        public System.DateTime INPUTDATE { get; set; }
        public int UPDATEBY { get; set; }
        public System.DateTime UPDATEDATE { get; set; }
        public string SEXNAME { get; set; }
        public string RACENAME { get; set; }
        public string RELIGIONNAME { get; set; }
        public string COUNTRYNAME { get; set; }
        public string EDUCATIONLEVELNAME { get; set; }
        public string INSTITUTIONNAME { get; set; }
        public string PROGRAMOFSTUDYNAME { get; set; }
        public string MARITALSTATUSNAME { get; set; }
        public string FAMILYRICESTATUSNAME { get; set; }
        public string FAMILYTAXSTATUSNAME { get; set; }
        public string NATURETYPENAME { get; set; }
        public string DEPARTMENTNAME { get; set; }
        public string GRADENAME { get; set; }
        public string EMPLOYEETYPENAME { get; set; }
        public string EMPLOYEESECTIONNAME { get; set; }
        public string EMPLOYEEPOSITIONNAME { get; set; }
        public string PAYMENTMETHODNAME { get; set; }
        public string BANKNAME { get; set; }


        public SelectList GetSelectListSex { get; set; }
        public SelectList GetSelectListRace { get; set; }
        public SelectList GetSelectListEducationLevel { get; set; }
        public SelectList GetSelectListReligion { get; set; }
        public SelectList GetSelectListNationality { get; set; }
        public SelectList GetSelectListInstitution { get; set; }
        public SelectList GetSelectListProgramOfStudy { get; set; }
        public SelectList GetSelectListMaritalStatus { get; set; }
        public SelectList GetSelectListFamilyRiceStatus { get; set; }
        public SelectList GetSelectListFamilyTaxStatus { get; set; }
        public SelectList GetSelectListNatureType { get; set; }
        public SelectList GetSelectListDepartment { get; set; }
        public SelectList GetSelectListGrade { get; set; }
        public SelectList GetSelectListEmployeeType { get; set; }
        public SelectList GetSelectListEmployeeSection { get; set; }
        public SelectList GetSelectListEmployeePosition { get; set; }
        public SelectList GetSelectListPaymentMethod { get; set; }
        public SelectList GetSelectListBank { get; set; }
        public SelectList GetSelectListOwnership { get; set; }
    }
}