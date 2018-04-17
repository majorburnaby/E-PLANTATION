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
    public class FixedAssetGroupRepository : IFixedAssetGroupRepository
    {
        private IDbConnection _db = new SqlConnection(Constant.DatabaseConnection);

        public FixedAssetGroup Add(FixedAssetGroup FAG, string userid)
        {
            var sqlQuery = @"INSERT INTO FIXEDASSETGROUP (IDFAGROUP, FAGROUPNAME, COSTCENTERID, INPUTBY, INPUTDATE, UPDATEBY, UPDATEDATE) VALUES
                           ('" + FAG.IDFAGROUP + @"', '" + FAG.FAGROUPNAME + "', '" + FAG.COSTCENTERID + @"', " + Convert.ToInt32(userid) + ", '" + DateTime.Now + @"', " + Convert.ToInt32(userid) + ", '" + DateTime.Now + @"'); " + "SELECT CAST(SCOPE_IDENTITY() as int)";
            var SID = _db.Query<int>(sqlQuery, FAG).Single();
            FAG.SID = SID;
            return FAG;
        }

        public FixedAssetGroup Find(int? SID)
        {
            return this._db.Query<FixedAssetGroup>("SELECT * FROM FIXEDASSETGROUP WHERE SID = @SID", new { SID }).SingleOrDefault();
        }

        public List<FixedAssetGroup> GetAll()
        {
            return this._db.Query<FixedAssetGroup>("SELECT" +
                               " FAG.SID," +
                               " FAG.IDFAGROUP," +
                               " FAG.FAGROUPNAME," +
                               " FAG.COSTCENTERID," +
                               " FAG.INPUTBY," +
                               " FAG.INPUTDATE," +
                               " FAG.UPDATEBY," +
                               " FAG.UPDATEDATE" +
                               " FROM" +
                               " FIXEDASSETGROUP AS FAG" +
                               " ORDER BY" +
                               " FAG.SID ASC").ToList();
        }

        public void Remove(int SID)
        {
            var sqlQuery = (string.Format("Delete From FIXEDASSETGROUP Where SID = {0}", SID));
            this._db.Execute(sqlQuery);
        }

        public FixedAssetGroup Update(FixedAssetGroup FAG, string userid)
        {
            var sqlQuery =
                string.Format(@"UPDATE FIXEDASSETGROUP SET IDFAGROUP = '{0}', FAGROUPNAME = '{1}', COSTCENTERID = '{2}', UPDATEBY = {3}, UPDATEDATE = '{4}' WHERE SID = {5}",
                FAG.IDFAGROUP, FAG.FAGROUPNAME, FAG.COSTCENTERID, Convert.ToInt32(userid), DateTime.Now, FAG.SID);
            this._db.Execute(sqlQuery, FAG);
            return FAG;
        }
    }
}