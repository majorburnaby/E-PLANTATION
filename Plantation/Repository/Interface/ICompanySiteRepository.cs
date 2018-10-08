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
        List<CompanySite> GetAll();
        CompanySite Find(int? SID);
        CompanySite Add(CompanySite CompanySite);
        CompanySite Update(CompanySite CompanySite);
        void Remove(int SID);
    }
}