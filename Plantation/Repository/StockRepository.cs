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
    public class StockRepository : IStockRepository
    {
        private IDbConnection _db = new SqlConnection(Constant.DatabaseConnection);

        public Stock Add(Stock STK, string userid)
        {
            var sqlQuery = @"INSERT INTO STOCK (IDSTOCK, STOCKNAME, STOCKGROUP, UOM, PARTNUMBER, DESCRIPTION, REMARK, ACTIVE, COMPANY, COMPANYSITE, INPUTBY, INPUTDATE, UPDATEBY, UPDATEDATE) VALUES
                           ('" + STK.IDSTOCK + @"', '" + STK.STOCKNAME + "', '" + STK.STOCKGROUP + @"', '" + STK.UOM + @"', '" + STK.PARTNUMBER + "', '" + STK.DESCRIPTION + @"', '" + STK.REMARK + "', '" + STK.ACTIVE + @"', '" + STK.COMPANY + @"', '" + STK.COMPANYSITE + @"'," + Convert.ToInt32(userid) + ", '" + DateTime.Now + @"', " + Convert.ToInt32(userid) + ", '" + DateTime.Now + @"'); " + "SELECT CAST(SCOPE_IDENTITY() as int)";
            var SID = _db.Query<int>(sqlQuery, STK).Single();
            STK.SID = SID;
            return STK;
        }

        public Stock Find(int? SID)
        {
            return this._db.Query<Stock>("SELECT * FROM Stock WHERE SID = @SID", new { SID }).SingleOrDefault();
        }

        public List<Stock> GetAll()
        {
            return this._db.Query<Stock>("SELECT" +
                               " STK.SID," +
                               " STK.IDSTOCK," +
                               " STK.STOCKNAME," +
                               " STK.STOCKGROUP," +
                               " STK.UOM," +
                               " STK.PARTNUMBER," +
                               " STK.DESCRIPTION," +
                               " STK.REMARK," +
                               " STK.ACTIVE," +
                               " STK.COMPANYSITE," +
                               " STK.INPUTBY," +
                               " STK.INPUTDATE," +
                               " STK.UPDATEBY," +
                               " STK.UPDATEDATE," +
                               " UNITOFMEASURE.UOMNAME," +
                               " STOCKGROUP.STOCKGROUPNAME" +
                               " FROM" +
                               " STOCK AS STK" +
                               " INNER JOIN UNITOFMEASURE ON STK.UOM = UNITOFMEASURE.SID" +
                               " INNER JOIN STOCKGROUP ON STK.STOCKGROUP = STOCKGROUP.SID" +
                               " ORDER BY" +
                               " STK.SID ASC").ToList();
        }

        public void Remove(int SID)
        {
            var sqlQuery = (string.Format("Delete From STOCK Where SID = {0}", SID));
            this._db.Execute(sqlQuery);
        }

        public Stock Update(Stock STK, string userid)
        {
            var sqlQuery =
                string.Format(@"UPDATE STOCK SET IDSTOCK = '{0}', STOCKNAME = '{1}', STOCKGROUP = '{2}', UOM = '{3}', PARTNUMBER = '{4}', DESCRIPTION = '{5}', REMARK = '{6}', ACTIVE = '{7}', COMPANY = '{8}', COMPANYSITE = '{9}', UPDATEBY = {10}, UPDATEDATE = '{11}' WHERE SID = {12}",
                STK.IDSTOCK, STK.STOCKNAME, STK.STOCKGROUP, STK.UOM, STK.PARTNUMBER, STK.DESCRIPTION, STK.REMARK, STK.ACTIVE, STK.COMPANY, STK.COMPANYSITE, Convert.ToInt32(userid), DateTime.Now, STK.SID);
            this._db.Execute(sqlQuery, STK);
            return STK;
        }
    }
}