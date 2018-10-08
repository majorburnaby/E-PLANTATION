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
    public class PeriodControlRepository : IPeriodControlRepository
    {
        private IDbConnection _db = new SqlConnection(Constant.DatabaseConnection);

        public PeriodControl Add(PeriodControl PC, string userid)
        {
            var sqlQuery = @"INSERT INTO PERIODCONTROL (SYSTEM, LASTPERIODPOSTED, CURRENTDATE, POSTINGPOSITION, CURRENTACCOUNTINGYEAR, CURRENTACCOUNTINGPERIOD, CLOSEACCOUNTINGYEAR, CLOSEACCOUNTINGPERIOD, NEXTACCOUNTINGYEAR, NEXTACCOUNTINGPERIOD, COMPANYSITE, INPUTBY, INPUTDATE, UPDATEBY, UPDATEDATE) 
                            VALUES('" + PC.SYSTEM + "', '" + PC.LASTPERIODPOSTED + "', '" + PC.CURRENTDATE + "', '" + PC.POSTINGPOSITION + "', '" + PC.CURRENTACCOUNTINGYEAR + "', '" + PC.CURRENTACCOUNTINGPERIOD + "', '" + PC.CLOSEACCOUNTINGYEAR + "', '" + PC.CLOSEACCOUNTINGPERIOD + "', '" + PC.NEXTACCOUNTINGYEAR + "', '" + PC.NEXTACCOUNTINGPERIOD + "', '" + PC.COMPANYSITE + "','" + userid + "', '" + DateTime.Now + "', '" + userid + "', '" + DateTime.Now + "'); " + "SELECT CAST(SCOPE_IDENTITY() as int)";
            var SYSTEM = _db.Query<string>(sqlQuery, PC).Single();
            PC.SYSTEM = SYSTEM;
            return PC;
        }

        public PeriodControl Find()
        {
            return this._db.Query<PeriodControl>("SELECT * FROM PERIODCONTROL WHERE SYSTEM = @SYSTEM", new {  }).SingleOrDefault();
        }

        public List<PeriodControl> GetAll()
        {
            return this._db.Query<PeriodControl>("SELECT" +
                               " PC.SYSTEM," +
                               " PC.LASTPERIODPOSTED," +
                               " PC.CURRENTDATE," +
                               " PC.POSTINGPOSITION," +
                               " PC.CURRENTACCOUNTINGYEAR," +
                               " PC.CURRENTACCOUNTINGPERIOD," +
                               " PC.CLOSEACCOUNTINGYEAR," +
                               " PC.CLOSEACCOUNTINGPERIOD," +
                               " PC.NEXTACCOUNTINGYEAR," +
                               " PC.NEXTACCOUNTINGPERIOD," +
                               " PC.COMPANYSITE," +
                               " PC.INPUTBY," +
                               " PC.INPUTDATE," +
                               " PC.UPDATEBY," +
                               " PC.UPDATEDATE" +
                               " FROM" +
                               " PERIODCONTROL AS PC" +
                               " ORDER BY" +
                               " PC.ACCOUNTINGYEAR,SID ASC").ToList();
        }

        public void Remove(int SID)
        {
            var sqlQuery = (string.Format("Delete From PeriodControl Where SID = {0}", SID));
            this._db.Execute(sqlQuery);
        }

        public PeriodControl Update(PeriodControl PC, string userid)
        {
            var sqlQuery =
                "UPDATE PERIODCONTROL " +
                "SET SYSTEM    = '" + PC.SYSTEM + "', " +
                "    LASTPERIODPOSTED  = '" + PC.LASTPERIODPOSTED + "', " +
                "    CURRENTDATE  = '" + PC.CURRENTDATE + "', " +
                "    POSTINGPOSITION  = '" + PC.POSTINGPOSITION + "', " +
                "    CURRENTACCOUNTINGYEAR  = '" + PC.CURRENTACCOUNTINGYEAR + "', " +
                "    CURRENTACCOUNTINGPERIOD  = '" + PC.CURRENTACCOUNTINGPERIOD + "', " +
                "    CLOSEACCOUNTINGYEAR  = '" + PC.CLOSEACCOUNTINGYEAR + "', " +
                "    CLOSEACCOUNTINGPERIOD  = '" + PC.CLOSEACCOUNTINGPERIOD + "', " +
                "    NEXTACCOUNTINGYEAR  = '" + PC.NEXTACCOUNTINGYEAR + "', " +
                "    NEXTACCOUNTINGPERIOD  = '" + PC.NEXTACCOUNTINGPERIOD + "', " +
                "    COMPANYSITE  = '" + PC.COMPANYSITE + "', " +
                "    UPDATEBY      = '" + Convert.ToInt32(userid) + "', " +
                "    UPDATEDATE    = '" + DateTime.Now + "' " +
                "WHERE SYSTEM = " + PC.SYSTEM + "";
            this._db.Execute(sqlQuery, PC);
            return PC;
        }        
    }
}