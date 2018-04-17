using Plantation.Models.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Plantation.Repository.Interface
{
    public interface IStoreRepository
    {
        List<Store> GetAll();
        Store Find(int? SID);
        Store Add(Store Store, string userid);
        Store Update(Store Store, string userid);
        void Remove(int SID);
    }
}