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
    public class SalesAgreementRepository : ISalesAgreementRepository
    {
        private IDbConnection _db = new SqlConnection(Constant.DatabaseConnection);

        public SALESAGREEMENT Add(SALESAGREEMENT SalesAgreement)
        {
            throw new NotImplementedException();
        }

        public SALESAGREEMENT Find(int? SID)
        {
            throw new NotImplementedException();
        }

        public List<SALESAGREEMENT> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Remove(int SID)
        {
            throw new NotImplementedException();
        }

        public SALESAGREEMENT Update(SALESAGREEMENT SalesAgreement)
        {
            throw new NotImplementedException();
        }
    }
}