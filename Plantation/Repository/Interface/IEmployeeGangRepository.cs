using Plantation.Models.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Plantation.Repository.Interface
{
    public interface IEmployeeGangRepository
    {
        List<EmployeeGang> GetAll();
        List<EmployeeGang> GetAllByCompanySite(int CompanySite);
        EmployeeGang Find(int? SID);
        EmployeeGang Add(EmployeeGang EGH, string userid);
        EmployeeGang Update(EmployeeGang EGH);
        void Remove(int SID);
        bool HasEmployeeGangDetail(int SID);
    }
}