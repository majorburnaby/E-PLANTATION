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
            var sqlQuery = @"INSERT INTO CUSTOMER (IDCUSTOMER, CUSTOMERNAME, CUSTOMERGROUP, CONTROLJOB, CONTACTNAME, BANK, BANKACCOUNT, ADDRESS, COUNTRY, PROVINCE, CITY, POSCODE, PHONE, FAX, EMAIL, REMARKS, ISACTIVE, ISACTIVEDATE, COMPANY, COMPANYSITE, INPUTBY, INPUTDATE, UPDATEBY, UPDATEDATE) VALUES
                           ('" + CTM.IDCUSTOMER + @"', '" + CTM.CUSTOMERNAME + "', '" + CTM.CUSTOMERGROUP + @"','" + CTM.CONTROLJOB + @"', '" + CTM.CONTACTNAME + @"', '" + CTM.BANK + @"', '" + CTM.BANKACCOUNT + @"', '" + CTM.ADDRESS + @"', '" + CTM.COUNTRY + @"', '" + CTM.PROVINCE + @"', '" + CTM.CITY + @"', '" + CTM.POSCODE + @"', '" + CTM.PHONE + @"', '" + CTM.FAX + @"', '" + CTM.EMAIL + @"', '" + CTM.REMARKS + @"', '" + CTM.ISACTIVE + @"', '" + CTM.ISACTIVEDATE + @"', '" + CTM.COMPANY + @"', '" + CTM.COMPANYSITE + @"'," + Convert.ToInt32(userid) + ", '" + DateTime.Now + @"', " + Convert.ToInt32(userid) + ", '" + DateTime.Now + @"'); " + "SELECT CAST(SCOPE_IDENTITY() as int)";
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
                               " CTM.ADDRESS," +
                               " CTM.COUNTRY," +
                               " CTM.PROVINCE," +
                               " CTM.CITY," +
                               " CTM.POSCODE," +
                               " CTM.PHONE," +
                               " CTM.FAX," +
                               " CTM.EMAIL," +
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
                               " COUNTRY.COUNTRYNAME," +
                               " PROVINCE.PROVINCENAME," +
                               " CITY.CITYNAME" +
                               " FROM" +
                               " Customer AS CTM" +
                               " INNER JOIN CONTROLJOB ON CTM.CONTROLJOB = CONTROLJOB.SID" +
                               " INNER JOIN CUSTOMERGROUP ON CTM.CUSTOMERGROUP = CUSTOMERGROUP.SID" +
                               " INNER JOIN PROVINCE ON CTM.PROVINCE = PROVINCE.SID" +
                               " INNER JOIN COUNTRY ON CTM.COUNTRY = COUNTRY.SID" +
                               " INNER JOIN CITY ON CTM.CITY = CITY.SID" +
                               " ORDER BY" +
                               " CTM.SID ASC").ToList();
        }

        public void Remove(int SID)
        {
            var sqlQuery = (string.Format("Delete From Customer Where SID = {0}", SID));
            this._db.Execute(sqlQuery);
        }

        public Customer Update(Customer CTM, string userid)
        {
            var sqlQuery =
                string.Format(@"UPDATE CUSTOMER SET IDCUSTOMER = '{0}', CUSTOMERNAME = '{1}', CUSTOMERGROUP = '{2}', CONTROLJOB = '{3}', CONTACTNAME = '{4}', BANK = '{5}', BANKACCOUNT = '{6}', ADDRESS = '{7}', COUNTRY = '{8}', PROVINCE = '{9}', CITY = '{10}', POSCODE = '{11}', PHONE = '{12}', FAX = '{13}', EMAIL = '{14}', REMARKS = '{15}', ISACTIVE = '{16}', ISACTIVEDATE = '{17}', COMPANY = '{18}', COMPANYSITE = '{19}', UPDATEBY = {= '{20}'}, UPDATEDATE = '{21}' WHERE SID = {22}",
                CTM.IDCUSTOMER, CTM.CUSTOMERNAME, CTM.CUSTOMERGROUP, CTM.CONTROLJOB, CTM.CONTACTNAME, CTM.BANK, CTM.BANKACCOUNT, CTM.ADDRESS, CTM.COUNTRY, CTM.PROVINCE, CTM.CITY, CTM.POSCODE, CTM.PHONE, CTM.FAX, CTM.EMAIL, CTM.REMARKS, CTM.ISACTIVE, CTM.ISACTIVEDATE, CTM.COMPANY, CTM.COMPANYSITE, Convert.ToInt32(userid), DateTime.Now, CTM.SID);
            this._db.Execute(sqlQuery, CTM);
            return CTM;
        }
    }
}