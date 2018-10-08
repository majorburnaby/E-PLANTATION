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
    public class DepartmentRepository : IDepartmentRepository
    {
        private IDbConnection _db = new SqlConnection(Constant.DatabaseConnection);

        public Department Add(Department DP, string userid)
        {
            var sqlQuery = @"INSERT INTO Department (IDDEPARTMENT, DEPARTMENTNAME, DESCRIPTION, INPUTBY, INPUTDATE, UPDATEBY, UPDATEDATE) VALUES
                           ('" + DP.IDDEPARTMENT + @"', '" + DP.DEPARTMENTNAME + "', '" + DP.DESCRIPTION + "', " + Convert.ToInt32(userid) + ", '" + DateTime.Now + @"', " + Convert.ToInt32(userid) + ", '" + DateTime.Now + @"'); " + "SELECT CAST(SCOPE_IDENTITY() as int)";
            var SID = _db.Query<int>(sqlQuery, DP).Single();
            DP.SID = SID;
            return DP;
        }

        public Department Find(int? SID)
        {
            return this._db.Query<Department>("SELECT * FROM Department WHERE SID = @SID", new { SID }).SingleOrDefault();
        }

        public List<Department> GetAll()
        {
            return this._db.Query<Department>("SELECT" +
                               " DP.SID," +
                               " DP.IDDEPARTMENT," +
                               " DP.DEPARTMENTNAME," +
                               " DP.DESCRIPTION," +
                               " DP.INPUTBY," +
                               " DP.INPUTDATE," +
                               " DP.UPDATEBY," +
                               " DP.UPDATEDATE" +
                               " FROM" +
                               " DEPARTMENT AS DP" +
                               " ORDER BY" +
                               " DP.SID ASC").ToList();
        }

        public void Remove(int SID)
        {
            var sqlQuery = (string.Format("Delete From DEPARTMENT Where SID = {0}", SID));
            this._db.Execute(sqlQuery);
        }

        public Department Update(Department DP, string userid)
        {
            var sqlQuery =
                string.Format(@"UPDATE DEPARTMENT SET IDDEPARTMENT = '{0}', DEPARTMENTNAME = '{1}', DESCRIPTION = '{2}', UPDATEBY = {3}, UPDATEDATE = '{4}' WHERE SID = {5}", DP.IDDEPARTMENT, DP.DEPARTMENTNAME, DP.DESCRIPTION, Convert.ToInt32(userid), DateTime.Now, DP.SID);
            this._db.Execute(sqlQuery, DP);
            return DP;
        }
    }
}