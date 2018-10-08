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
    public class StationRepository : IStationRepository
    {
        private IDbConnection _db = new SqlConnection(Constant.DatabaseConnection);

        public Station Add(Station ST, string userid)
        {
            var sqlQuery = @"INSERT INTO STATION (IDSTATION, STATIONNAME, ISACTIVE, INPUTBY, INPUTDATE, UPDATEBY, UPDATEDATE) VALUES
                           ('" + ST.IDSTATION + @"', '" + ST.STATIONNAME + "', '" + ST.ISACTIVE + "', " + Convert.ToInt32(userid) + ", '" + DateTime.Now + @"', " + Convert.ToInt32(userid) + ", '" + DateTime.Now + @"'); " + "SELECT CAST(SCOPE_IDENTITY() as int)";
            var SID = _db.Query<int>(sqlQuery, ST).Single();
            ST.SID = SID;
            return ST;
        }

        public Station Find(int? SID)
        {
            return this._db.Query<Station>("SELECT * FROM STATION WHERE SID = @SID", new { SID }).SingleOrDefault();
        }

        public List<Station> GetAll()
        {
            return this._db.Query<Station>("SELECT" +
                               " ST.SID," +
                               " ST.IDSTATION," +
                               " ST.STATIONNAME," +
                               " ST.ISACTIVE," +
                               " ST.INPUTBY," +
                               " ST.INPUTDATE," +
                               " ST.UPDATEBY," +
                               " ST.UPDATEDATE" +
                               " FROM" +
                               " STATION AS ST" +
                               " ORDER BY" +
                               " ST.SID ASC").ToList();
        }

        public void Remove(int SID)
        {
            var sqlQuery = (string.Format("Delete From Station Where SID = {0}", SID));
            this._db.Execute(sqlQuery);
        }

        public Station Update(Station ST, string userid)
        {
            var sqlQuery =
                string.Format(@"UPDATE STATION SET IDSTATION = '{0}', STATIONNAME = '{1}', ISACTIVE = '{2}', UPDATEBY = {3}, UPDATEDATE = '{4}' WHERE SID = {5}", ST.IDSTATION, ST.STATIONNAME, ST.ISACTIVE, Convert.ToInt32(userid), DateTime.Now, ST.SID);
            this._db.Execute(sqlQuery, ST);
            return ST;
        }
    }
}