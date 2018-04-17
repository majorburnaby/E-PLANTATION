using System.Linq;
using Dapper;
using Plantation.Models.DB;
using System.Data.SqlClient;
using Plantation.Utility;

namespace Plantation.Repository
{
    public class LoginRepository
    {
        public Login FindByUsername(string username)
        {
            using (SqlConnection conn = new SqlConnection(Constant.DatabaseConnection))
            {
                var result = conn.Query<Login>(@"SELECT username, userpassword, sid FROM userprofile WHERE username = @Id", new { Id = username }).FirstOrDefault();
                return result;
            }
        }
    }
}