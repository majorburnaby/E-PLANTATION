using Plantation.Models.DB;
using System.Collections.Generic;

namespace Plantation.Repository.Interface
{
    public interface IJobRepository
    {
        List<Job> GetAll();
        List<Job> GetAllByCompanySite(int? CompanySite);
        Job Find(int? SID);
        Job Add(Job JO, string userid);
        Job Update(Job JO, string userid);
        void Remove(int SID);
    }
}