using Plantation.Models.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Plantation.Repository.Interface
{
    public interface IWorkshopRepository
    {
        List<Workshop> GetAll();
        Workshop Find(int? SID);
        Workshop Add(Workshop WS, string userid);
        Workshop Update(Workshop WS, string userid);
        void Remove(int SID);
    }
}