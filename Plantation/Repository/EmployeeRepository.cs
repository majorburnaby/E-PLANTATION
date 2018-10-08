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
    public class EmployeeRepository : IEmployeeRepository
    {
        private IDbConnection _db = new SqlConnection(Constant.DatabaseConnection);

        public Employee Add(Employee EM, string userid)
        {
            var sqlQuery = @"INSERT INTO EMPLOYEE (IDEMPLOYEE, EMPLOYEENAME, OTHERNAME, IDCARD, IDFINGERPRINT, BIRTHPLACE, DOB, SEX, RACE, [ADDRESS], HOMEPHONE, MOBILEPHONE, EMAIL, RELIGION, NATIONALITY, ZIPCODE, EDUCATIONLEVEL, INSTITUTION, PROGRAMOFSTUDY, MARITALSTATUS, SPOUSENAME, NUMBEROFCHILD, FAMILYRICESTATUS, NATURETYPE, DATEJOIN, DEPARTMENT, GRADE, EMPLOYEETYPE, EMPLOYEESECTION, EMPLOYEEPOSITION, BASICSALARY, PAYMENTMETHOD, BANK, [OWNERSHIP], BANKACCOUNT, FAMILYTAXSTATUS, NPWPNO, JAMSOSTEKNO, TERMINATE, DATETERMINATE, REMARK, COMPANYSITE, INPUTBY, INPUTDATE, UPDATEBY, UPDATEDATE) VALUES
                           ('" + EM.IDEMPLOYEE + "', '" + EM.EMPLOYEENAME + "', '" + EM.OTHERNAME + "', '" + EM.IDCARD + "', '" + EM.IDFINGERPRINT + "', '" + EM.BIRTHPLACE + "', '" + EM.DOB + "', '" + EM.SEX + "', '" + EM.RACE + "', '" + EM.ADDRESS + "', '" + EM.HOMEPHONE + "', '" + EM.MOBILEPHONE + "', '" + EM.EMAIL + "', '" + EM.RELIGION + "', '" + EM.NATIONALITY + "', '" + EM.ZIPCODE + "', '" + EM.EDUCATIONLEVEL + "', '" + EM.INSTITUTION + "', '" + EM.PROGRAMOFSTUDY + "', '" + EM.MARITALSTATUS + "', '" + EM.SPOUSENAME + "', '" + EM.NUMBEROFCHILD + "', '" + EM.FAMILYRICESTATUS + "', '" + EM.NATURETYPE + "', '" + EM.DATEJOIN + "', '" + EM.DEPARTMENT + "', '" + EM.GRADE + "', '" + EM.EMPLOYEETYPE + "', '" + EM.EMPLOYEESECTION + "', '" + EM.EMPLOYEEPOSITION + "', '" + EM.BASICSALARY + "', '" + EM.PAYMENTMETHOD + "', '" + EM.BANK + "', '" + EM.OWNERSHIP + "', '" + EM.BANKACCOUNT + "', '" + EM.FAMILYTAXSTATUS + "', '" + EM.NPWPNO + "', '" + EM.JAMSOSTEKNO + "', '" + EM.TERMINATE + "', '" + EM.DATETERMINATE + "', '" + EM.REMARK + "', '" + EM.COMPANYSITE + "', " + Convert.ToInt32(userid) + ", '" + DateTime.Now + @"', " + Convert.ToInt32(userid) + ", '" + DateTime.Now + @"'); " + "SELECT CAST(SCOPE_IDENTITY() as int)";
            var SID = _db.Query<int>(sqlQuery, EM).Single();
            EM.SID = SID;
            return EM;
        }

        public void AddHistory(Employee EM, string userid)
        {
            var sqlQuery = @"INSERT INTO EMPLOYEEHISTORY (SIDEMPLOYEE, IDEMPLOYEE, EMPLOYEENAME, OTHERNAME, IDCARD, IDFINGERPRINT, BIRTHPLACE, DOB, SEX, RACE, [ADDRESS], HOMEPHONE, MOBILEPHONE, EMAIL, RELIGION, NATIONALITY, ZIPCODE, EDUCATIONLEVEL, INSTITUTION, PROGRAMOFSTUDY, MARITALSTATUS, SPOUSENAME, NUMBEROFCHILD, FAMILYRICESTATUS, NATURETYPE, DATEJOIN, DEPARTMENT, GRADE, EMPLOYEETYPE, EMPLOYEESECTION, EMPLOYEEPOSITION, BASICSALARY, PAYMENTMETHOD, BANK, [OWNERSHIP], BANKACCOUNT, FAMILYTAXSTATUS, NPWPNO, JAMSOSTEKNO, TERMINATE, DATETERMINATE, REMARK, COMPANYSITE, UPDATEBY, UPDATEDATE) VALUES
                           ('" + EM.SIDEMPLOYEE + "', '" + EM.IDEMPLOYEE + "', '" + EM.EMPLOYEENAME + "', '" + EM.OTHERNAME + "', '" + EM.IDCARD + "', '" + EM.IDFINGERPRINT + "', '" + EM.BIRTHPLACE + "', '" + EM.DOB + "', '" + EM.SEX + "', '" + EM.RACE + "', '" + EM.ADDRESS + "', '" + EM.HOMEPHONE + "', '" + EM.MOBILEPHONE + "', '" + EM.EMAIL + "', '" + EM.RELIGION + "', '" + EM.NATIONALITY + "', '" + EM.ZIPCODE + "', '" + EM.EDUCATIONLEVEL + "', '" + EM.INSTITUTION + "', '" + EM.PROGRAMOFSTUDY + "', '" + EM.MARITALSTATUS + "', '" + EM.SPOUSENAME + "', '" + EM.NUMBEROFCHILD + "', '" + EM.FAMILYRICESTATUS + "', '" + EM.NATURETYPE + "', '" + EM.DATEJOIN + "', '" + EM.DEPARTMENT + "', '" + EM.GRADE + "', '" + EM.EMPLOYEETYPE + "', '" + EM.EMPLOYEESECTION + "', '" + EM.EMPLOYEEPOSITION + "', '" + EM.BASICSALARY + "', '" + EM.PAYMENTMETHOD + "', '" + EM.BANK + "', '" + EM.OWNERSHIP + "', '" + EM.BANKACCOUNT + "', '" + EM.FAMILYTAXSTATUS + "', '" + EM.NPWPNO + "', '" + EM.JAMSOSTEKNO + "', '" + EM.TERMINATE + "', '" + EM.DATETERMINATE + "', '" + EM.REMARK + "', '" + EM.COMPANYSITE + @"', " + Convert.ToInt32(userid) + ", '" + DateTime.Now + @"'); " + "SELECT CAST(SCOPE_IDENTITY() as int)";
            _db.Execute(sqlQuery);
        }

        public Employee Find(int? SID)
        {
            return this._db.Query<Employee>("SELECT * FROM EMPLOYEE WHERE SID = @SID", new { SID }).SingleOrDefault();
        }

        public List<Employee> GetAll()
        {
            return this._db.Query<Employee>("SELECT" +
                               " EM.SID," +
                               " EM.IDEMPLOYEE," +
                               " EM.EMPLOYEENAME," +
                               " EM.OTHERNAME," +
                               " EM.IDCARD," +
                               " EM.IDFINGERPRINT," +
                               " EM.BIRTHPLACE," +
                               " EM.DOB," +
                               " EM.SEX," +
                               " EM.RACE," +
                               " EM.ADDRESS," +
                               " EM.HOMEPHONE," +
                               " EM.MOBILEPHONE," +
                               " EM.EMAIL," +
                               " EM.RELIGION," +
                               " EM.NATIONALITY," +
                               " EM.ZIPCODE," +
                               " EM.EDUCATIONLEVEL," +
                               " EM.INSTITUTION," +
                               " EM.PROGRAMOFSTUDY," +
                               " EM.MARITALSTATUS," +
                               " EM.SPOUSENAME," +
                               " EM.NUMBEROFCHILD," +
                               " EM.FAMILYRICESTATUS," +
                               " EM.NATURETYPE," +
                               " EM.DATEJOIN," +
                               " EM.DEPARTMENT," +
                               " EM.GRADE," +
                               " EM.EMPLOYEETYPE," +
                               " EM.EMPLOYEESECTION," +
                               " EM.EMPLOYEEPOSITION," +
                               " EM.BASICSALARY," +
                               " EM.PAYMENTMETHOD," +
                               " EM.BANK," +
                               " EM.OWNERSHIP," +
                               " EM.BANKACCOUNT," +
                               " EM.FAMILYTAXSTATUS," +
                               " EM.NPWPNO," +
                               " EM.JAMSOSTEKNO," +
                               " EM.TERMINATE," +
                               " EM.DATETERMINATE," +
                               " EM.REMARK," +
                               " EM.COMPANYSITE," +
                               " EM.INPUTBY," +
                               " EM.INPUTDATE," +
                               " EM.UPDATEBY," +
                               " EM.UPDATEDATE," +
                               " SX.PARAMETERVALUENAME AS SEXNAME," +
                               " RC.PARAMETERVALUENAME AS RACENAME," +
                               " RL.PARAMETERVALUENAME AS RELIGIONNAME," +
                               " COUNTRY.COUNTRYNAME," +
                               " EL.PARAMETERVALUENAME AS EDUCATIONLEVELNAME," +
                               " IT.PARAMETERVALUENAME AS INSTITUTIONNAME," +
                               " PS.PARAMETERVALUENAME AS PROGRAMOFSTUDYNAME," +
                               " MS.PARAMETERVALUENAME AS MARITALSTATUSNAME," +
                               " MR.PARAMETERVALUENAME AS FAMILYRICESTATUSNAME," +
                               " NT.PARAMETERVALUENAME AS NATURETYPENAME," +
                               " DEPARTMENT.DEPARTMENTNAME," +
                               " GD.PARAMETERVALUENAME AS GRADENAME," +
                               " ET.PARAMETERVALUENAME AS EMPLOYEETYPENAME," +
                               " ES.PARAMETERVALUENAME AS EMPLOYEESECTIONNAME," +
                               " EP.PARAMETERVALUENAME AS EMPLOYEEPOSITIONNAME," +
                               " PM.PARAMETERVALUENAME AS PAYMENTMETHODNAME," +
                               " BK.PARAMETERVALUENAME AS BANKNAME" +
                               " FROM" +
                               " EMPLOYEE AS EM" +
                               " LEFT JOIN PARAMETERVALUE SX ON EM.SEX = SX.SID" +
                               " LEFT JOIN PARAMETERVALUE RC ON EM.RACE = RC.SID" +
                               " LEFT JOIN PARAMETERVALUE RL ON EM.RELIGION = RL.SID" +
                               " LEFT JOIN COUNTRY ON EM.NATIONALITY = COUNTRY.SID" +
                               " LEFT JOIN PARAMETERVALUE EL ON EM.EDUCATIONLEVEL = EL.SID" +
                               " LEFT JOIN PARAMETERVALUE IT ON EM.INSTITUTION = IT.SID" +
                               " LEFT JOIN PARAMETERVALUE PS ON EM.PROGRAMOFSTUDY = PS.SID" +
                               " LEFT JOIN PARAMETERVALUE MS ON EM.MARITALSTATUS = MS.SID" +
                               " LEFT JOIN PARAMETERVALUE MR ON EM.FAMILYRICESTATUS = MR.SID" +
                               " LEFT JOIN PARAMETERVALUE NT ON EM.NATURETYPE = NT.SID" +
                               " LEFT JOIN DEPARTMENT ON EM.DEPARTMENT = DEPARTMENT.SID" +
                               " LEFT JOIN PARAMETERVALUE GD ON EM.GRADE = GD.SID" +
                               " LEFT JOIN PARAMETERVALUE ET ON EM.EMPLOYEETYPE = ET.SID" +
                               " LEFT JOIN PARAMETERVALUE ES ON EM.EMPLOYEESECTION = ES.SID" +
                               " LEFT JOIN PARAMETERVALUE EP ON EM.EMPLOYEEPOSITION = EP.SID" +
                               " LEFT JOIN PARAMETERVALUE PM ON EM.PAYMENTMETHOD = PM.SID" +
                               " LEFT JOIN PARAMETERVALUE BK ON EM.BANK = BK.SID" +
                               " ORDER BY" +
                               " EM.SID ASC").ToList();
        }

        public List<Employee> GetAllByCompanySite(int? CompanySite)
        {
            return this._db.Query<Employee>("SELECT" +
                                " EM.SID," +
                                " EM.IDEMPLOYEE," +
                                " EM.EMPLOYEENAME," +
                                " EM.OTHERNAME," +
                                " EM.IDCARD," +
                                " EM.IDFINGERPRINT," +
                                " EM.BIRTHPLACE," +
                                " EM.DOB," +
                                " EM.SEX," +
                                " EM.RACE," +
                                " EM.ADDRESS," +
                                " EM.HOMEPHONE," +
                                " EM.MOBILEPHONE," +
                                " EM.EMAIL," +
                                " EM.RELIGION," +
                                " EM.NATIONALITY," +
                                " EM.ZIPCODE," +
                                " EM.EDUCATIONLEVEL," +
                                " EM.INSTITUTION," +
                                " EM.PROGRAMOFSTUDY," +
                                " EM.MARITALSTATUS," +
                                " EM.SPOUSENAME," +
                                " EM.NUMBEROFCHILD," +
                                " EM.FAMILYRICESTATUS," +
                                " EM.NATURETYPE," +
                                " EM.DATEJOIN," +
                                " EM.DEPARTMENT," +
                                " EM.GRADE," +
                                " EM.EMPLOYEETYPE," +
                                " EM.EMPLOYEESECTION," +
                                " EM.EMPLOYEEPOSITION," +
                                " EM.BASICSALARY," +
                                " EM.PAYMENTMETHOD," +
                                " EM.BANK," +
                                " EM.OWNERSHIP," +
                                " EM.BANKACCOUNT," +
                                " EM.FAMILYTAXSTATUS," +
                                " EM.NPWPNO," +
                                " EM.JAMSOSTEKNO," +
                                " EM.TERMINATE," +
                                " EM.DATETERMINATE," +
                                " EM.REMARK," +
                                " EM.COMPANYSITE," +
                                " EM.INPUTBY," +
                                " EM.INPUTDATE," +
                                " EM.UPDATEBY," +
                                " EM.UPDATEDATE," +
                                " SX.PARAMETERVALUENAME AS SEXNAME," +
                                " RC.PARAMETERVALUENAME AS RACENAME," +
                                " RL.PARAMETERVALUENAME AS RELIGIONNAME," +
                                " COUNTRY.COUNTRYNAME," +
                                " EL.PARAMETERVALUENAME AS EDUCATIONLEVELNAME," +
                                " IT.PARAMETERVALUENAME AS INSTITUTIONNAME," +
                                " PS.PARAMETERVALUENAME AS PROGRAMOFSTUDYNAME," +
                                " MS.PARAMETERVALUENAME AS MARITALSTATUSNAME," +
                                " MR.PARAMETERVALUENAME AS FAMILYRICESTATUSNAME," +
                                " NT.PARAMETERVALUENAME AS NATURETYPENAME," +
                                " DEPARTMENT.DEPARTMENTNAME," +
                                " GD.PARAMETERVALUENAME AS GRADENAME," +
                                " ET.PARAMETERVALUENAME AS EMPLOYEETYPENAME," +
                                " ES.PARAMETERVALUENAME AS EMPLOYEESECTIONNAME," +
                                " EP.PARAMETERVALUENAME AS EMPLOYEEPOSITIONNAME," +
                                " PM.PARAMETERVALUENAME AS PAYMENTMETHODNAME," +
                                " BK.PARAMETERVALUENAME AS BANKNAME" +
                                " FROM" +
                                " EMPLOYEE AS EM" +
                                " LEFT JOIN PARAMETERVALUE SX ON EM.SEX = SX.SID" +
                                " LEFT JOIN PARAMETERVALUE RC ON EM.RACE = RC.SID" +
                                " LEFT JOIN PARAMETERVALUE RL ON EM.RELIGION = RL.SID" +
                                " LEFT JOIN COUNTRY ON EM.NATIONALITY = COUNTRY.SID" +
                                " LEFT JOIN PARAMETERVALUE EL ON EM.EDUCATIONLEVEL = EL.SID" +
                                " LEFT JOIN PARAMETERVALUE IT ON EM.INSTITUTION = IT.SID" +
                                " LEFT JOIN PARAMETERVALUE PS ON EM.PROGRAMOFSTUDY = PS.SID" +
                                " LEFT JOIN PARAMETERVALUE MS ON EM.MARITALSTATUS = MS.SID" +
                                " LEFT JOIN PARAMETERVALUE MR ON EM.FAMILYRICESTATUS = MR.SID" +
                                " LEFT JOIN PARAMETERVALUE NT ON EM.NATURETYPE = NT.SID" +
                                " LEFT JOIN DEPARTMENT ON EM.DEPARTMENT = DEPARTMENT.SID" +
                                " LEFT JOIN PARAMETERVALUE GD ON EM.GRADE = GD.SID" +
                                " LEFT JOIN PARAMETERVALUE ET ON EM.EMPLOYEETYPE = ET.SID" +
                                " LEFT JOIN PARAMETERVALUE ES ON EM.EMPLOYEESECTION = ES.SID" +
                                " LEFT JOIN PARAMETERVALUE EP ON EM.EMPLOYEEPOSITION = EP.SID" +
                                " LEFT JOIN PARAMETERVALUE PM ON EM.PAYMENTMETHOD = PM.SID" +
                                " LEFT JOIN PARAMETERVALUE BK ON EM.BANK = BK.SID" +
                                " WHERE EM.COMPANYSITE = @COMPANYSITE" +
                                " ORDER BY" +
                                " EM.SID ASC", new { CompanySite }).ToList();
        }

        public void Remove(int SID)
        {
            var sqlQuery = (string.Format("Delete From Employee Where SID = {0}", SID));
            this._db.Execute(sqlQuery);
        }

        public Employee Update(Employee EM, string userid)
        {
            var sqlQuery =
                string.Format(@"UPDATE EMPLOYEE SET IDEMPLOYEE = '{0}', EMPLOYEENAME = '{1}', OTHERNAME = '{2}', IDCARD = '{3}', IDFINGERPRINT = '{4}', BIRTHPLACE = '{5}', DOB = '{6}', SEX = '{7}', RACE = '{8}', [ADDRESS] = '{9}', HOMEPHONE = '{10}', MOBILEPHONE = '{11}', EMAIL = '{12}', RELIGION = '{13}', NATIONALITY = '{14}', ZIPCODE = '{15}', EDUCATIONLEVEL = '{16}', INSTITUTION = '{17}', PROGRAMOFSTUDY = '{18}', MARITALSTATUS = '{19}', SPOUSENAME = '{20}', NUMBEROFCHILD = '{21}', FAMILYRICESTATUS = '{22}', NATURETYPE = '{23}', DATEJOIN = '{24}', DEPARTMENT = '{25}', GRADE = '{26}', EMPLOYEETYPE = '{27}', EMPLOYEESECTION = '{28}', EMPLOYEEPOSITION = '{29}', BASICSALARY = '{30}', PAYMENTMETHOD = '{31}', BANK = '{32}', [OWNERSHIP] = '{33}', BANKACCOUNT = '{34}', FAMILYTAXSTATUS = '{35}', NPWPNO = '{36}', JAMSOSTEKNO = '{37}', TERMINATE = '{38}', DATETERMINATE = '{39}', REMARK = '{40}', COMPANYSITE = '{41}', UPDATEBY = {42}, UPDATEDATE = '{43}' WHERE SID = {44}", 
                EM.IDEMPLOYEE, EM.EMPLOYEENAME, EM.OTHERNAME, EM.IDCARD, EM.IDFINGERPRINT, EM.BIRTHPLACE, EM.DOB, EM.SEX, EM.RACE, EM.ADDRESS, EM.HOMEPHONE, EM.MOBILEPHONE, EM.EMAIL, EM.RELIGION, EM.NATIONALITY, EM.ZIPCODE, EM.EDUCATIONLEVEL, EM.INSTITUTION, EM.PROGRAMOFSTUDY, EM.MARITALSTATUS, EM.SPOUSENAME, EM.NUMBEROFCHILD, EM.FAMILYRICESTATUS, EM.NATURETYPE, EM.DATEJOIN, EM.DEPARTMENT, EM.GRADE, EM.EMPLOYEETYPE, EM.EMPLOYEESECTION, EM.EMPLOYEEPOSITION, EM.BASICSALARY, EM.PAYMENTMETHOD, EM.BANK, EM.OWNERSHIP, EM.BANKACCOUNT, EM.FAMILYTAXSTATUS, EM.NPWPNO, EM.JAMSOSTEKNO, EM.TERMINATE, EM.DATETERMINATE, EM.REMARK, EM.COMPANYSITE, Convert.ToInt32(userid), DateTime.Now, EM.SID);
            this._db.Execute(sqlQuery, EM);
            return EM;
        }
    }
}