using Plantation.Models.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Plantation.Repository.Interface
{
    public interface ICompanyRepository
    {
        List<Company> GetAll();
        Company Find(int? SID);
        Company Add(Company CP);
        Company Update(Company CP);
        void Remove(int SID);
    }
}