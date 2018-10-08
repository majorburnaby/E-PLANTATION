using Plantation.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Plantation.Models.DB;
using System.Data;
using Plantation.Utility;
using System.Data.SqlClient;
using Dapper;

namespace Plantation.Repository
{
    public class PurchaseRequestHeaderRepository : IPurchaseRequestHeaderRepository
    {
        private IDbConnection _db = new SqlConnection(Constant.DatabaseConnection);

        public PurchaseRequestHeader Add(PurchaseRequestHeader PRH, string userid)
        {
            var sqlQuery = @"INSERT INTO PURCHASEREQUESTHEADER (PURCHASEREQUESTDATE, REQUESTORID, PRIORITY, REMARKS, STATUS, COMPANYSITE, INPUTBY, INPUTDATE, UPDATEBY, UPDATEDATE) VALUES
                           ('" + PRH.PURCHASEREQUESTDATE + "', '" + PRH.REQUESTORID + "', '" + PRH.PRIORITY + "', '" + PRH.REMARKS + "', '" + PRH.STATUS + "', '" + PRH.COMPANYSITE +  @"', " + Convert.ToInt32(userid) + ", '" + DateTime.Now + @"', " + Convert.ToInt32(userid) + ", '" + DateTime.Now + @"'); " + "SELECT CAST(SCOPE_IDENTITY() as int)";
            var SID = _db.Query<int>(sqlQuery, PRH).Single();
            PRH.SID = SID;
            AddDocumentNo(SID);
            return PRH;
        }

        private void AddDocumentNo(int SID)
        {
            var sqlQuery = @"UPDATE PURCHASEREQUESTHEADER SET IDPURCHASEREQUEST = (SELECT COUNT(*) FROM PURCHASEREQUESTHEADER) WHERE SID = " + SID;
            _db.Query(sqlQuery);
        }

        public PurchaseRequestHeader Find(int? SID)
        {
            return this._db.Query<PurchaseRequestHeader>("SELECT *, CONCAT(YEAR(PURCHASEREQUESTDATE), '/', MONTH(PURCHASEREQUESTDATE), '/PR/', SID) DOCNAME FROM PURCHASEREQUESTHEADER WHERE SID = @SID", new { SID }).SingleOrDefault();
        }

        public List<PurchaseRequestHeader> GetAll()
        {
            return this._db.Query<PurchaseRequestHeader>("SELECT" +
                               " CONCAT(YEAR(PRH.PURCHASEREQUESTDATE), '/', MONTH(PRH.PURCHASEREQUESTDATE), '/PR/', PRH.IDPURCHASEREQUEST) DOCNAME," +
                               " PRH.SID," +
                               " PRH.IDPURCHASEREQUEST," +
                               " PRH.PURCHASEREQUESTDATE," +
                               " PRH.REQUESTORID," +
                               " PRH.PRIORITY," +
                               " PRH.REMARKS," +
                               " PRH.STATUS," +
                               " PRH.COMPANYSITE," +
                               " PRH.INPUTBY," +
                               " PRH.INPUTDATE," +
                               " PRH.UPDATEBY," +
                               " PRH.UPDATEDATE" +
                               " FROM" +
                               " PURCHASEREQUESTHEADER AS PRH" +
                               " ORDER BY" +
                               " PRH.SID ASC").ToList();
        }

        public List<PurchaseRequestHeader> GetAllByCompanySite(int CompanySite)
        {
            return this._db.Query<PurchaseRequestHeader>("SELECT" +
                            " CONCAT(YEAR(PRH.PURCHASEREQUESTDATE), '/', MONTH(PRH.PURCHASEREQUESTDATE), '/PR/', PRH.IDPURCHASEREQUEST) DOCNAME," +
                              " PRH.SID," +
                              " PRH.IDPURCHASEREQUEST," +
                              " PRH.PURCHASEREQUESTDATE," +
                              " PRH.REQUESTORID," +
                              " ST.STORENAME REQUESTORIDNAME," +
                              " PRH.PRIORITY," +
                              " PV1.PARAMETERVALUENAME PRIORITYNAME," +
                              " PRH.REMARKS," +
                              " PRH.STATUS," +
                              " PRH.COMPANYSITE," +
                              " PRH.INPUTBY," +
                              " PRH.INPUTDATE," +
                              " PRH.UPDATEBY," +
                              " PRH.UPDATEDATE" +
                              " FROM" +
                              " PURCHASEREQUESTHEADER AS PRH" +
                              " LEFT JOIN STORE AS ST" +
                              " ON PRH.REQUESTORID = ST.SID" +
                              " LEFT JOIN PARAMETERVALUE AS PV1" +
                              " ON PRH.PRIORITY = PV1.SID AND PV1.PARAMETER = 50" +
                              " WHERE PRH.COMPANYSITE = " + CompanySite +
                              " ORDER BY" +
                              " PRH.SID ASC").ToList();
        }

        public bool HasPurchaseRequestDetails(int SID)
        {
            return this._db.Query<bool>("SELECT COUNT(*) FROM PURCHASEREQUESTDETAILS WHERE IDPURCHASEREQUEST = @SID", new { SID }).FirstOrDefault();
        }

        public void Remove(int SID)
        {
            var sqlQuery = (string.Format("Delete From PURCHASEREQUESTHEADER Where SID = {0}", SID));
            this._db.Execute(sqlQuery);
        }

        public PurchaseRequestHeader Update(PurchaseRequestHeader PRH)
        {
            var sqlQuery =
                "UPDATE PURCHASEREQUESTHEADER " +
                "SET IDPURCHASEREQUEST    = '" + PRH.IDPURCHASEREQUEST + "', " +
                "    PURCHASEREQUESTDATE  = '" + PRH.PURCHASEREQUESTDATE + "', " +
                "    REQUESTORID   = '" + PRH.REQUESTORID + "', " +
                "    PRIORITY   = '" + PRH.PRIORITY + "', " +
                "    REMARKS   = '" + PRH.REMARKS + "', " +
                "    STATUS   = '" + PRH.STATUS + "', " +
                "    COMPANYSITE   = '" + PRH.COMPANYSITE + "', " +
                "    UPDATEBY      = '" + Convert.ToInt32(PRH.UPDATEBY) + "', " +
                "    UPDATEDATE    = '" + PRH.UPDATEDATE + "' " +
                "WHERE SID = " + PRH.SID + "";
            this._db.Execute(sqlQuery, PRH);
            return PRH;
        }

    }
}