using Plantation.Models.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Plantation.Repository.Interface
{
    public interface IStockRepository
    {
        List<Stock> GetAll();
        Stock Find(int? SID);
        Stock Add(Stock STK, string userid);
        Stock Update(Stock STK, string userid);
        void Remove(int SID);
    }
}