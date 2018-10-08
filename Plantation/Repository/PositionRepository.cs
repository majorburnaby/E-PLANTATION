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
    public class PositionRepository : IPositionRepository
    {
        private IDbConnection _db = new SqlConnection(Constant.DatabaseConnection);

        public Position Add(Position PS, string userid)
        {
            var sqlQuery = @"INSERT INTO Position (IDPOSITION, POSITIONNAME, DESCRIPTION, STATUS, INPUTBY, INPUTDATE, UPDATEBY, UPDATEDATE) VALUES
                           ('" + PS.IDPOSITION + @"', '" + PS.POSITIONNAME + "', '" + PS.DESCRIPTION + "'," + PS.STATUS + ", " + Convert.ToInt32(userid) + ", '" + DateTime.Now + @"', " + Convert.ToInt32(userid) + ", '" + DateTime.Now + @"'); " + "SELECT CAST(SCOPE_IDENTITY() as int)";
            var SID = _db.Query<int>(sqlQuery, PS).Single();
            PS.SID = SID;
            return PS;
        }

        public Position Find(int? SID)
        {
            return this._db.Query<Position>("SELECT * FROM Position WHERE SID = @SID", new { SID }).SingleOrDefault();
        }

        public List<Position> GetAll()
        {
            return this._db.Query<Position>("SELECT" +
                               " PS.SID," +
                               " PS.IDPOSITION," +
                               " PS.POSITIONNAME," +
                               " PS.DESCRIPTION," +
                               " PS.STATUS," +
                               " PS.INPUTBY," +
                               " PS.INPUTDATE," +
                               " PS.UPDATEBY," +
                               " PS.UPDATEDATE" +
                               " FROM" +
                               " POSITION AS PS" +
                               " ORDER BY" +
                               " PS.SID ASC").ToList();
        }

        public void Remove(int SID)
        {
            var sqlQuery = (string.Format("Delete From POSITION Where SID = {0}", SID));
            this._db.Execute(sqlQuery);
        }

        public Position Update(Position PS, string userid)
        {
            var sqlQuery =
                string.Format(@"UPDATE POSITION SET IDPOSITION = '{0}', POSITIONNAME = '{1}', DESCRIPTION = '{2}', STATUS = {3}, UPDATEBY = {4}, UPDATEDATE = '{5}' WHERE SID = {6}", PS.IDPOSITION, PS.POSITIONNAME, PS.DESCRIPTION, PS.STATUS, Convert.ToInt32(userid), DateTime.Now, PS.SID);
            this._db.Execute(sqlQuery, PS);
            return PS;
        }
    }
}