using Plantation.Models.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Plantation.Repository.Interface
{
    public interface IBankRepository
    {
        List<Bank> GetAll();
        List<Bank> GetAllByCompanySite(int? CompanySite);
        Bank Find(int? SID);
        Bank Add(Bank BK, string userid);
        Bank Update(Bank BK, string userid);
        void Remove(int SID);
    }
}