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
    public class RegionRepository : IRegionRepository
    {
        private IDbConnection _db = new SqlConnection(Constant.DatabaseConnection);

        public Region Add(Region RG, string userid)
        {
            var sqlQuery = @"INSERT INTO REGION (IDREGION, REGIONNAME, INPUTBY, INPUTDATE, UPDATEBY, UPDATEDATE) VALUES
                           ('" + RG.IDREGION + @"', '" + RG.REGIONNAME + "', " + Convert.ToInt32(userid) + ", '" + DateTime.Now + @"', " + Convert.ToInt32(userid) + ", '" + DateTime.Now + @"'); " + "SELECT CAST(SCOPE_IDENTITY() as int)";
            var SID = _db.Query<int>(sqlQuery, RG).Single();
            RG.SID = SID;
            return RG;
        }

        public Region Find(int? SID)
        {
            return this._db.Query<Region>("SELECT * FROM Region WHERE SID = @SID", new { SID }).SingleOrDefault();
        }

        public List<Region> GetAll()
        {
            return this._db.Query<Region>("SELECT" +
                               " RG.SID," +
                               " RG.IDREGION," +
                               " RG.REGIONNAME," +
                               " RG.INPUTBY," +
                               " RG.INPUTDATE," +
                               " RG.UPDATEBY," +
                               " RG.UPDATEDATE" +
                               " FROM" +
                               " REGION AS RG" +
                               " ORDER BY" +
                               " RG.SID ASC").ToList();
        }

        public void Remove(int SID)
        {
            var sqlQuery = (string.Format("Delete From Region Where SID = {0}", SID));
            this._db.Execute(sqlQuery);
        }

        public Region Update(Region RG, string userid)
        {
            var sqlQuery =
                string.Format(@"UPDATE REGION SET IDREGION = '{0}', REGIONNAME = '{1}', UPDATEBY = {2}, UPDATEDATE = '{3}' WHERE SID = {4}", RG.IDREGION, RG.REGIONNAME, Convert.ToInt32(userid), DateTime.Now, RG.SID);
            this._db.Execute(sqlQuery, RG);
            return RG;
        }
    }
}