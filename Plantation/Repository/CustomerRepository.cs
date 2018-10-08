using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dapper;
using Plantation.Models.DB;
using System.Data;
using System.Data.SqlClient;
using Plantation.Utility;
using Plantation.Repository.Interface;

namespace Plantation.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private IDbConnection _db = new SqlConnection(Constant.DatabaseConnection);

        public Customer Add(Customer CTM, string userid)
        {
            var sqlQuery = @"INSERT INTO Customer (IDCUSTOMER, CUSTOMERNAME, CUSTOMERGROUP, CONTROLJOB, CONTACTNAME, BANK, BANKACCOUNT, PHONE, FAX, EMAIL, ADDRESS, COUNTRY, PROVINCE, CITY, POSCODE, REMARKS, ISACTIVE, ISACTIVEDATE, COMPANY, COMPANYSITE, INPUTBY, INPUTDATE, UPDATEBY, UPDATEDATE) VALUES
                           ('" + CTM.IDCUSTOMER + @"', '" + CTM.CUSTOMERNAME + "', '" + CTM.CUSTOMERGROUP + @"','" + CTM.CONTROLJOB + @"', '" + CTM.CONTACTNAME + @"', '" + CTM.BANK + @"', '" + CTM.BANKACCOUNT + @"', '" + CTM.PHONE + @"', '" + CTM.FAX + @"', '" + CTM.EMAIL + @"', '" + CTM.ADDRESS + @"', '" + CTM.COUNTRY + @"', '" + CTM.PROVINCE + @"', '" + CTM.CITY + @"', '" + CTM.POSCODE + @"', '" + CTM.REMARKS + @"', '" + CTM.ISACTIVE + @"', '" + CTM.ISACTIVEDATE + @"', '" + CTM.COMPANY + @"', '" + CTM.COMPANYSITE + @"'," + Convert.ToInt32(userid) + ", '" + DateTime.Now + @"', " + Convert.ToInt32(userid) + ", '" + DateTime.Now + @"'); " + "SELECT CAST(SCOPE_IDENTITY() as int)";
            var SID = _db.Query<int>(sqlQuery, CTM).Single();
            CTM.SID = SID;
            return CTM;
        }

        public Customer Find(int? SID)
        {
            return this._db.Query<Customer>("SELECT * FROM CUSTOMER WHERE SID = @SID", new { SID }).SingleOrDefault();
        }

        public List<Customer> GetAll()
        {
            return this._db.Query<Customer>("SELECT" +
                               " CTM.SID," +
                               " CTM.IDCUSTOMER," +
                               " CTM.CUSTOMERNAME," +
                               " CTM.CUSTOMERGROUP," +
                               " CTM.CONTROLJOB," +
                               " CTM.CONTACTNAME," +
                               " CTM.BANK," +
                               " CTM.BANKACCOUNT," +
                               " CTM.PHONE," +
                               " CTM.FAX," +
                               " CTM.EMAIL," +
                               " CTM.ADDRESS," +
                               " CTM.COUNTRY," +
                               " CTM.PROVINCE," +
                               " CTM.CITY," +
                               " CTM.POSCODE," +                               
                               " CTM.REMARKS," +
                               " CTM.ISACTIVE," +
                               " CTM.ISACTIVEDATE," +
                               " CTM.COMPANYSITE," +
                               " CTM.INPUTBY," +
                               " CTM.INPUTDATE," +
                               " CTM.UPDATEBY," +
                               " CTM.UPDATEDATE," +
                               " CONTROLJOB.ITEMDESCRIPTION," +
                               " CUSTOMERGROUP.CUSTOMERGROUPNAME," +
                               " PARAMETERVALUE.PARAMETERVALUENAME," +
                               " COUNTRY.COUNTRYNAME," +
                               " PROVINCE.PROVINCENAME," +
                               " CITY.CITYNAME" +
                               " FROM" +
                               " CUSTOMER AS CTM" +
                               " LEFT JOIN CONTROLJOB ON CTM.CONTROLJOB = CONTROLJOB.SID" +
                               " LEFT JOIN CUSTOMERGROUP ON CTM.CUSTOMERGROUP = CUSTOMERGROUP.SID" +
                               " LEFT JOIN PROVINCE ON CTM.PROVINCE = PROVINCE.SID" +
                               " LEFT JOIN COUNTRY ON CTM.COUNTRY = COUNTRY.SID" +
                               " LEFT JOIN CITY ON CTM.CITY = CITY.SID" +
                               " LEFT JOIN PARAMETERVALUE ON CTM.BANK = PARAMETERVALUE.SID" +
                               " ORDER BY" +
                               " CTM.SID ASC").ToList();
        }

        public List<Customer> GetAllByCompanySite(int? CompanySite)
        {
            return this._db.Query<Customer>("SELECT" +
                               " CTM.SID," +
                               " CTM.IDCUSTOMER," +
                               " CTM.CUSTOMERNAME," +
                               " CTM.CUSTOMERGROUP," +
                               " CTM.CONTROLJOB," +
                               " CTM.CONTACTNAME," +
                               " CTM.BANK," +
                               " CTM.BANKACCOUNT," +
                               " CTM.PHONE," +
                               " CTM.FAX," +
                               " CTM.EMAIL," +
                               " CTM.ADDRESS," +
                               " CTM.COUNTRY," +
                               " CTM.PROVINCE," +
                               " CTM.CITY," +
                               " CTM.POSCODE," +
                               " CTM.REMARKS," +
                               " CTM.ISACTIVE," +
                               " CTM.ISACTIVEDATE," +
                               " CTM.COMPANYSITE," +
                               " CTM.INPUTBY," +
                               " CTM.INPUTDATE," +
                               " CTM.UPDATEBY," +
                               " CTM.UPDATEDATE," +
                               " CONTROLJOB.ITEMDESCRIPTION," +
                               " CUSTOMERGROUP.CUSTOMERGROUPNAME," +
                               " PARAMETERVALUE.PARAMETERVALUENAME," +
                               " COUNTRY.COUNTRYNAME," +
                               " PROVINCE.PROVINCENAME," +
                               " CITY.CITYNAME" +
                               " FROM" +
                               " CUSTOMER AS CTM" +
                               " LEFT JOIN CONTROLJOB ON CTM.CONTROLJOB = CONTROLJOB.SID" +
                               " LEFT JOIN CUSTOMERGROUP ON CTM.CUSTOMERGROUP = CUSTOMERGROUP.SID" +
                               " LEFT JOIN PROVINCE ON CTM.PROVINCE = PROVINCE.SID" +
                               " LEFT JOIN COUNTRY ON CTM.COUNTRY = COUNTRY.SID" +
                               " LEFT JOIN CITY ON CTM.CITY = CITY.SID" +
                               " LEFT JOIN PARAMETERVALUE ON CTM.BANK = PARAMETERVALUE.SID" +
                               " WHERE CTM.COMPANYSITE = @COMPANYSITE" +
                               " ORDER BY" +
                               " CTM.SID ASC", new { CompanySite }).ToList();
        }

        public void Remove(int SID)
        {
            var sqlQuery = (string.Format("Delete From Customer Where SID = {0}", SID));
            this._db.Execute(sqlQuery);
        }

        public Customer Update(Customer CTM, string userid)
        {            
            var sqlQuery = @"UPDATE CUSTOMER SET IDCUSTOMER = @IDCUSTOMER, CUSTOMERNAME = @CUSTOMERNAME, CUSTOMERGROUP = @CUSTOMERGROUP, CONTROLJOB = @CONTROLJOB, CONTACTNAME = @CONTACTNAME, BANK = @BANK, BANKACCOUNT = @BANKACCOUNT, PHONE = @PHONE, FAX = @FAX, EMAIL = @EMAIL, ADDRESS = @ADDRESS, COUNTRY = @COUNTRY, PROVINCE = @PROVINCE, CITY = @CITY, POSCODE = @POSCODE, REMARKS = @REMARKS, ISACTIVE = @ISACTIVE, ISACTIVEDATE = @ISACTIVEDATE, COMPANY = @COMPANY, COMPANYSITE = @COMPANYSITE, UPDATEBY = @UPDATEBY, UPDATEDATE = @UPDATEDATE WHERE SID = " + CTM.SID;
            this._db.Execute(sqlQuery, CTM);
            return CTM;
        }
    }
}