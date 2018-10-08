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
    public class ParameterRepository : IParameterRepository
    {
        private IDbConnection _db = new SqlConnection(Constant.DatabaseConnection);

        public Parameter Add(Parameter PR, string userid)
        {
            var sqlQuery = @"INSERT INTO Parameter (IDPARAMETER, PARAMETERNAME, INPUTBY, INPUTDATE, UPDATEBY, UPDATEDATE) VALUES
                           ('" + PR.IDPARAMETER + @"', '" + PR.PARAMETERNAME + "', " + Convert.ToInt32(userid) + ", '" + DateTime.Now + @"', " + Convert.ToInt32(userid) + ", '" + DateTime.Now + @"'); " + "SELECT CAST(SCOPE_IDENTITY() as int)";
            var SID = _db.Query<int>(sqlQuery, PR).Single();
            PR.SID = SID;
            return PR;
        }

        public Parameter Find(int? SID)
        {
            return this._db.Query<Parameter>("SELECT * FROM Parameter WHERE SID = @SID", new { SID }).SingleOrDefault();
        }

        public List<Parameter> GetAll()
        {
            return this._db.Query<Parameter>("SELECT" +
                               " PR.SID," +
                               " PR.IDPARAMETER," +
                               " PR.PARAMETERNAME," +
                               " PR.INPUTBY," +
                               " PR.INPUTDATE," +
                               " PR.UPDATEBY," +
                               " PR.UPDATEDATE" +
                               " FROM" +
                               " PARAMETER AS PR" +
                               " ORDER BY" +
                               " PR.SID ASC").ToList();
        }

        public void Remove(int SID)
        {
            var sqlQuery = (string.Format("Delete From PARAMETER Where SID = {0}", SID));
            this._db.Execute(sqlQuery);
        }

        public Parameter Update(Parameter PR, string userid)
        {
            var sqlQuery =
                string.Format(@"UPDATE PARAMETER SET IDPARAMETER = '{0}', PARAMETERNAME = '{1}', UPDATEBY = {2}, UPDATEDATE = '{3}' WHERE SID = {4}", PR.IDPARAMETER, PR.PARAMETERNAME, Convert.ToInt32(userid), DateTime.Now, PR.SID);
            this._db.Execute(sqlQuery, PR);
            return PR;
        }
    }
}