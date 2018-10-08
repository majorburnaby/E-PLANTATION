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
    public class VehicleMasterRepository : IVehicleMasterRepository
    {
        private IDbConnection _db = new SqlConnection(Constant.DatabaseConnection);

        public VehicleMaster Add(VehicleMaster VM, string userid)
        {
            var sqlQuery = @"INSERT INTO VEHICLE (IDVEHICLE, VEHICLENAME, VEHICLEGROUP, OWNERSHIP, FIXEDASSET, PURCHASEDATE, PURCHASECOST, ISACTIVE, ISACTIVEDATE, COMPANYSITE, INPUTBY, INPUTDATE, UPDATEBY, UPDATEDATE) VALUES
                           ('" + VM.IDVEHICLE + @"', '" + VM.VEHICLENAME + "', '" + VM.VEHICLEGROUP + "', '" + VM.OWNERSHIP + "', '" + VM.FIXEDASSET + "', '" + VM.PURCHASEDATE + "', '" + VM.PURCHASECOST + "', '" + VM.ISACTIVE + "', '" + VM.ISACTIVEDATE + "', '" + VM.COMPANYSITE + "', " + Convert.ToInt32(userid) + ", '" + DateTime.Now + @"', " + Convert.ToInt32(userid) + ", '" + DateTime.Now + @"'); " + "SELECT CAST(SCOPE_IDENTITY() as int)";
            var SID = _db.Query<int>(sqlQuery, VM).Single();
            VM.SID = SID;
            return VM;
        }

        public VehicleMaster Find(int? SID)
        {
            return this._db.Query<VehicleMaster>("SELECT * FROM VEHICLE WHERE SID = @SID", new { SID }).SingleOrDefault();
        }

        public List<VehicleMaster> GetAll()
        {
            return this._db.Query<VehicleMaster>("SELECT" +
                               " VM.SID," +
                               " VM.IDVEHICLE," +
                               " VM.VEHICLENAME," +
                               " VM.VEHICLEGROUP," +
                               " VM.OWNERSHIP," +
                               " VM.FIXEDASSET," +
                               " VM.PURCHASEDATE," +
                               " VM.PURCHASECOST," +
                               " VM.ISACTIVE," +
                               " VM.ISACTIVEDATE," +
                               " VM.COMPANYSITE," +
                               " VM.INPUTBY," +
                               " VM.INPUTDATE," +
                               " VM.UPDATEBY," +
                               " VM.UPDATEDATE" +
                               " FROM" +
                               " VEHICLE AS VM" +
                               " LEFT JOIN VEHICLEGROUP ON VM.VEHICLEGROUP = VEHICLEGROUP.SID" +
                               " LEFT JOIN PARAMETERVALUE ON VM.OWNERSHIP = PARAMETERVALUE.SID" +
                               " LEFT JOIN FIXEDASSET ON VM.FIXEDASSET = FIXEDASSET.SID" +
                               " ORDER BY" +
                               " VM.SID ASC").ToList();
        }

        public List<VehicleMaster> GetAllByCompanySite(int? CompanySite)
        {
            return this._db.Query<VehicleMaster>("SELECT" +
                               " VM.SID," +
                               " VM.IDVEHICLE," +
                               " VM.VEHICLENAME," +
                               " VM.VEHICLEGROUP," +
                               " VM.OWNERSHIP," +
                               " VM.FIXEDASSET," +
                               " VM.PURCHASEDATE," +
                               " VM.PURCHASECOST," +
                               " VM.ISACTIVE," +
                               " VM.ISACTIVEDATE," +
                               " VM.COMPANYSITE," +
                               " VM.INPUTBY," +
                               " VM.INPUTDATE," +
                               " VM.UPDATEBY," +
                               " VM.UPDATEDATE," +
                               " VEHICLEGROUP.VEHICLEGROUPNAME," +
                               " PARAMETERVALUE.PARAMETERVALUENAME," +
                               " FIXEDASSET.FIXEDASSETNAME" +
                               " FROM" +
                               " VEHICLE AS VM" +
                               " LEFT JOIN VEHICLEGROUP ON VM.VEHICLEGROUP = VEHICLEGROUP.SID" +
                               " LEFT JOIN PARAMETERVALUE ON VM.OWNERSHIP = PARAMETERVALUE.SID" +
                               " LEFT JOIN FIXEDASSET ON VM.FIXEDASSET = FIXEDASSET.SID" +
                               " WHERE VM.COMPANYSITE = @COMPANYSITE" +
                               " ORDER BY" +
                               " VM.SID ASC", new { CompanySite }).ToList();
        }

        public void Remove(int SID)
        {
            var sqlQuery = (string.Format("Delete From VEHICLE Where SID = {0}", SID));
            this._db.Execute(sqlQuery);
        }

        public VehicleMaster Update(VehicleMaster VM, string userid)
        {
            var sqlQuery =
                string.Format(@"UPDATE VEHICLE SET IDVEHICLE = '{0}', VEHICLENAME = '{1}', VEHICLEGROUP = '{2}', OWNERSHIP = '{3}', FIXEDASSET = '{4}', PURCHASEDATE = '{5}', PURCHASECOST = '{6}', ISACTIVE = '{7}', ISACTIVEDATE = '{8}', COMPANYSITE = '{9}', UPDATEBY = {10}, UPDATEDATE = '{11}' WHERE SID = {12}", 
                VM.IDVEHICLE, VM.VEHICLENAME, VM.VEHICLEGROUP, VM.OWNERSHIP, VM.FIXEDASSET, VM.PURCHASEDATE, VM.PURCHASECOST, VM.ISACTIVE, VM.ISACTIVEDATE, VM.COMPANYSITE, Convert.ToInt32(userid), DateTime.Now, VM.SID);
            this._db.Execute(sqlQuery, VM);
            return VM;
        }
    }
}