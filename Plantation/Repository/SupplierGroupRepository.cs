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
    public class SupplierGroupRepository : ISupplierGroupRepository
    {
        private IDbConnection _db = new SqlConnection(Constant.DatabaseConnection);

        public SupplierGroup Add(SupplierGroup SPG, string userid)
        {
            var sqlQuery = @"INSERT INTO SUPPLIERGROUP (IDSUPPLIERGROUP, SUPPLIERGROUPNAME, INPUTBY, INPUTDATE, UPDATEBY, UPDATEDATE) VALUES
                           ('" + SPG.IDSUPPLIERGROUP + @"', '" + SPG.SUPPLIERGROUPNAME + "', " + Convert.ToInt32(userid) + ", '" + DateTime.Now + @"', " + Convert.ToInt32(userid) + ", '" + DateTime.Now + @"'); " + "SELECT CAST(SCOPE_IDENTITY() as int)";
            var SID = _db.Query<int>(sqlQuery, SPG).Single();
            SPG.SID = SID;
            return SPG;
        }

        public SupplierGroup Find(int? SID)
        {
            return this._db.Query<SupplierGroup>("SELECT * FROM SUPPLIERGROUP WHERE SID = @SID", new { SID }).SingleOrDefault();
        }

        public List<SupplierGroup> GetAll()
        {
            return this._db.Query<SupplierGroup>("SELECT" +
                               " SPG.SID," +
                               " SPG.IDSUPPLIERGROUP," +
                               " SPG.SUPPLIERGROUPNAME," +
                               " SPG.INPUTBY," +
                               " SPG.INPUTDATE," +
                               " SPG.UPDATEBY," +
                               " SPG.UPDATEDATE" +
                               " FROM" +
                               " SUPPLIERGROUP AS SPG" +
                               " ORDER BY" +
                               " SPG.SID ASC").ToList();
        }

        public void Remove(int SID)
        {
            var sqlQuery = (string.Format("Delete From SupplierGroup Where SID = {0}", SID));
            this._db.Execute(sqlQuery);
        }

        public SupplierGroup Update(SupplierGroup SPG, string userid)
        {
            var sqlQuery =
                "UPDATE SUPPLIERGROUP " +
                "SET IDSUPPLIERGROUP    = '" + SPG.IDSUPPLIERGROUP + "', " +
                "    SUPPLIERGROUPNAME  = '" + SPG.SUPPLIERGROUPNAME + "', " +
                "    UPDATEBY      = '" + Convert.ToInt32(userid) + "', " +
                "    UPDATEDATE    = '" + DateTime.Now + "' " +
                "WHERE SID = " + SPG.SID + "";
            this._db.Execute(sqlQuery, SPG);
            return SPG;
        }
    }
}