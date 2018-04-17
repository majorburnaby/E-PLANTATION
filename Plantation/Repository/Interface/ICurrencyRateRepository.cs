using Plantation.Models.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Plantation.Repository.Interface
{
    public interface ICurrencyRateRepository
    {
        List<CurrencyRate> GetAll();
        CurrencyRate Find(int? SID);
        CurrencyRate Add(CurrencyRate CR, string userid);
        CurrencyRate Update(CurrencyRate CR, string userid);
        void Remove(int SID);
    }
}