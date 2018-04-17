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
    public class CustomerGroupRepository : ICustomerGroupRepository
    {
        private IDbConnection _db = new SqlConnection(Constant.DatabaseConnection);

        public CustomerGroup Add(CustomerGroup CSG, string userid)
        {
            var sqlQuery = @"INSERT INTO CustomerGroup (IDCUSTOMERGROUP, CUSTOMERGROUPNAME, CONTROLJOB, INPUTBY, INPUTDATE, UPDATEBY, UPDATEDATE) VALUES
                           ('" + CSG.IDCUSTOMERGROUP + @"', '" + CSG.CUSTOMERGROUPNAME + "', '" + CSG.CONTROLJOB + @"', " + Convert.ToInt32(userid) + ", '" + DateTime.Now + @"', " + Convert.ToInt32(userid) + ", '" + DateTime.Now + @"'); " + "SELECT CAST(SCOPE_IDENTITY() as int)";
            var SID = _db.Query<int>(sqlQuery, CSG).Single();
            CSG.SID = SID;
            return CSG;
        }

        public CustomerGroup Find(int? SID)
        {
            return this._db.Query<CustomerGroup>("SELECT * FROM CUSTOMERGROUP WHERE SID = @SID", new { SID }).SingleOrDefault();
        }

        public List<CustomerGroup> GetAll()
        {
            return this._db.Query<CustomerGroup>("SELECT" +
                               " CSG.SID," +
                               " CSG.IDCUSTOMERGROUP," +
                               " CSG.CUSTOMERGROUPNAME," +
                               " CSG.CONTROLJOB," +
                               " CSG.INPUTBY," +
                               " CSG.INPUTDATE," +
                               " CSG.UPDATEBY," +
                               " CSG.UPDATEDATE" +
                               " FROM" +
                               " CUSTOMERGROUP AS CSG" +
                               " ORDER BY" +
                               " CSG.SID ASC").ToList();
        }

        public void Remove(int SID)
        {
            var sqlQuery = (string.Format("Delete From CustomerGroup Where SID = {0}", SID));
            this._db.Execute(sqlQuery);
        }

        public CustomerGroup Update(CustomerGroup CSG, string userid)
        {
            var sqlQuery =
                string.Format(@"UPDATE CUSTOMERGROUP SET IDCUSTOMERGROUP = '{0}', CUSTOMERGROUPNAME = '{1}', CONTROLJOB = '{2}', UPDATEBY = {3}, UPDATEDATE = '{4}' WHERE SID = {5}",
                CSG.IDCUSTOMERGROUP, CSG.CUSTOMERGROUPNAME, CSG.CONTROLJOB, Convert.ToInt32(userid), DateTime.Now, CSG.SID);
            this._db.Execute(sqlQuery, CSG);
            return CSG;
        }
    }
}