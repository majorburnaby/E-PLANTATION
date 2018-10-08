using Plantation.Models.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Plantation.Repository.Interface
{
    public interface ICostCenterRepository
    {
        List<CostCenter> GetAll();
        CostCenter Find(int? SID);
        CostCenter Add(CostCenter CC, string userid);
        CostCenter Update(CostCenter CC, string userid);
        void Remove(int SID);
    }
}