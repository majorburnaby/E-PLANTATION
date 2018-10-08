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
    public class UserCompanySiteRepository : IUserCompanySiteRepository
    {
        private IDbConnection _db = new SqlConnection(Constant.DatabaseConnection);

        public UserCompanySite Add(UserCompanySite UserCompanySite)
        {
            throw new NotImplementedException();
        }

        public UserCompanySite Find(int? SID)
        {
            throw new NotImplementedException();
        }

        public List<UserCompanySite> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Remove(int SID)
        {
            throw new NotImplementedException();
        }

        public UserCompanySite Update(UserCompanySite UserCompanySite)
        {
            throw new NotImplementedException();
        }
    }
}