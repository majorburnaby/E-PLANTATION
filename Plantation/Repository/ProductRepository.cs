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
    public class ProductRepository : IProductRepository
    {
        private IDbConnection _db = new SqlConnection(Constant.DatabaseConnection);

        public Product Add(Product PRD, string userid)
        {
            var sqlQuery = @"INSERT INTO Product (IDPRODUCT, PRODUCTNAME, CROP, LOADTYPE, UOM, INPUTBY, INPUTDATE, UPDATEBY, UPDATEDATE) VALUES
                           ('" + PRD.IDPRODUCT + @"', '" + PRD.PRODUCTNAME + "', '" + PRD.CROP + @"', '" + PRD.LOADTYPE + "', '" + PRD.UOM + "', " + Convert.ToInt32(userid) + ", '" + DateTime.Now + @"', " + Convert.ToInt32(userid) + ", '" + DateTime.Now + @"'); " + "SELECT CAST(SCOPE_IDENTITY() as int)";
            var SID = _db.Query<int>(sqlQuery, PRD).Single();
            PRD.SID = SID;
            return PRD;
        }

        public Product Find(int? SID)
        {
            return this._db.Query<Product>("SELECT * FROM PRODUCT WHERE SID = @id", new { SID }).SingleOrDefault();
        }

        public List<Product> GetAll()
        {
            return this._db.Query<Product>(" SELECT" +
                               " PRD.SID," +
                               " PRD.IDPRODUCT," +
                               " PRD.PRODUCTNAME," +
                               " PRD.CROP," +
                               " PRD.LOADTYPE," +
                               " PRD.UOM," +
                               " PRD.INPUTBY," +
                               " PRD.INPUTDATE," +
                               " PRD.UPDATEBY," +
                               " PRD.UPDATEDATE," +
                               " CROP.CROPNAME," +
                               " LOADTYPE.LOADTYPENAME," +
                               " UNITOFMEASURE.UOMNAME" +
                               " FROM" +
                               " PRODUCT AS PRD" +
                               " INNER JOIN CROP ON PRD.CROP = CROP.SID" +
                               " INNER JOIN LOADTYPE ON PRD.LOADTYPE = LOADTYPE.SID" +
                               " INNER JOIN UNITOFMEASURE ON PRD.UOM = UNITOFMEASURE.SID" +
                               " ORDER BY" +
                               " PRD.SID ASC").ToList();
        }

        public void Remove(int SID)
        {
            var sqlQuery = (string.Format("Delete From PRODUCT Where SID = {0}", SID));
            this._db.Execute(sqlQuery);
        }

        public Product Update(Product PRD, string userid)
        {
            var sqlQuery =
                string.Format(@"UPDATE PRODUCT SET IDPRODUCT = '{0}', PRODUCTNAME = '{1}', CROP = '{2}', LOADTYPE = '{3}', UOM = '{4}', UPDATEBY = {5}, UPDATEDATE = '{6}' WHERE SID = {7}", PRD.IDPRODUCT, PRD.PRODUCTNAME, PRD.CROP, PRD.LOADTYPE, PRD.UOM, Convert.ToInt32(userid), DateTime.Now, PRD.SID);
            this._db.Execute(sqlQuery, PRD);
            return PRD;
        }

        public List<Product> GetAllByUserId(int id)
        {
            using (SqlConnection conn = new SqlConnection(Constant.DatabaseConnection))
            {
                string query = @" SELECT" +
                               " SID," +
                               " IDPRODUCT," +
                               " PRODUCTNAME," +
                               " CROP," +
                               " LOADTYPE," +
                               " UOM," +
                               " FROM" +
                               " PRODUCT AS PRD" +
                               " ORDER BY" +
                               " PRD.SID ASC";
                return conn.Query<Product>(query, new { Id = id }).ToList();
            }
        }
    }
}