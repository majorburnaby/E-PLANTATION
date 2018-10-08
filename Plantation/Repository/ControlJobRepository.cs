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
            var sqlQuery = @"INSERT INTO CONTROLJOB (ITEMCODE, ITEMDESCRIPTION, CONTROLSYSTEM, JOB, ISACTIVE, INPUTBY, INPUTDATE, UPDATEBY, UPDATEDATE) VALUES
                           ('" + CJ.ITEMCODE + "', '" + CJ.ITEMDESCRIPTION + "', '" + CJ.CONTROLSYSTEM + @"', '" + CJ.JOB + "', '" + CJ.ISACTIVE + "', " + Convert.ToInt32(userid) + ", '" + DateTime.Now + @"', " + Convert.ToInt32(userid) + ", '" + DateTime.Now + @"'); " + "SELECT CAST(SCOPE_IDENTITY() as int)";
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
                               " CJ.ITEMCODE," +
                               " CJ.ITEMDESCRIPTION," +
                               " CJ.CONTROLSYSTEM," +
                               " CJ.JOB," +
                               " CJ.ISACTIVE," +
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
                string.Format(@"UPDATE CONTROLJOB SET ITEMCODE = '{0}', ITEMDESCRIPTION = '{1}', CONTROLSYSTEM = '{2}', JOB = '{3}', ISACTIVE = '{4}', UPDATEBY = {5}, UPDATEDATE = '{6}' WHERE SID = {7}", CJ.ITEMCODE, CJ.ITEMDESCRIPTION, CJ.CONTROLSYSTEM, CJ.JOB, CJ.ISACTIVE, Convert.ToInt32(userid), DateTime.Now, CJ.SID);
            this._db.Execute(sqlQuery, CJ);
            return CJ;
        }
    }
}