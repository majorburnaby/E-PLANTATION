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
    public class RoleMasterRepository : IRoleMasterRepository
    {
        private IDbConnection _db = new SqlConnection(Constant.DatabaseConnection);

        public RoleMaster Add(RoleMaster RM, string userid)
        {
            var sqlQuery = @"INSERT INTO RoleMaster (IDROLE, ROLENAME, ISACTIVE, INPUTBY, INPUTDATE, UPDATEBY, UPDATEDATE) VALUES
                           ('" + RM.IDROLE + @"', '" + RM.ROLENAME + "'," + RM.ISACTIVE + ", " + Convert.ToInt32(userid) + ", '" + DateTime.Now + @"', " + Convert.ToInt32(userid) + ", '" + DateTime.Now + @"'); " + "SELECT CAST(SCOPE_IDENTITY() as int)";
            var SID = _db.Query<int>(sqlQuery, RM).Single();
            RM.SID = SID;
            return RM;
        }

        public RoleMaster Find(int? SID)
        {
            return this._db.Query<RoleMaster>("SELECT * FROM RoleMaster WHERE SID = @SID", new { SID }).SingleOrDefault();
        }

        public List<RoleMaster> GetAll()
        {
            return this._db.Query<RoleMaster>("SELECT" +
                               " RM.SID," +
                               " RM.IDROLE," +
                               " RM.ROLENAME," +
                               " RM.ISACTIVE," +
                               " RM.INPUTBY," +
                               " RM.INPUTDATE," +
                               " RM.UPDATEBY," +
                               " RM.UPDATEDATE" +
                               " FROM" +
                               " ROLEMASTER AS RM" +
                               " ORDER BY" +
                               " RM.SID ASC").ToList();
        }

        public void Remove(int SID)
        {
            var sqlQuery = (string.Format("Delete From ROLEMASTER Where SID = {0}", SID));
            this._db.Execute(sqlQuery);
        }

        public RoleMaster Update(RoleMaster RM, string userid)
        {
            var sqlQuery =
                string.Format(@"UPDATE ROLEMASTER SET IDROLE = '{0}', ROLENAME = '{1}', ISACTIVE = '{2}', UPDATEBY = {3}, UPDATEDATE = '{4}' WHERE SID = {5}", RM.IDROLE, RM.ROLENAME, RM.ISACTIVE, Convert.ToInt32(userid), DateTime.Now, RM.SID);
            this._db.Execute(sqlQuery, RM);
            return RM;
        }
    }
}