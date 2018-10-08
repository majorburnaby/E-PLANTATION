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
    public class MachineMasterRepository : IMachineMasterRepository
    {
        private IDbConnection _db = new SqlConnection(Constant.DatabaseConnection);

        public MachineMaster Add(MachineMaster MA, string userid)
        {
            var sqlQuery = @"INSERT INTO MACHINE (IDMACHINE, MACHINENAME, STATION, OWNERSHIP, FIXEDASSET, PURCHASEDATE, PURCHASECOST, ISACTIVE, ISACTIVEDATE, COMPANYSITE, INPUTBY, INPUTDATE, UPDATEBY, UPDATEDATE) VALUES
                           ('" + MA.IDMACHINE + @"', '" + MA.MACHINENAME + "', '" + MA.STATION + "', '" + MA.OWNERSHIP + "', '" + MA.FIXEDASSET + "', '" + MA.PURCHASEDATE + "', '" + MA.PURCHASECOST + "', '" + MA.ISACTIVE + "', '" + MA.ISACTIVEDATE + "', '" + MA.COMPANYSITE + "', " + Convert.ToInt32(userid) + ", '" + DateTime.Now + @"', " + Convert.ToInt32(userid) + ", '" + DateTime.Now + @"'); " + "SELECT CAST(SCOPE_IDENTITY() as int)";
            var SID = _db.Query<int>(sqlQuery, MA).Single();
            MA.SID = SID;
            return MA;
        }

        public MachineMaster Find(int? SID)
        {
            return this._db.Query<MachineMaster>("SELECT * FROM MACHINE WHERE SID = @SID", new { SID }).SingleOrDefault();
        }

        public List<MachineMaster> GetAll()
        {
            return this._db.Query<MachineMaster>("SELECT" +
                               " MA.SID," +
                               " MA.IDMACHINE," +
                               " MA.MACHINENAME," +
                               " MA.STATION," +
                               " MA.OWNERSHIP," +
                               " MA.FIXEDASSET," +
                               " MA.PURCHASEDATE," +
                               " MA.PURCHASECOST," +
                               " MA.ISACTIVE," +
                               " MA.ISACTIVEDATE," +
                               " MA.COMPANYSITE," +
                               " MA.INPUTBY," +
                               " MA.INPUTDATE," +
                               " MA.UPDATEBY," +
                               " MA.UPDATEDATE," +
                               " STATION.STATIONNAME," +
                               " PARAMETERVALUE.PARAMETERVALUENAME," +
                               " FIXEDASSET.FIXEDASSETNAME" +
                               " FROM" +
                               " MACHINE AS MA" +
                               " LEFT JOIN STATION ON MA.STATION = STATION.SID" +
                               " LEFT JOIN PARAMETERVALUE ON MA.OWNERSHIP = PARAMETERVALUE.SID" +
                               " LEFT JOIN FIXEDASSET ON MA.FIXEDASSET = FIXEDASSET.SID" +
                               " ORDER BY" +
                               " MA.SID ASC").ToList();
        }

        public List<MachineMaster> GetAllByCompanySite(int? CompanySite)
        {
            return this._db.Query<MachineMaster>("SELECT" +
                               " MA.SID," +
                               " MA.IDMACHINE," +
                               " MA.MACHINENAME," +
                               " MA.STATION," +
                               " MA.OWNERSHIP," +
                               " MA.FIXEDASSET," +
                               " MA.PURCHASEDATE," +
                               " MA.PURCHASECOST," +
                               " MA.ISACTIVE," +
                               " MA.ISACTIVEDATE," +
                               " MA.COMPANYSITE," +
                               " MA.INPUTBY," +
                               " MA.INPUTDATE," +
                               " MA.UPDATEBY," +
                               " MA.UPDATEDATE," +
                               " STATION.STATIONNAME," +
                               " PARAMETERVALUE.PARAMETERVALUENAME," +
                               " FIXEDASSET.FIXEDASSETNAME" +
                               " FROM" +
                               " MACHINE AS MA" +
                               " LEFT JOIN STATION ON MA.STATION = STATION.SID" +
                               " LEFT JOIN PARAMETERVALUE ON MA.OWNERSHIP = PARAMETERVALUE.SID" +
                               " LEFT JOIN FIXEDASSET ON MA.FIXEDASSET = FIXEDASSET.SID" +
                               " WHERE MA.COMPANYSITE = @COMPANYSITE" +
                               " ORDER BY" +
                               " MA.SID ASC", new { CompanySite }).ToList();
        }

        public void Remove(int SID)
        {
            var sqlQuery = (string.Format("Delete From Machine Where SID = {0}", SID));
            this._db.Execute(sqlQuery);
        }

        public MachineMaster Update(MachineMaster MA, string userid)
        {
            var sqlQuery =
                string.Format(@"UPDATE MACHINE SET IDMACHINE = '{0}', MACHINENAME = '{1}', STATION = '{2}', OWNERSHIP = '{3}', FIXEDASSET = '{4}', PURCHASEDATE = '{5}', PURCHASECOST = '{6}', ISACTIVE = '{7}', ISACTIVEDATE = '{8}', COMPANYSITE = '{9}', UPDATEBY = {10}, UPDATEDATE = '{11}' WHERE SID = {12}", 
                MA.IDMACHINE, MA.MACHINENAME, MA.STATION, MA.OWNERSHIP, MA.FIXEDASSET, MA.PURCHASEDATE, MA.PURCHASECOST, MA.ISACTIVE, MA.ISACTIVEDATE, MA.COMPANYSITE, Convert.ToInt32(userid), DateTime.Now, MA.SID);
            this._db.Execute(sqlQuery, MA);
            return MA;
        }
    }
}