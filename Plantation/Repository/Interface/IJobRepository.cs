using Plantation.Models.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Plantation.Repository.Interface
{
    public interface IJobRepository
    {
        List<Job> GetAll();
        Job Find(int? SID);
        Job Add(Job JO, string userid);
        Job Update(Job JO, string userid);
        void Remove(int SID);
    }
}