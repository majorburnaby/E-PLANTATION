using Plantation.Models.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Plantation.Repository.Interface
{
    public interface ICompanySiteRepository
    {
        List<COMPANYSITE> GetAll();
        COMPANYSITE Find(int? SID);
        COMPANYSITE Add(COMPANYSITE CompanySite);
        COMPANYSITE Update(COMPANYSITE CompanySite);
        void Remove(int SID);
    }
}