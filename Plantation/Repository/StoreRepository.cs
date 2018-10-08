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
    public class StoreRepository : IStoreRepository
    {
        private IDbConnection _db = new SqlConnection(Constant.DatabaseConnection);

        public Store Add(Store STR, string userid)
        {
            var sqlQuery = @"INSERT INTO STORE (IDSTORE, STORENAME, DESCRIPTION, WAREHOUSETYPE, ISACTIVE, COMPANYSITE, INPUTBY, INPUTDATE, UPDATEBY, UPDATEDATE) VALUES
                           ('" + STR.IDSTORE + @"', '" + STR.STORENAME + "', '" + STR.DESCRIPTION + @"', '" + STR.WAREHOUSETYPE + @"', '" + STR.ISACTIVE + @"', '" + STR.COMPANYSITE + @"', " + Convert.ToInt32(userid) + ", '" + DateTime.Now + @"', " + Convert.ToInt32(userid) + ", '" + DateTime.Now + @"'); " + "SELECT CAST(SCOPE_IDENTITY() as int)";
            var SID = _db.Query<int>(sqlQuery, STR).Single();
            STR.SID = SID;
            return STR;
        }

        public Store Find(int? SID)
        {
            return this._db.Query<Store>("SELECT * FROM STORE WHERE SID = @SID", new { SID }).SingleOrDefault();
        }

        public List<Store> GetAll()
        {
            return this._db.Query<Store>("SELECT" +
                               " STR.SID," +
                               " STR.IDSTORE," +
                               " STR.STORENAME," +
                               " STR.DESCRIPTION," +
                               " STR.WAREHOUSETYPE," +
                               " STR.ISACTIVE," +
                               " STR.COMPANYSITE," +
                               " STR.INPUTBY," +
                               " STR.INPUTDATE," +
                               " STR.UPDATEBY," +
                               " STR.UPDATEDATE" +
                               " FROM" +
                               " STORE AS STR" +
                               " ORDER BY" +
                               " STR.SID ASC").ToList();
        }

        public void Remove(int SID)
        {
            var sqlQuery = (string.Format("Delete From Store Where SID = {0}", SID));
            this._db.Execute(sqlQuery);
        }

        public Store Update(Store STR, string userid)
        {
            var sqlQuery =
                string.Format(@"UPDATE STORE SET IDSTORE = '{0}', STORENAME = '{1}', DESCRIPTION = '{2}', WAREHOUSETYPE = '{3}', ISACTIVE = '{4}', COMPANYSITE = '{5}', UPDATEBY = {6}, UPDATEDATE = '{7}' WHERE SID = {8}",
                STR.IDSTORE, STR.STORENAME, STR.DESCRIPTION, STR.WAREHOUSETYPE, STR.ISACTIVE, STR.COMPANYSITE, Convert.ToInt32(userid), DateTime.Now, STR.SID);
            this._db.Execute(sqlQuery, STR);
            return STR;
        }
    }
}