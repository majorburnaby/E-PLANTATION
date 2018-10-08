using Plantation.Models.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Plantation.Repository.Interface
{
    public interface IEmployeeGangDetailRepository
    {
        List<EmployeeGangDetail> GetAll();
        EmployeeGangDetail Find(int? SID);
        EmployeeGangDetail Add(EmployeeGangDetail EGD);
        EmployeeGangDetail Update(EmployeeGangDetail EGD);
        void Remove(int SID);
    }
}