using Plantation.Models.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Plantation.Repository.Interface
{
    public interface IContractorRepository
    {
        List<Contractor> GetAll();
        Contractor Find(int? SID);
        Contractor Add(Contractor CTR, string userid);
        Contractor Update(Contractor CTR, string userid);
        void Remove(int SID);
    }
}