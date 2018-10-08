using Plantation.Models.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Plantation.Repository.Interface
{
    public interface IParameterValueRepository
    {
        List<ParameterValue> GetAll();
        ParameterValue Find(int? SID);
        ParameterValue Add(ParameterValue parametervalue, string userid);
        ParameterValue Update(ParameterValue parametervalue, string userid);
        void Remove(int SID);
    }
}