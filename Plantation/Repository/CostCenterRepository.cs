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
    public class CostCenterRepository : ICostCenterRepository
    {
        private IDbConnection _db = new SqlConnection(Constant.DatabaseConnection);

        public CostCenter Add(CostCenter CC, string userid)
        {
            var sqlQuery = @"INSERT INTO COSTCENTER (IDCOSTCENTER, COSTCENTERNAME, PARENTCOSTCENTER, COSTCENTERTYPE, ALIASNAME, ALLOCATIONTYPE, BUDGETTYPE, TRANSACTIONS, REMARKS, ISACTIVE, ISACTIVEDATE, COMPANY, COMPANYSITE, INPUTBY, INPUTDATE, UPDATEBY, UPDATEDATE) VALUES
                           ('" + CC.IDCOSTCENTER + "', '" + CC.COSTCENTERNAME + "', '" + CC.PARENTCOSTCENTER + "', '" + CC.COSTCENTERTYPE + "', '" + CC.ALIASNAME + "', '" + CC.ALLOCATIONTYPE + "', '" + CC.BUDGETTYPE + "', '" + CC.TRANSACTIONS + "', '" + CC.REMARKS + "', '" + CC.ISACTIVE + "', '" + CC.ISACTIVEDATE + "', '" + CC.COMPANY + "', '" + CC.COMPANYSITE + "', " + Convert.ToInt32(userid) + ", '" + DateTime.Now + @"', " + Convert.ToInt32(userid) + ", '" + DateTime.Now + @"'); " + "SELECT CAST(SCOPE_IDENTITY() as int)";
            var SID = _db.Query<int>(sqlQuery, CC).Single();
            CC.SID = SID;
            return CC;
        }

        public CostCenter Find(int? SID)
        {
            return this._db.Query<CostCenter>("SELECT * FROM COSTCENTER WHERE SID = @SID", new { SID }).SingleOrDefault();
        }

        public List<CostCenter> GetAll()
        {
            return this._db.Query<CostCenter>("SELECT" +
                               " CC.SID," +
                               " CC.IDCOSTCENTER," +
                               " CC.COSTCENTERNAME," +
                               " CC.PARENTCOSTCENTER," +
                               " CC.COSTCENTERTYPE," +
                               " CC.ALIASNAME," +
                               " CC.ALLOCATIONTYPE," +
                               " CC.BUDGETTYPE," +
                               " CC.TRANSACTIONS," +
                               " CC.REMARKS," +
                               " CC.ISACTIVE," +
                               " CC.ISACTIVEDATE," +
                               " CC.COMPANY," +
                               " CC.COMPANYSITE," +
                               " CC.INPUTBY," +
                               " CC.INPUTDATE," +
                               " CC.UPDATEBY," +
                               " CC.UPDATEDATE" +
                               " FROM" + 
                               " COSTCENTER AS CC" +
                               " ORDER BY" +
                               " CC.SID ASC").ToList();
        }

        public void Remove(int SID)
        {
            var sqlQuery = (string.Format("Delete From CostCenter Where SID = {0}", SID));
            this._db.Execute(sqlQuery);
        }

        public CostCenter Update(CostCenter CC, string userid)
        {
            var sqlQuery =
                string.Format(@"UPDATE COSTCENTER SET IDCOSTCENTER = '{0}', COSTCENTERNAME = '{1}', PARENTCOSTCENTER = '{2}', COSTCENTERTYPE = '{3}', ALIASNAME = '{4}', ALLOCATIONTYPE = '{5}', BUDGETTYPE = '{6}', TRANSACTIONS = '{7}', REMARKS = '{8}', ISACTIVE = '{9}', ISACTIVEDATE = '{10}', COMPANY = '{11}', COMPANYSITE = '{12}', UPDATEBY = {13}, UPDATEDATE = '{14}' WHERE SID = {15}", CC.IDCOSTCENTER, CC.COSTCENTERNAME, CC.PARENTCOSTCENTER, CC.COSTCENTERTYPE, CC.ALIASNAME, CC.ALLOCATIONTYPE, CC.BUDGETTYPE, CC.TRANSACTIONS, CC.REMARKS, CC.ISACTIVE, CC.ISACTIVEDATE, CC.COMPANY, CC.COMPANYSITE, Convert.ToInt32(userid), DateTime.Now, CC.SID);
            this._db.Execute(sqlQuery, CC);
            return CC;
        }
    }
}