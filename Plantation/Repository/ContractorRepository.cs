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
    public class ContractorRepository : IContractorRepository
    {
        private IDbConnection _db = new SqlConnection(Constant.DatabaseConnection);

        public Contractor Add(Contractor CTR, string userid)
        {
            var sqlQuery = @"INSERT INTO CONTRACTOR (IDCONTRACTOR, CONTRACTORNAME, CONTRACTORGROUP, CONTROLJOB, CONTACTNAME, BANK, BANKACCOUNT, PHONE, FAX, EMAIL, ADDRESS, COUNTRY, PROVINCE, CITY, POSCODE, REMARKS, ISACTIVE, ISACTIVEDATE, COMPANY, COMPANYSITE, INPUTBY, INPUTDATE, UPDATEBY, UPDATEDATE) VALUES
                           ('" + CTR.IDCONTRACTOR + @"', '" + CTR.CONTRACTORNAME + "', '" + CTR.CONTRACTORGROUP + @"','" + CTR.CONTROLJOB + @"', '" + CTR.CONTACTNAME + @"', '" + CTR.BANK + @"', '" + CTR.BANKACCOUNT + @"', '" + CTR.PHONE + @"', '" + CTR.FAX + @"', '" + CTR.EMAIL + @"', '" + CTR.ADDRESS + @"', '" + CTR.COUNTRY + @"', '" + CTR.PROVINCE + @"', '" + CTR.CITY + @"', '" + CTR.POSCODE + @"', '" + CTR.REMARKS + @"', '" + CTR.ISACTIVE + @"', '" + CTR.ISACTIVEDATE + @"', '" + CTR.COMPANY + @"', '" + CTR.COMPANYSITE + @"'," + Convert.ToInt32(userid) + ", '" + DateTime.Now + @"', " + Convert.ToInt32(userid) + ", '" + DateTime.Now + @"'); " + "SELECT CAST(SCOPE_IDENTITY() as int)";
            var SID = _db.Query<int>(sqlQuery, CTR).Single();
            CTR.SID = SID;
            return CTR;
        }

        public Contractor Find(int? SID)
        {
            return this._db.Query<Contractor>("SELECT * FROM Contractor WHERE SID = @SID", new { SID }).SingleOrDefault();
        }

        public List<Contractor> GetAll()
        {
            return this._db.Query<Contractor>("SELECT" +
                               " CTR.SID," +
                               " CTR.IDCONTRACTOR," +
                               " CTR.CONTRACTORNAME," +
                               " CTR.CONTRACTORGROUP," +
                               " CTR.CONTROLJOB," +
                               " CTR.CONTACTNAME," +
                               " CTR.BANK," +
                               " CTR.BANKACCOUNT," +
                               " CTR.PHONE," +
                               " CTR.FAX," +
                               " CTR.EMAIL," +
                               " CTR.ADDRESS," +
                               " CTR.COUNTRY," +
                               " CTR.PROVINCE," +
                               " CTR.CITY," +
                               " CTR.POSCODE," +                               
                               " CTR.REMARKS," +
                               " CTR.ISACTIVE," +
                               " CTR.ISACTIVEDATE," +
                               " CTR.COMPANYSITE," +
                               " CTR.INPUTBY," +
                               " CTR.INPUTDATE," +
                               " CTR.UPDATEBY," +
                               " CTR.UPDATEDATE," +
                               " CONTROLJOB.ITEMDESCRIPTION," +
                               " CONTRACTORGROUP.CONTRACTORGROUPNAME," +
                               " PARAMETERVALUE.PARAMETERVALUENAME," +
                               " COUNTRY.COUNTRYNAME," +
                               " PROVINCE.PROVINCENAME," +
                               " CITY.CITYNAME" +
                               " FROM" +
                               " CONTRACTOR AS CTR" +
                               " LEFT JOIN CONTROLJOB ON CTR.CONTROLJOB = CONTROLJOB.SID" +
                               " LEFT JOIN CONTRACTORGROUP ON CTR.CONTRACTORGROUP = CONTRACTORGROUP.SID" +
                               " LEFT JOIN PROVINCE ON CTR.PROVINCE = PROVINCE.SID" +
                               " LEFT JOIN COUNTRY ON CTR.COUNTRY = COUNTRY.SID" +
                               " LEFT JOIN CITY ON CTR.CITY = CITY.SID" +
                               " LEFT JOIN PARAMETERVALUE ON CTR.BANK = PARAMETERVALUE.SID" +
                               " ORDER BY" +
                               " CTR.SID ASC").ToList();
        }

        public List<Contractor> GetAllByCompanySite(int? CompanySite)
        {
            return this._db.Query<Contractor>("SELECT" +
                               " CTR.SID," +
                               " CTR.IDCONTRACTOR," +
                               " CTR.CONTRACTORNAME," +
                               " CTR.CONTRACTORGROUP," +
                               " CTR.CONTROLJOB," +
                               " CTR.CONTACTNAME," +
                               " CTR.BANK," +
                               " CTR.BANKACCOUNT," +
                               " CTR.PHONE," +
                               " CTR.FAX," +
                               " CTR.EMAIL," +
                               " CTR.ADDRESS," +
                               " CTR.COUNTRY," +
                               " CTR.PROVINCE," +
                               " CTR.CITY," +
                               " CTR.POSCODE," +
                               " CTR.REMARKS," +
                               " CTR.ISACTIVE," +
                               " CTR.ISACTIVEDATE," +
                               " CTR.COMPANYSITE," +
                               " CTR.INPUTBY," +
                               " CTR.INPUTDATE," +
                               " CTR.UPDATEBY," +
                               " CTR.UPDATEDATE," +
                               " CONTROLJOB.ITEMDESCRIPTION," +
                               " CONTRACTORGROUP.CONTRACTORGROUPNAME," +
                               " PARAMETERVALUE.PARAMETERVALUENAME," +
                               " COUNTRY.COUNTRYNAME," +
                               " PROVINCE.PROVINCENAME," +
                               " CITY.CITYNAME" +
                               " FROM" +
                               " CONTRACTOR AS CTR" +
                               " LEFT JOIN CONTROLJOB ON CTR.CONTROLJOB = CONTROLJOB.SID" +
                               " LEFT JOIN CONTRACTORGROUP ON CTR.CONTRACTORGROUP = CONTRACTORGROUP.SID" +
                               " LEFT JOIN PROVINCE ON CTR.PROVINCE = PROVINCE.SID" +
                               " LEFT JOIN COUNTRY ON CTR.COUNTRY = COUNTRY.SID" +
                               " LEFT JOIN CITY ON CTR.CITY = CITY.SID" +
                               " LEFT JOIN PARAMETERVALUE ON CTR.BANK = PARAMETERVALUE.SID" +
                               " WHERE CTR.COMPANYSITE = @COMPANYSITE" +
                               " ORDER BY" +
                               " CTR.SID ASC", new { CompanySite }).ToList();
        }

        public void Remove(int SID)
        {
            var sqlQuery = (string.Format("Delete From Contractor Where SID = {0}", SID));
            this._db.Execute(sqlQuery);
        }

        public Contractor Update(Contractor CTR, string userid)
        {            
            var sqlQuery = @"UPDATE CONTRACTOR SET IDCONTRACTOR = @IDCONTRACTOR, CONTRACTORNAME = @CONTRACTORNAME, CONTRACTORGROUP = @CONTRACTORGROUP, CONTROLJOB = @CONTROLJOB, CONTACTNAME = @CONTACTNAME, BANK = @BANK, BANKACCOUNT = @BANKACCOUNT, PHONE = @PHONE, FAX = @FAX, EMAIL = @EMAIL, ADDRESS = @ADDRESS, COUNTRY = @COUNTRY, PROVINCE = @PROVINCE, CITY = @CITY, POSCODE = @POSCODE, REMARKS = @REMARKS, ISACTIVE = @ISACTIVE, ISACTIVEDATE = @ISACTIVEDATE, COMPANY = @COMPANY, COMPANYSITE = @COMPANYSITE, UPDATEBY = @UPDATEBY, UPDATEDATE = @UPDATEDATE WHERE SID = " + CTR.SID;
            this._db.Execute(sqlQuery, CTR);
            return CTR;
        }
    }
}