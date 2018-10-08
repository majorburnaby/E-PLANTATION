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
    public class BlockOrganizationRepository : IBlockOrganizationRepository
    {
        private IDbConnection _db = new SqlConnection(Constant.DatabaseConnection);

        public BlockOrganization Add(BlockOrganization BLO, string userid)
        {
            var sqlQuery = @"INSERT INTO BLOCKORGANIZATION (IDBLOCKORGANIZATION, BLOCKORGANIZATIONNAME, COMPANYSITE, INPUTBY, INPUTDATE, UPDATEBY, UPDATEDATE) VALUES
                           ('" + BLO.IDBLOCKORGANIZATION + @"', '" + BLO.BLOCKORGANIZATIONNAME + "', '" + BLO.COMPANYSITE + "', " + Convert.ToInt32(userid) + ", '" + DateTime.Now + @"', " + Convert.ToInt32(userid) + ", '" + DateTime.Now + @"'); " + "SELECT CAST(SCOPE_IDENTITY() as int)";
            var SID = _db.Query<int>(sqlQuery, BLO).Single();
            BLO.SID = SID;
            return BLO;
        }

        public BlockOrganization Find(int? SID)
        {
            return this._db.Query<BlockOrganization>("SELECT * FROM BLOCKORGANIZATION WHERE SID = @SID", new { SID }).SingleOrDefault();
        }

        public List<BlockOrganization> GetAll()
        {
            return this._db.Query<BlockOrganization>("SELECT" +
                               " BLO.SID," +
                               " BLO.IDBLOCKORGANIZATION," +
                               " BLO.BLOCKORGANIZATIONNAME," +
                               " BLO.COMPANYSITE," +
                               " BLO.INPUTBY," +
                               " BLO.INPUTDATE," +
                               " BLO.UPDATEBY," +
                               " BLO.UPDATEDATE" +
                               " FROM" +
                               " BLOCKORGANIZATION AS BLO" +
                               " ORDER BY" +
                               " BLO.SID ASC").ToList();
        }

        public void Remove(int SID)
        {
            var sqlQuery = (string.Format("Delete From BlockOrganization Where SID = {0}", SID));
            this._db.Execute(sqlQuery);
        }

        public BlockOrganization Update(BlockOrganization BLO, string userid)
        {
            var sqlQuery =
                string.Format(@"UPDATE BLOCKORGANIZATION SET IDBLOCKORGANIZATION = '{0}', BLOCKORGANIZATIONNAME = '{1}', COMPANYSITE = '{2}', UPDATEBY = {3}, UPDATEDATE = '{4}' WHERE SID = {5}", BLO.IDBLOCKORGANIZATION, BLO.BLOCKORGANIZATIONNAME, BLO.COMPANYSITE, Convert.ToInt32(userid), DateTime.Now, BLO.SID);
            this._db.Execute(sqlQuery, BLO);
            return BLO;
        }
    }
}