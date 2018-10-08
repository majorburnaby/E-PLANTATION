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
    public class CompanyRepository : ICompanyRepository
    {
        private IDbConnection _db = new SqlConnection(Constant.DatabaseConnection);

        public Company Add(Company CP)
        {
            var sqlQuery = "INSERT INTO Company (IDCOMPANY, COMPANYNAME, ADDRESS1, ADDRESS2, COUNTRY, PROVINCE, CITY, TELEPHONE1, TELEPHONE2, FAX1, FAX2, EMAIL, WEBSITE, POSCODE, INPUTBY, INPUTDATE, UPDATEBY, UPDATEDATE) VALUES(@IDCOMPANY, @COMPANYNAME, @ADDRESS1, @ADDRESS2, @COUNTRY, @PROVINCE, @CITY, @TELEPHONE1, @TELEPHONE2, @FAX1, @FAX2, @EMAIL, @WEBSITE, @POSCODE, @INPUTBY, @INPUTDATE, @UPDATEBY, @UPDATEDATE); " + "SELECT CAST(SCOPE_IDENTITY() as int)";
            var SID = _db.Query<int>(sqlQuery, CP).Single();
            CP.SID = SID;
            return CP;
        }

        public Company Find(int? SID)
        {
            return this._db.Query<Company>("SELECT * FROM COMPANY WHERE SID = @SID", new { SID }).SingleOrDefault();
        }

        public List<Company> GetAll()
        {
            return this._db.Query<Company>("SELECT" +
                               " CP.SID," +
                               " CP.IDCOMPANY," +
                               " CP.COMPANYNAME," +
                               " CP.ADDRESS1," +
                               " CP.ADDRESS2," +
                               " CP.COUNTRY," +
                               " CP.PROVINCE," +
                               " CP.CITY," +
                               " CP.TELEPHONE1," +
                               " CP.TELEPHONE2," +
                               " CP.FAX1," +
                               " CP.FAX2," +
                               " CP.EMAIL," +
                               " CP.WEBSITE," +
                               " CP.POSCODE," +
                               " CP.INPUTBY," +
                               " CP.INPUTDATE," +
                               " CP.UPDATEBY," +
                               " CP.UPDATEDATE," +
                               " COUNTRY.COUNTRYNAME," +
                               " PROVINCE.PROVINCENAME," +
                               " CITY.CITYNAME" +
                               " FROM" +
                               " COMPANY AS CP" +
                               " INNER JOIN COUNTRY ON CP.COUNTRY = COUNTRY.SID" +
                               " INNER JOIN PROVINCE ON CP.PROVINCE = PROVINCE.SID" +
                               " INNER JOIN CITY ON CP.CITY = CITY.SID" +
                               " ORDER BY" +
                               " CP.SID ASC").ToList();
        }

        public void Remove(int SID)
        {
            var sqlQuery = ("Delete From Company Where SID = " + SID + "");
            this._db.Execute(sqlQuery);
        }

        public Company Update(Company CP)
        {
            var sqlQuery =
                "UPDATE Company " +
                "SET IDCOMPANY    = @IDCOMPANY, " +
                "    COMPANYNAME  = @COMPANYNAME, " +
                "    ADDRESS1     = @ADDRESS1, " +
                "    ADDRESS2     = @ADDRESS2, " +
                "    COUNTRY      = @COUNTRY, " +
                "    PROVINCE     = @PROVINCE, " +
                "    CITY         = @CITY, " +
                "    TELEPHONE1   = @TELEPHONE1, " +
                "    TELEPHONE2   = @TELEPHONE2, " +
                "    FAX1         = @FAX1, " +
                "    FAX2         = @FAX2, " +
                "    EMAIL        = @EMAIL, " +
                "    WEBSITE      = @WEBSITE, " +
                "    POSCODE      = @POSCODE, " +
                "    UPDATEBY     = @UPDATEBY, " +
                "    UPDATEDATE   = @UPDATEDATE " +
                "WHERE SID = @SID";
            this._db.Execute(sqlQuery, CP);
            return CP;
        }

        public List<CompanySite> GetAllCompanySiteByCompany(int Company)
        {
            return this._db.Query<CompanySite>("SELECT" +
                               " CS.SID," +
                               " CS.IDCOMPANYSITE," +
                               " CS.COMPANYSITENAME," +
                               " LOC.LOCATIONNAME," +
                               " CS.ADDRESS1," +
                               " CS.COUNTRY," +
                               " CS.PROVINCE," +
                               " CS.CITY," +
                               " CS.TELEPHONE1," +
                               " CS.TELEPHONE2," +
                               " CS.FAX1," +
                               " CS.FAX2," +
                               " CS.EMAIL," +
                                " CS.LOGO," +
                               " CS.WEBSITE," +
                               " CS.INPUTBY," +
                               " CS.INPUTDATE," +
                               " CS.UPDATEBY," +
                               " CS.UPDATEDATE," +
                               " CT.COUNTRYNAME," +
                               " PR.PROVINCENAME," +
                               " CI.CITYNAME" +
                               " FROM" +
                               " COMPANYSITE AS CS" +
                               " LEFT JOIN LOCATION LOC ON CS.LOCATION = LOC.SID" +
                               " LEFT JOIN COUNTRY CT ON CS.COUNTRY = CT.SID" +
                               " LEFT JOIN PROVINCE PR ON CS.PROVINCE = PR.SID" +
                                " LEFT JOIN CITY CI ON CS.CITY = CI.SID" +
                                 " WHERE CS.COMPANY = " + Company +
                               " ORDER BY" +
                               " CS.SID ASC").ToList();
        }

        public bool HasCompanySite(int SID)
        {
            return this._db.Query<bool>("SELECT COUNT(*) FROM COMPANYSITE WHERE COMPANY = @SID", new { SID }).FirstOrDefault();
        }
    }
}