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
    public class JobRepository : IJobRepository
    {
        private IDbConnection _db = new SqlConnection(Constant.DatabaseConnection);

        public Job Add(Job JO, string userid)
        {
            var sqlQuery = @"INSERT INTO JOB (IDJOB, JOBNAME, ACCOUNT, JOBTYPE, JOBGROUP, COMPANY, COMPANYSITE, INPUTBY, INPUTDATE, UPDATEBY, UPDATEDATE) VALUES
                           ('" + JO.IDJOB + @"', '" + JO.JOBNAME + "', '" + JO.ACCOUNT + "', '" + JO.JOBTYPE + @"', '" + JO.JOBGROUP + @"', '" + JO.COMPANY + @"', '" + JO.COMPANYSITE + "', " + Convert.ToInt32(userid) + ", '" + DateTime.Now + @"', " + Convert.ToInt32(userid) + ", '" + DateTime.Now + @"'); " + "SELECT CAST(SCOPE_IDENTITY() as int)";
            var SID = _db.Query<int>(sqlQuery, JO).Single();
            JO.SID = SID;
            return JO;
        }

        public Job Find(int? SID)
        {
            return this._db.Query<Job>("SELECT * FROM JOB WHERE SID = @SID", new { SID }).SingleOrDefault();
        }

        public List<Job> GetAll()
        {
            return this._db.Query<Job>(" SELECT" +
                               " JO.SID," +
                               " JO.IDJOB," +
                               " JO.JOBNAME," +
                               " JO.ACCOUNT," +
                               " JO.JOBTYPE," +
                               " JO.JOBGROUP," +
                               " JO.COMPANYSITE," +
                               " JO.INPUTBY," +
                               " JO.INPUTDATE," +
                               " JO.UPDATEBY," +
                               " JO.UPDATEDATE," +
                               " JOBGROUP.JOBGROUPNAME," +
                               " JOBTYPE.JOBTYPENAME" +
                               " FROM" +
                               " JOB AS JO" +
                               " INNER JOIN JOBGROUP ON JO.JOBGROUP = JOBGROUP.SID" +
                               " INNER JOIN JOBTYPE ON JO.JOBTYPE = JOBTYPE.SID" +
                               " ORDER BY" +
                               " JO.SID ASC").ToList();
        }

        public void Remove(int SID)
        {
            var sqlQuery = (string.Format("Delete From JOB Where SID = {0}", SID));
            this._db.Execute(sqlQuery);
        }

        public Job Update(Job JO, string userid)
        {
            var sqlQuery =
                string.Format(@"UPDATE JOB SET IDJOB = '{0}', JOBNAME = '{1}', ACCOUNT = '{2}', JOBTYPE = '{3}', JOBGROUP = '{4}', COMPANY = '{5}', COMPANYSITE = '{6}', UPDATEBY = {7}, UPDATEDATE = '{8}' WHERE SID = {9}", JO.IDJOB, JO.JOBNAME, JO.ACCOUNT, JO.JOBTYPE, JO.JOBGROUP, JO.COMPANY, JO.COMPANYSITE, Convert.ToInt32(userid), DateTime.Now, JO.SID);
            this._db.Execute(sqlQuery, JO);
            return JO;
        }
    }
}