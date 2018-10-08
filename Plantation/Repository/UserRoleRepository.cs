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
    public class UserRoleRepository : IUserRoleRepository
    {
        private IDbConnection _db = new SqlConnection(Constant.DatabaseConnection);

        public UserRole Add(UserRole UserRole)
        {
            throw new NotImplementedException();
        }

        public UserRole Find(int? SID)
        {
            throw new NotImplementedException();
        }

        public List<UserRole> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Remove(int SID)
        {
            throw new NotImplementedException();
        }

        public UserRole Update(UserRole UserRole)
        {
            throw new NotImplementedException();
        }
    }
}