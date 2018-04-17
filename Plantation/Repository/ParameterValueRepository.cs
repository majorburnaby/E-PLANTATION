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
    public class ParameterValueRepository : IParameterValueRepository
    {
        private IDbConnection _db = new SqlConnection(Constant.DatabaseConnection);

        public ParameterValue Add(ParameterValue PV, string userid)
        {
            var sqlQuery = @"INSERT INTO ParameterValue (IDPARAMETERVALUE, PARAMETERVALUENAME, PARAMETER, INPUTBY, INPUTDATE, UPDATEBY, UPDATEDATE) VALUES
                           ('" + PV.IDPARAMETERVALUE + @"', '" + PV.PARAMETERVALUENAME + "', '" + PV.PARAMETER + "', " + Convert.ToInt32(userid) + ", '" + DateTime.Now + @"', " + Convert.ToInt32(userid) + ", '" + DateTime.Now + @"'); " + "SELECT CAST(SCOPE_IDENTITY() as int)";
            var SID = _db.Query<int>(sqlQuery, PV).Single();
            PV.SID = SID;
            return PV;
        }

        public ParameterValue Find(int? SID)
        {
            return this._db.Query<ParameterValue>("SELECT * FROM ParameterValue WHERE SID = @SID", new { SID }).SingleOrDefault();
        }

        public List<ParameterValue> GetAll()
        {
            return this._db.Query<ParameterValue>("SELECT" +
                               " PV.SID," +
                               " PV.IDPARAMETERVALUE," +
                               " PV.PARAMETERVALUENAME," +
                               " PV.PARAMETER," +
                               " PV.INPUTBY," +
                               " PV.INPUTDATE," +
                               " PV.UPDATEBY," +
                               " PV.UPDATEDATE," +
                               " PARAMETER.PARAMETERNAME" +
                               " FROM" +
                               " PARAMETERVALUE AS PV" +
                               " INNER JOIN PARAMETER ON PV.PARAMETER = PARAMETER.SID" +
                               " ORDER BY" +
                               " PV.PARAMETER,SID ASC").ToList();
        }

        public void Remove(int SID)
        {
            var sqlQuery = (string.Format("Delete From PARAMETERVALUE Where SID = {0}", SID));
            this._db.Execute(sqlQuery);
        }

        public ParameterValue Update(ParameterValue PV, string userid)
        {
            var sqlQuery =
                string.Format(@"UPDATE PARAMETERVALUE SET IDPARAMETERVALUE = '{0}', PARAMETERVALUENAME = '{1}', PARAMETER = '{2}', UPDATEBY = {3}, UPDATEDATE = '{4}' WHERE SID = {5}", PV.IDPARAMETERVALUE, PV.PARAMETERVALUENAME, PV.PARAMETER, Convert.ToInt32(userid), DateTime.Now, PV.SID);
            this._db.Execute(sqlQuery, PV);
            return PV;
        }
    }
}