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
    public class CropRepository : ICropRepository
    {
        private IDbConnection _db = new SqlConnection(Constant.DatabaseConnection);

        public Crop Add(Crop CR, string userid)
        {
            var sqlQuery = @"INSERT INTO Crop (IDCROP, CROPNAME, INPUTBY, INPUTDATE, UPDATEBY, UPDATEDATE) VALUES
                           ('" + CR.IDCROP + @"', '" + CR.CROPNAME + "', " + Convert.ToInt32(userid) + ", '" + DateTime.Now + @"', " + Convert.ToInt32(userid) + ", '" + DateTime.Now + @"'); " + "SELECT CAST(SCOPE_IDENTITY() as int)";
            var SID = _db.Query<int>(sqlQuery, CR).Single();
            CR.SID = SID;
            return CR;
        }

        public Crop Find(int? SID)
        {
            return this._db.Query<Crop>("SELECT * FROM Crop WHERE SID = @SID", new { SID }).SingleOrDefault();
        }

        public List<Crop> GetAll()
        {
            return this._db.Query<Crop>(" SELECT" +
                               " CR.SID," +
                               " CR.IDCROP," +
                               " CR.CROPNAME," +
                               " CR.INPUTBY," +
                               " CR.INPUTDATE," +
                               " CR.UPDATEBY," +
                               " CR.UPDATEDATE" +
                               " FROM" +
                               " CROP AS CR" +
                               " ORDER BY" +
                               " CR.SID ASC").ToList();
        }

        public void Remove(int SID)
        {
            var sqlQuery = (string.Format("Delete From CROP Where SID = {0}", SID));
            this._db.Execute(sqlQuery);
        }

        public Crop Update(Crop CR, string userid)
        {
            var sqlQuery =
                string.Format(@"UPDATE CROP SET IDCROP = '{0}', CROPNAME = '{1}', UPDATEBY = {2}, UPDATEDATE = '{3}' WHERE SID = {4}", CR.IDCROP, CR.CROPNAME, Convert.ToInt32(userid), DateTime.Now, CR.SID);
            this._db.Execute(sqlQuery, CR);
            return CR;
        }
    }
}