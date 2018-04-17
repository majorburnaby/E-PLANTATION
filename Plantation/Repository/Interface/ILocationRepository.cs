using Plantation.Models.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Plantation.Repository.Interface
{
    public interface ILocationRepository
    {
        List<Location> GetAll();
        Location Find(int? SID);
        Location Add(Location Location, string userID);
        Location Update(Location Location, string userID);
        void Remove(int SID);
    }
}