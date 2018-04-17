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
    public class RoleMenuRepository : IRoleMenuRepository
    {
        private IDbConnection _db = new SqlConnection(Constant.DatabaseConnection);

        public ROLEMENU Add(ROLEMENU RoleMenu)
        {
            throw new NotImplementedException();
        }

        public ROLEMENU Find(int? SID)
        {
            throw new NotImplementedException();
        }

        public List<ROLEMENU> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Remove(int SID)
        {
            throw new NotImplementedException();
        }

        public ROLEMENU Update(ROLEMENU RoleMenu)
        {
            throw new NotImplementedException();
        }
    }
}