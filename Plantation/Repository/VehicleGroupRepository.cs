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
    public class VehicleGroupRepository : IVehicleGroupRepository
    {
        private IDbConnection _db = new SqlConnection(Constant.DatabaseConnection);

        public VehicleGroup Add(VehicleGroup VG, string userid)
        {
            var sqlQuery = @"INSERT INTO VEHICLEGROUP (IDVEHICLEGROUP, VEHICLEGROUPNAME, INPUTBY, INPUTDATE, UPDATEBY, UPDATEDATE) VALUES
                           ('" + VG.IDVEHICLEGROUP + @"', '" + VG.VEHICLEGROUPNAME + "', " + Convert.ToInt32(userid) + ", '" + DateTime.Now + @"', " + Convert.ToInt32(userid) + ", '" + DateTime.Now + @"'); " + "SELECT CAST(SCOPE_IDENTITY() as int)";
            var SID = _db.Query<int>(sqlQuery, VG).Single();
            VG.SID = SID;
            return VG;
        }

        public VehicleGroup Find(int? SID)
        {
            return this._db.Query<VehicleGroup>("SELECT * FROM VEHICLEGROUP WHERE SID = @SID", new { SID }).SingleOrDefault();
        }

        public List<VehicleGroup> GetAll()
        {
            return this._db.Query<VehicleGroup>("SELECT" +
                               " VG.SID," +
                               " VG.IDVEHICLEGROUP," +
                               " VG.VEHICLEGROUPNAME," +
                               " VG.INPUTBY," +
                               " VG.INPUTDATE," +
                               " VG.UPDATEBY," +
                               " VG.UPDATEDATE" +
                               " FROM" +
                               " VEHICLEGROUP AS VG" +
                               " ORDER BY" +
                               " VG.SID ASC").ToList();
        }

        public void Remove(int SID)
        {
            var sqlQuery = (string.Format("Delete From VEHICLEGROUP Where SID = {0}", SID));
            this._db.Execute(sqlQuery);
        }

        public VehicleGroup Update(VehicleGroup VG, string userid)
        {
            var sqlQuery =
                string.Format(@"UPDATE VEHICLEGROUP SET IDVEHICLEGROUP = '{0}', VEHICLEGROUPNAME = '{1}', UPDATEBY = {2}, UPDATEDATE = '{3}' WHERE SID = {4}", VG.IDVEHICLEGROUP, VG.VEHICLEGROUPNAME, Convert.ToInt32(userid), DateTime.Now, VG.SID);
            this._db.Execute(sqlQuery, VG);
            return VG;
        }
    }
}