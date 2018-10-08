using Plantation.Models.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Plantation.Repository.Interface
{
    public interface IProductRepository
    {
        List<Product> GetAll();
        Product Find(int? SID);
        Product Add(Product product, string userid);
        Product Update(Product product, string userid);
        void Remove(int SID);
    }
}