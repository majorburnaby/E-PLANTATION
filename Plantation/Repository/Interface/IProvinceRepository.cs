using Plantation.Models.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Plantation.Repository.Interface
{
    public interface IProvinceRepository
    {
        List<Province> GetAll();
        Province Find(int? SID);
        Province Add(Province Province, string userid);
        Province Update(Province Province, string userid);
        void Remove(int SID);
    }
}