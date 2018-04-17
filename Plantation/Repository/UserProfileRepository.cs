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
    public class UserProfileRepository : IUserProfileRepository
    {
        private IDbConnection _db = new SqlConnection(Constant.DatabaseConnection);
        public List<UserProfile> GetAll()
        {
            return this._db.Query<UserProfile>("SELECT * FROM USERPROFILE").ToList();
        }
            
        public UserProfile Find(int? id)
        {
            return this._db.Query<UserProfile>("SELECT * FROM UserProfile WHERE SID = @id", new { id }).SingleOrDefault();
        }

        public UserProfile Add(UserProfile UP)
        {
            var sqlQuery = "INSERT INTO UserProfile (SID, IDUSER, USERNAME, POSITION, DEPARTMENT, USERPASSWORD, DESCRIPTION, ISACTIVE, EMAIL, ACTIVEFROMDATE, ACTIVEENDDATE) VALUES(@SID, @IDUSER, @USERNAME, @POSITION, @DEPARTMENT, @USERPASSWORD, @DESCRIPTION, @ISACTIVE, @EMAIL, @ACTIVEFROMDATE, @ACTIVEENDDATE); " + "SELECT CAST(SCOPE_IDENTITY() as int)";
            var SID = this._db.Query<int>(sqlQuery, UP).Single();
            UP.SID = SID;
            return UP;
        }

        public UserProfile Update(UserProfile UP)
        {
            var sqlQuery =
                "UPDATE UserProfile " +
                "SET IDUSER         = @IDUSER, " +
                "    USERNAME       = @USERNAME " +
                "    POSITION       = @POSITION, " +
                "    DEPARTMENT     = @DEPARTMENT " +
                "    USERPASSWORD   = @USERPASSWORD, " +
                "    DESCRIPTION    = @DESCRIPTION " +
                "    ISACTIVE       = @ISACTIVE, " +
                "    EMAIL          = @EMAIL " +
                "    ACTIVEFROMDATE = @ACTIVEFROMDATE, " +
                "    ACTIVEENDDATE  = @ACTIVEENDDATE " +
                "WHERE SID = @SID";
            this._db.Execute(sqlQuery, UP);
            return UP;
        }

        public void Remove(int id)
        {
            var sqlQuery = ("Delete From tblUserProfile Where SID = " + id + "");
            this._db.Execute(sqlQuery);
        }
    }
}