using Plantation.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Plantation.Models.DB;
using System.Data;
using Plantation.Utility;
using System.Data.SqlClient;
using Dapper;

namespace Plantation.Repository
{
    public class BlockMasterRepository : IBlockMasterRepository
    {
        private IDbConnection _db = new SqlConnection(Constant.DatabaseConnection);

        public BlockMaster Add(BlockMaster BLO, string userid)
        {
            var sqlQuery = @"INSERT INTO BLOCKMASTER (IDBLOCKMASTER, BLOCKMASTERNAME, LANDCONCESSION, BLOCKORGANIZATION, SOILTYPE, VEGETATION, TOPOGRAPH, HECTARAGE, COMPANYSITE, INPUTBY, INPUTDATE, UPDATEBY, UPDATEDATE) VALUES
                           ('" + BLO.IDBLOCKMASTER + @"', '" + BLO.BLOCKMASTERNAME + "', '" + BLO.LANDCONCESSION + "', '" + BLO.BLOCKORGANIZATION + "', '" + BLO.SOILTYPE + "', '" + BLO.VEGETATION + "', '" + BLO.TOPOGRAPH + "', '" + BLO.HECTARAGE + "', '" + BLO.COMPANYSITE +  @"', " + Convert.ToInt32(userid) + ", '" + DateTime.Now + @"', " + Convert.ToInt32(userid) + ", '" + DateTime.Now + @"'); " + "SELECT CAST(SCOPE_IDENTITY() as int)";
            var SID = _db.Query<int>(sqlQuery, BLO).Single();
            BLO.SID = SID;
            return BLO;
        }

        public BlockMaster Find(int? SID)
        {
            return this._db.Query<BlockMaster>("SELECT *, HECTARAGE AS HECTARAGE_TEMP FROM BLOCKMASTER WHERE SID = @SID", new { SID }).SingleOrDefault();
        }

        public List<BlockMaster> GetAll()
        {
            return this._db.Query<BlockMaster>("SELECT" +
                               " BM.SID," +
                               " BM.IDBLOCKMASTER," +
                               " BM.BLOCKMASTERNAME," +
                               " BM.LANDCONCESSION," +
                               " BM.BLOCKORGANIZATION," +
                               " BM.SOILTYPE," +
                               " BM.VEGETATION," +
                               " BM.TOPOGRAPH," +
                               " BM.HECTARAGE," +
                               " BM.HECTARAGE AS HECTARAGE_TEMP," +
                               " BM.INPUTBY," +
                               " BM.INPUTDATE," +
                               " BM.UPDATEBY," +
                               " BM.UPDATEDATE" +
                               " FROM" +
                               " BLOCKMASTER AS BM" +
                               " ORDER BY" +
                               " BM.SID ASC").ToList();
        }

        public List<BlockMaster> GetAllByCompanySite(int CompanySite)
        {
            return this._db.Query<BlockMaster>("SELECT" +
                              " BM.SID," +
                              " BM.IDBLOCKMASTER," +
                              " BM.BLOCKMASTERNAME," +
                              " BM.LANDCONCESSION," +
                              " LA.CONCESSIONNAME,"+
                              " BM.BLOCKORGANIZATION," +
                              " BM.SOILTYPE," +
                              " PV1.PARAMETERVALUENAME SOILTYPENAME," +
                              " BM.VEGETATION," +
                              " PV2.PARAMETERVALUENAME VEGETATIONNAME," +
                              " BM.TOPOGRAPH," +
                              " PV3.PARAMETERVALUENAME TOPOGRAPHNAME," +
                              " BM.HECTARAGE," +
                              " BM.HECTARAGE AS HECTARAGE_TEMP," +
                              " BM.INPUTBY," +
                              " BM.INPUTDATE," +
                              " BM.UPDATEBY," +
                              " BM.UPDATEDATE" +
                              " FROM" +
                              " BLOCKMASTER AS BM" +
                              " LEFT JOIN PARAMETERVALUE AS PV1" +
                              " ON BM.SOILTYPE = PV1.SID AND PV1.PARAMETER = 14" +
                              " LEFT JOIN PARAMETERVALUE AS PV2" +
                              " ON BM.VEGETATION = PV2.SID AND PV2.PARAMETER = 16" +
                              " LEFT JOIN PARAMETERVALUE AS PV3" +
                              " ON BM.TOPOGRAPH = PV3.SID AND PV3.PARAMETER = 15" +
                              " LEFT JOIN LANDCONCESSION AS LA" +
                              " ON BM.LANDCONCESSION = LA.SID" +
                              " WHERE BM.COMPANYSITE = " + CompanySite +
                              " ORDER BY" +
                              " BM.SID ASC").ToList();
        }

        public bool HasBlockUsage(int SID)
        {
            return this._db.Query<bool>("SELECT COUNT(*) FROM BLOCKUSAGE WHERE BLOCKMASTER = @SID", new { SID }).FirstOrDefault();
        }

        public void Remove(int SID)
        {
            var sqlQuery = (string.Format("Delete From BLOCKMASTER Where SID = {0}", SID));
            this._db.Execute(sqlQuery);
        }

        public BlockMaster Update(BlockMaster BLO)
        {
            var sqlQuery =
                "UPDATE BLOCKMASTER " +
                "SET IDBLOCKMASTER    = '" + BLO.IDBLOCKMASTER + "', " +
                "    BLOCKMASTERNAME  = '" + BLO.BLOCKMASTERNAME + "', " +
                "    LANDCONCESSION   = '" + BLO.LANDCONCESSION + "', " +
                "    BLOCKORGANIZATION   = '" + BLO.BLOCKORGANIZATION + "', " +
                "    SOILTYPE   = '" + BLO.SOILTYPE + "', " +
                "    VEGETATION   = '" + BLO.VEGETATION + "', " +
                "    TOPOGRAPH   = '" + BLO.TOPOGRAPH + "', " +
                "    HECTARAGE   = '" + BLO.HECTARAGE + "', " +
                "    COMPANYSITE   = '" + BLO.COMPANYSITE + "', " +
                "    UPDATEBY      = '" + Convert.ToInt32(BLO.UPDATEBY) + "', " +
                "    UPDATEDATE    = '" + BLO.UPDATEDATE + "' " +
                "WHERE SID = " + BLO.SID + "";
            this._db.Execute(sqlQuery, BLO);
            return BLO;
        }

        public void UpdateHectarage(int SID, int Hectarage)
        {
            var sqlQuery = "UPDATE BLOCKMASTER SET HECTARAGE = " + Hectarage + " WHERE SID = " + SID;
            this._db.Execute(sqlQuery);
        }
    }
}