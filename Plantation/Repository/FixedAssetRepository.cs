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
    public class FixedAssetRepository : IFixedAssetRepository
    {
        private IDbConnection _db = new SqlConnection(Constant.DatabaseConnection);

        public FixedAsset Add(FixedAsset FA, string userid)
        {
            var sqlQuery = @"INSERT INTO FixedAsset (IDFIXEDASSET, FIXEDASSETNAME, FIXEDASSETGROUP, UOM, DESCRIPTION, REMARK, ACTIVE, COMPANY, COMPANYSITE, INPUTBY, INPUTDATE, UPDATEBY, UPDATEDATE) VALUES
                           ('" + FA.IDFIXEDASSET + @"', '" + FA.FIXEDASSETNAME + "', '" + FA.FIXEDASSETGROUP + @"', '" + FA.UOM + @"', '" + FA.DESCRIPTION + @"', '" + FA.REMARK + "', '" + FA.ACTIVE + @"', '" + FA.COMPANY + @"', '" + FA.COMPANYSITE + @"'," + Convert.ToInt32(userid) + ", '" + DateTime.Now + @"', " + Convert.ToInt32(userid) + ", '" + DateTime.Now + @"'); " + "SELECT CAST(SCOPE_IDENTITY() as int)";
            var SID = _db.Query<int>(sqlQuery, FA).Single();
            FA.SID = SID;
            return FA;
        }

        public FixedAsset Find(int? SID)
        {
            return this._db.Query<FixedAsset>("SELECT * FROM FIXEDASSET WHERE SID = @SID", new { SID }).SingleOrDefault();
        }

        public List<FixedAsset> GetAll()
        {
            return this._db.Query<FixedAsset>("SELECT" +
                               " FA.SID," +
                               " FA.IDFixedAsset," +
                               " FA.FixedAssetNAME," +
                               " FA.FixedAssetGROUP," +
                               " FA.UOM," +
                               " FA.DESCRIPTION," +
                               " FA.REMARK," +
                               " FA.ACTIVE," +
                               " FA.COMPANYSITE," +
                               " FA.INPUTBY," +
                               " FA.INPUTDATE," +
                               " FA.UPDATEBY," +
                               " FA.UPDATEDATE," +
                               " UNITOFMEASURE.UOMNAME," +
                               " FIXEDASSETGROUP.FAGROUPNAME" +
                               " FROM" +
                               " FIXEDASSET AS FA" +
                               " INNER JOIN UNITOFMEASURE ON FA.UOM = UNITOFMEASURE.SID" +
                               " INNER JOIN FIXEDASSETGROUP ON FA.FIXEDASSETGROUP = FIXEDASSETGROUP.SID" +
                               " ORDER BY" +
                               " FA.SID ASC").ToList();
        }

        public void Remove(int SID)
        {
            var sqlQuery = (string.Format("Delete From FixedAsset Where SID = {0}", SID));
            this._db.Execute(sqlQuery);
        }

        public FixedAsset Update(FixedAsset FA, string userid)
        {
            var sqlQuery =
                string.Format(@"UPDATE FIXEDASSET SET IDFIXEDASSET = '{0}', FIXEDASSETNAME = '{1}', FIXEDASSETGROUP = '{2}', UOM = '{3}', DESCRIPTION = '{4}', REMARK = '{5}', ACTIVE = '{6}', COMPANY = '{7}', COMPANYSITE = '{8}', UPDATEBY = {9}, UPDATEDATE = '{10}' WHERE SID = {11}",
                FA.IDFIXEDASSET, FA.FIXEDASSETNAME, FA.FIXEDASSETGROUP, FA.UOM, FA.DESCRIPTION, FA.REMARK, FA.ACTIVE, FA.COMPANY, FA.COMPANYSITE, Convert.ToInt32(userid), DateTime.Now, FA.SID);
            this._db.Execute(sqlQuery, FA);
            return FA;
        }
    }
}