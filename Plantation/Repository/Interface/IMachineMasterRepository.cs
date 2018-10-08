using Plantation.Models.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Plantation.Repository.Interface
{
    public interface IMachineMasterRepository
    {
        List<MachineMaster> GetAll();
        List<MachineMaster> GetAllByCompanySite(int? CompanySite);
        MachineMaster Find(int? SID);
        MachineMaster Add(MachineMaster MA, string userid);
        MachineMaster Update(MachineMaster MA, string userid);
        void Remove(int SID);
    }
}