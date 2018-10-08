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
    public class CurrencyMasterRepository : ICurrencyMasterRepository
    {
        private IDbConnection _db = new SqlConnection(Constant.DatabaseConnection);

        public CurrencyMaster Add(CurrencyMaster CR, string userid)
        {
            var sqlQuery = @"INSERT INTO CurrencyMaster (IDCURRENCY, CURRENCYNAME, INPUTBY, INPUTDATE, UPDATEBY, UPDATEDATE) VALUES
                           ('" + CR.IDCURRENCY + @"', '" + CR.CURRENCYNAME + "', " + Convert.ToInt32(userid) + ", '" + DateTime.Now + @"', " + Convert.ToInt32(userid) + ", '" + DateTime.Now + @"'); " + "SELECT CAST(SCOPE_IDENTITY() as int)";
            var SID = _db.Query<int>(sqlQuery, CR).Single();
            CR.SID = SID;
            return CR;
        }

        public CurrencyMaster Find(int? SID)
        {
            return this._db.Query<CurrencyMaster>("SELECT * FROM CurrencyMaster WHERE SID = @SID", new { SID }).SingleOrDefault();
        }

        public List<CurrencyMaster> GetAll()
        {
            return this._db.Query<CurrencyMaster>("SELECT" +
                              " CR.SID," +
                              " CR.IDCURRENCY," +
                              " CR.CURRENCYNAME," +
                              " CR.INPUTBY," +
                              " CR.INPUTDATE," +
                              " CR.UPDATEBY," +
                              " CR.UPDATEDATE" +
                              " FROM" +
                              " CURRENCYMASTER AS CR" +
                              " ORDER BY" +
                              " CR.SID ASC").ToList();
        }

        internal void Update(CurrencyRate currencyrate, string v)
        {
            throw new NotImplementedException();
        }

        public void Remove(int SID)
        {
            var sqlQuery = (string.Format("Delete From CURRENCYMASTER Where SID = {0}", SID));
            this._db.Execute(sqlQuery);
        }

        public CurrencyMaster Update(CurrencyMaster CR, string userid)
        {
            var sqlQuery =
                string.Format(@"UPDATE CURRENCYMASTER SET IDCURRENCY = '{0}', CURRENCYNAME = '{1}', UPDATEBY = {2}, UPDATEDATE = '{3}' WHERE SID = {4}", CR.IDCURRENCY, CR.CURRENCYNAME, Convert.ToInt32(userid), DateTime.Now, CR.SID);
            this._db.Execute(sqlQuery, CR);
            return CR;
        }
    }
}