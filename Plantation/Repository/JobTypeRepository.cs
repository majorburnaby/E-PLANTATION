using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dapper;
using Plantation.Models.DB;
using System.Data;
using System.Data.SqlClient;
using Plantation.Utility;
using System;
using Plantation.Repository.Interface;

namespace Plantation.Repository
{
    public class JobTypeRepository : IJobTypeRepository
    {
        private IDbConnection _db = new SqlConnection(Constant.DatabaseConnection);

        public JobType Add(JobType JT, string userid)
        {
            var sqlQuery = @"INSERT INTO JobType (IDJOBTYPE, JOBTYPENAME, INPUTBY, INPUTDATE, UPDATEBY, UPDATEDATE) VALUES
                           ('" + JT.IDJOBTYPE + @"', '" + JT.IDJOBTYPE + "', " + Convert.ToInt32(userid) + ", '" + DateTime.Now + @"', " + Convert.ToInt32(userid) + ", '" + DateTime.Now + @"'); " + "SELECT CAST(SCOPE_IDENTITY() as int)";
            var SID = _db.Query<int>(sqlQuery, JT).Single();
            JT.SID = SID;
            return JT;
        }

        public JobType Find(int? SID)
        {
            return this._db.Query<JobType>("SELECT * FROM JobType WHERE SID = @SID", new { SID }).SingleOrDefault();
        }

        public List<JobType> GetAll()
        {
            return this._db.Query<JobType>("SELECT" +
                               " JT.SID," +
                               " JT.IDJOBTYPE," +
                               " JT.JOBTYPENAME," +
                               " JT.INPUTBY," +
                               " JT.INPUTDATE," +
                               " JT.UPDATEBY," +
                               " JT.UPDATEDATE" +
                               " FROM" +
                               " JOBTYPE AS JT" +
                               " ORDER BY" +
                               " JT.SID ASC").ToList();
        }

        public void Remove(int SID)
        {
            var sqlQuery = (string.Format("Delete From JOBTYPE Where SID = {0}", SID));
            this._db.Execute(sqlQuery);
        }

        public JobType Update(JobType JT, string userid)
        {
            var sqlQuery =
                string.Format(@"UPDATE JOBTYPE SET IDJOBTYPE = '{0}', JOBTYPENAME = '{1}', UPDATEBY = {2}, UPDATEDATE = '{3}' WHERE SID = {4}", JT.IDJOBTYPE, JT.JOBTYPENAME, Convert.ToInt32(userid), DateTime.Now, JT.SID);
            this._db.Execute(sqlQuery, JT);
            return JT;
        }
    }
}