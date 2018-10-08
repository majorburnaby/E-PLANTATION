using Plantation.Models.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Plantation.Repository.Interface
{
    public interface IUserCompanySiteRepository
    {
        List<UserCompanySite> GetAll();
        UserCompanySite Find(int? SID);
        UserCompanySite Add(UserCompanySite UserCompanySite);
        UserCompanySite Update(UserCompanySite UserCompanySite);
        void Remove(int SID);
    }
}