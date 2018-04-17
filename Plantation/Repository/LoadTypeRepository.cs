using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dapper;
using Plantation.Models.DB;
using System.Data;
using System.Data.SqlClient;
using Plantation.Utility;
using System;
using Plantation.Repository.Interface;

namespace Plantation.Repository
{
    public class LoadTypeRepository : ILoadTypeRepository
    {
        private IDbConnection _db = new SqlConnection(Constant.DatabaseConnection);

        public LoadType Add(LoadType LT, string userid)
        {
            var sqlQuery = @"INSERT INTO LoadType (IDLOADTYPE, LOADTYPENAME, UOM, INPUTBY, INPUTDATE, UPDATEBY, UPDATEDATE) VALUES
                           ('" + LT.IDLOADTYPE + @"', '" + LT.LOADTYPENAME + "', '" + LT.UOM + "', " + Convert.ToInt32(userid) + ", '" + DateTime.Now + @"', " + Convert.ToInt32(userid) + ", '" + DateTime.Now + @"'); " + "SELECT CAST(SCOPE_IDENTITY() as int)";
            var SID = _db.Query<int>(sqlQuery, LT).Single();
            LT.SID = SID;
            return LT;
        }

        public LoadType Find(int? SID)
        {
            return this._db.Query<LoadType>("SELECT * FROM LoadType WHERE SID = @SID", new { SID }).SingleOrDefault();
        }

        public List<LoadType> GetAll()
        {
            return this._db.Query<LoadType>(" SELECT" +
                               " LT.SID," +
                               " LT.IDLOADTYPE," +
                               " LT.LOADTYPENAME," +
                               " LT.UOM," +
                               " LT.INPUTBY," +
                               " LT.INPUTDATE," +
                               " LT.UPDATEBY," +
                               " LT.UPDATEDATE," +
                               " UNITOFMEASURE.UOMNAME" +
                               " FROM" +
                               " LOADTYPE AS LT" +
                               " INNER JOIN UNITOFMEASURE ON LT.UOM = UNITOFMEASURE.SID" +
                               " ORDER BY" +
                               " LT.SID ASC").ToList();
        }

        public void Remove(int SID)
        {
            var sqlQuery = (string.Format("Delete From LOADTYPE Where SID = {0}", SID));
            this._db.Execute(sqlQuery);
        }

        public LoadType Update(LoadType LT, string userid)
        {
            var sqlQuery =
                string.Format(@"UPDATE LOADTYPE SET IDLOADTYPE = '{0}', LOADTYPENAME = '{1}', UOM = '{2}', UPDATEBY = {3}, UPDATEDATE = '{4}' WHERE SID = {5}", LT.IDLOADTYPE, LT.LOADTYPENAME, LT.UOM, Convert.ToInt32(userid), DateTime.Now, LT.SID);
            this._db.Execute(sqlQuery, LT);
            return LT;
        }

        public List<LoadType> GetAllByUserId(int id)
        {
            using (SqlConnection conn = new SqlConnection(Constant.DatabaseConnection))
            {
                string query = @" SELECT" +
                               " SID," +
                               " IDLOADTYPE," +
                               " LOADTYPENAME," +
                               " UOM," +
                               " FROM" +
                               " LOADTYPE AS LT" +
                               " ORDER BY" +
                               " LT.SID ASC";
                return conn.Query<LoadType>(query, new { Id = id }).ToList();
            }
        }
    }
}