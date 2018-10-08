using Plantation.Models.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Plantation.Repository.Interface
{
    public interface IAccountingPeriodRepository
    {
        List<AccountingPeriod> GetAll();
        AccountingPeriod Find(int? SID);
        AccountingPeriod Add(AccountingPeriod AccountingPeriod, string userid);
        AccountingPeriod Update(AccountingPeriod AccountingPeriod, string userid);
        void Remove(int SID);
    }
}