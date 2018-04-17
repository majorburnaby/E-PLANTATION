using System;

namespace Plantation.Models
{
    public class JoinModelCity
    {
        public decimal SID { get; set; }
        public string IDCITY { get; set; }
        public string CITYNAME { get; set; }
        public int COUNTRY { get; set; }
        public int PROVINCE { get; set; }
        public decimal INPUTBY { get; set; }
        public DateTime INPUTDATE { get; set; }
        public decimal UPDATEBY { get; set; }
        public DateTime UPDATEDATE { get; set; }
    }

    public class JoinModelProvince
    {
        public decimal SID { get; set; }
        public string IDPROVINCE { get; set; }
        public string PROVINCENAME { get; set; }
        public int COUNTRY { get; set; }
        public decimal INPUTBY { get; set; }
        public System.DateTime INPUTDATE { get; set; }
        public decimal UPDATEBY { get; set; }
        public System.DateTime UPDATEDATE { get; set; }
    }

    public class JoinModelCountry
    {
        public string COUNTRYNAME { get; set; }
    }

    public class JoinModelProduct
    {
        public int SID { get; set; }
        public string IDPRODUCT { get; set; }
        public string PRODUCTNAME { get; set; }
        public int CROP { get; set; }
        public int UOM { get; set; }
        public int LOADTYPE { get; set; }
        public int INPUTBY { get; set; }
        public System.DateTime INPUTDATE { get; set; }
        public int UPDATEBY { get; set; }
        public System.DateTime UPDATEDATE { get; set; }
    }

    public class JoinModelLoadType
    {
        public int SID { get; set; }
        public string IDLOADTYPE { get; set; }
        public string LOADTYPENAME { get; set; }
        public int UOM { get; set; }
        public int INPUTBY { get; set; }
        public System.DateTime INPUTDATE { get; set; }
        public int UPDATEBY { get; set; }
        public System.DateTime UPDATEDATE { get; set; }
    }

    public class JoinModelUOM
    {
        public string UOMNAME { get; set; }
    }

    public class JoinModelCrop
    {
        public string CROPNAME { get; set; }
    }

    public class JoinModelJob
    {
        public int SID { get; set; }
        public string IDJOB { get; set; }
        public string JOBNAME { get; set; }
        public int JOBTYPE { get; set; }
        public int JOBGROUP { get; set; }
        public int COMPANY { get; set; }
        public int COMPANYSITE { get; set; }
        public int INPUTBY { get; set; }
        public System.DateTime INPUTDATE { get; set; }
        public int UPDATEBY { get; set; }
        public System.DateTime UPDATEDATE { get; set; }
    }

    public class JoinModelJobType
    {
        public string JOBTYPENAME { get; set; }
    }

    public class JoinModelJobGroup
    {
        public string JOBGROUPNAME { get; set; }
    }

    public class JoinModelParameterValue
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
    }

    public class JoinModelParameter
    {
        public string PARAMETERNAME { get; set; }
    }
}