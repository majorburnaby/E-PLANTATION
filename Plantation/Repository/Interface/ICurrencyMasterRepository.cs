using Plantation.Models.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Plantation.Repository.Interface
{
    public interface ICurrencyMasterRepository
    {
        List<CurrencyMaster> GetAll();
        CurrencyMaster Find(int? SID);
        CurrencyMaster Add(CurrencyMaster CM, string userid);
        CurrencyMaster Update(CurrencyMaster CM, string userid);
        void Remove(int SID);
    }
}