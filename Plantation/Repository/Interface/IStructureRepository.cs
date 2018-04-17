using Plantation.Models.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Plantation.Repository.Interface
{
    public interface IStructureRepository
    {
        List<Structure> GetAll();
        Structure Find(int? SID);
        Structure Add(Structure Structure);
        Structure Update(Structure Structure);
        void Remove(int SID);
    }
}