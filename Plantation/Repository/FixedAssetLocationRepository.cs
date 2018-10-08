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
    public class FixedAssetLocationRepository : IFixedAssetLocationRepository
    {
        private IDbConnection _db = new SqlConnection(Constant.DatabaseConnection);

        public FixedAssetLocation Add(FixedAssetLocation FAL, string userid)
        {
            var sqlQuery = @"INSERT INTO FIXEDASSETLOCATION (IDFIXEDASSETLOCATION, FIXEDASSETLOCATIONNAME, DESCRIPTION, COSTCENTER, ISACTIVE, COMPANYSITE, INPUTBY, INPUTDATE, UPDATEBY, UPDATEDATE) VALUES
                           ('" + FAL.IDFIXEDASSETLOCATION + @"', '" + FAL.FIXEDASSETLOCATIONNAME + "', '" + FAL.DESCRIPTION + @"', '" + FAL.COSTCENTER + @"', '" + FAL.ISACTIVE + @"', '" + FAL.COMPANYSITE + @"'," + Convert.ToInt32(userid) + ", '" + DateTime.Now + @"', " + Convert.ToInt32(userid) + ", '" + DateTime.Now + @"'); " + "SELECT CAST(SCOPE_IDENTITY() as int)";
            var SID = _db.Query<int>(sqlQuery, FAL).Single();
            FAL.SID = SID;
            return FAL;
        }

        public FixedAssetLocation Find(int? SID)
        {
            return this._db.Query<FixedAssetLocation>("SELECT * FROM FixedAssetLocation WHERE SID = @SID", new { SID }).SingleOrDefault();
        }

        public List<FixedAssetLocation> GetAll()
        {
            return this._db.Query<FixedAssetLocation>("SELECT" +
                               " FAL.SID," +
                               " FAL.IDFIXEDASSETLOCATION," +
                               " FAL.FIXEDASSETLOCATIONNAME," +
                               " FAL.DESCRIPTION," +
                               " FAL.COSTCENTER," +
                               " FAL.ISACTIVE," +
                               " FAL.COMPANYSITE," +
                               " FAL.INPUTBY," +
                               " FAL.INPUTDATE," +
                               " FAL.UPDATEBY," +
                               " FAL.UPDATEDATE," +
                               " COSTCENTER.COSTCENTERNAME" +
                               " FROM" +
                               " FIXEDASSETLOCATION AS FAL" +
                               " INNER JOIN COSTCENTER ON FAL.COSTCENTER = COSTCENTER.SID" +
                               " ORDER BY" +
                               " FAL.SID ASC").ToList();
        }

        public void Remove(int SID)
        {
            var sqlQuery = (string.Format("Delete From FixedAssetLocation Where SID = {0}", SID));
            this._db.Execute(sqlQuery);
        }

        public FixedAssetLocation Update(FixedAssetLocation FAL, string userid)
        {
            var sqlQuery =
                string.Format(@"UPDATE FIXEDASSETLOCATION SET IDFIXEDASSETLOCATION = '{0}', FIXEDASSETLOCATIONNAME = '{1}', DESCRIPTION = '{2}', COSCENTER = '{3}', ISACTIVE = '{4}', COMPANYSITE = '{5}', UPDATEBY = {6}, UPDATEDATE = '{7}' WHERE SID = {8}",
                FAL.IDFIXEDASSETLOCATION, FAL.FIXEDASSETLOCATIONNAME, FAL.DESCRIPTION, FAL.COSTCENTER, FAL.ISACTIVE, FAL.COMPANYSITE, Convert.ToInt32(userid), DateTime.Now, FAL.SID);
            this._db.Execute(sqlQuery, FAL);
            return FAL;
        }
    }
}