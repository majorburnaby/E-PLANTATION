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
            var sqlQuery = @"INSERT INTO STOCK (IDSTOCK, STOCKNAME, STOCKGROUP, PARTNUMBER, UOM, ITEMTYPE, MINIMUMSTOCK, MAXIMUMSTOCK, DESCRIPTION, INPUTBY, INPUTDATE, UPDATEBY, UPDATEDATE) VALUES
                           ('" + STK.IDSTOCK + @"', '" + STK.STOCKNAME + "', '" + STK.STOCKGROUP + @"', '" + STK.PARTNUMBER + "', '" + STK.UOM + @"', '" + STK.ITEMTYPE + @"', '" + STK.MINIMUMSTOCK + @"', '" + STK.MAXIMUMSTOCK + @"', '" + STK.DESCRIPTION + @"', " + Convert.ToInt32(userid) + ", '" + DateTime.Now + @"', " + Convert.ToInt32(userid) + ", '" + DateTime.Now + @"'); " + "SELECT CAST(SCOPE_IDENTITY() as int)";
            var SID = _db.Query<int>(sqlQuery, STK).Single();
            STK.SID = SID;
            return STK;
        }

        public Stock Find(int? SID)
        {
            return this._db.Query<Stock>("SELECT * FROM STOCK WHERE SID = @SID", new { SID }).SingleOrDefault();
        }

        public List<Stock> GetAll()
        {
            return this._db.Query<Stock>("SELECT" +
                               " STK.SID," +
                               " STK.IDSTOCK," +
                               " STK.STOCKNAME," +
                               " STK.STOCKGROUP," +
                               " STK.PARTNUMBER," +
                               " STK.UOM," +
                               " STK.ITEMTYPE," +
                               " STK.MINIMUMSTOCK," +
                               " STK.MAXIMUMSTOCK," +
                               " STK.DESCRIPTION," +
                               " STK.INPUTBY," +
                               " STK.INPUTDATE," +
                               " STK.UPDATEBY," +
                               " STK.UPDATEDATE," +
                               " UNITOFMEASURE.UOMNAME," +
                               " STOCKGROUP.STOCKGROUPNAME," +
                               " PARAMETERVALUE.PARAMETERVALUENAME ITEMTYPENAME" +
                               " FROM" +
                               " STOCK AS STK" +
                               " LEFT JOIN UNITOFMEASURE ON STK.UOM = UNITOFMEASURE.SID" +
                               " LEFT JOIN STOCKGROUP ON STK.STOCKGROUP = STOCKGROUP.SID" +
                               " LEFT JOIN PARAMETERVALUE ON STK.ITEMTYPE = PARAMETERVALUE.SID AND PARAMETERVALUE.PARAMETER = 49" +
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
                string.Format(@"UPDATE STOCK SET IDSTOCK = '{0}', STOCKNAME = '{1}', STOCKGROUP = '{2}', PARTNUMBER = '{3}', UOM = '{4}', ITEMTYPE = '{5}', MINIMUMSTOCK = '{6}', MAXIMUMSTOCK = '{7}', DESCRIPTION = '{8}', UPDATEBY = '{9}', UPDATEDATE = '{10}' WHERE SID = {11}",
                STK.IDSTOCK, STK.STOCKNAME, STK.STOCKGROUP, STK.PARTNUMBER, STK.UOM, STK.ITEMTYPE, STK.MINIMUMSTOCK, STK.MAXIMUMSTOCK, STK.DESCRIPTION, Convert.ToInt32(userid), DateTime.Now, STK.SID);
            this._db.Execute(sqlQuery, STK);
            return STK;
        }
    }
}