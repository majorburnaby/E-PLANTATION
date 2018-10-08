using Plantation.Models.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Plantation.Repository.Interface
{
    public interface IRegionRepository
    {
        List<Region> GetAll();
        Region Find(int? SID);
        Region Add(Region RG, string userid);
        Region Update(Region RG, string userid);
        void Remove(int SID);
    }
}