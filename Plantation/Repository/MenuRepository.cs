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
    public class MenuRepository : IMenuRepository
    {
        private IDbConnection _db = new SqlConnection(Constant.DatabaseConnection);

        public MENU Add(MENU Menu)
        {
            throw new NotImplementedException();
        }

        public MENU Find(int? id)
        {
            throw new NotImplementedException();
        }

        public List<MENU> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public MENU Update(MENU Menu)
        {
            throw new NotImplementedException();
        }

        public List<MENU> GetAllByUserId(int id)
        {
            using (SqlConnection conn = new SqlConnection(Constant.DatabaseConnection))
            {
                string query = @" SELECT" +
                               " mn.IDMENU," +
                               " mn.MENUNAME," +
                               " mn.IDMENUPARENT," +
                               " mn.MENUPATH," +
                               " mn.ISACTIVE," +
                               " mn.DESCRIPTION," +
                               " mn.INPUTBY," +
                               " mn.INPUTDATE," +
                               " mn.UPDATEBY," +
                               " mn.UPDATEDATE" +
                               " FROM" +
                               " USERPROFILE AS up" +
                               " INNER JOIN USERROLE AS ur ON up.SID = ur.IDUSER" +
                               " INNER JOIN ROLEMENU AS rm ON ur.IDROLE = rm.IDROLE" +
                               " INNER JOIN MENU AS mn ON rm.IDMENU = mn.IDMENU" +
                               " WHERE" +
                               " up.SID = @Id AND" +
                               " up.ISACTIVE = '1' AND" +
                               " ur.ISACTIVE = '1'" +
                               " ORDER BY" +
                               " rm.IDMENU ASC";
                return conn.Query<MENU>(query, new { Id = id }).ToList();
            }
        }
    }
}