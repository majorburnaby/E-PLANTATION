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
    public class CurrencyRateRepository : ICurrencyRateRepository
    {
        private IDbConnection _db = new SqlConnection(Constant.DatabaseConnection);

        public CurrencyRate Add(CurrencyRate CR, string userid)
        {
            var sqlQuery = @"INSERT INTO CurrencyRate (CURRENCYMASTER, RATE, INPUTBY, INPUTDATE, UPDATEBY, UPDATEDATE) VALUES
                           ('" + CR.CURRENCYMASTER + @"', '" + CR.RATE + "', " + Convert.ToInt32(userid) + ", '" + DateTime.Now + @"', " + Convert.ToInt32(userid) + ", '" + DateTime.Now + @"'); " + "SELECT CAST(SCOPE_IDENTITY() as int)";
            var SID = _db.Query<int>(sqlQuery, CR).Single();
            CR.SID = SID;
            return CR;
        }

        public CurrencyRate Find(int? SID)
        {
            return this._db.Query<CurrencyRate>("SELECT * FROM CurrencyRate WHERE SID = @SID", new { SID }).SingleOrDefault();
        }

        public List<CurrencyRate> GetAll()
        {
            return this._db.Query<CurrencyRate>("SELECT" +
                              " CR.SID," +
                              " CR.CURRENCYMASTER," +
                              " CR.RATE," +
                              " CR.INPUTBY," +
                              " CR.INPUTDATE," +
                              " CR.UPDATEBY," +
                              " CR.UPDATEDATE" +
                              " FROM" +
                              " CURRENCYRATE AS CR" +
                              " ORDER BY" +
                              " CR.SID ASC").ToList();
        }

        public void Remove(int SID)
        {
            var sqlQuery = (string.Format("Delete From CURRENCYRATE Where SID = {0}", SID));
            this._db.Execute(sqlQuery);
        }

        public CurrencyRate Update(CurrencyRate CR, string userid)
        {
            var sqlQuery =
                string.Format(@"UPDATE CURRENCYRATE SET CURRENCYMASTER = '{0}', RATE = '{1}', UPDATEBY = {2}, UPDATEDATE = '{3}' WHERE SID = {4}", CR.CURRENCYMASTER, CR.RATE, Convert.ToInt32(userid), DateTime.Now, CR.SID);
            this._db.Execute(sqlQuery, CR);
            return CR;
        }
    }
}