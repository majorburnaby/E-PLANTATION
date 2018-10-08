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
        public string ACCOUNT { get; set; }
        public int JOBGROUP { get; set; }
        public int JOBTYPE { get; set; }
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

    public class JoinModelTransporterGroup
    {
        public int SID { get; set; }
        public string IDTRANSPORTERGROUP { get; set; }
        public string TRANSPORTERGROUPNAME { get; set; }
        public int INPUTBY { get; set; }
        public System.DateTime INPUTDATE { get; set; }
        public int UPDATEBY { get; set; }
        public System.DateTime UPDATEDATE { get; set; }
    }

    public class JoinModelControlJob
    {
        public int SID { get; set; }
        public string ITEMCODE { get; set; }
        public string ITEMDESCRIPTION { get; set; }
        public string CONTROLSYSTEM { get; set; }
        public string JOB { get; set; }
        public int ISACTIVE { get; set; }
        //public int COMPANYSITE { get; set; }
        public int INPUTBY { get; set; }
        public System.DateTime INPUTDATE { get; set; }
        public int UPDATEBY { get; set; }
        public System.DateTime UPDATEDATE { get; set; }
    }

    public class JoinModelFixedAssetGroup
    {
        public int SID { get; set; }
        public string IDFAGROUP { get; set; }
        public string FAGROUPNAME { get; set; }
        public Nullable<int> COSTCENTER { get; set; }
        public int INPUTBY { get; set; }
        public System.DateTime INPUTDATE { get; set; }
        public int UPDATEBY { get; set; }
        public System.DateTime UPDATEDATE { get; set; }
    }

    public class JoinModelCostCenter
    {
        public int SID { get; set; }
        public string IDCOSTCENTER { get; set; }
        public string COSTCENTERNAME { get; set; }
        public string PARENTCOSTCENTER { get; set; }
        public string COSTCENTERTYPE { get; set; }
        public string ALIASNAME { get; set; }
        public string ALLOCATIONTYPE { get; set; }
        public string BUDGETTYPE { get; set; }
        public string TRANSACTIONS { get; set; }
        public string REMARKS { get; set; }
        public int ISACTIVE { get; set; }
        public Nullable<System.DateTime> ISACTIVEDATE { get; set; }
        public int COMPANYSITE { get; set; }
        public int INPUTBY { get; set; }
        public System.DateTime INPUTDATE { get; set; }
        public int UPDATEBY { get; set; }
        public System.DateTime UPDATEDATE { get; set; }
    }

    public class JoinModelStockGroup
    {
        public int SID { get; set; }
        public string IDSTOCKGROUP { get; set; }
        public string STOCKGROUPNAME { get; set; }
        public Nullable<int> CONTROLJOB { get; set; }
        public int INPUTBY { get; set; }
        public System.DateTime INPUTDATE { get; set; }
        public int UPDATEBY { get; set; }
        public System.DateTime UPDATEDATE { get; set; }
    }

    public class JoinModelSupplierGroup
    {
        public int SID { get; set; }
        public string IDSUPPLIERGROUP { get; set; }
        public string SUPPLIERGROUPNAME { get; set; }
        public int INPUTBY { get; set; }
        public System.DateTime INPUTDATE { get; set; }
        public int UPDATEBY { get; set; }
        public System.DateTime UPDATEDATE { get; set; }
    }

    public class JoinModelContractorGroup
    {
        public int SID { get; set; }
        public string IDCONTRACTORGROUP { get; set; }
        public string CONTRACTORGROUPNAME { get; set; }
        public int INPUTBY { get; set; }
        public System.DateTime INPUTDATE { get; set; }
        public int UPDATEBY { get; set; }
        public System.DateTime UPDATEDATE { get; set; }
    }

    public class JoinModelCustomerGroup
    {
        public int SID { get; set; }
        public string IDCUSTOMERGROUP { get; set; }
        public string CUSTOMERGROUPNAME { get; set; }
        public int INPUTBY { get; set; }
        public System.DateTime INPUTDATE { get; set; }
        public int UPDATEBY { get; set; }
        public System.DateTime UPDATEDATE { get; set; }
    }

    public class JoinModelFixedAsset
    {
        public int SID { get; set; }
        public string IDFIXEDASSET { get; set; }
        public string FIXEDASSETNAME { get; set; }
        public string DESCRIPTION { get; set; }
        public int FIXEDASSETGROUP { get; set; }
        public int UOM { get; set; }
        public int ISACTIVE { get; set; }
        public string COMPANYSITE { get; set; }
        public Nullable<int> INPUTBY { get; set; }
        public Nullable<System.DateTime> INPUTDATE { get; set; }
        public Nullable<int> UPDATEBY { get; set; }
        public Nullable<System.DateTime> UPDATEDATE { get; set; }
    }

    public class JoinModelStore
    {
        public int SID { get; set; }
        public string IDSTORE { get; set; }
        public string STORENAME { get; set; }
        public Nullable<int> WAREHOUSETYPE { get; set; }
        public string DESCRIPTION { get; set; }
        public int ISACTIVE { get; set; }
        public int COMPANYSITE { get; set; }
        public int INPUTBY { get; set; }
        public System.DateTime INPUTDATE { get; set; }
        public int UPDATEBY { get; set; }
        public System.DateTime UPDATEDATE { get; set; }
    }

    public class JoinModelStock
    {
        public int SID { get; set; }
        public string IDSTOCK { get; set; }
        public string STOCKNAME { get; set; }
        public int STOCKGROUP { get; set; }
        public string PARTNUMBER { get; set; }
        public string DESCRIPTION { get; set; }
        public int UOM { get; set; }
        public string COMPANYSITE { get; set; }
        public Nullable<int> INPUTBY { get; set; }
        public Nullable<System.DateTime> INPUTDATE { get; set; }
        public Nullable<int> UPDATEBY { get; set; }
        public Nullable<System.DateTime> UPDATEDATE { get; set; }
    }

    public class JoinModelFixedAssetLocation
    {
        public int SID { get; set; }
        public string IDFIXEDASSETLOCATION { get; set; }
        public string FIXEDASSETLOCATIONNAME { get; set; }
        public string DESCRIPTION { get; set; }
        public int COSTCENTER { get; set; }
        public int ISACTIVE { get; set; }
        public int COMPANYSITE { get; set; }
        public Nullable<int> INPUTBY { get; set; }
        public Nullable<System.DateTime> INPUTDATE { get; set; }
        public Nullable<int> UPDATEBY { get; set; }
        public Nullable<System.DateTime> UPDATEDATE { get; set; }
    }

    public class JoinModelWorkshop
    {
        public int SID { get; set; }
        public string IDWORKSHOP { get; set; }
        public string WORKSHOPNAME { get; set; }
        public int WORKSHOPGROUP { get; set; }
        public int ISACTIVE { get; set; }
        public int COMPANYSITE { get; set; }
        public int INPUTBY { get; set; }
        public System.DateTime INPUTDATE { get; set; }
        public int UPDATEBY { get; set; }
        public System.DateTime UPDATEDATE { get; set; }
    }

    public class JoinModelWorkshopGroup
    {
        public int SID { get; set; }
        public string IDWORKSHOPGROUP { get; set; }
        public string WORKSHOPGROUPNAME { get; set; }
        public int INPUTBY { get; set; }
        public System.DateTime INPUTDATE { get; set; }
        public int UPDATEBY { get; set; }
        public System.DateTime UPDATEDATE { get; set; }
    }

    public class JoinModelBank
    {
        public int SID { get; set; }
        public string IDBANK { get; set; }
        public string BANKNAME { get; set; }
        public string BANKACCOUNTCODE { get; set; }
        public string ADDRESS { get; set; }
        public int COUNTRY { get; set; }
        public int PROVINCE { get; set; }
        public int CITY { get; set; }
        public string PHONE { get; set; }
        public string FAX { get; set; }
        public int CURRENCY { get; set; }
        public int ISACTIVE { get; set; }
        public System.DateTime ISACTIVEDATE { get; set; }
        public string POSCODE { get; set; }
        public int COMPANY { get; set; }
        public int COMPANYSITE { get; set; }
        public int INPUTBY { get; set; }
        public System.DateTime INPUTDATE { get; set; }
        public int UPDATEBY { get; set; }
        public System.DateTime UPDATEDATE { get; set; }
    }

    public class JoinModelBlockMaster
    {
        public int SID { get; set; }
        public string IDBLOCKMASTER { get; set; }
        public string BLOCKMASTERNAME { get; set; }
        public int LANDCONCESSION { get; set; }
        public int BLOCKORGANIZATION { get; set; }
        public string SOILTYPE { get; set; }
        public string VEGETATION { get; set; }
        public string TOPOGRAPH { get; set; }
        public string HECTARAGE { get; set; }
        public string PLANTABLE { get; set; }
        public int COMPANYSITE { get; set; }
        public int INPUTBY { get; set; }
        public System.DateTime INPUTDATE { get; set; }
        public int UPDATEBY { get; set; }
        public System.DateTime UPDATEDATE { get; set; }
    }

    public class JoinModelBlockUsage
    {
        public int SID { get; set; }
        public int BLOCKMASTER { get; set; }
        public int USAGE { get; set; }
        public int HECTARAGE { get; set; }
        public int COMPANYSITE { get; set; }
        public Nullable<int> INPUTBY { get; set; }
        public Nullable<System.DateTime> INPUTDATE { get; set; }
        public Nullable<int> UPDATEBY { get; set; }
        public Nullable<System.DateTime> UPDATEDATE { get; set; }
    }

    public class JoinModelEmployee
    {
        public int SID { get; set; }
        public string IDEMPLOYEE { get; set; }
        public string EMPLOYEENAME { get; set; }
        public string OTHERNAME { get; set; }
        public string IDCARD { get; set; }
        public string IDFINGERPRINT { get; set; }
        public string BIRTHPLACE { get; set; }
        public System.DateTime DOB { get; set; }
        public int SEX { get; set; }
        public int RACE { get; set; }
        public string ADDRESS { get; set; }
        public string HOMEPHONE { get; set; }
        public string MOBILEPHONE { get; set; }
        public string EMAIL { get; set; }
        public int RELIGION { get; set; }
        public int NATIONALITY { get; set; }
        public string ZIPCODE { get; set; }
        public int EDUCATIONLEVEL { get; set; }
        public int INSTITUTION { get; set; }
        public int PROGRAMOFSTUDY { get; set; }
        public int MARITALSTATUS { get; set; }
        public string SPOUSENAME { get; set; }
        public int NUMBEROFCHILD { get; set; }
        public int FAMILYRICESTATUS { get; set; }
        public int NATURETYPE { get; set; }
        public System.DateTime DATEJOIN { get; set; }
        public int DEPARTMENT { get; set; }
        public int GRADE { get; set; }
        public int EMPLOYEETYPE { get; set; }
        public int EMPLOYEESECTION { get; set; }
        public int EMPLOYEEPOSITION { get; set; }
        public int BASICSALARY { get; set; }
        public int PAYMENTMETHOD { get; set; }
        public int BANK { get; set; }
        public string OWNERSHIP { get; set; }
        public string BANKACCOUNT { get; set; }
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
    }

    public partial class JoinModelAllowanceDeduction
    {
        public decimal SID { get; set; }
        public string IDALLOWANCEDEDUCTION { get; set; }
        public string ALLOWANCEDEDUCTIONNAME { get; set; }
        public int ALLOWANCEDEDUCTIONTYPE { get; set; }
        public decimal INPUTBY { get; set; }
        public System.DateTime INPUTDATE { get; set; }
        public decimal UPDATEBY { get; set; }
        public System.DateTime UPDATEDATE { get; set; }
    }

    public class JoinModelRainfall
    {
        public int SID { get; set; }
        public System.DateTime RAINDATE { get; set; }
        public int BLOCKORGANIZATION { get; set; }
        public System.DateTime RAINSTART { get; set; }
        public System.DateTime RAINEND { get; set; }
        public int RAINQUANTITY { get; set; }
        public string REMARK { get; set; }
        public int COMPANYSITE { get; set; }
        public Nullable<int> INPUTBY { get; set; }
        public Nullable<System.DateTime> INPUTDATE { get; set; }
        public Nullable<int> UPDATEBY { get; set; }
        public Nullable<System.DateTime> UPDATEDATE { get; set; }
    }

    public partial class JoinModelBlockOrganization
    {
        public decimal SID { get; set; }
        public string IDBLOCKORGANIZATION { get; set; }
        public string BLOCKORGANIZATIONNAME { get; set; }
        public int COMPANYSITE { get; set; }
        public decimal INPUTBY { get; set; }
        public DateTime INPUTDATE { get; set; }
        public decimal UPDATEBY { get; set; }
        public DateTime UPDATEDATE { get; set; }
    }

    public class JoinModelUnitOfMeasure
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

    public class ViewModelPurchaseDetail
    {
        public int SID { get; set; }
        public string IDPURCHASEREQUEST { get; set; }
        public int ITEMCODE { get; set; }
        public string IDSTOCK { get; set; }
        public string STOCKNAME { get; set; }
        public int QUANTITY { get; set; }
        public int UOM { get; set; }
        public string IDUOM { get; set; }
        public string UOMNAME { get; set; }
        public DateTime EXPECTDATE { get; set; }
        public int MANAGEBY { get; set; }
        public string IDMANAGEBY { get; set; }
        public string MANAGEBYNAME { get; set; }
        public int ESTIMATEPRICE { get; set; }
        public string REMARK { get; set; }
        public int APPROVEQUANTITY { get; set; }
        public int COMPANYSITE { get; set; }
        public int INPUTBY { get; set; }
        public DateTime INPUTDATE { get; set; }
        public int UPDATEBY { get; set; }
        public DateTime UPDATEDATE { get; set; }
    }
}