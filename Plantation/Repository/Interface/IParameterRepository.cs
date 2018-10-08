using Plantation.Models.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Plantation.Repository.Interface
{
    public interface IParameterRepository
    {
        List<Parameter> GetAll();
        Parameter Find(int? SID);
        Parameter Add(Parameter parameter, string userid);
        Parameter Update(Parameter parameter, string userid);
        void Remove(int SID);
    }
}