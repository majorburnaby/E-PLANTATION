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
    public class StockGroupRepository : IStockGroupRepository
    {
        private IDbConnection _db = new SqlConnection(Constant.DatabaseConnection);

        public StockGroup Add(StockGroup STG, string userid)
        {
            var sqlQuery = @"INSERT INTO STOCKGROUP (IDSTOCKGROUP, STOCKGROUPNAME, CONTROLJOB, INPUTBY, INPUTDATE, UPDATEBY, UPDATEDATE) VALUES
                           ('" + STG.IDSTOCKGROUP + @"', '" + STG.STOCKGROUPNAME + "', '" + STG.CONTROLJOB + @"', " + Convert.ToInt32(userid) + ", '" + DateTime.Now + @"', " + Convert.ToInt32(userid) + ", '" + DateTime.Now + @"'); " + "SELECT CAST(SCOPE_IDENTITY() as int)";
            var SID = _db.Query<int>(sqlQuery, STG).Single();
            STG.SID = SID;
            return STG;
        }

        public StockGroup Find(int? SID)
        {
            return this._db.Query<StockGroup>("SELECT * FROM STOCKGROUP WHERE SID = @SID", new { SID }).SingleOrDefault();
        }

        public List<StockGroup> GetAll()
        {
            return this._db.Query<StockGroup>("SELECT" +
                               " STG.SID," +
                               " STG.IDSTOCKGROUP," +
                               " STG.STOCKGROUPNAME," +
                               " STG.CONTROLJOB," +
                               " STG.INPUTBY," +
                               " STG.INPUTDATE," +
                               " STG.UPDATEBY," +
                               " STG.UPDATEDATE" +
                               " FROM" +
                               " STOCKGROUP AS STG" +
                               " ORDER BY" +
                               " STG.SID ASC").ToList();
        }

        public void Remove(int SID)
        {
            var sqlQuery = (string.Format("Delete From STOCKGROUP Where SID = {0}", SID));
            this._db.Execute(sqlQuery);
        }

        public StockGroup Update(StockGroup STG, string userid)
        {
            var sqlQuery =
                string.Format(@"UPDATE STOCKGROUP SET IDSTOCKGROUP = '{0}', STOCKGROUPNAME = '{1}', CONTROLJOB = '{2}', UPDATEBY = {3}, UPDATEDATE = '{4}' WHERE SID = {5}",
                STG.IDSTOCKGROUP, STG.STOCKGROUPNAME, STG.CONTROLJOB, Convert.ToInt32(userid), DateTime.Now, STG.SID);
            this._db.Execute(sqlQuery, STG);
            return STG;
        }
    }
}