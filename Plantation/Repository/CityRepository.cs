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
    public class CityRepository : ICityRepository
    {
        private IDbConnection _db = new SqlConnection(Constant.DatabaseConnection);

        public City Add(City CT, string userid)
        {
            var sqlQuery = @"INSERT INTO City (IDCITY, CITYNAME, COUNTRY, PROVINCE, INPUTBY, INPUTDATE, UPDATEBY, UPDATEDATE) VALUES
                           ('" + CT.IDCITY + @"', '" + CT.CITYNAME + "', '" + CT.COUNTRY + @"', '" + CT.PROVINCE + "', " + Convert.ToInt32(userid) + ", '" + DateTime.Now + @"', " + Convert.ToInt32(userid) + ", '" + DateTime.Now + @"'); " + "SELECT CAST(SCOPE_IDENTITY() as int)";
            var SID = _db.Query<int>(sqlQuery, CT).Single();
            CT.SID = SID;
            return CT;
        }

        public City Find(int? SID)
        {
            return this._db.Query<City>("SELECT * FROM City WHERE SID = @id", new { SID }).SingleOrDefault();
        }

        public List<City> GetAll()
        {
            return this._db.Query<City>(" SELECT" +
                               " CT.SID," +
                               " CT.IDCITY," +
                               " CT.CITYNAME," +
                               " CT.COUNTRY," +
                               " CT.PROVINCE," +
                               " CT.INPUTBY," +
                               " CT.INPUTDATE," +
                               " CT.UPDATEBY," +
                               " CT.UPDATEDATE," +
                               " COUNTRY.COUNTRYNAME," +
                               " PROVINCE.PROVINCENAME" +
                               " FROM" +
                               " CITY AS CT" +
                               " INNER JOIN PROVINCE ON CT.PROVINCE = PROVINCE.SID" +
                               " INNER JOIN COUNTRY ON CT.COUNTRY = COUNTRY.SID" +
                               " ORDER BY" +
                               " CT.COUNTRY,PROVINCE,IDCITY ASC").ToList();
        }

        public void Remove(int SID)
        {
            var sqlQuery = (string.Format("Delete From CITY Where SID = {0}", SID));
            this._db.Execute(sqlQuery);
        }

        public  City Update(City CT, string userid)
        {
            var sqlQuery =
                string.Format(@"UPDATE CITY SET IDCITY = '{0}', CITYNAME = '{1}', COUNTRY = '{2}', PROVINCE = '{3}', UPDATEBY = {4}, UPDATEDATE = '{5}' WHERE SID = {6}", CT.IDCITY, CT.CITYNAME, CT.COUNTRY, CT.PROVINCE, Convert.ToInt32(userid), DateTime.Now, CT.SID);
            this._db.Execute(sqlQuery,CT);

                return CT;
        }
    }
}