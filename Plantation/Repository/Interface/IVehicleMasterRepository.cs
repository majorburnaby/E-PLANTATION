using Plantation.Models.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Plantation.Repository.Interface
{
    public interface IVehicleMasterRepository
    {
        List<VehicleMaster> GetAll();
        List<VehicleMaster> GetAllByCompanySite(int? CompanySite);
        VehicleMaster Find(int? SID);
        VehicleMaster Add(VehicleMaster VM, string userid);
        VehicleMaster Update(VehicleMaster VM, string userid);
        void Remove(int SID);
    }
}