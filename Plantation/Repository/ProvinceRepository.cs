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
    public class ProvinceRepository : IProvinceRepository
    {
        private IDbConnection _db = new SqlConnection(Constant.DatabaseConnection);

        public Province Add(Province PV, string userid)
        {
            var sqlQuery = @"INSERT INTO PROVINCE (IDPROVINCE, PROVINCENAME, COUNTRY, INPUTBY, INPUTDATE, UPDATEBY, UPDATEDATE) 
                            VALUES('" + PV.IDPROVINCE + "', '" + PV.PROVINCENAME + "', '" + PV.COUNTRY + "', '" + userid + "', '" + DateTime.Now + "', '" + userid + "', '" + DateTime.Now + "'); " + "SELECT CAST(SCOPE_IDENTITY() as int)";
            var SID = _db.Query<int>(sqlQuery, PV).Single();
            PV.SID = SID;
            return PV;
        }

        public Province Find(int? SID)
        {
            return this._db.Query<Province>("SELECT * FROM PROVINCE WHERE SID = @SID", new { SID }).SingleOrDefault();
        }

        public List<Province> GetAll()
        {
            return this._db.Query<Province>("SELECT" +
                               " PV.SID," +
                               " PV.IDPROVINCE," +
                               " PV.PROVINCENAME," +
                               " PV.COUNTRY," +
                               " PV.INPUTBY," +
                               " PV.INPUTDATE," +
                               " PV.UPDATEBY," +
                               " PV.UPDATEDATE," +
                               " COUNTRY.COUNTRYNAME" +
                               " FROM" +
                               " PROVINCE AS PV" +
                               " INNER JOIN COUNTRY ON PV.COUNTRY = COUNTRY.SID" +
                               " ORDER BY" +
                               " PV.COUNTRY,SID ASC").ToList();
        }

        public void Remove(int SID)
        {
            var sqlQuery = (string.Format("Delete From PROVINCE Where SID = {0}", SID));
            this._db.Execute(sqlQuery);
        }

        public Province Update(Province PV, string userid)
        {
            var sqlQuery =
                "UPDATE PROVINCE " +
                "SET IDPROVINCE    = '" + PV.IDPROVINCE + "', " +
                "    PROVINCENAME  = '" + PV.PROVINCENAME + "', " +
                "    COUNTRY       = '" + PV.COUNTRY + "', " +
                "    UPDATEBY      = '" + Convert.ToInt32(userid) + "', " +
                "    UPDATEDATE    = '" + DateTime.Now + "' " +
                "WHERE SID = " + PV.SID + "";
            this._db.Execute(sqlQuery, PV);
            return PV;
        }        
    }
}