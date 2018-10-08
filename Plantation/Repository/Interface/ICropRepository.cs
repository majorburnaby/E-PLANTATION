using Plantation.Models.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Plantation.Repository.Interface
{
    public interface ICropRepository
    {
        List<Crop> GetAll();
        Crop Find(int? SID);
        Crop Add(Crop CR, string userid);
        Crop Update(Crop CR, string userid);
        void Remove(int SID);
    }
}