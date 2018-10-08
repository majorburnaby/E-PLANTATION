using Plantation.Models.DB;
using System.Collections.Generic;

namespace Plantation.Repository.Interface
{
    public interface IJobGroupRepository
    {
        List<JobGroup> GetAll();
        JobGroup Find(int? SID);
        JobGroup Add(JobGroup JG, string userid);
        JobGroup Update(JobGroup JG, string userid);
        void Remove(int SID);
    }
}