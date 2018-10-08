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
    public class AccountingPeriodRepository : IAccountingPeriodRepository
    {
        private IDbConnection _db = new SqlConnection(Constant.DatabaseConnection);

        public AccountingPeriod Add(AccountingPeriod AP, string userid)
        {
            var sqlQuery = @"INSERT INTO ACCOUNTINGPERIOD (ACCOUNTINGYEAR, PERIODSEQUENT, STARTDATE, ENDDATE, ISSTATUS, COMPANYSITE, INPUTBY, INPUTDATE, UPDATEBY, UPDATEDATE) 
                            VALUES('" + AP.ACCOUNTINGYEAR + "', '" + AP.PERIODSEQUENT + "', '" + AP.STARTDATE + "', '" + AP.ENDDATE + "', '" + AP.ISSTATUS + "', '" + AP.COMPANYSITE + "','" + userid + "', '" + DateTime.Now + "', '" + userid + "', '" + DateTime.Now + "'); " + "SELECT CAST(SCOPE_IDENTITY() as int)";
            var SID = _db.Query<int>(sqlQuery, AP).Single();
            AP.SID = SID;
            return AP;
        }

        public AccountingPeriod Find(int? SID)
        {
            return this._db.Query<AccountingPeriod>("SELECT * FROM ACCOUNTINGPERIOD WHERE SID = @SID", new { SID }).SingleOrDefault();
        }

        public List<AccountingPeriod> GetAll()
        {
            return this._db.Query<AccountingPeriod>("SELECT" +
                               " AP.ACCOUNTINGYEAR," +
                               " AP.PERIODSEQUENT," +
                               " AP.STARTDATE," +
                               " AP.ENDDATE," +
                               " AP.ISSTATUS," +
                               " AP.COMPANYSITE," +
                               " AP.INPUTBY," +
                               " AP.INPUTDATE," +
                               " AP.UPDATEBY," +
                               " AP.UPDATEDATE" +
                               " FROM" +
                               " ACCOUNTINGPERIOD AS AP" +
                               " ORDER BY" +
                               " AP.ACCOUNTINGYEAR,SID ASC").ToList();
        }

        public void Remove(int SID)
        {
            var sqlQuery = (string.Format("Delete From AccountingPeriod Where SID = {0}", SID));
            this._db.Execute(sqlQuery);
        }

        public AccountingPeriod Update(AccountingPeriod AP, string userid)
        {
            var sqlQuery =
                "UPDATE ACCOUNTINGPERIOD " +
                "SET ACCOUNTINGYEAR    = '" + AP.ACCOUNTINGYEAR + "', " +
                "    PERIODSEQUENT  = '" + AP.PERIODSEQUENT + "', " +
                "    STARTDATE  = '" + AP.STARTDATE + "', " +
                "    ENDDATE  = '" + AP.ENDDATE + "', " +
                "    ISSTATUS  = '" + AP.ISSTATUS + "', " +
                "    COMPANYSITE  = '" + AP.COMPANYSITE + "', " +
                "    UPDATEBY      = '" + Convert.ToInt32(userid) + "', " +
                "    UPDATEDATE    = '" + DateTime.Now + "' " +
                "WHERE SID = " + AP.SID + "";
            this._db.Execute(sqlQuery, AP);
            return AP;
        }        
    }
}