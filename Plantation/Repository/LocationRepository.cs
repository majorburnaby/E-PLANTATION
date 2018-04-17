using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dapper;
using Plantation.Models.DB;
using System.Data;
using System.Data.SqlClient;
using Plantation.Utility;
using System;
using Plantation.Repository.Interface;

namespace Plantation.Repository
{
    public class LocationRepository : ILocationRepository
    {
        private IDbConnection _db = new SqlConnection(Constant.DatabaseConnection);

        public Location Add(Location LC, string userid)
        {
            var sqlQuery = @"INSERT INTO Location (IDLOCATION, LOCATIONNAME, INPUTBY, INPUTDATE, UPDATEBY, UPDATEDATE) VALUES
                           ('" + LC.IDLOCATION + @"', '" + LC.LOCATIONNAME + "', " + Convert.ToInt32(userid) + ", '" + DateTime.Now + @"', " + Convert.ToInt32(userid) + ", '" + DateTime.Now + @"'); " + "SELECT CAST(SCOPE_IDENTITY() as int)";
            var SID = _db.Query<int>(sqlQuery, LC).Single();
            LC.SID = SID;
            return LC;
        }

        public Location Find(int? SID)
        {
            return this._db.Query<Location>("SELECT * FROM Location WHERE SID = @SID", new { SID }).SingleOrDefault();
        }

        public List<Location> GetAll()
        {
            return this._db.Query<Location>(" SELECT" +
                               " LC.SID," +
                               " LC.IDLOCATION," +
                               " LC.LOCATIONNAME," +
                               " LC.INPUTBY," +
                               " LC.INPUTDATE," +
                               " LC.UPDATEBY," +
                               " LC.UPDATEDATE" +
                               " FROM" +
                               " LOCATION AS LC" +
                               " ORDER BY" +
                               " LC.SID ASC").ToList();
        }

        public void Remove(int SID)
        {
            var sqlQuery = (string.Format("Delete From LOCATION Where SID = {0}", SID));
            this._db.Execute(sqlQuery);
        }

        public Location Update(Location LC, string userid)
        {
            var sqlQuery =
                string.Format(@"UPDATE LOCATION SET IDLOCATION = '{0}', LOCATIONNAME = '{1}', UPDATEBY = {2}, UPDATEDATE = '{3}' WHERE SID = {4}", LC.IDLOCATION, LC.LOCATIONNAME, Convert.ToInt32(userid), DateTime.Now, LC.SID);
            this._db.Execute(sqlQuery, LC);
            return LC;
        }
    }
}