using Plantation.Models.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Plantation.Repository.Interface
{
    public interface IUserCompanyRepository
    {
        List<USERCOMPANY> GetAll();
        USERCOMPANY Find(int? SID);
        USERCOMPANY Add(USERCOMPANY UserCompany);
        USERCOMPANY Update(USERCOMPANY UserCompany);
        void Remove(int SID);
    }
}