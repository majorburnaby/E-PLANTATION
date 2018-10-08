using Plantation.Models.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Plantation.Repository.Interface
{
    public interface IPeriodControlRepository
    {
        List<PeriodControl> GetAll();
        PeriodControl Find();
        PeriodControl Add(PeriodControl PeriodControl, string userid);
        PeriodControl Update(PeriodControl PeriodControl, string userid);
        void Remove(int SID);
    }
}