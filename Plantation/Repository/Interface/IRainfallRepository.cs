using Plantation.Models.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Plantation.Repository.Interface
{
    public interface IRainfallRepository
    {
        List<Rainfall> GetAll();
        List<Rainfall> GetAllByCompanySite(int? CompanySite);
        Rainfall Find(int? SID);
        Rainfall Add(Rainfall RF, string userid);
        Rainfall Update(Rainfall RF, string userid);
        void Remove(int SID);
    }
}