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
            string _query = "SELECT SID, CITYNAME FROM CITY";
            var result = this._db.Query<City>(_query).ToList();
            return result;
        }
        public List<Province> GetProvince()
        {
            string _query = "SELECT SID, PROVINCENAME FROM PROVINCE";
            var result = this._db.Query<Province>(_query).ToList();
            return result;
        }

        public List<Province> GetProvinceByCountry(int Country)
        {
            string _query = "SELECT SID, PROVINCENAME FROM PROVINCE WHERE COUNTRY=" + Country;
            var result = this._db.Query<Province>(_query).ToList();
            return result;
        }

        public List<Country> GetCountry()
        {
            string _query = "SELECT SID, COUNTRYNAME FROM COUNTRY";
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
            string _query = "SELECT SID, UOMNAME FROM UNITOFMEASURE";
            var result = this._db.Query<UnitOfMeasure>(_query).ToList();
            return result;
        }
        public List<Crop> GetCrop()
        {
            string _query = "SELECT SID, CROPNAME FROM CROP";
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
            string _query = "SELECT SID, STOCKGROUPNAME FROM STOCKGROUP";
            var result = this._db.Query<StockGroup>(_query).ToList();
            return result;
        }

        public List<JobGroup> GetJobGroup()
        {
            string _query = "SELECT SID, JOBGROUPNAME FROM JOBGROUP";
            var result = this._db.Query<JobGroup>(_query).ToList();
            return result;
        }

        public List<JobType> GetJobType()
        {
            string _query = "SELECT SID, JOBTYPENAME FROM JOBTYPE";
            var result = this._db.Query<JobType>(_query).ToList();
            return result;
        }

        public List<SupplierGroup> GetSupplierGroup()
        {
            string _query = "SELECT SID, SUPPLIERGROUPNAME FROM SUPPLIERGROUP";
            var result = this._db.Query<SupplierGroup>(_query).ToList();
            return result;
        }

        public List<Supplier> GetSupplier()
        {
            string _query = "SELECT SID, SUPPLIERNAME FROM SUPPLIER";
            var result = this._db.Query<Supplier>(_query).ToList();
            return result;
        }

        public List<ParameterValue> GetParameterValueBK()
        {
            string _query = "SELECT SID, PARAMETERVALUENAME FROM PARAMETERVALUE WHERE PARAMETER='36'";
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
    }
}