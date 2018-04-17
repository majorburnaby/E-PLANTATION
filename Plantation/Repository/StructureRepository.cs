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
    public class StructureRepository : IStructureRepository
    {
        private IDbConnection _db = new SqlConnection(Constant.DatabaseConnection);

        public Structure Add(Structure Structure)
        {
            throw new NotImplementedException();
        }

        public Structure Find(int? SID)
        {
            throw new NotImplementedException();
        }

        public List<Structure> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Remove(int SID)
        {
            throw new NotImplementedException();
        }

        public Structure Update(Structure Structure)
        {
            throw new NotImplementedException();
        }
    }
}