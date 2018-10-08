using Plantation.Models.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Plantation.Repository.Interface
{
    public interface IEmployeeRepository
    {
        List<Employee> GetAll();
        List<Employee> GetAllByCompanySite(int? CompanySite);
        Employee Find(int? SID);
        Employee Add(Employee SPP, string userid);
        void AddHistory(Employee EM, string userid);
        Employee Update(Employee SPP, string userid);
        void Remove(int SID);
    }
}