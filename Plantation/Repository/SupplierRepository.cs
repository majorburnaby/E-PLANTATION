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
            var sqlQuery = @"INSERT INTO SUPPLIER (IDSUPPLIER, SUPPLIERNAME, SUPPLIERGROUP, CONTROLJOB, CONTACTNAME, BANK, BANKACCOUNT, PHONE, FAX, EMAIL, ADDRESS, COUNTRY, PROVINCE, CITY, POSCODE, REMARKS, ISACTIVE, ISACTIVEDATE, COMPANY, COMPANYSITE, INPUTBY, INPUTDATE, UPDATEBY, UPDATEDATE) VALUES
                           ('" + SPP.IDSUPPLIER + @"', '" + SPP.SUPPLIERNAME + "', '" + SPP.SUPPLIERGROUP + @"','" + SPP.CONTROLJOB + @"', '" + SPP.CONTACTNAME + @"', '" + SPP.BANK + @"', '" + SPP.BANKACCOUNT + @"', '" + SPP.PHONE + @"', '" + SPP.FAX + @"', '" + SPP.EMAIL + @"', '" + SPP.ADDRESS + @"', '" + SPP.COUNTRY + @"', '" + SPP.PROVINCE + @"', '" + SPP.CITY + @"', '" + SPP.POSCODE + @"', '" + SPP.REMARKS + @"', '" + SPP.ISACTIVE + @"', '" + SPP.ISACTIVEDATE + @"', '" + SPP.COMPANY + @"', '" + SPP.COMPANYSITE + @"'," + Convert.ToInt32(userid) + ", '" + DateTime.Now + @"', " + Convert.ToInt32(userid) + ", '" + DateTime.Now + @"'); " + "SELECT CAST(SCOPE_IDENTITY() as int)";
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
                               " INNER JOIN CONTROLJOB ON SPP.CONTROLJOB = CONTROLJOB.SID" +
                               " INNER JOIN SUPPLIERGROUP ON SPP.SUPPLIERGROUP = SUPPLIERGROUP.SID" +
                               " INNER JOIN PROVINCE ON SPP.PROVINCE = PROVINCE.SID" +
                               " INNER JOIN COUNTRY ON SPP.COUNTRY = COUNTRY.SID" +
                               " INNER JOIN CITY ON SPP.CITY = CITY.SID" +
                               " INNER JOIN PARAMETERVALUE ON SPP.BANK = PARAMETERVALUE.SID" +
                               " ORDER BY" +
                               " SPP.SID ASC").ToList();
        }

        public void Remove(int SID)
        {
            var sqlQuery = (string.Format("Delete From Supplier Where SID = {0}", SID));
            this._db.Execute(sqlQuery);
        }

        public Supplier Update(Supplier SPP, string userid)
        {
            var sqlQuery =
                string.Format(@"UPDATE SUPPLIER SET IDSUPPLIER = '{0}', SUPPLIERNAME = '{1}', SUPPLIERGROUP = '{2}', CONTROLJOB = '{3}', CONTACTNAME = '{4}', BANK = '{5}', BANKACCOUNT = '{6}', PHONE = '{7}', FAX = '{8}', EMAIL = '{9}', ADDRESS = '{10}', COUNTRY = '{11}', PROVINCE = '{12}', CITY = '{13}', POSCODE = '{14}', REMARKS = '{15}', ISACTIVE = '{16}', ISACTIVEDATE = '{17}', COMPANY = '{18}', COMPANYSITE = '{19}', UPDATEBY = {= '{20}'}, UPDATEDATE = '{21}' WHERE SID = {22}",
                SPP.IDSUPPLIER, SPP.SUPPLIERNAME, SPP.SUPPLIERGROUP, SPP.CONTROLJOB, SPP.CONTACTNAME, SPP.BANK, SPP.BANKACCOUNT, SPP.PHONE, SPP.FAX, SPP.EMAIL, SPP.ADDRESS, SPP.COUNTRY, SPP.PROVINCE, SPP.CITY, SPP.POSCODE, SPP.REMARKS, SPP.ISACTIVE, SPP.ISACTIVEDATE, SPP.COMPANY, SPP.COMPANYSITE, Convert.ToInt32(userid), DateTime.Now, SPP.SID);
            this._db.Execute(sqlQuery, SPP);
            return SPP;
        }
    }
}