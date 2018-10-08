using Plantation.Models.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Plantation.Repository.Interface
{
    public interface IAllowanceDeductionRepository
    {
        List<AllowanceDeduction> GetAll();
        AllowanceDeduction Find(int? SID);
        AllowanceDeduction Add(AllowanceDeduction AllowanceDeduction, string userid);
        AllowanceDeduction Update(AllowanceDeduction AllowanceDeduction, string userid);
        void Remove(int SID);
    }
}