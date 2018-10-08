using Plantation.Models.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Plantation.Repository.Interface
{
    public interface IVehicleGroupRepository
    {
        List<VehicleGroup> GetAll();
        VehicleGroup Find(int? SID);
        VehicleGroup Add(VehicleGroup VG, string userid);
        VehicleGroup Update(VehicleGroup VG, string userid);
        void Remove(int SID);
    }
}