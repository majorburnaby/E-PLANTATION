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
                var result = conn.Query<Login>(@"SELECT a.username, a.userpassword, a.sid, b.companysite FROM userprofile a left join usercompanysite b on a.SID = b.iduser and b.isdefault = 1 WHERE username = @Id", new { Id = username }).FirstOrDefault();
                return result;
            }
        }
    }
}