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
    public class CompanySiteRepository : ICompanySiteRepository
    {
        private IDbConnection _db = new SqlConnection(Constant.DatabaseConnection);

        public CompanySite Add(CompanySite CS)
        {
            var sqlQuery = "INSERT INTO CompanySite (IDCOMPANYSITE, COMPANYSITENAME, COMPANY, REGION, LOCATION, ADDRESS1, ADDRESS2, COUNTRY, PROVINCE, CITY, TELEPHONE1, TELEPHONE2, FAX1, FAX2, EMAIL, WEBSITE, POSCODE, INPUTBY, INPUTDATE, UPDATEBY, UPDATEDATE) VALUES(@IDCOMPANYSITE, @COMPANYSITENAME, @COMPANY, @REGION, @LOCATION, @ADDRESS1, @ADDRESS2, @COUNTRY, @PROVINCE, @CITY, @TELEPHONE1, @TELEPHONE2, @FAX1, @FAX2, @EMAIL, @WEBSITE, @POSCODE, @INPUTBY, @INPUTDATE, @UPDATEBY, @UPDATEDATE); " + "SELECT CAST(SCOPE_IDENTITY() as int)";
            var SID = _db.Query<int>(sqlQuery, CS).Single();
            CS.SID = SID;
            return CS;
        }

        public CompanySite Find(int? SID)
        {
            return this._db.Query<CompanySite>("SELECT b.CompanyName, a.* FROM CompanySite a LEFT JOIN Company b on a.Company = b.SID WHERE a.SID = @SID", new { SID }).SingleOrDefault();
        }

        public List<CompanySite> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Remove(int SID)
        {
            var sqlQuery = ("DELETE From COMPANYSITE Where SID = " + SID + "");
            this._db.Execute(sqlQuery);
        }

        public CompanySite Update(CompanySite CompanySite)
        {
            var sqlQuery =
                 "UPDATE CompanySite " +
                 "SET IDCOMPANYSITE    = @IDCOMPANYSITE, " +
                 "    COMPANYSITENAME  = @COMPANYSITENAME, " +
                   "    LOCATION  = @LOCATION, " +
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
                  "    LOGO        = @LOGO, " +
                 "    WEBSITE      = @WEBSITE, " +
                 "    POSCODE      = @POSCODE, " +
                 "    UPDATEBY     = @UPDATEBY, " +
                 "    UPDATEDATE   = @UPDATEDATE " +
                 "  WHERE SID = @SID";
            this._db.Execute(sqlQuery, CompanySite);
            return CompanySite;
        }
    }
}