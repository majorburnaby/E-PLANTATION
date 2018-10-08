using Plantation.Models.DB;
using Plantation.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Dapper;

namespace Plantation.Models
{
    public class ComboBoxContext
    {
        private IDbConnection _db = new SqlConnection(Constant.DatabaseConnection);

        public List<City> GetCity()
        {
            string _query = "SELECT SID,(IDCITY+' - '+CITYNAME) as CITYNAME FROM CITY";
            var result = this._db.Query<City>(_query).ToList();
            return result;
        }
        public List<Province> GetProvince()
        {
            string _query = "SELECT SID, (IDPROVINCE+' - '+PROVINCENAME) as PROVINCENAME FROM PROVINCE";
            var result = this._db.Query<Province>(_query).ToList();
            return result;
        }

        public List<Province> GetProvinceByCountry(int Country)
        {
            string _query = "SELECT SID, (IDPROVINCE+' - '+PROVINCENAME) as PROVINCENAME FROM PROVINCE WHERE COUNTRY=" + Country;
            var result = this._db.Query<Province>(_query).ToList();
            return result;
        }

        public List<City> GetCityByProvince(int Province)
        {
            string _query = "SELECT SID,(IDCITY+' - '+CITYNAME) as CITYNAME FROM CITY WHERE PROVINCE=" + Province;
            var result = this._db.Query<City>(_query).ToList();
            return result;
        }

        public List<Country> GetCountry()
        {
            string _query = "SELECT SID,(IDCOUNTRY+' - '+COUNTRYNAME) as COUNTRYNAME FROM COUNTRY";
            var result = this._db.Query<Country>(_query).ToList();
            return result;
        }
        public List<LoadType> GetLoadType()
        {
            string _query = "SELECT SID, LOADTYPENAME FROM LOADTYPE";
            var result = this._db.Query<LoadType>(_query).ToList();
            return result;
        }

        public List<UnitOfMeasure> GetUnitOfMeasure()
        {
            string _query = "SELECT SID,(IDUOM+' - '+UOMNAME) as UOMNAME FROM UNITOFMEASURE";
            var result = this._db.Query<UnitOfMeasure>(_query).ToList();
            return result;
        }
        public List<Crop> GetCrop()
        {
            string _query = "SELECT SID,(IDCROP+' - '+CROPNAME) as CROPNAME FROM CROP";
            var result = this._db.Query<Crop>(_query).ToList();
            return result;
        }
        public List<Parameter> GetParameter()
        {
            string _query = "SELECT SID, PARAMETERNAME FROM PARAMETER";
            var result = this._db.Query<Parameter>(_query).ToList();
            return result;
        }

        public List<CurrencyMaster> GetCurrencyMaster()
        {
            string _query = "SELECT SID, CURRENCYNAME FROM CURRENCYMASTER";
            var result = this._db.Query<CurrencyMaster>(_query).ToList();
            return result;
        }

        public List<ControlJob> GetControlJob()
        {
            string _query = "SELECT SID, ITEMDESCRIPTION FROM CONTROLJOB";
            var result = this._db.Query<ControlJob>(_query).ToList();
            return result;
        }

        public List<StockGroup> GetStockGroup()
        {
            string _query = "SELECT SID,(IDSTOCKGROUP+' - '+STOCKGROUPNAME) as STOCKGROUPNAME FROM STOCKGROUP";
            var result = this._db.Query<StockGroup>(_query).ToList();
            return result;
        }

        public List<Stock> GetStockByCompanySite(int CompanySite)
        {
            string _query = "SELECT SID,(IDSTOCK+' - '+STOCKNAME) as STOCKNAME FROM STOCK ";//WHERE COMPANYSITE = " + CompanySite;
            var result = this._db.Query<Stock>(_query).ToList();
            return result;
        }

        public List<JobGroup> GetJobGroup()
        {
            string _query = "SELECT SID, JOBGROUPNAME FROM JOBGROUP";
            var result = this._db.Query<JobGroup>(_query).ToList();
            return result;
        }

        public List<ParameterValue> GetUnusedParameterValueBlockUsage(int BlockMaster)
        {
            string _query = "SELECT SID, PARAMETERVALUENAME FROM PARAMETERVALUE WHERE PARAMETER='17' AND SID NOT IN (SELECT DISTINCT USAGE FROM BLOCKUSAGE WHERE BLOCKMASTER = " + BlockMaster + ")";
            var result = this._db.Query<ParameterValue>(_query).ToList();
            return result;
        }

        public List<JobType> GetJobType()
        {
            string _query = "SELECT SID, JOBTYPENAME FROM JOBTYPE";
            var result = this._db.Query<JobType>(_query).ToList();
            return result;
        }

        public List<Job> GetJob()
        {
            string _query = "SELECT SID, JOBNAME FROM JOB";
            var result = this._db.Query<Job>(_query).ToList();
            return result;
        }

        public List<SupplierGroup> GetSupplierGroup()
        {
            string _query = "SELECT SID, SUPPLIERGROUPNAME FROM SUPPLIERGROUP";
            var result = this._db.Query<SupplierGroup>(_query).ToList();
            return result;
        }

        public List<Transporter> GetSupplier()
        {
            string _query = "SELECT SID, SUPPLIERNAME FROM SUPPLIER";
            var result = this._db.Query<Transporter>(_query).ToList();
            return result;
        }

        public List<ParameterValue> GetParameterValueBK()
        {
            string _query = "SELECT SID, PARAMETERVALUENAME FROM PARAMETERVALUE WHERE PARAMETER='36'";
            var result = this._db.Query<ParameterValue>(_query).ToList();
            return result;
        }

        public List<ParameterValue> GetPlantingMaterial()
        {
            string _query = "SELECT SID, PARAMETERVALUENAME FROM PARAMETERVALUE WHERE PARAMETER='33'";
            var result = this._db.Query<ParameterValue>(_query).ToList();
            return result;
        }

        public List<ParameterValue> GetItemType()
        {
            string _query = "SELECT SID, PARAMETERVALUENAME FROM PARAMETERVALUE WHERE PARAMETER='49'";
            var result = this._db.Query<ParameterValue>(_query).ToList();
            return result;
        }

        public List<ParameterValue> GetEmployeePosition()
        {
            string _query = "SELECT SID, PARAMETERVALUENAME FROM PARAMETERVALUE WHERE PARAMETER='47'";
            var result = this._db.Query<ParameterValue>(_query).ToList();
            return result;
        }

        public List<ParameterValue> GetPlantingDistance()
        {
            string _query = "SELECT SID, PARAMETERVALUENAME FROM PARAMETERVALUE WHERE PARAMETER='20'";
            var result = this._db.Query<ParameterValue>(_query).ToList();
            return result;
        }

        public List<ParameterValue> GetFieldStatus()
        {
            string _query = "SELECT SID, PARAMETERVALUENAME FROM PARAMETERVALUE WHERE PARAMETER='13'";
            var result = this._db.Query<ParameterValue>(_query).ToList();
            return result;
        }

        public List<ParameterValue> GetPermissionType()
        {
            string _query = "SELECT SID, PARAMETERVALUENAME FROM PARAMETERVALUE WHERE PARAMETER='6'";
            var result = this._db.Query<ParameterValue>(_query).ToList();
            return result;
        }

        public List<ParameterValue> GetParameterValueSTR()
        {
            string _query = "SELECT SID, PARAMETERVALUENAME FROM PARAMETERVALUE WHERE PARAMETER='37'";
            var result = this._db.Query<ParameterValue>(_query).ToList();
            return result;
        }

        public List<ParameterValue> GetParameterValueManageBy()
        {
            string _query = "SELECT SID, PARAMETERVALUENAME FROM PARAMETERVALUE WHERE PARAMETER='37' AND IDPARAMETERVALUE <='03'";
            var result = this._db.Query<ParameterValue>(_query).ToList();
            return result;
        }

        public List<ParameterValue> GetProgeny()
        {
            string _query = "SELECT SID, PARAMETERVALUENAME FROM PARAMETERVALUE WHERE PARAMETER='34'";
            var result = this._db.Query<ParameterValue>(_query).ToList();
            return result;
        }

        public List<ParameterValue> GetParameterValueVegetation()
        {
            string _query = "SELECT SID, PARAMETERVALUENAME FROM PARAMETERVALUE WHERE PARAMETER='16'";
            var result = this._db.Query<ParameterValue>(_query).ToList();
            return result;
        }

        public List<ContractorGroup> GetContractorGroup()
        {
            string _query = "SELECT SID, CONTRACTORGROUPNAME FROM CONTRACTORGROUP";
            var result = this._db.Query<ContractorGroup>(_query).ToList();
            return result;
        }

        public List<Contractor> GetContractor()
        {
            string _query = "SELECT SID, CONTRACTORNAME FROM CONTRACTOR";
            var result = this._db.Query<Contractor>(_query).ToList();
            return result;
        }

        public List<CustomerGroup> GetCustomerGroup()
        {
            string _query = "SELECT SID, CUSTOMERGROUPNAME FROM CUSTOMERGROUP";
            var result = this._db.Query<CustomerGroup>(_query).ToList();
            return result;
        }

        public List<Customer> GetCustomer()
        {
            string _query = "SELECT SID, CUSTOMERNAME FROM CUSTOMER";
            var result = this._db.Query<Customer>(_query).ToList();
            return result;
        }

        public List<FixedAssetGroup> GetFixedAssetGroup()
        {
            string _query = "SELECT SID, FAGROUPNAME FROM FIXEDASSETGROUP";
            var result = this._db.Query<FixedAssetGroup>(_query).ToList();
            return result;
        }

        public List<FixedAsset> GetFixedAsset()
        {
            string _query = "SELECT SID, FIXEDASSETNAME  FROM FIXEDASSET";
            var result = this._db.Query<FixedAsset>(_query).ToList();
            return result;
        }

        public List<CostCenter> GetCostCenter()
        {
            string _query = "SELECT SID, COSTCENTERNAME FROM COSTCENTER";
            var result = this._db.Query<CostCenter>(_query).ToList();
            return result;
        }

        public List<TransporterGroup> GetTransporterGroup()
        {
            string _query = "SELECT SID, TRANSPORTERGROUPNAME FROM TRANSPORTERGROUP";
            var result = this._db.Query<TransporterGroup>(_query).ToList();
            return result;
        }

        public List<WorkshopGroup> GetWorkshopGroup()
        {
            string _query = "SELECT SID, WORKSHOPGROUPNAME FROM WORKSHOPGROUP";
            var result = this._db.Query<WorkshopGroup>(_query).ToList();
            return result;
        }

        public List<ParameterValue> GetOwnership()
        {
            string _query = "SELECT SID, PARAMETERVALUENAME  FROM PARAMETERVALUE WHERE PARAMETER='38'";
            var result = this._db.Query<ParameterValue>(_query).ToList();
            return result;
        }

        public List<ParameterValue> GetOwnership2()
        {
            string _query = "SELECT SID, PARAMETERVALUENAME  FROM PARAMETERVALUE WHERE PARAMETER='40'";
            var result = this._db.Query<ParameterValue>(_query).ToList();
            return result;
        }

        public List<ParameterValue> GetSex()
        {
            string _query = "SELECT SID, PARAMETERVALUENAME  FROM PARAMETERVALUE WHERE PARAMETER='41'";
            var result = this._db.Query<ParameterValue>(_query).ToList();
            return result;
        }

        public List<ParameterValue> GetEducationLevel()
        {
            string _query = "SELECT SID, PARAMETERVALUENAME  FROM PARAMETERVALUE WHERE PARAMETER='26'";
            var result = this._db.Query<ParameterValue>(_query).ToList();
            return result;
        }

        public List<ParameterValue> GetInstitution()
        {
            string _query = "SELECT SID, PARAMETERVALUENAME  FROM PARAMETERVALUE WHERE PARAMETER='28'";
            var result = this._db.Query<ParameterValue>(_query).ToList();
            return result;
        }

        public List<ParameterValue> GetProgramOfStudy()
        {
            string _query = "SELECT SID, PARAMETERVALUENAME  FROM PARAMETERVALUE WHERE PARAMETER='27'";
            var result = this._db.Query<ParameterValue>(_query).ToList();
            return result;
        }

        public List<ParameterValue> GetMaritalStatus()
        {
            string _query = "SELECT SID, PARAMETERVALUENAME  FROM PARAMETERVALUE WHERE PARAMETER='42'";
            var result = this._db.Query<ParameterValue>(_query).ToList();
            return result;
        }

        public List<ParameterValue> GetFamilyRiceStatus()
        {
            string _query = "SELECT SID, PARAMETERVALUENAME  FROM PARAMETERVALUE WHERE PARAMETER='2'";
            var result = this._db.Query<ParameterValue>(_query).ToList();
            return result;
        }

        public List<ParameterValue> GetFamilyTaxStatus()
        {
            string _query = "SELECT SID, PARAMETERVALUENAME  FROM PARAMETERVALUE WHERE PARAMETER='2'";
            var result = this._db.Query<ParameterValue>(_query).ToList();
            return result;
        }

        public List<ParameterValue> GetRace()
        {
            string _query = "SELECT SID, PARAMETERVALUENAME  FROM PARAMETERVALUE WHERE PARAMETER='22'";
            var result = this._db.Query<ParameterValue>(_query).ToList();
            return result;
        }

        public List<ParameterValue> GetNatureType()
        {
            string _query = "SELECT SID, PARAMETERVALUENAME  FROM PARAMETERVALUE WHERE PARAMETER='43'";
            var result = this._db.Query<ParameterValue>(_query).ToList();
            return result;
        }

        public List<ParameterValue> GetGrade()
        {
            string _query = "SELECT SID, PARAMETERVALUENAME  FROM PARAMETERVALUE WHERE PARAMETER='44'";
            var result = this._db.Query<ParameterValue>(_query).ToList();
            return result;
        }

        public List<ParameterValue> GetEmployeeType()
        {
            string _query = "SELECT SID, PARAMETERVALUENAME  FROM PARAMETERVALUE WHERE PARAMETER='1'";
            var result = this._db.Query<ParameterValue>(_query).ToList();
            return result;
        }

        public List<ParameterValue> GetEmployeeSection()
        {
            string _query = "SELECT SID, PARAMETERVALUENAME  FROM PARAMETERVALUE WHERE PARAMETER='45'";
            var result = this._db.Query<ParameterValue>(_query).ToList();
            return result;
        }

        public List<ParameterValue> GetPaymentMethod()
        {
            string _query = "SELECT SID, PARAMETERVALUENAME  FROM PARAMETERVALUE WHERE PARAMETER='46'";
            var result = this._db.Query<ParameterValue>(_query).ToList();
            return result;
        }

        public List<ParameterValue> GetReligion()
        {
            string _query = "SELECT SID, PARAMETERVALUENAME  FROM PARAMETERVALUE WHERE PARAMETER='23'";
            var result = this._db.Query<ParameterValue>(_query).ToList();
            return result;
        }

        public List<VehicleGroup> GetVehicleGroup()
        {
            string _query = "SELECT SID, VEHICLEGROUPNAME FROM VEHICLEGROUP";
            var result = this._db.Query<VehicleGroup>(_query).ToList();
            return result;
        }

        public List<Station> GetStation()
        {
            string _query = "SELECT SID, STATIONNAME FROM STATION WHERE ISACTIVE='1'";
            var result = this._db.Query<Station>(_query).ToList();
            return result;
        }

        public List<LandConcession> GetLandConcession()
        {
            string _query = "SELECT SID, CONCESSIONNAME FROM LANDCONCESSION";
            var result = this._db.Query<LandConcession>(_query).ToList();
            return result;
        }

        public List<LandConcession> GetLandConcessionByCompanySite(int CompanySite)
        {
            string _query = "SELECT SID, CONCESSIONNAME FROM LANDCONCESSION WHERE COMPANYSITE = " + CompanySite;
            var result = this._db.Query<LandConcession>(_query).ToList();
            return result;
        }

        public List<BlockOrganization> GetBlockOrganization()
        {
            string _query = "SELECT SID, BLOCKORGANIZATIONNAME FROM BLOCKORGANIZATION";
            var result = this._db.Query<BlockOrganization>(_query).ToList();
            return result;
        }

        public List<BlockOrganization> GetBlockOrganizationByCompanySite(int CompanySite)
        {
            string _query = "SELECT SID, BLOCKORGANIZATIONNAME FROM BLOCKORGANIZATION WHERE COMPANYSITE = " + CompanySite;
            var result = this._db.Query<BlockOrganization>(_query).ToList();
            return result;
        }

        public List<ParameterValue> GetParameterValueBlockUsage()
        {
            string _query = "SELECT SID, PARAMETERVALUENAME FROM PARAMETERVALUE WHERE PARAMETER='17'";
            var result = this._db.Query<ParameterValue>(_query).ToList();
            return result;
        }

        public List<BlockMaster> GetBlockMaster()
        {
            string _query = "SELECT SID,(IDBLOCKMASTER+' - '+BLOCKMASTERNAME) as BLOCKMASTERNAME FROM BLOCKMASTER";
            var result = this._db.Query<BlockMaster>(_query).ToList();
            return result;
        }

        public List<BlockMaster> GetBlockMasterByCompanySite(int CompanySite)
        {
            string _query = "SELECT SID,(IDBLOCKMASTER+' - '+BLOCKMASTERNAME) as BLOCKMASTERNAME FROM BLOCKMASTER WHERE COMPANYSITE = " + CompanySite;
            var result = this._db.Query<BlockMaster>(_query).ToList();
            return result;
        }

        public List<Region> GetRegion()
        {
            string _query = "SELECT SID, REGIONNAME FROM REGION";
            var result = this._db.Query<Region>(_query).ToList();
            return result;
        }

        public List<Location> GetLocation()
        {
            string _query = "SELECT SID, LOCATIONNAME FROM LOCATION";
            var result = this._db.Query<Location>(_query).ToList();
            return result;
        }

        public List<Department> GetDepartment()
        {
            string _query = "SELECT SID, DEPARTMENTNAME FROM DEPARTMENT";
            var result = this._db.Query<Department>(_query).ToList();
            return result;
        }

        public List<ParameterValue> GetParameterValueSoilType()
        {
            string _query = "SELECT SID, PARAMETERVALUENAME FROM PARAMETERVALUE WHERE PARAMETER='14'";
            var result = this._db.Query<ParameterValue>(_query).ToList();
            return result;
        }

        public List<ParameterValue> GetParameterValueSoilCategory()
        {
            string _query = "SELECT SID, PARAMETERVALUENAME FROM PARAMETERVALUE WHERE PARAMETER='17'";
            var result = this._db.Query<ParameterValue>(_query).ToList();
            return result;
        }

        public List<ParameterValue> GetParameterValueTopograph()
        {
            string _query = "SELECT SID, PARAMETERVALUENAME FROM PARAMETERVALUE WHERE PARAMETER='15'";
            var result = this._db.Query<ParameterValue>(_query).ToList();
            return result;
        }

        public List<ParameterValue> GetParameterValueGangType()
        {
            string _query = "SELECT SID, PARAMETERVALUENAME FROM PARAMETERVALUE WHERE PARAMETER='4'";
            var result = this._db.Query<ParameterValue>(_query).ToList();
            return result;
        }

        public List<Employee> GetForeman1(int CompanySite)
        {
            string _query = "SELECT SID, EMPLOYEENAME FROM EMPLOYEE WHERE COMPANYSITE = " + CompanySite;
            var result = this._db.Query<Employee>(_query).ToList();
            return result;
        }

        public List<Employee> GetForeman(int CompanySite)
        {
            string _query = "SELECT SID, EMPLOYEENAME FROM EMPLOYEE WHERE COMPANYSITE = " + CompanySite;
            var result = this._db.Query<Employee>(_query).ToList();
            return result;
        }

        public List<Employee> GetAdmin(int CompanySite)
        {
            string _query = "SELECT SID, EMPLOYEENAME FROM EMPLOYEE WHERE COMPANYSITE = " + CompanySite;
            var result = this._db.Query<Employee>(_query).ToList();
            return result;
        }

        public List<Employee> GetEmployee(int CompanySite)
        {
            string _query = "SELECT SID, EMPLOYEENAME FROM EMPLOYEE WHERE COMPANYSITE = " + CompanySite;
            var result = this._db.Query<Employee>(_query).ToList();
            return result;
        }

        public List<ParameterValue> GetAllowanceDeduction()
        {
            string _query = "SELECT SID, PARAMETERVALUENAME FROM PARAMETERVALUE WHERE PARAMETER='48'";
            var result = this._db.Query<ParameterValue>(_query).ToList();
            return result;
        }

        public List<ParameterValue> GetPriority()
        {
            string _query = "SELECT SID, PARAMETERVALUENAME FROM PARAMETERVALUE WHERE PARAMETER='50'";
            var result = this._db.Query<ParameterValue>(_query).ToList();
            return result;
        }

        public List<AccountingPeriod> GetAccountingYear()
        {
            string _query = "SELECT ACCOUNTINGYEAR FROM ACCOUNTINGPERIOD";
            var result = this._db.Query<AccountingPeriod>(_query).ToList();
            return result;
        }

        public List<Store> GetRequestorID(int CompanySite)
        {
            string _query = "SELECT SID, STORENAME FROM STORE WHERE COMPANYSITE = " + CompanySite;
            var result = this._db.Query<Store>(_query).ToList();
            return result;
        }
    }
}