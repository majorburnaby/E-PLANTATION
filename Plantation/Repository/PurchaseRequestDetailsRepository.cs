using Dapper;
using Plantation.Models;
using Plantation.Models.DB;
using Plantation.Repository.Interface;
using Plantation.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;

namespace Plantation.Repository
{
    public class PurchaseRequestDetailsRepository : IPurchaseRequestDetailsRepository
    {
        private IDbConnection _db = new SqlConnection(Constant.DatabaseConnection);

        public List<ViewModelPurchaseDetail> GetPurchaseDetail(int IDPURCHASEREQUEST)
        {
            return this._db.Query<ViewModelPurchaseDetail>(@"select a.SID, a.IDPURCHASEREQUEST, a.ITEMCODE, b.IDSTOCK, b.STOCKNAME, a.QUANTITY, a.UOM, c.IDUOM, c.UOMNAME, cast(a.EXPECTDATE as varchar) EXPECTDATE, 
	                                                            a.MANAGEBY, d.IDPARAMETERVALUE IDMANAGEBY, d.PARAMETERVALUENAME MANAGEBYNAME, 
                                                                a.ESTIMATEPRICE, a.REMARK, a.APPROVEQUANTITY, a.COMPANYSITE
                                                            from PURCHASEREQUESTDETAILS a
                                                            left join STOCK b
                                                            on a.ITEMCODE = b.SID
                                                            left join UNITOFMEASURE c
                                                            on a.UOM = c.SID
                                                            left join PARAMETERVALUE d
                                                            on a.MANAGEBY = d.SID
                                                            where IDPURCHASEREQUEST = @IDPURCHASEREQUEST", new { IDPURCHASEREQUEST}).ToList();
        }

        public PurchaseRequestDetails Add(PurchaseRequestDetails PRD)
        {
            var sqlQuery = @"INSERT INTO PURCHASEREQUESTDETAILS (IDPURCHASEREQUEST, ITEMCODE, QUANTITY, UOM, EXPECTDATE, MANAGEBY, ESTIMATEPRICE, REMARK, APPROVEQUANTITY, COMPANYSITE, INPUTBY, INPUTDATE, UPDATEBY, UPDATEDATE) VALUES
                           (" + PRD.IDPURCHASEREQUEST + @", " + PRD.ITEMCODE + ", '" + PRD.QUANTITY + "', '" + PRD.UOM + "', '" + PRD.EXPECTDATE + "', '" + PRD.MANAGEBY + "', '" + PRD.ESTIMATEPRICE + "', '" + PRD.REMARKS + "', '" + PRD.APPROVEQUANTITY + "', '" + PRD.COMPANYSITE + @"', " + PRD.INPUTBY + ", '" + PRD.INPUTDATE + @"', " + PRD.UPDATEBY + ", '" + PRD.UPDATEDATE + @"'); " + "SELECT CAST(SCOPE_IDENTITY() as int)";
            var SID = _db.Query<int>(sqlQuery, PRD).Single();
            PRD.SID = SID;
            return PRD;
        }

        public PurchaseRequestDetails Find(int? SID)
        {
            return this._db.Query<PurchaseRequestDetails>("SELECT * FROM PURCHASEREQUESTDETAILS WHERE SID = @SID", new { SID }).SingleOrDefault();
        }

        public List<PurchaseRequestDetails> GetAll()
        {
            return this._db.Query<PurchaseRequestDetails>("SELECT" +
                              " PRD.SID," +
                              " PRD.IDPURCHASEREQUEST," +
                              " PRD.ITEMCODE," +
                              " PRD.QUANTITY," +
                              " PRD.UOM," +
                              " PRD.EXPECTDATE," +
                              " PRD.MANAGEBY," +
                              " PRD.ESTIMATEPRICE," +
                              " PRD.REMARK," +
                              " PRD.APPROVEQUANTITY," +
                              " PRD.COMPANYSITE," +
                              " PRD.INPUTBY," +
                              " PRD.INPUTDATE," +
                              " PRD.UPDATEBY," +
                              " PRD.UPDATEDATE" +
                              " FROM" +
                              " PURCHASEREQUESTDETAILS AS PRD" +
                              " ORDER BY" +
                              " PRD.SID ASC").ToList();
        }

        public void Remove(int SID)
        {
            var sqlQuery = (string.Format("Delete From PURCHASEREQUESTDETAILS Where SID = {0}", SID));
            this._db.Execute(sqlQuery);
        }

        public PurchaseRequestDetails Update(PurchaseRequestDetails PRD)
        {
            var sqlQuery =
                "UPDATE PURCHASEREQUESTDETAILS " +
                "SET IDPURCHASEREQUEST    = '" + PRD.IDPURCHASEREQUEST + "', " +
                "    ITEMCODE   = '" + PRD.ITEMCODE + "', " +
                "    QUANTITY   = '" + PRD.QUANTITY + "', " +
                "    UOM        = '" + PRD.UOM + "', " +
                "    EXPECTDATE = '" + PRD.EXPECTDATE + "', " +
                "    MANAGEBY   = '" + PRD.MANAGEBY + "', " +
                "    ESTIMATEPRICE  = '" + PRD.ESTIMATEPRICE + "', " +
                "    REMARK  = '" + PRD.REMARKS + "', " +
                "    APPROVEQUANTITY  = '" + PRD.APPROVEQUANTITY + "', " +
                "    UPDATEBY      = '" + Convert.ToInt32(PRD.UPDATEBY) + "', " +
                "    UPDATEDATE    = '" + PRD.UPDATEDATE + "' " +
                "WHERE SID = " + PRD.SID + "";
            this._db.Execute(sqlQuery, PRD);
            return PRD;
        }
    }
}