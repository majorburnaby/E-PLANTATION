using Plantation.Models.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Plantation.Repository.Interface
{
    public interface IUserRoleRepository
    {
        List<UserRole> GetAll();
        UserRole Find(int? SID);
        UserRole Add(UserRole UserRole);
        UserRole Update(UserRole UserRole);
        void Remove(int SID);
    }
}