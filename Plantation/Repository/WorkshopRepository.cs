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
    public class WorkshopRepository : IWorkshopRepository
    {
        private IDbConnection _db = new SqlConnection(Constant.DatabaseConnection);

        public Workshop Add(Workshop WS, string userid)
        {
            var sqlQuery = @"INSERT INTO WORKSHOP (IDWORKSHOP, WORKSHOPNAME, WORKSHOPGROUP, ISACTIVE, COMPANYSITE, INPUTBY, INPUTDATE, UPDATEBY, UPDATEDATE) VALUES
                           ('" + WS.IDWORKSHOP + @"', '" + WS.WORKSHOPNAME + "', '" + WS.WORKSHOPGROUP + "', '" + WS.ISACTIVE + "', '" + WS.COMPANYSITE + "', " + Convert.ToInt32(userid) + ", '" + DateTime.Now + @"', " + Convert.ToInt32(userid) + ", '" + DateTime.Now + @"'); " + "SELECT CAST(SCOPE_IDENTITY() as int)";
            var SID = _db.Query<int>(sqlQuery, WS).Single();
            WS.SID = SID;
            return WS;
        }

        public Workshop Find(int? SID)
        {
            return this._db.Query<Workshop>("SELECT * FROM WORKSHOP WHERE SID = @SID", new { SID }).SingleOrDefault();
        }

        public List<Workshop> GetAll()
        {
            return this._db.Query<Workshop>("SELECT" +
                               " WS.SID," +
                               " WS.IDWORKSHOP," +
                               " WS.WORKSHOPNAME," +
                               " WS.WORKSHOPGROUP," +
                               " WS.ISACTIVE," +
                               " WS.COMPANYSITE," +
                               " WS.INPUTBY," +
                               " WS.INPUTDATE," +
                               " WS.UPDATEBY," +
                               " WS.UPDATEDATE," +
                               " WORKSHOPGROUP.WORKSHOPGROUPNAME" +
                               " FROM" +
                               " WORKSHOP AS WS" +
                               " INNER JOIN WORKSHOPGROUP ON WS.WORKSHOPGROUP = WORKSHOPGROUP.SID" +
                               " ORDER BY" +
                               " WS.SID ASC").ToList();
        }

        public void Remove(int SID)
        {
            var sqlQuery = (string.Format("Delete From Workshop Where SID = {0}", SID));
            this._db.Execute(sqlQuery);
        }

        public Workshop Update(Workshop WS, string userid)
        {
            var sqlQuery =
                string.Format(@"UPDATE WORKSHOP SET IDWORKSHOP = '{0}', WORKSHOPNAME = '{1}', WORKSHOPGROUP = '{2}', ISACTIVE = '{3}', COMPANYSITE = '{4}', UPDATEBY = {5}, UPDATEDATE = '{6}' WHERE SID = {7}", WS.IDWORKSHOP, WS.WORKSHOPNAME, WS.WORKSHOPGROUP, WS.ISACTIVE, WS.COMPANYSITE, Convert.ToInt32(userid), DateTime.Now, WS.SID);
            this._db.Execute(sqlQuery, WS);
            return WS;
        }
    }
}