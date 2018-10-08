using Plantation.Models.DB;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;

namespace Plantation.Repository.Interface
{
    public interface IRoleMenuRepository
    {
        List<ROLEMENU> GetAll();
        List<ROLEMENU> GetAllByRole(int? IDROLE);
        ROLEMENU Find(int? SID);
        ROLEMENU Add(ROLEMENU RoleMenu);
        ROLEMENU Update(ROLEMENU RoleMenu);
        void Remove(int SID);

        bool BulkInsert(int IDROLE, DataTable dt);
    }
}