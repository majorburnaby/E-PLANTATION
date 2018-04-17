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
    public class UnitOfMeasureRepository : IUnitOfMeasureRepository
    {
        private IDbConnection _db = new SqlConnection(Constant.DatabaseConnection);

        public UnitOfMeasure Add(UnitOfMeasure UOM, string userid)
        {
            var sqlQuery = @"INSERT INTO UnitOfMeasure (IDUOM, UOMNAME, ISACTIVE, INPUTBY, INPUTDATE, UPDATEBY, UPDATEDATE) VALUES
                           ('" + UOM.IDUOM + @"', '" + UOM.UOMNAME + "', '" + UOM.ISACTIVE + "', " + Convert.ToInt32(userid) + ", '" + DateTime.Now + @"', " + Convert.ToInt32(userid) + ", '" + DateTime.Now + @"'); " + "SELECT CAST(SCOPE_IDENTITY() as int)";
            var SID = _db.Query<int>(sqlQuery, UOM).Single();
            UOM.SID = SID;
            return UOM;
        }

        public UnitOfMeasure Find(int? SID)
        {
            return this._db.Query<UnitOfMeasure>("SELECT * FROM UnitOfMeasure WHERE SID = @SID", new { SID }).SingleOrDefault();
        }

        public List<UnitOfMeasure> GetAll()
        {
            return this._db.Query<UnitOfMeasure>("SELECT" +
                               " UOM.SID," +
                               " UOM.IDUOM," +
                               " UOM.UOMNAME," +
                               " UOM.ISACTIVE," +
                               " UOM.INPUTBY," +
                               " UOM.INPUTDATE," +
                               " UOM.UPDATEBY," +
                               " UOM.UPDATEDATE" +
                               " FROM" +
                               " UNITOFMEASURE AS UOM" +
                               " ORDER BY" +
                               " UOM.SID ASC").ToList();
        }

        public void Remove(int SID)
        {
            var sqlQuery = (string.Format("Delete From UNITOFMEASURE Where SID = {0}", SID));
            this._db.Execute(sqlQuery);
        }

        public UnitOfMeasure Update(UnitOfMeasure UOM, string userid)
        {
            var sqlQuery =
                string.Format(@"UPDATE UNITOFMEASURE SET IDUOM = '{0}', UOMNAME = '{1}', ISACTIVE = '{2}', UPDATEBY = {3}, UPDATEDATE = '{4}' WHERE SID = {5}", UOM.IDUOM, UOM.UOMNAME, UOM.ISACTIVE, Convert.ToInt32(userid), DateTime.Now, UOM.SID);
            this._db.Execute(sqlQuery, UOM);
            return UOM;
        }

        public List<UnitOfMeasure> GetAllByUserId(int id)
        {
            using (SqlConnection conn = new SqlConnection(Constant.DatabaseConnection))
            {
                string query = @" SELECT" +
                               " SID," +
                               " IDUOM," +
                               " UOMNAME," +
                               " ISACTIVE," +
                               " FROM" +
                               " UNITOFMEASURE AS UOM" +
                               " ORDER BY" +
                               " UOM.SID ASC";
                return conn.Query<UnitOfMeasure>(query, new { Id = id }).ToList();
            }
        }
    }
}