using Dapper;
using Plantation.Models.DB;
using Plantation.Repository.Interface;
using Plantation.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Plantation.Repository
{
    public class EmployeeGangDetailRepository : IEmployeeGangDetailRepository
    {
        private IDbConnection _db = new SqlConnection(Constant.DatabaseConnection);

        public EmployeeGangDetail Add(EmployeeGangDetail EGD)
        {
            var sqlQuery = @"INSERT INTO EMPLOYEEGANGDETAIL (EMPLOYEEGANG, EMPLOYEE, COMPANYSITE, INPUTBY, INPUTDATE, UPDATEBY, UPDATEDATE) VALUES
                           (" + EGD.EMPLOYEEGANG + @", " + EGD.EMPLOYEE + ", '" + EGD.COMPANYSITE + @"', " + EGD.INPUTBY + ", '" + EGD.INPUTDATE + @"', " + EGD.UPDATEBY + ", '" + EGD.UPDATEDATE + @"'); " + "SELECT CAST(SCOPE_IDENTITY() as int)";
            var SID = _db.Query<int>(sqlQuery, EGD).Single();
            EGD.SID = SID;
            return EGD;
        }

        public EmployeeGangDetail Find(int? SID)
        {
            return this._db.Query<EmployeeGangDetail>("SELECT * FROM EMPLOYEEGANGDETAIL WHERE SID = @SID", new { SID }).SingleOrDefault();
        }

        public List<EmployeeGangDetail> GetAll()
        {
            return this._db.Query<EmployeeGangDetail>("SELECT *" +
                              " FROM" +
                              " EmployeeGangDetail AS EGD" +
                              " ORDER BY" +
                              " EGD.SID ASC").ToList();
        }

        public int GetTotalEmployeeGangDetail()
        {
            return this._db.Query<int>("SELECT * FROM EMPLOYEEGANGDETAIL").FirstOrDefault();
        }

        public void Remove(int SID)
        {
            var sqlQuery = (string.Format("Delete From EmployeeGangDetail Where SID = {0}", SID));
            this._db.Execute(sqlQuery);
        }

        public EmployeeGangDetail Update(EmployeeGangDetail EGD)
        {
            var sqlQuery =
                "UPDATE EMPLOYEEGANGDETAIL " +
                "SET EMPLOYEEGANG    = '" + EGD.EMPLOYEEGANG + "', " +
                "    EMPLOYEE  = '" + EGD.EMPLOYEE + "', " +
                "    COMPANYSITE  = '" + EGD.COMPANYSITE + "', " +
                "    UPDATEBY      = '" + Convert.ToInt32(EGD.UPDATEBY) + "', " +
                "    UPDATEDATE    = '" + EGD.UPDATEDATE + "' " +
                "WHERE SID = " + EGD.SID + "";
            this._db.Execute(sqlQuery, EGD);
            return EGD;
        }
    }
}