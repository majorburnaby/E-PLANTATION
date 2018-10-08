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
    public class ContractorGroupRepository : IContractorGroupRepository
    {
        private IDbConnection _db = new SqlConnection(Constant.DatabaseConnection);

        public ContractorGroup Add(ContractorGroup CTG, string userid)
        {
            var sqlQuery = @"INSERT INTO contractorGroup (IDCONTRACTORGROUP, CONTRACTORGROUPNAME, INPUTBY, INPUTDATE, UPDATEBY, UPDATEDATE) VALUES
                           ('" + CTG.IDCONTRACTORGROUP + @"', '" + CTG.CONTRACTORGROUPNAME + "', " + Convert.ToInt32(userid) + ", '" + DateTime.Now + @"', " + Convert.ToInt32(userid) + ", '" + DateTime.Now + @"'); " + "SELECT CAST(SCOPE_IDENTITY() as int)";
            var SID = _db.Query<int>(sqlQuery, CTG).Single();
            CTG.SID = SID;
            return CTG;
        }

        public ContractorGroup Find(int? SID)
        {
            return this._db.Query<ContractorGroup>("SELECT * FROM CONTRACTORGROUP WHERE SID = @SID", new { SID }).SingleOrDefault();
        }

        public List<ContractorGroup> GetAll()
        {
            return this._db.Query<ContractorGroup>("SELECT" +
                               " CTG.SID," +
                               " CTG.IDCONTRACTORGROUP," +
                               " CTG.CONTRACTORGROUPNAME," +
                               " CTG.INPUTBY," +
                               " CTG.INPUTDATE," +
                               " CTG.UPDATEBY," +
                               " CTG.UPDATEDATE" +
                               " FROM" +
                               " CONTRACTORGROUP AS CTG" +
                               " ORDER BY" +
                               " CTG.SID ASC").ToList();
        }

        public void Remove(int SID)
        {
            var sqlQuery = (string.Format("Delete From contractorGroup Where SID = {0}", SID));
            this._db.Execute(sqlQuery);
        }

        public ContractorGroup Update(ContractorGroup CTG, string userid)
        {
            var sqlQuery =
                string.Format(@"UPDATE CONTRACTORGROUP SET IDCONTRACTORGROUP = '{0}', CONTRACTORGROUPNAME = '{1}', UPDATEBY = {2}, UPDATEDATE = '{3}' WHERE SID = {4}",
                CTG.IDCONTRACTORGROUP, CTG.CONTRACTORGROUPNAME, Convert.ToInt32(userid), DateTime.Now, CTG.SID);
            this._db.Execute(sqlQuery, CTG);
            return CTG;
        }
    }
}