using Plantation.Models.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Plantation.Repository.Interface
{
    public interface ICustomerRepository
    {
        List<Customer> GetAll();
        List<Customer> GetAllByCompanySite(int? CompanySite);
        Customer Find(int? SID);
        Customer Add(Customer CTM, string userid);
        Customer Update(Customer CTM, string userid);
        void Remove(int SID);
    }
}