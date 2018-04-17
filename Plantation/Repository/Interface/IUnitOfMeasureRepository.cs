using Plantation.Models.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Plantation.Repository.Interface
{
    public interface IUnitOfMeasureRepository
    {
        List<UnitOfMeasure> GetAll();
        UnitOfMeasure Find(int? SID);
        UnitOfMeasure Add(UnitOfMeasure UOM, string userid);
        UnitOfMeasure Update(UnitOfMeasure UOM, string userid);
        void Remove(int SID);
    }
}