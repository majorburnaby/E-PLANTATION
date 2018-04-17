using Plantation.Models.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Plantation.Repository.Interface
{
    public interface ICustomerGroupRepository
    {
        List<CustomerGroup> GetAll();
        CustomerGroup Find(int? SID);
        CustomerGroup Add(CustomerGroup CSG, string userid);
        CustomerGroup Update(CustomerGroup CSG, string userid);
        void Remove(int SID);
    }
}