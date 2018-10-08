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
    public class LandConcessionRepository : ILandConcessionRepository
    {
        private IDbConnection _db = new SqlConnection(Constant.DatabaseConnection);

        public LandConcession Add(LandConcession LCC, string userid)
        {
            var sqlQuery = @"INSERT INTO LANDCONCESSION (IDCONCESSION, CONCESSIONNAME, PERMISSIONNO, PERMISSIONTYPE, PERMISSIONDATE, STARTDATE, ENDDATE, COUNTRY, PROVINCE, CITY, HECTARAGE, COMPANYSITE, INPUTBY, INPUTDATE, UPDATEBY, UPDATEDATE) VALUES
                           ('" + LCC.IDCONCESSION + @"', '" + LCC.CONCESSIONNAME + "', '" + LCC.PERMISSIONNO + @"','" + LCC.PERMISSIONTYPE + @"', '" + LCC.PERMISSIONDATE + @"', '" + LCC.STARTDATE + @"', '" + LCC.ENDDATE + @"', '" + LCC.COUNTRY + @"', '" + LCC.PROVINCE + @"', '" + LCC.CITY + @"', '" + LCC.HECTARAGE + @"', '" + LCC.COMPANYSITE + @"'," + Convert.ToInt32(userid) + ", '" + DateTime.Now + @"', " + Convert.ToInt32(userid) + ", '" + DateTime.Now + @"'); " + "SELECT CAST(SCOPE_IDENTITY() as int)";
            var SID = _db.Query<int>(sqlQuery, LCC).Single();
            LCC.SID = SID;
            return LCC;
        }

        public LandConcession Find(int? SID)
        {
            return this._db.Query<LandConcession>("SELECT * FROM LANDCONCESSION WHERE SID = @SID", new { SID }).SingleOrDefault();
        }

        public List<LandConcession> GetAll()
        {
            return this._db.Query<LandConcession>("SELECT" +
                               " LCC.SID," +
                               " LCC.IDCONCESSION," +
                               " LCC.CONCESSIONNAME," +
                               " LCC.PERMISSIONNO," +
                               " LCC.PERMISSIONTYPE," +
                               " LCC.PERMISSIONDATE," +
                               " LCC.STARTDATE," +
                               " LCC.ENDDATE," +
                               " LCC.COUNTRY," +
                               " LCC.PROVINCE," +
                               " LCC.CITY," +
                               " LCC.HECTARAGE," +
                               " LCC.COMPANYSITE," +
                               " LCC.INPUTBY," +
                               " LCC.INPUTDATE," +
                               " LCC.UPDATEBY," +
                               " LCC.UPDATEDATE," +
                               " PARAMETERVALUE.PARAMETERVALUENAME," +
                               " COUNTRY.COUNTRYNAME," +
                               " PROVINCE.PROVINCENAME," +
                               " CITY.CITYNAME" +
                               " FROM" +
                               " LANDCONCESSION AS LCC" +
                               " LEFT JOIN PROVINCE ON LCC.PROVINCE = PROVINCE.SID" +
                               " LEFT JOIN COUNTRY ON LCC.COUNTRY = COUNTRY.SID" +
                               " LEFT JOIN CITY ON LCC.CITY = CITY.SID" +
                               " LEFT JOIN PARAMETERVALUE ON LCC.PERMISSIONTYPE = PARAMETERVALUE.SID" +
                               " ORDER BY" +
                               " PROVINCE.PROVINCENAME, LCC.SID ASC").ToList();
        }

        public List<LandConcession> GetAllByCompanySite(int? CompanySite)
        {
            return this._db.Query<LandConcession>("SELECT" +
                               " LCC.SID," +
                               " LCC.IDCONCESSION," +
                               " LCC.CONCESSIONNAME," +
                               " LCC.PERMISSIONNO," +
                               " LCC.PERMISSIONTYPE," +
                               " LCC.PERMISSIONDATE," +
                               " LCC.STARTDATE," +
                               " LCC.ENDDATE," +
                               " LCC.COUNTRY," +
                               " LCC.PROVINCE," +
                               " LCC.CITY," +
                               " LCC.HECTARAGE," +
                               " LCC.COMPANYSITE," +
                               " LCC.INPUTBY," +
                               " LCC.INPUTDATE," +
                               " LCC.UPDATEBY," +
                               " LCC.UPDATEDATE," +
                               " PARAMETERVALUE.PARAMETERVALUENAME," +
                               " COUNTRY.COUNTRYNAME," +
                               " PROVINCE.PROVINCENAME," +
                               " CITY.CITYNAME" +
                               " FROM" +
                               " LANDCONCESSION AS LCC" +
                               " LEFT JOIN PROVINCE ON LCC.PROVINCE = PROVINCE.SID" +
                               " LEFT JOIN COUNTRY ON LCC.COUNTRY = COUNTRY.SID" +
                               " LEFT JOIN CITY ON LCC.CITY = CITY.SID" +
                               " LEFT JOIN PARAMETERVALUE ON LCC.PERMISSIONTYPE = PARAMETERVALUE.SID" +
                               " WHERE LCC.COMPANYSITE = @COMPANYSITE" +
                               " ORDER BY" +
                               " PROVINCE.PROVINCENAME, LCC.SID ASC", new { CompanySite }).ToList();
        }

        public void Remove(int SID)
        {
            var sqlQuery = (string.Format("Delete From LandConcession Where SID = {0}", SID));
            this._db.Execute(sqlQuery);
        }

        public LandConcession Update(LandConcession LCC, string userid)
        {
            var sqlQuery =
                string.Format(@"UPDATE LANDCONCESSION SET CONCESSIONNAME = '{0}', PERMISSIONNO = '{1}', PERMISSIONTYPE = '{2}', PERMISSIONDATE = '{3}', STARTDATE = '{4}', ENDDATE = '{5}', COUNTRY = '{6}', PROVINCE = '{7}', CITY = '{8}', HECTARAGE = '{9}', COMPANYSITE = '{10}', UPDATEBY = {11}, UPDATEDATE = '{12}' WHERE SID = {13}",
                LCC.CONCESSIONNAME, LCC.PERMISSIONNO, LCC.PERMISSIONTYPE, LCC.PERMISSIONDATE, LCC.STARTDATE, LCC.ENDDATE, LCC.COUNTRY, LCC.PROVINCE, LCC.CITY, LCC.HECTARAGE, LCC.COMPANYSITE, Convert.ToInt32(userid), DateTime.Now, LCC.SID);
            this._db.Execute(sqlQuery, LCC);
            return LCC;
        }
    }
}