using Plantation.Models.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Plantation.Repository.Interface
{
    public interface IWorkshopGroupRepository
    {
        List<WorkshopGroup> GetAll();
        WorkshopGroup Find(int? SID);
        WorkshopGroup Add(WorkshopGroup WG, string userid);
        WorkshopGroup Update(WorkshopGroup WG, string userid);
        void Remove(int SID);
    }
}