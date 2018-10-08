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
    public class SupplierRepository : ISupplierRepository
    {
        private IDbConnection _db = new SqlConnection(Constant.DatabaseConnection);

        public Supplier Add(Supplier SPP, string userid)
        {
            var sqlQuery = @"INSERT INTO SUPPLIER (IDSUPPLIER, SUPPLIERNAME, SUPPLIERGROUP, CONTROLJOB, CONTACTNAME, BANK, BANKACCOUNT, PHONE, FAX, EMAIL, ADDRESS, COUNTRY, PROVINCE, CITY, POSCODE, REMARKS, ISACTIVE, ISACTIVEDATE, COMPANYSITE, INPUTBY, INPUTDATE, UPDATEBY, UPDATEDATE) VALUES
                           ('" + SPP.IDSUPPLIER + @"', '" + SPP.SUPPLIERNAME + "', '" + SPP.SUPPLIERGROUP + @"','" + SPP.CONTROLJOB + @"', '" + SPP.CONTACTNAME + @"', '" + SPP.BANK + @"', '" + SPP.BANKACCOUNT + @"', '" + SPP.PHONE + @"', '" + SPP.FAX + @"', '" + SPP.EMAIL + @"', '" + SPP.ADDRESS + @"', '" + SPP.COUNTRY + @"', '" + SPP.PROVINCE + @"', '" + SPP.CITY + @"', '" + SPP.POSCODE + @"', '" + SPP.REMARKS + @"', '" + SPP.ISACTIVE + @"', '" + SPP.ISACTIVEDATE + @"',  '" + SPP.COMPANYSITE + @"'," + Convert.ToInt32(userid) + ", '" + DateTime.Now + @"', " + Convert.ToInt32(userid) + ", '" + DateTime.Now + @"'); " + "SELECT CAST(SCOPE_IDENTITY() as int)";
            var SID = _db.Query<int>(sqlQuery, SPP).Single();
            SPP.SID = SID;
            return SPP;
        }

        public Supplier Find(int? SID)
        {
            return this._db.Query<Supplier>("SELECT * FROM SUPPLIER WHERE SID = @SID", new { SID }).SingleOrDefault();
        }

        public List<Supplier> GetAll()
        {
            return this._db.Query<Supplier>("SELECT" +
                               " SPP.SID," +
                               " SPP.IDSUPPLIER," +
                               " SPP.SUPPLIERNAME," +
                               " SPP.SUPPLIERGROUP," +
                               " SPP.CONTROLJOB," +
                               " SPP.CONTACTNAME," +
                               " SPP.BANK," +
                               " SPP.BANKACCOUNT," +
                               " SPP.PHONE," +
                               " SPP.FAX," +
                               " SPP.EMAIL," +
                               " SPP.ADDRESS," +
                               " SPP.COUNTRY," +
                               " SPP.PROVINCE," +
                               " SPP.CITY," +
                               " SPP.POSCODE," +
                               " SPP.REMARKS," +
                               " SPP.ISACTIVE," +
                               " SPP.ISACTIVEDATE," +
                               " SPP.COMPANYSITE," +
                               " SPP.INPUTBY," +
                               " SPP.INPUTDATE," +
                               " SPP.UPDATEBY," +
                               " SPP.UPDATEDATE," +
                               " CONTROLJOB.ITEMDESCRIPTION," +
                               " SUPPLIERGROUP.SUPPLIERGROUPNAME," +
                               " PARAMETERVALUE.PARAMETERVALUENAME," +
                               " COUNTRY.COUNTRYNAME," +
                               " PROVINCE.PROVINCENAME," +
                               " CITY.CITYNAME" +
                               " FROM" +
                               " SUPPLIER AS SPP" +
                               " LEFT JOIN CONTROLJOB ON SPP.CONTROLJOB = CONTROLJOB.SID" +
                               " LEFT JOIN SUPPLIERGROUP ON SPP.SUPPLIERGROUP = SUPPLIERGROUP.SID" +
                               " LEFT JOIN PROVINCE ON SPP.PROVINCE = PROVINCE.SID" +
                               " LEFT JOIN COUNTRY ON SPP.COUNTRY = COUNTRY.SID" +
                               " LEFT JOIN CITY ON SPP.CITY = CITY.SID" +
                               " LEFT JOIN PARAMETERVALUE ON SPP.BANK = PARAMETERVALUE.SID" +
                               " ORDER BY" +
                               " PROVINCE.PROVINCENAME, SPP.SID ASC").ToList();
        }

        public List<Supplier> GetAllByCompanySite(int? CompanySite)
        {
            return this._db.Query<Supplier>("SELECT" +
                               " SPP.SID," +
                               " SPP.IDSUPPLIER," +
                               " SPP.SUPPLIERNAME," +
                               " SPP.SUPPLIERGROUP," +
                               " SPP.CONTROLJOB," +
                               " SPP.CONTACTNAME," +
                               " SPP.BANK," +
                               " SPP.BANKACCOUNT," +
                               " SPP.PHONE," +
                               " SPP.FAX," +
                               " SPP.EMAIL," +
                               " SPP.ADDRESS," +
                               " SPP.COUNTRY," +
                               " SPP.PROVINCE," +
                               " SPP.CITY," +
                               " SPP.POSCODE," +
                               " SPP.REMARKS," +
                               " SPP.ISACTIVE," +
                               " SPP.ISACTIVEDATE," +
                               " SPP.COMPANYSITE," +
                               " SPP.INPUTBY," +
                               " SPP.INPUTDATE," +
                               " SPP.UPDATEBY," +
                               " SPP.UPDATEDATE," +
                               " CONTROLJOB.ITEMDESCRIPTION," +
                               " SUPPLIERGROUP.SUPPLIERGROUPNAME," +
                               " PARAMETERVALUE.PARAMETERVALUENAME," +
                               " COUNTRY.COUNTRYNAME," +
                               " PROVINCE.PROVINCENAME," +
                               " CITY.CITYNAME" +
                               " FROM" +
                               " SUPPLIER AS SPP" +
                               " LEFT JOIN CONTROLJOB ON SPP.CONTROLJOB = CONTROLJOB.SID" +
                               " LEFT JOIN SUPPLIERGROUP ON SPP.SUPPLIERGROUP = SUPPLIERGROUP.SID" +
                               " LEFT JOIN PROVINCE ON SPP.PROVINCE = PROVINCE.SID" +
                               " LEFT JOIN COUNTRY ON SPP.COUNTRY = COUNTRY.SID" +
                               " LEFT JOIN CITY ON SPP.CITY = CITY.SID" +
                               " LEFT JOIN PARAMETERVALUE ON SPP.BANK = PARAMETERVALUE.SID" +
                               " WHERE SPP.COMPANYSITE = @COMPANYSITE" +
                               " ORDER BY" +
                               " PROVINCE.PROVINCENAME, SPP.SID ASC", new { CompanySite }).ToList();
        }

        public void Remove(int SID)
        {
            var sqlQuery = (string.Format("Delete From Supplier Where SID = {0}", SID));
            this._db.Execute(sqlQuery);
        }

        public Supplier Update(Supplier SPP, string userid)
        {
            var sqlQuery =
                string.Format(@"UPDATE SUPPLIER SET SUPPLIERNAME = '{0}', SUPPLIERGROUP = '{1}', CONTROLJOB = '{2}', CONTACTNAME = '{3}', BANK = '{4}', BANKACCOUNT = '{5}', PHONE = '{6}', FAX = '{7}', EMAIL = '{8}', ADDRESS = '{9}', COUNTRY = '{10}', PROVINCE = '{11}', CITY = '{12}', POSCODE = '{13}', REMARKS = '{14}', ISACTIVE = '{15}', ISACTIVEDATE = '{16}', COMPANYSITE = '{17}', UPDATEBY = {18}, UPDATEDATE = '{19}' WHERE SID = {20}",
                SPP.SUPPLIERNAME, SPP.SUPPLIERGROUP, SPP.CONTROLJOB, SPP.CONTACTNAME, SPP.BANK, SPP.BANKACCOUNT, SPP.PHONE, SPP.FAX, SPP.EMAIL, SPP.ADDRESS, SPP.COUNTRY, SPP.PROVINCE, SPP.CITY, SPP.POSCODE, SPP.REMARKS, SPP.ISACTIVE, SPP.ISACTIVEDATE, SPP.COMPANYSITE, Convert.ToInt32(userid), DateTime.Now, SPP.SID);
            //var sqlQuery = @"UPDATE SUPPLIER SET IDSUPPLIER = @IDSUPPLIER, SUPPLIERNAME = @SUPPLIERNAME, SUPPLIERGROUP = @SUPPLIERGROUP, CONTROLJOB = @CONTROLJOB, CONTACTNAME = @CONTACTNAME, BANK = @BANK, BANKACCOUNT = @BANKACCOUNT, PHONE = @PHONE, FAX = @FAX, EMAIL = @EMAIL, ADDRESS = @ADDRESS, COUNTRY = @COUNTRY, PROVINCE = @PROVINCE, CITY = @CITY, POSCODE = @POSCODE, REMARKS = @REMARKS, ISACTIVE = @ISACTIVE, ISACTIVEDATE = @ISACTIVEDATE, COMPANY = @COMPANY, COMPANYSITE = @COMPANYSITE, UPDATEBY = @UPDATEBY, UPDATEDATE = @UPDATEDATE WHERE SID = " + SPP.SID;
            this._db.Execute(sqlQuery, SPP);
            return SPP;
        }
    }
}