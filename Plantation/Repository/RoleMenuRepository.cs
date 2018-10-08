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

        public bool BulkInsert(int IDROLE, DataTable dataTable)
        {
            var sqlQuery = (string.Format("Delete From ROLEMENU Where IDROLE = {0}", IDROLE));
            this._db.Execute(sqlQuery);

            bool isSuccess;
            try
            {
                SqlConnection SqlConnectionObj = new SqlConnection(Constant.DatabaseConnection);
                SqlConnectionObj.Open();
                SqlBulkCopy bulkCopy = new SqlBulkCopy(SqlConnectionObj, SqlBulkCopyOptions.TableLock | SqlBulkCopyOptions.FireTriggers | SqlBulkCopyOptions.UseInternalTransaction, null);
                bulkCopy.DestinationTableName = "ROLEMENU";
                bulkCopy.WriteToServer(dataTable);
                isSuccess = true;
                SqlConnectionObj.Close();
            }
            catch
            {
                isSuccess = false;
            }
            return isSuccess;
        }

        public ROLEMENU Find(int? SID)
        {
            throw new NotImplementedException();
        }

        public List<ROLEMENU> GetAll()
        {
            throw new NotImplementedException();
        }

        public List<ROLEMENU> GetAllByRole(int? IDROLE)
        {
            return this._db.Query<ROLEMENU>(
                                @"select a.IDMENU, a.IDMENUPARENT, a.MENUNAME, a.DESCRIPTION, case when IDMENUPARENT <> '0' and StatusMenu = 'Root' then 'Sub Root'
	                                else StatusMenu end as STATUSMENU,
	                                isnull(b.ISCANREAD,0) ISCANREAD, isnull(b.ISCANADD,0) ISCANADD, 
	                                isnull(b.ISCANEDIT,0) ISCANEDIT, isnull(b.ISCANDELETE,0) ISCANDELETE
                                from
                                (
	                                select 
		                                a.IDMENU, a.IDMENUPARENT, a.MENUNAME, a.DESCRIPTION,
		                                case when (select count(*) from MENU b where b.IDMENUPARENT = a.IDMENU) > 0 then 'Root'
		                                else 'Menu' end as StatusMenu
	                                from MENU a
                                )a
                                left join ROLEMENU b
                                on a.IDMENU = b.IDMENU and b.IDROLE = " + IDROLE
                               ).ToList();
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