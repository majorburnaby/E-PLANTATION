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
    public class BankRepository : IBankRepository
    {
        private IDbConnection _db = new SqlConnection(Constant.DatabaseConnection);

        public Bank Add(Bank BK, string userid)
        {
            var sqlQuery = @"INSERT INTO Bank (IDBANK, BANKNAME, BANKACCOUNTCODE, ADDRESS, COUNTRY, PROVINCE, CITY, PHONE, FAX, DATEREGISTERED, CURRENCY, INACTIVE, INACTIVEDATE, POSCODE, COMPANY, COMPANYSITE, INPUTBY, INPUTDATE, UPDATEBY, UPDATEDATE) VALUES
                           ('" + BK.IDBANK + @"', '" + BK.BANKNAME + "', '" + BK.BANKACCOUNTCODE + @"', '" + BK.ADDRESS + "', '" + BK.COUNTRY + @"', '" + BK.PROVINCE + "', '" + BK.CITY + @"', '" + BK.PHONE + "', '" + BK.FAX + @"', '" + BK.DATEREGISTERED + "', '" + BK.CURRENCY + @"', '" + BK.INACTIVE + "', '" + BK.INACTIVEDATE + @"', '" + BK.POSCODE + "', '" + BK.COMPANY + "', '" + BK.COMPANYSITE + "', " + Convert.ToInt32(userid) + ", '" + DateTime.Now + @"', " + Convert.ToInt32(userid) + ", '" + DateTime.Now + @"'); " + "SELECT CAST(SCOPE_IDENTITY() as int)";
            var SID = _db.Query<int>(sqlQuery, BK).Single();
            BK.SID = SID;
            return BK;
        }

        public Bank Find(int? SID)
        {
            return this._db.Query<Bank>("SELECT * FROM Bank WHERE SID = @SID", new { SID }).SingleOrDefault();
        }

        public List<Bank> GetAll()
        {
            return this._db.Query<Bank>("SELECT" +
                               " BK.SID," +
                               " BK.IDBANK," +
                               " BK.BANKNAME," +
                               " BK.BANKACCOUNTCODE," +
                               " BK.ADDRESS," +
                               " BK.COUNTRY," +
                               " BK.PROVINCE," +
                               " BK.CITY," +
                               " BK.PHONE," +
                               " BK.FAX," +
                               " BK.DATEREGISTERED," +
                               " BK.CURRENCY," +
                               " BK.INACTIVE," +
                               " BK.INACTIVEDATE," +
                               " BK.POSCODE," +
                               " BK.COMPANYSITE," +
                               " BK.INPUTBY," +
                               " BK.INPUTDATE," +
                               " BK.UPDATEBY," +
                               " BK.UPDATEDATE," +
                               " COUNTRY.COUNTRYNAME," +
                               " PROVINCE.PROVINCENAME," +
                               " CITY.CITYNAME," +
                               " CURRENCYMASTER.CURRENCYNAME" +
                               " FROM" +
                               " BANK AS BK" +
                               " INNER JOIN COUNTRY ON BK.COUNTRY = COUNTRY.SID" +
                               " INNER JOIN PROVINCE ON BK.PROVINCE = PROVINCE.SID" +
                               " INNER JOIN CITY ON BK.CITY = CITY.SID" +
                               " INNER JOIN CURRENCYMASTER ON BK.CURRENCY = CURRENCYMASTER.SID" +
                               " ORDER BY" +
                               " BK.SID ASC").ToList();
        }

        public void Remove(int SID)
        {
            var sqlQuery = (string.Format("Delete From BANK Where SID = {0}", SID));
            this._db.Execute(sqlQuery);
        }

        public Bank Update(Bank BK, string userid)
        {
            var sqlQuery =
                string.Format(@"UPDATE BANK SET IDBANK = '{0}', BANKNAME = '{1}', BANKACCOUNTCODE = '{2}', ADDRESS = '{3}', COUNTRY = '{4}', PROVINCE = '{5}', CITY = '{6}', PHONE = '{7}', FAX = '{8}', DATEREGISTERED = '{9}', CURRENCY = '{10}', INACTIVE = '{11}', INACTIVEDATE = '{12}', POSCODE = '{13}', COMPANY = '{14}', COMPANYSITE = '{15}', UPDATEBY = {16}, UPDATEDATE = '{17}' WHERE SID = {18}", 
                BK.IDBANK, BK.BANKNAME, BK.BANKACCOUNTCODE, BK.ADDRESS, BK.COUNTRY, BK.PROVINCE, BK.CITY, BK.PHONE, BK.FAX, BK.DATEREGISTERED, BK.CURRENCY, BK.INACTIVE, BK.INACTIVEDATE, BK.POSCODE, BK.COMPANY, BK.COMPANYSITE, Convert.ToInt32(userid), DateTime.Now, BK.SID);
            this._db.Execute(sqlQuery, BK);
            return BK;
        }
    }
}