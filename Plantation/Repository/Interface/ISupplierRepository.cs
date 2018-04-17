using Plantation.Models.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Plantation.Repository.Interface
{
    public interface ISupplierRepository
    {
        List<Supplier> GetAll();
        Supplier Find(int? SID);
        Supplier Add(Supplier SPP, string userid);
        Supplier Update(Supplier SPP, string userid);
        void Remove(int SID);
    }
}