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
            var sqlQuery = @"INSERT INTO Bank (IDBANK, BANKNAME, BANKACCOUNTCODE, ADDRESS, COUNTRY, PROVINCE, CITY, PHONE, FAX, CURRENCY, ISACTIVE, ISACTIVEDATE, POSCODE, COMPANYSITE, INPUTBY, INPUTDATE, UPDATEBY, UPDATEDATE) VALUES
                           ('" + BK.IDBANK + @"', '" + BK.BANKNAME + "', '" + BK.BANKACCOUNTCODE + @"', '" + BK.ADDRESS + "', '" + BK.COUNTRY + @"', '" + BK.PROVINCE + "', '" + BK.CITY + @"', '" + BK.PHONE + "', '" + BK.FAX + @"', '" + BK.CURRENCY + @"', '" + BK.ISACTIVE + "', '" + BK.ISACTIVEDATE + @"', '" + BK.POSCODE + "', '" + BK.COMPANYSITE + "', " + Convert.ToInt32(userid) + ", '" + DateTime.Now + @"', " + Convert.ToInt32(userid) + ", '" + DateTime.Now + @"'); " + "SELECT CAST(SCOPE_IDENTITY() as int)";
            var SID = _db.Query<int>(sqlQuery, BK).Single();
            BK.SID = SID;
            return BK;
        }

        public Bank Find(int? SID)
        {
            return this._db.Query<Bank>("SELECT * FROM BANK WHERE SID = @SID", new { SID }).SingleOrDefault();
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
                               " BK.CURRENCY," +
                               " BK.ISACTIVE," +
                               " BK.ISACTIVEDATE," +
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
                               " LEFT JOIN COUNTRY ON BK.COUNTRY = COUNTRY.SID" +
                               " LEFT JOIN PROVINCE ON BK.PROVINCE = PROVINCE.SID" +
                               " LEFT JOIN CITY ON BK.CITY = CITY.SID" +
                               " LEFT JOIN CURRENCYMASTER ON BK.CURRENCY = CURRENCYMASTER.SID" +
                               " ORDER BY" +
                               " BK.SID ASC").ToList();
        }

        public List<Bank> GetAllByCompanySite(int? CompanySite)
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
                               " BK.CURRENCY," +
                               " BK.ISACTIVE," +
                               " BK.ISACTIVEDATE," +
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
                               " LEFT JOIN COUNTRY ON BK.COUNTRY = COUNTRY.SID" +
                               " LEFT JOIN PROVINCE ON BK.PROVINCE = PROVINCE.SID" +
                               " LEFT JOIN CITY ON BK.CITY = CITY.SID" +
                               " LEFT JOIN CURRENCYMASTER ON BK.CURRENCY = CURRENCYMASTER.SID" +
                               " WHERE BK.COMPANYSITE = @COMPANYSITE" +
                               " ORDER BY" +
                               " BK.SID ASC", new { CompanySite }).ToList();
        }

        public void Remove(int SID)
        {
            var sqlQuery = (string.Format("Delete From BANK Where SID = {0}", SID));
            this._db.Execute(sqlQuery);
        }

        public Bank Update(Bank BK, string userid)
        {
            var sqlQuery =
                string.Format(@"UPDATE BANK SET IDBANK = '{0}', BANKNAME = '{1}', BANKACCOUNTCODE = '{2}', ADDRESS = '{3}', COUNTRY = '{4}', PROVINCE = '{5}', CITY = '{6}', PHONE = '{7}', FAX = '{8}', CURRENCY = '{9}', ISACTIVE = '{10}', ISACTIVEDATE = '{11}', POSCODE = '{12}', COMPANYSITE = '{13}', UPDATEBY = {14}, UPDATEDATE = '{15}' WHERE SID = {16}", 
                BK.IDBANK, BK.BANKNAME, BK.BANKACCOUNTCODE, BK.ADDRESS, BK.COUNTRY, BK.PROVINCE, BK.CITY, BK.PHONE, BK.FAX, BK.CURRENCY, BK.ISACTIVE, BK.ISACTIVEDATE, BK.POSCODE, BK.COMPANYSITE, Convert.ToInt32(userid), DateTime.Now, BK.SID);
            this._db.Execute(sqlQuery, BK);
            return BK;
        }
    }
}