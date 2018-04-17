using Plantation.Models.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Plantation.Repository.Interface
{
    public interface IPositionRepository
    {
        List<Position> GetAll();
        Position Find(int? SID);
        Position Add(Position PS, string userid);
        Position Update(Position PS, string userid);
        void Remove(int SID);
    }
}