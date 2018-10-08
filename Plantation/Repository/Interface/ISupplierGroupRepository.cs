using Plantation.Models.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Plantation.Repository.Interface
{
    public interface ISupplierGroupRepository
    {
        List<SupplierGroup> GetAll();
        SupplierGroup Find(int? SID);
        SupplierGroup Add(SupplierGroup SPG, string userid);
        SupplierGroup Update(SupplierGroup SPG, string userid);
        void Remove(int SID);
    }
}