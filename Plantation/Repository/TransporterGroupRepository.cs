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
    public class TransporterGroupRepository : ITransporterGroupRepository
    {
        private IDbConnection _db = new SqlConnection(Constant.DatabaseConnection);

        public TransporterGroup Add(TransporterGroup TG, string userid)
        {
            var sqlQuery = @"INSERT INTO TransporterGroup (IDTRANSPORTERGROUP, TRANSPORTERGROUPNAME, INPUTBY, INPUTDATE, UPDATEBY, UPDATEDATE) VALUES
                           ('" + TG.IDTRANSPORTERGROUP + @"', '" + TG.TRANSPORTERGROUPNAME + "', " + Convert.ToInt32(userid) + ", '" + DateTime.Now + @"', " + Convert.ToInt32(userid) + ", '" + DateTime.Now + @"'); " + "SELECT CAST(SCOPE_IDENTITY() as int)";
            var SID = _db.Query<int>(sqlQuery, TG).Single();
            TG.SID = SID;
            return TG;
        }

        public TransporterGroup Find(int? SID)
        {
            return this._db.Query<TransporterGroup>("SELECT * FROM TRANSPORTERGROUP WHERE SID = @SID", new { SID }).SingleOrDefault();
        }

        public List<TransporterGroup> GetAll()
        {
            return this._db.Query<TransporterGroup>("SELECT" +
                               " TG.SID," +
                               " TG.IDTRANSPORTERGROUP," +
                               " TG.TRANSPORTERGROUPNAME," +
                               " TG.INPUTBY," +
                               " TG.INPUTDATE," +
                               " TG.UPDATEBY," +
                               " TG.UPDATEDATE" +
                               " FROM" +
                               " TRANSPORTERGROUP AS TG" +
                               " ORDER BY" +
                               " TG.SID ASC").ToList();
        }

        public void Remove(int SID)
        {
            var sqlQuery = (string.Format("Delete From TRANSPORTERGROUP Where SID = {0}", SID));
            this._db.Execute(sqlQuery);
        }

        public TransporterGroup Update(TransporterGroup TG, string userid)
        {
            var sqlQuery =
               string.Format(@"UPDATE TRANSPORTERGROUP SET IDTRANSPORTERGROUP = '{0}', TRANSPORTERGROUPNAME = '{1}', UPDATEBY = {2}, UPDATEDATE = '{3}' WHERE SID = {4}", TG.IDTRANSPORTERGROUP, TG.TRANSPORTERGROUPNAME, Convert.ToInt32(userid), DateTime.Now, TG.SID);
            this._db.Execute(sqlQuery, TG);
            return TG;
        }
    }
}