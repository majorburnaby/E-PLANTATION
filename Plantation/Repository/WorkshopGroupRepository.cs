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
    public class WorkshopGroupRepository : IWorkshopGroupRepository
    {
        private IDbConnection _db = new SqlConnection(Constant.DatabaseConnection);

        public WorkshopGroup Add(WorkshopGroup WG, string userid)
        {
            var sqlQuery = @"INSERT INTO WORKSHOPGROUP (IDWORKSHOPGROUP, WORKSHOPGROUPNAME, INPUTBY, INPUTDATE, UPDATEBY, UPDATEDATE) VALUES
                           ('" + WG.IDWORKSHOPGROUP + @"', '" + WG.WORKSHOPGROUPNAME + "', " + Convert.ToInt32(userid) + ", '" + DateTime.Now + @"', " + Convert.ToInt32(userid) + ", '" + DateTime.Now + @"'); " + "SELECT CAST(SCOPE_IDENTITY() as int)";
            var SID = _db.Query<int>(sqlQuery, WG).Single();
            WG.SID = SID;
            return WG;
        }

        public WorkshopGroup Find(int? SID)
        {
            return this._db.Query<WorkshopGroup>("SELECT * FROM WORKSHOPGROUP WHERE SID = @SID", new { SID }).SingleOrDefault();
        }

        public List<WorkshopGroup> GetAll()
        {
            return this._db.Query<WorkshopGroup>("SELECT" +
                               " WG.SID," +
                               " WG.IDWORKSHOPGROUP," +
                               " WG.WORKSHOPGROUPNAME," +
                               " WG.INPUTBY," +
                               " WG.INPUTDATE," +
                               " WG.UPDATEBY," +
                               " WG.UPDATEDATE" +
                               " FROM" +
                               " WORKSHOPGROUP AS WG" +
                               " ORDER BY" +
                               " WG.SID ASC").ToList();
        }

        public void Remove(int SID)
        {
            var sqlQuery = (string.Format("Delete From WorkshopGROUP Where SID = {0}", SID));
            this._db.Execute(sqlQuery);
        }

        public WorkshopGroup Update(WorkshopGroup WG, string userid)
        {
            var sqlQuery =
                string.Format(@"UPDATE WORKSHOPGROUP SET IDWORKSHOPGROUP = '{0}', WORKSHOPGROUPNAME = '{1}', UPDATEBY = {2}, UPDATEDATE = '{3}' WHERE SID = {4}", WG.IDWORKSHOPGROUP, WG.WORKSHOPGROUPNAME, Convert.ToInt32(userid), DateTime.Now, WG.SID);
            this._db.Execute(sqlQuery, WG);
            return WG;
        }
    }
}