using Dapper;
using Plantation.Models.DB;
using Plantation.Repository.Interface;
using Plantation.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Plantation.Repository
{
    public class BlockUsageRepository : IBlockUsageRepository
    {
        private IDbConnection _db = new SqlConnection(Constant.DatabaseConnection);

        public BlockUsage Add(BlockUsage BU)
        {
            var sqlQuery = @"INSERT INTO BLOCKUSAGE (BLOCKMASTER, USAGE, HECTARAGE, COMPANYSITE, INPUTBY, INPUTDATE, UPDATEBY, UPDATEDATE) VALUES
                           (" + BU.BLOCKMASTER + @", " + BU.USAGE + ", '" + BU.HECTARAGE + "', '" + BU.COMPANYSITE + @"', " + BU.INPUTBY + ", '" + BU.INPUTDATE + @"', " + BU.UPDATEBY + ", '" + BU.UPDATEDATE + @"'); " + "SELECT CAST(SCOPE_IDENTITY() as int)";
            var SID = _db.Query<int>(sqlQuery, BU).Single();
            BU.SID = SID;
            return BU;
        }

        public BlockUsage Find(int? SID)
        {
            return this._db.Query<BlockUsage>("SELECT * FROM BLOCKUSAGE WHERE SID = @SID", new { SID }).SingleOrDefault();
        }

        public List<BlockUsage> GetAll()
        {
            return this._db.Query<BlockUsage>("SELECT *" +
                              " FROM" +
                              " BLOCKUSAGE AS BU" +
                              " ORDER BY" +
                              " BU.SID ASC").ToList();
        }

        public int GetTotalBlockUsage(int BlockMaster)
        {
            return this._db.Query<int>("SELECT isnull(SUM(cast(HECTARAGE as int)),0) FROM BLOCKUSAGE WHERE BLOCKMASTER = " + BlockMaster).FirstOrDefault();
        }

        public void Remove(int SID)
        {
            var sqlQuery = (string.Format("Delete From BLOCKUSAGE Where SID = {0}", SID));
            this._db.Execute(sqlQuery);
        }

        public BlockUsage Update(BlockUsage BU)
        {
            var sqlQuery =
                "UPDATE BLOCKUSAGE " +
                "SET USAGE    = '" + BU.USAGE + "', " +
                "    HECTARAGE  = '" + BU.HECTARAGE + "', " +
                "    UPDATEBY      = '" + Convert.ToInt32(BU.UPDATEBY) + "', " +
                "    UPDATEDATE    = '" + BU.UPDATEDATE + "' " +
                "WHERE SID = " + BU.SID + "";
            this._db.Execute(sqlQuery, BU);
            return BU;
        }
    }
}