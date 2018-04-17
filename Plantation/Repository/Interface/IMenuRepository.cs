using Plantation.Models.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Plantation.Repository.Interface
{
    public interface IMenuRepository
    {
        List<MENU> GetAll();
        MENU Find(int? id);
        MENU Add(MENU Menu);
        MENU Update(MENU Menu);
        void Remove(int id);
        List<MENU> GetAllByUserId(int id);
    }
}