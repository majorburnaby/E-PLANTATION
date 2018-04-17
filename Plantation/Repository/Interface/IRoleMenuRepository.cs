using Plantation.Models.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Plantation.Repository.Interface
{
    public interface IRoleMenuRepository
    {
        List<ROLEMENU> GetAll();
        ROLEMENU Find(int? SID);
        ROLEMENU Add(ROLEMENU RoleMenu);
        ROLEMENU Update(ROLEMENU RoleMenu);
        void Remove(int SID);
    }
}