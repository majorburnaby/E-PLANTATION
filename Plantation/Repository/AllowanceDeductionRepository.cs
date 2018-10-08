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
    public class AllowanceDeductionRepository : IAllowanceDeductionRepository
    {
        private IDbConnection _db = new SqlConnection(Constant.DatabaseConnection);

        public AllowanceDeduction Add(AllowanceDeduction AD, string userid)
        {
            var sqlQuery = @"INSERT INTO ALLOWANCEDEDUCTION (IDALLOWANCEDEDUCTION, ALLOWANCEDEDUCTIONNAME, ALLOWANCEDEDUCTIONTYPE, INPUTBY, INPUTDATE, UPDATEBY, UPDATEDATE) 
                            VALUES('" + AD.IDALLOWANCEDEDUCTION + "', '" + AD.ALLOWANCEDEDUCTIONNAME + "', '" + AD.ALLOWANCEDEDUCTIONTYPE + "', '" + userid + "', '" + DateTime.Now + "', '" + userid + "', '" + DateTime.Now + "'); " + "SELECT CAST(SCOPE_IDENTITY() as int)";
            var SID = _db.Query<int>(sqlQuery, AD).Single();
            AD.SID = SID;
            return AD;
        }

        public AllowanceDeduction Find(int? SID)
        {
            return this._db.Query<AllowanceDeduction>("SELECT * FROM ALLOWANCEDEDUCTION WHERE SID = @SID", new { SID }).SingleOrDefault();
        }

        public List<AllowanceDeduction> GetAll()
        {
            return this._db.Query<AllowanceDeduction>("SELECT" +
                               " AD.SID," +
                               " AD.IDALLOWANCEDEDUCTION," +
                               " AD.ALLOWANCEDEDUCTIONNAME," +
                               " AD.ALLOWANCEDEDUCTIONTYPE," +
                               " AD.INPUTBY," +
                               " AD.INPUTDATE," +
                               " AD.UPDATEBY," +
                               " AD.UPDATEDATE," +
                               " PARAMETERVALUE.PARAMETERVALUENAME" +
                               " FROM" +
                               " ALLOWANCEDEDUCTION AS AD" +
                               " LEFT JOIN PARAMETERVALUE ON AD.ALLOWANCEDEDUCTIONTYPE = PARAMETERVALUE.SID" +
                               " ORDER BY" +
                               " AD.ALLOWANCEDEDUCTIONTYPE,SID ASC").ToList();
        }

        public void Remove(int SID)
        {
            var sqlQuery = (string.Format("Delete From AllowanceDeduction Where SID = {0}", SID));
            this._db.Execute(sqlQuery);
        }

        public AllowanceDeduction Update(AllowanceDeduction AD, string userid)
        {
            var sqlQuery =
                "UPDATE ALLOWANCEDEDUCTION " +
                "SET IDALLOWANCEDEDUCTION    = '" + AD.IDALLOWANCEDEDUCTION + "', " +
                "    ALLOWANCEDEDUCTIONNAME  = '" + AD.ALLOWANCEDEDUCTIONNAME + "', " +
                "    ALLOWANCEDEDUCTIONTYPE  = '" + AD.ALLOWANCEDEDUCTIONTYPE + "', " +
                "    UPDATEBY      = '" + Convert.ToInt32(userid) + "', " +
                "    UPDATEDATE    = '" + DateTime.Now + "' " +
                "WHERE SID = " + AD.SID + "";
            this._db.Execute(sqlQuery, AD);
            return AD;
        }        
    }
}