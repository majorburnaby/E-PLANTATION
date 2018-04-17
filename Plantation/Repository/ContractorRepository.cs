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
            var sqlQuery = @"INSERT INTO Contractor (IDCONTRACTOR, CONTRACTORNAME, CONTRACTORGROUP, CONTROLJOB, CONTACTNAME, BANK, BANKACCOUNT, ADDRESS, COUNTRY, PROVINCE, CITY, POSCODE, PHONE, FAX, EMAIL, REMARKS, ISACTIVE, ISACTIVEDATE, COMPANY, COMPANYSITE, INPUTBY, INPUTDATE, UPDATEBY, UPDATEDATE) VALUES
                           ('" + CTR.IDCONTRACTOR + @"', '" + CTR.CONTRACTORNAME + "', '" + CTR.CONTRACTORGROUP + @"','" + CTR.CONTROLJOB + @"', '" + CTR.CONTACTNAME + @"', '" + CTR.BANK + @"', '" + CTR.BANKACCOUNT + @"', '" + CTR.ADDRESS + @"', '" + CTR.COUNTRY + @"', '" + CTR.PROVINCE + @"', '" + CTR.CITY + @"', '" + CTR.POSCODE + @"', '" + CTR.PHONE + @"', '" + CTR.FAX + @"', '" + CTR.EMAIL + @"', '" + CTR.REMARKS + @"', '" + CTR.ISACTIVE + @"', '" + CTR.ISACTIVEDATE + @"', '" + CTR.COMPANY + @"', '" + CTR.COMPANYSITE + @"'," + Convert.ToInt32(userid) + ", '" + DateTime.Now + @"', " + Convert.ToInt32(userid) + ", '" + DateTime.Now + @"'); " + "SELECT CAST(SCOPE_IDENTITY() as int)";
            var SID = _db.Query<int>(sqlQuery, CTR).Single();
            CTR.SID = SID;
            return CTR;
        }

        public Contractor Find(int? SID)
        {
            return this._db.Query<Contractor>("SELECT * FROM CONTRACTOR WHERE SID = @SID", new { SID }).SingleOrDefault();
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
                               " CTR.ADDRESS," +
                               " CTR.COUNTRY," +
                               " CTR.PROVINCE," +
                               " CTR.CITY," +
                               " CTR.POSCODE," +
                               " CTR.PHONE," +
                               " CTR.FAX," +
                               " CTR.EMAIL," +
                               " CTR.REMARKS," +
                               " CTR.ISACTIVE," +
                               " CTR.ISACTIVEDATE," +
                               " CTR.COMPANY," +
                               " CTR.COMPANYSITE," +
                               " CTR.INPUTBY," +
                               " CTR.INPUTDATE," +
                               " CTR.UPDATEBY," +
                               " CTR.UPDATEDATE," +
                               " CONTROLJOB.ITEMDESCRIPTION," +
                               " CONTRACTORGROUP.CONTRACTORGROUPNAME," +
                               " COUNTRY.COUNTRYNAME," +
                               " PROVINCE.PROVINCENAME," +
                               " CITY.CITYNAME" +
                               " FROM" +
                               " CONTRACTOR AS CTR" +
                               " INNER JOIN CONTROLJOB ON CTR.CONTROLJOB = CONTROLJOB.SID" +
                               " INNER JOIN CONTRACTORGROUP ON CTR.CONTRACTORGROUP = CONTRACTORGROUP.SID" +
                               " INNER JOIN PROVINCE ON CTR.PROVINCE = PROVINCE.SID" +
                               " INNER JOIN COUNTRY ON CTR.COUNTRY = COUNTRY.SID" +
                               " INNER JOIN CITY ON CTR.CITY = CITY.SID" +
                               " ORDER BY" +
                               " CTR.SID ASC").ToList();
        }

        public void Remove(int SID)
        {
            var sqlQuery = (string.Format("Delete From Contractor Where SID = {0}", SID));
            this._db.Execute(sqlQuery);
        }

        public Contractor Update(Contractor CTR, string userid)
        {
            var sqlQuery =
                string.Format(@"UPDATE CONTRACTOR SET IDCONTRACTOR = '{0}', CONTRACTORNAME = '{1}', CONTRACTORGROUP = '{2}', CONTROLJOB = '{3}', CONTACTNAME = '{4}', BANK = '{5}', BANKACCOUNT = '{6}', ADDRESS = '{7}', COUNTRY = '{8}', PROVINCE = '{9}', CITY = '{10}', POSCODE = '{11}', PHONE = '{12}', FAX = '{13}', EMAIL = '{14}', REMARKS = '{15}', ISACTIVE = '{16}', ISACTIVEDATE = '{17}', COMPANY = '{18}', COMPANYSITE = '{19}', UPDATEBY = {= '{20}'}, UPDATEDATE = '{21}' WHERE SID = {22}",
                CTR.IDCONTRACTOR, CTR.CONTRACTORNAME, CTR.CONTRACTORGROUP, CTR.CONTROLJOB, CTR.CONTACTNAME, CTR.BANK, CTR.BANKACCOUNT, CTR.ADDRESS, CTR.COUNTRY, CTR.PROVINCE, CTR.CITY, CTR.POSCODE, CTR.PHONE, CTR.FAX, CTR.EMAIL, CTR.REMARKS, CTR.ISACTIVE, CTR.ISACTIVEDATE, CTR.COMPANY, CTR.COMPANYSITE, Convert.ToInt32(userid), DateTime.Now, CTR.SID);
            this._db.Execute(sqlQuery, CTR);
            return CTR;
        }
    }
}