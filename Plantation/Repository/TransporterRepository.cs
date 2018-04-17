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
    public class TransporterRepository : ITransporterRepository
    {
        private IDbConnection _db = new SqlConnection(Constant.DatabaseConnection);

        public TRANSPORTER Add(TRANSPORTER Transporter)
        {
            throw new NotImplementedException();
        }

        public TRANSPORTER Find(int? SID)
        {
            throw new NotImplementedException();
        }

        public List<TRANSPORTER> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Remove(int SID)
        {
            throw new NotImplementedException();
        }

        public TRANSPORTER Update(TRANSPORTER Transporter)
        {
            throw new NotImplementedException();
        }
    }
}