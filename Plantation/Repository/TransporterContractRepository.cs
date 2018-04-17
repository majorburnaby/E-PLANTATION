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
    public class TransporterContractRepository : ITransporterContractRepository
    {
        private IDbConnection _db = new SqlConnection(Constant.DatabaseConnection);

        public TRANSPORTERCONTRACT Add(TRANSPORTERCONTRACT TransporterContract)
        {
            throw new NotImplementedException();
        }

        public TRANSPORTERCONTRACT Find(int? SID)
        {
            throw new NotImplementedException();
        }

        public List<TRANSPORTERCONTRACT> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Remove(int SID)
        {
            throw new NotImplementedException();
        }

        public TRANSPORTERCONTRACT Update(TRANSPORTERCONTRACT TransporterContract)
        {
            throw new NotImplementedException();
        }
    }
}