using Plantation.Models.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Plantation.Repository.Interface
{
    public interface IJobTypeRepository
    {
        List<JobType> GetAll();
        JobType Find(int? SID);
        JobType Add(JobType JobType, string userid);
        JobType Update(JobType JobType, string userid);
        void Remove(int SID);
    }
}