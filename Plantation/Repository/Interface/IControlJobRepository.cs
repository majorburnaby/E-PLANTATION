using Plantation.Models.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Plantation.Repository.Interface
{
    public interface IControlJobRepository
    {
        List<ControlJob> GetAll();
        ControlJob Find(int? SID);
        ControlJob Add(ControlJob CJ, string userid);
        ControlJob Update(ControlJob CJ, string userid);
        void Remove(int SID);
    }
}