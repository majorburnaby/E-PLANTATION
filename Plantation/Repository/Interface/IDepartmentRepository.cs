using Plantation.Models.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Plantation.Repository.Interface
{
    public interface IDepartmentRepository
    {
        List<Department> GetAll();
        Department Find(int? SID);
        Department Add(Department DP, string userid);
        Department Update(Department DP, string userid);
        void Remove(int SID);
    }
}