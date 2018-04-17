using Plantation.Models.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Plantation.Repository.Interface
{
    public interface ICityRepository
    {
        List<City> GetAll();
        City Find(int? SID);
        City Add(City City, string userid);
        City Update(City City, string userid);
        void Remove(int SID);
    }
}