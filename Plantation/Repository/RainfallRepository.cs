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
    public class RainfallRepository : IRainfallRepository
    {
        private IDbConnection _db = new SqlConnection(Constant.DatabaseConnection);

        public Rainfall Add(Rainfall RF, string userid)
        {
            var sqlQuery = @"INSERT INTO RAINFALL (RAINDATE, BLOCKORGANIZATION, RAINSTART, RAINEND, RAINQUANTITY, REMARK, COMPANYSITE, INPUTBY, INPUTDATE, UPDATEBY, UPDATEDATE) VALUES
                           ('" + RF.RAINDATE + @"', '" + RF.BLOCKORGANIZATION + "', '" + RF.RAINSTART + @"', '" + RF.RAINEND + "', '" + RF.RAINQUANTITY + @"', '" + RF.REMARK + @"', '" + RF.COMPANYSITE + @"'," + Convert.ToInt32(userid) + ", '" + DateTime.Now + @"', " + Convert.ToInt32(userid) + ", '" + DateTime.Now + @"'); " + "SELECT CAST(SCOPE_IDENTITY() as int)";
            var SID = _db.Query<int>(sqlQuery, RF).Single();
            RF.SID = SID;
            return RF;
        }

        public Rainfall Find(int? SID)
        {
            return this._db.Query<Rainfall>("SELECT * FROM RAINFALL WHERE SID = @SID", new { SID }).SingleOrDefault();
        }

        public List<Rainfall> GetAll()
        {
            return this._db.Query<Rainfall>("SELECT" +
                               " RF.SID," +
                               " RF.RAINDATE," +
                               " RF.BLOCKORGANIZATION," +
                               " RF.RAINSTART," +
                               " RF.RAINEND," +
                               " RF.RAINQUANTITY," +
                               " RF.REMARK," +
                               " RF.COMPANYSITE," +
                               " RF.INPUTBY," +
                               " RF.INPUTDATE," +
                               " RF.UPDATEBY," +
                               " RF.UPDATEDATE," +
                               " BLOCKORGANIZATION.BLOCKORGANIZATIONNAME" +
                               " FROM" +
                               " RAINFALL AS RF" +
                               " INNER JOIN BLOCKORGANIZATION ON RF.BLOCKORGANIZATION = BLOCKORGANIZATION.SID" +
                               " ORDER BY" +
                               " RF.SID ASC").ToList();
        }

        public List<Rainfall> GetAllByCompanySite(int? CompanySite)
        {
            return this._db.Query<Rainfall>("SELECT" +
                               " RF.SID," +
                               " RF.RAINDATE," +
                               " RF.BLOCKORGANIZATION," +
                               " RF.RAINSTART," +
                               " RF.RAINEND," +
                               " RF.RAINQUANTITY," +
                               " RF.REMARK," +
                               " RF.COMPANYSITE," +
                               " RF.INPUTBY," +
                               " RF.INPUTDATE," +
                               " RF.UPDATEBY," +
                               " RF.UPDATEDATE," +
                               " BLOCKORGANIZATION.BLOCKORGANIZATIONNAME" +
                               " FROM" +
                               " RAINFALL AS RF" +
                               " LEFT JOIN BLOCKORGANIZATION ON RF.BLOCKORGANIZATION = BLOCKORGANIZATION.SID" +
                               " WHERE RF.COMPANYSITE = @COMPANYSITE" +
                               " ORDER BY" +
                               " RF.SID ASC", new { CompanySite }).ToList();
        }
        public void Remove(int SID)
        {
            var sqlQuery = (string.Format("Delete From RAINFALL Where SID = {0}", SID));
            this._db.Execute(sqlQuery);
        }

        public Rainfall Update(Rainfall RF, string userid)
        {
            var sqlQuery =
                string.Format(@"UPDATE RAINFALL SET RAINDATE = '{0}', BLOCKORGANIZATION = '{1}', RAINSTART = '{2}', RAINEND = '{3}', RAINQUANTITY = '{4}', REMARK = '{5}', COMPANYSITE = '{6}', UPDATEBY = {7}, UPDATEDATE = '{8}' WHERE SID = {9}",
                RF.RAINDATE, RF.BLOCKORGANIZATION, RF.RAINSTART, RF.RAINEND, RF.RAINQUANTITY, RF.REMARK, RF.COMPANYSITE, Convert.ToInt32(userid), DateTime.Now, RF.SID);
            this._db.Execute(sqlQuery, RF);
            return RF;
        }
    }
}