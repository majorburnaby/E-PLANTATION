using Plantation.Models.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Plantation.Repository.Interface
{
    public interface ICountryRepository
    {
        List<Country> GetAll();
        Country Find(int? SID);
        Country Add(Country Country, string userid);
        Country Update(Country Country, string userid);
        void Remove(int SID);
    }
}