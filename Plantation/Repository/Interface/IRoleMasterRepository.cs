using Plantation.Models.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Plantation.Repository.Interface
{
    public interface IRoleMasterRepository
    {
        List<RoleMaster> GetAll();
        RoleMaster Find(int? SID);
        RoleMaster Add(RoleMaster RM, string userid);
        RoleMaster Update(RoleMaster RM, string userid);
        void Remove(int SID);
    }
}