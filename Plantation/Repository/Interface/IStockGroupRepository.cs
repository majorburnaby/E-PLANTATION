using Plantation.Models.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Plantation.Repository.Interface
{
    public interface IStockGroupRepository
    {
        List<StockGroup> GetAll();
        StockGroup Find(int? SID);
        StockGroup Add(StockGroup STG, string userid);
        StockGroup Update(StockGroup STG, string userid);
        void Remove(int SID);
    }
}