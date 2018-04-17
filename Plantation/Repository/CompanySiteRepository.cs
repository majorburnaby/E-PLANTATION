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
    public class CompanySiteRepository : ICompanySiteRepository
    {
        private IDbConnection _db = new SqlConnection(Constant.DatabaseConnection);

        public COMPANYSITE Add(COMPANYSITE CompanySite)
        {
            throw new NotImplementedException();
        }

        public COMPANYSITE Find(int? SID)
        {
            throw new NotImplementedException();
        }

        public List<COMPANYSITE> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Remove(int SID)
        {
            throw new NotImplementedException();
        }

        public COMPANYSITE Update(COMPANYSITE CompanySite)
        {
            throw new NotImplementedException();
        }
    }
}