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
    public class TransporterRepository : ITransporterRepository
    {
        private IDbConnection _db = new SqlConnection(Constant.DatabaseConnection);

        public Transporter Add(Transporter TSP, string userid)
        {
            var sqlQuery = @"INSERT INTO TRANSPORTER (IDTRANSPORTER, TRANSPORTERNAME, TRANSPORTERGROUP, CONTROLJOB, CONTACTNAME, BANK, BANKACCOUNT, PHONE, FAX, EMAIL, ADDRESS, COUNTRY, PROVINCE, CITY, POSCODE, REMARKS, ISACTIVE, ISACTIVEDATE, COMPANY, COMPANYSITE, INPUTBY, INPUTDATE, UPDATEBY, UPDATEDATE) VALUES
                           ('" + TSP.IDTRANSPORTER + @"', '" + TSP.TRANSPORTERNAME + "', '" + TSP.TRANSPORTERGROUP + @"','" + TSP.CONTROLJOB + @"', '" + TSP.CONTACTNAME + @"', '" + TSP.BANK + @"', '" + TSP.BANKACCOUNT + @"', '" + TSP.PHONE + @"', '" + TSP.FAX + @"', '" + TSP.EMAIL + @"', '" + TSP.ADDRESS + @"', '" + TSP.COUNTRY + @"', '" + TSP.PROVINCE + @"', '" + TSP.CITY + @"', '" + TSP.POSCODE + @"', '" + TSP.REMARKS + @"', '" + TSP.ISACTIVE + @"', '" + TSP.ISACTIVEDATE + @"', '" + TSP.COMPANY + @"', '" + TSP.COMPANYSITE + @"'," + Convert.ToInt32(userid) + ", '" + DateTime.Now + @"', " + Convert.ToInt32(userid) + ", '" + DateTime.Now + @"'); " + "SELECT CAST(SCOPE_IDENTITY() as int)";
            var SID = _db.Query<int>(sqlQuery, TSP).Single();
            TSP.SID = SID;
            return TSP;
        }

        public Transporter Find(int? SID)
        {
            return this._db.Query<Transporter>("SELECT * FROM TRANSPORTER WHERE SID = @SID", new { SID }).SingleOrDefault();
        }

        public List<Transporter> GetAll()
        {
            return this._db.Query<Transporter>("SELECT" +
                               " TSP.SID," +
                               " TSP.IDTRANSPORTER," +
                               " TSP.TRANSPORTERNAME," +
                               " TSP.TRANSPORTERGROUP," +
                               " TSP.CONTROLJOB," +
                               " TSP.CONTACTNAME," +
                               " TSP.BANK," +
                               " TSP.BANKACCOUNT," +
                               " TSP.PHONE," +
                               " TSP.FAX," +
                               " TSP.EMAIL," +
                               " TSP.ADDRESS," +
                               " TSP.COUNTRY," +
                               " TSP.PROVINCE," +
                               " TSP.CITY," +
                               " TSP.POSCODE," +
                               " TSP.REMARKS," +
                               " TSP.ISACTIVE," +
                               " TSP.ISACTIVEDATE," +
                               " TSP.COMPANY," +
                               " TSP.COMPANYSITE," +
                               " TSP.INPUTBY," +
                               " TSP.INPUTDATE," +
                               " TSP.UPDATEBY," +
                               " TSP.UPDATEDATE," +
                               " CONTROLJOB.ITEMDESCRIPTION," +
                               " TRANSPORTERGROUP.TRANSPORTERGROUPNAME," +
                               " PARAMETERVALUE.PARAMETERVALUENAME," +
                               " COUNTRY.COUNTRYNAME," +
                               " PROVINCE.PROVINCENAME," +
                               " CITY.CITYNAME" +
                               " FROM" +
                               " TRANSPORTER AS TSP" +
                               " LEFT JOIN CONTROLJOB ON TSP.CONTROLJOB = CONTROLJOB.SID" +
                               " LEFT JOIN TRANSPORTERGROUP ON TSP.TRANSPORTERGROUP = TRANSPORTERGROUP.SID" +
                               " LEFT JOIN PROVINCE ON TSP.PROVINCE = PROVINCE.SID" +
                               " LEFT JOIN COUNTRY ON TSP.COUNTRY = COUNTRY.SID" +
                               " LEFT JOIN CITY ON TSP.CITY = CITY.SID" +
                               " LEFT JOIN PARAMETERVALUE ON TSP.BANK = PARAMETERVALUE.SID" +
                               " ORDER BY" +
                               " TSP.SID ASC").ToList();
        }

        public List<Transporter> GetAllByCompanySite(int? CompanySite)
        {
            return this._db.Query<Transporter>("SELECT" +
                               " TSP.SID," +
                               " TSP.IDTRANSPORTER," +
                               " TSP.TRANSPORTERNAME," +
                               " TSP.TRANSPORTERGROUP," +
                               " TSP.CONTROLJOB," +
                               " TSP.CONTACTNAME," +
                               " TSP.BANK," +
                               " TSP.BANKACCOUNT," +
                               " TSP.PHONE," +
                               " TSP.FAX," +
                               " TSP.EMAIL," +
                               " TSP.ADDRESS," +
                               " TSP.COUNTRY," +
                               " TSP.PROVINCE," +
                               " TSP.CITY," +
                               " TSP.POSCODE," +
                               " TSP.REMARKS," +
                               " TSP.ISACTIVE," +
                               " TSP.ISACTIVEDATE," +
                               " TSP.COMPANY," +
                               " TSP.COMPANYSITE," +
                               " TSP.INPUTBY," +
                               " TSP.INPUTDATE," +
                               " TSP.UPDATEBY," +
                               " TSP.UPDATEDATE," +
                               " CONTROLJOB.ITEMDESCRIPTION," +
                               " TRANSPORTERGROUP.TRANSPORTERGROUPNAME," +
                               " PARAMETERVALUE.PARAMETERVALUENAME," +
                               " COUNTRY.COUNTRYNAME," +
                               " PROVINCE.PROVINCENAME," +
                               " CITY.CITYNAME" +
                               " FROM" +
                               " TRANSPORTER AS TSP" +
                               " LEFT JOIN CONTROLJOB ON TSP.CONTROLJOB = CONTROLJOB.SID" +
                               " LEFT JOIN TRANSPORTERGROUP ON TSP.TRANSPORTERGROUP = TRANSPORTERGROUP.SID" +
                               " LEFT JOIN PROVINCE ON TSP.PROVINCE = PROVINCE.SID" +
                               " LEFT JOIN COUNTRY ON TSP.COUNTRY = COUNTRY.SID" +
                               " LEFT JOIN CITY ON TSP.CITY = CITY.SID" +
                               " LEFT JOIN PARAMETERVALUE ON TSP.BANK = PARAMETERVALUE.SID" +
                               " WHERE TSP.COMPANYSITE = @COMPANYSITE" +
                               " ORDER BY" +
                               " TSP.SID ASC", new { CompanySite }).ToList();
        }

        public void Remove(int SID)
        {
            var sqlQuery = (string.Format("Delete From Transporter Where SID = {0}", SID));
            this._db.Execute(sqlQuery);
        }

        public Transporter Update(Transporter TSP, string userid)
        {
            var sqlQuery = @"UPDATE TRANSPORTER SET IDTRANSPORTER = @IDTRANSPORTER, TRANSPORTERNAME = @TRANSPORTERNAME, TRANSPORTERGROUP = @TRANSPORTERGROUP, CONTROLJOB = @CONTROLJOB, CONTACTNAME = @CONTACTNAME, BANK = @BANK, BANKACCOUNT = @BANKACCOUNT, PHONE = @PHONE, FAX = @FAX, EMAIL = @EMAIL, ADDRESS = @ADDRESS, COUNTRY = @COUNTRY, PROVINCE = @PROVINCE, CITY = @CITY, POSCODE = @POSCODE, REMARKS = @REMARKS, ISACTIVE = @ISACTIVE, ISACTIVEDATE = @ISACTIVEDATE, COMPANY = @COMPANY, COMPANY = @COMPANY, COMPANYSITE = @COMPANYSITE, UPDATEBY = @UPDATEBY, UPDATEDATE = @UPDATEDATE WHERE SID = " + TSP.SID;
            this._db.Execute(sqlQuery, TSP);
            return TSP;
        }
    }
}