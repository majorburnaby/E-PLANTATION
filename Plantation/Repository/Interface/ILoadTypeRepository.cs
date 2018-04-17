using Plantation.Models.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Plantation.Repository.Interface
{
    public interface ILoadTypeRepository
    {
        List<LoadType> GetAll();
        LoadType Find(int? SID);
        LoadType Add(LoadType loadtype, string userid);
        LoadType Update(LoadType loadtype, string userid);
        void Remove(int SID);
    }
}