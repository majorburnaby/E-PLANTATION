using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using Plantation.Models.DB;
using System.Data;
using System.Data.SqlClient;
using Plantation.Utility;
using Plantation.Repository.Interface;

namespace Plantation.Repository
{
    public class CountryRepository : ICountryRepository
    {
        private IDbConnection _db = new SqlConnection(Constant.DatabaseConnection);

        public Country Add(Country CN, string userid)
        {
            var sqlQuery = @"INSERT INTO Country (IDCOUNTRY, COUNTRYNAME, INPUTBY, INPUTDATE, UPDATEBY, UPDATEDATE) VALUES
                           ('" + CN.IDCOUNTRY + @"', '" + CN.COUNTRYNAME + "', " + Convert.ToInt32(userid) + ", '" + DateTime.Now + @"', " + Convert.ToInt32(userid) + ", '" + DateTime.Now + @"'); " + "SELECT CAST(SCOPE_IDENTITY() as int)";
            var SID = _db.Query<int>(sqlQuery, CN).Single();
            CN.SID = SID;
            return CN;
        }

        public Country Find(int? SID)
        {
            return this._db.Query<Country>("SELECT * FROM Country WHERE SID = @SID", new { SID }).SingleOrDefault();
        }

        public List<Country> GetAll()
        {
            return this._db.Query<Country>("SELECT" +
                               " CN.SID," +
                               " CN.IDCOUNTRY," +
                               " CN.COUNTRYNAME," +
                               " CN.INPUTBY," +
                               " CN.INPUTDATE," +
                               " CN.UPDATEBY," +
                               " CN.UPDATEDATE" +
                               " FROM" +
                               " COUNTRY AS CN" +
                               " ORDER BY" +
                               " CN.SID ASC").ToList();
        }

        public void Remove(int SID)
        {
            var sqlQuery = (string.Format("Delete From COUNTRY Where SID = {0}", SID));
            this._db.Execute(sqlQuery);
        }

        public Country Update(Country CN, string userid)
        {
            var sqlQuery =
                string.Format(@"UPDATE COUNTRY SET IDCOUNTRY = '{0}', COUNTRYNAME = '{1}', UPDATEBY = {2}, UPDATEDATE = '{3}' WHERE SID = {4}", CN.IDCOUNTRY, CN.COUNTRYNAME, Convert.ToInt32(userid), DateTime.Now, CN.SID);
            this._db.Execute(sqlQuery, CN);
            return CN;
        }
    }
}