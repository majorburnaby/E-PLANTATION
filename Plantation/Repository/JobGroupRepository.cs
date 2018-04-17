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
    public class JobGroupRepository : IJobGroupRepository
    {
        private IDbConnection _db = new SqlConnection(Constant.DatabaseConnection);

        public JobGroup Add(JobGroup JG, string userid)
        {
            var sqlQuery = @"INSERT INTO JOBGROUP (IDJOBGROUP, JOBGROUPNAME, INPUTBY, INPUTDATE, UPDATEBY, UPDATEDATE) VALUES
                           ('" + JG.IDJOBGROUP + @"', '" + JG.JOBGROUPNAME + "', " + Convert.ToInt32(userid) + ", '" + DateTime.Now + @"', " + Convert.ToInt32(userid) + ", '" + DateTime.Now + @"'); " + "SELECT CAST(SCOPE_IDENTITY() as int)";
            var SID = _db.Query<int>(sqlQuery, JG).Single();
            JG.SID = SID;
            return JG;
        }

        public JobGroup Find(int? SID)
        {
            return this._db.Query<JobGroup>("SELECT * FROM JOBGROUP WHERE SID = @SID", new { SID }).SingleOrDefault();
        }

        public List<JobGroup> GetAll()
        {
            return this._db.Query<JobGroup>("SELECT" +
                               " JG.SID," +
                               " JG.IDJOBGROUP," +
                               " JG.JOBGROUPNAME," +
                               " JG.INPUTBY," +
                               " JG.INPUTDATE," +
                               " JG.UPDATEBY," +
                               " JG.UPDATEDATE" +
                               " FROM" +
                               " JOBGROUP AS JG" +
                               " ORDER BY" +
                               " JG.SID ASC").ToList();
        }

        public void Remove(int SID)
        {
            var sqlQuery = (string.Format("Delete From JOBGROUP Where SID = {0}", SID));
            this._db.Execute(sqlQuery);
        }

        public JobGroup Update(JobGroup JG, string userid)
        {
            var sqlQuery =
                string.Format(@"UPDATE JOBGROUP SET IDJOBGROUP = '{0}', JOBGROUPNAME = '{1}', UPDATEBY = {2}, UPDATEDATE = '{3}' WHERE SID = {4}", JG.IDJOBGROUP, JG.JOBGROUPNAME, Convert.ToInt32(userid), DateTime.Now, JG.SID);
            this._db.Execute(sqlQuery, JG);
            return JG;
        }
    }
}