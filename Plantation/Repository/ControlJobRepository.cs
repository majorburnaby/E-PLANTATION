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
    public class ControlJobRepository : IControlJobRepository
    {
        private IDbConnection _db = new SqlConnection(Constant.DatabaseConnection);

        public ControlJob Add(ControlJob CJ, string userid)
        {
            var sqlQuery = @"INSERT INTO CONTROLJOB (CONTROLSYSTEM, ITEMCODE, ITEMDESCRIPTION, REMARKS, ISACTIVE, ISACTIVEDATE, COMPANY, COMPANYSITE, INPUTBY, INPUTDATE, UPDATEBY, UPDATEDATE) VALUES
                           ('" + CJ.CONTROLSYSTEM + @"', '" + CJ.ITEMCODE + "', '" + CJ.ITEMDESCRIPTION + "', '" + CJ.REMARKS + "', '" + CJ.ISACTIVE + "', '" + CJ.ISACTIVEDATE + "', '" + CJ.COMPANY + "', '" + CJ.COMPANYSITE + "', " + Convert.ToInt32(userid) + ", '" + DateTime.Now + @"', " + Convert.ToInt32(userid) + ", '" + DateTime.Now + @"'); " + "SELECT CAST(SCOPE_IDENTITY() as int)";
            var SID = _db.Query<int>(sqlQuery, CJ).Single();
            CJ.SID = SID;
            return CJ;
        }

        public ControlJob Find(int? SID)
        {
            return this._db.Query<ControlJob>("SELECT * FROM CONTROLJOB WHERE SID = @SID", new { SID }).SingleOrDefault();
        }

        public List<ControlJob> GetAll()
        {
            return this._db.Query<ControlJob>("SELECT" +
                               " CJ.SID," +
                               " CJ.CONTROLSYSTEM," +
                               " CJ.ITEMCODE," +
                               " CJ.ITEMDESCRIPTION," +
                               " CJ.REMARKS," +
                               " CJ.ISACTIVE," +
                               " CJ.ISACTIVEDATE," +
                               " CJ.COMPANY," +
                               " CJ.COMPANYSITE," +
                               " CJ.INPUTBY," +
                               " CJ.INPUTDATE," +
                               " CJ.UPDATEBY," +
                               " CJ.UPDATEDATE" +
                               " FROM" + 
                               " CONTROLJOB AS CJ" +
                               " ORDER BY" +
                               " CJ.SID ASC").ToList();
        }

        public void Remove(int SID)
        {
            var sqlQuery = (string.Format("Delete From CONTROLJOB Where SID = {0}", SID));
            this._db.Execute(sqlQuery);
        }

        public ControlJob Update(ControlJob CJ, string userid)
        {
            var sqlQuery =
                string.Format(@"UPDATE CONTROLJOB SET CONTROLSYSTEM = '{0}', ITEMCODE = '{1}', ITEMDESCRIPTION = '{2}', REMARKS = '{3}', ISACTIVE = '{4}', ISACTIVEDATE = '{5}', COMPANY = '{6}', COMPANYSITE = '{7}', UPDATEBY = {8}, UPDATEDATE = '{9}' WHERE SID = {10}", CJ.CONTROLSYSTEM, CJ.ITEMCODE, CJ.ITEMDESCRIPTION, CJ.REMARKS, CJ.ISACTIVE, CJ.ISACTIVEDATE, CJ.COMPANY, CJ.COMPANYSITE, Convert.ToInt32(userid), DateTime.Now, CJ.SID);
            this._db.Execute(sqlQuery, CJ);
            return CJ;
        }
    }
}