using Plantation.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Plantation.Models.DB;
using System.Data;
using Plantation.Utility;
using System.Data.SqlClient;
using Dapper;

namespace Plantation.Repository
{
    public class EmployeeGangRepository : IEmployeeGangRepository
    {
        private IDbConnection _db = new SqlConnection(Constant.DatabaseConnection);

        public EmployeeGang Add(EmployeeGang EGH, string userid)
        {
            var sqlQuery = @"INSERT INTO EMPLOYEEGANG (IDEMPLOYEEGANG, EMPLOYEEGANGNAME, FOREMAN1, FOREMAN, ADMIN, GANGTYPE, BLOCKORGANIZATION, COMPANYSITE, INPUTBY, INPUTDATE, UPDATEBY, UPDATEDATE) VALUES
                           ('" + EGH.IDEMPLOYEEGANG + @"', '" + EGH.EMPLOYEEGANGNAME + "', '" + EGH.FOREMAN1 + "', '" + EGH.FOREMAN + "', '" + EGH.ADMIN + "', '" + EGH.GANGTYPE + "', '" + EGH.BLOCKORGANIZATION + "', '" + EGH.COMPANYSITE +  @"', " + Convert.ToInt32(userid) + ", '" + DateTime.Now + @"', " + Convert.ToInt32(userid) + ", '" + DateTime.Now + @"'); " + "SELECT CAST(SCOPE_IDENTITY() as int)";
            var SID = _db.Query<int>(sqlQuery, EGH).Single();
            EGH.SID = SID;
            return EGH;
        }

        public EmployeeGang Find(int? SID)
        {
            return this._db.Query<EmployeeGang>("SELECT * FROM EMPLOYEEGANG WHERE SID = @SID", new { SID }).SingleOrDefault();
        }

        public List<EmployeeGang> GetAll()
        {
            return this._db.Query<EmployeeGang>("SELECT" +
                               " EGH.SID," +
                               " EGH.IDEMPLOYEEGANG," +
                               " EGH.EMPLOYEEGANGNAME," +
                               " EGH.FOREMAN1," +
                               " EGH.FOREMAN," +
                               " EGH.ADMIN," +
                               " EGH.GANGTYPE," +
                               " EGH.BLOCKORGANIZATION," +
                               " EGH.COMPANYSITE," +
                               " EGH.INPUTBY," +
                               " EGH.INPUTDATE," +
                               " EGH.UPDATEBY," +
                               " EGH.UPDATEDATE" +
                               " FROM" +
                               " EMPLOYEEGANG AS EGH" +
                               " ORDER BY" +
                               " EGH.SID ASC").ToList();
        }

        public List<EmployeeGang> GetAllByCompanySite(int CompanySite)
        {
            return this._db.Query<EmployeeGang>("SELECT" +
                               " EGH.SID," +
                               " EGH.IDEMPLOYEEGANG," +
                               " EGH.EMPLOYEEGANGNAME," +
                               " EGH.FOREMAN1," +
                               " EGH.FOREMAN," +
                               " EGH.ADMIN," +
                               " EGH.GANGTYPE," +
                               " EGH.BLOCKORGANIZATION," +
                               " EGH.COMPANYSITE," +
                               " EGH.INPUTBY," +
                               " EGH.INPUTDATE," +
                               " EGH.UPDATEBY," +
                               " EGH.UPDATEDATE," +
                               " F1.EMPLOYEENAME AS FOREMAN1NAME," +
                               " F.EMPLOYEENAME AS FOREMANNAME," +
                               " A.EMPLOYEENAME AS ADMINNAME," +
                               " PV.PARAMETERVALUENAME AS GANGTYPENAME," +
                               " BLOCKORGANIZATION.BLOCKORGANIZATIONNAME" +
                               " FROM" +
                               " EMPLOYEEGANG AS EGH" +
                               " LEFT JOIN EMPLOYEE F1 ON EGH.FOREMAN1 = F1.SID" +
                               " LEFT JOIN EMPLOYEE F ON EGH.FOREMAN = F.SID" +
                               " LEFT JOIN EMPLOYEE A ON EGH.ADMIN = A.SID" +
                               " LEFT JOIN PARAMETERVALUE PV ON EGH.GANGTYPE = PV.SID" +
                               " LEFT JOIN BLOCKORGANIZATION ON EGH.BLOCKORGANIZATION = BLOCKORGANIZATION.SID" +
                               " WHERE EGH.COMPANYSITE = @COMPANYSITE" +
                               " ORDER BY" +
                               " EGH.SID ASC", new { CompanySite }).ToList();
        }

        public bool HasEmployeeGangDetail(int SID)
        {
            return this._db.Query<bool>("SELECT COUNT(*) FROM EMPLOYEEGANGDETAIL WHERE EMPLOYEEGANG = @SID", new { SID }).FirstOrDefault();
        }

        public void Remove(int SID)
        {
            var sqlQuery = (string.Format("Delete From EmployeeGang Where SID = {0}", SID));
            this._db.Execute(sqlQuery);
        }

        public EmployeeGang Update(EmployeeGang EGH)
        {
            var sqlQuery =
                "UPDATE EmployeeGang " +
                "SET IDEMPLOYEEGANG    = '" + EGH.IDEMPLOYEEGANG + "', " +
                "    EMPLOYEEGANGNAME  = '" + EGH.EMPLOYEEGANGNAME + "', " +
                "    FOREMAN1   = '" + EGH.FOREMAN1 + "', " +
                "    FOREMAN   = '" + EGH.FOREMAN + "', " +
                "    ADMIN   = '" + EGH.ADMIN + "', " +
                "    GANGTYPE   = '" + EGH.GANGTYPE + "', " +
                "    BLOCKORGANIZATION   = '" + EGH.BLOCKORGANIZATION + "', " +
                "    COMPANYSITE   = '" + EGH.COMPANYSITE + "', " +
                "    UPDATEBY      = '" + Convert.ToInt32(EGH.UPDATEBY) + "', " +
                "    UPDATEDATE    = '" + EGH.UPDATEDATE + "' " +
                "WHERE SID = " + EGH.SID + "";
            this._db.Execute(sqlQuery, EGH);
            return EGH;
        }
    }
}