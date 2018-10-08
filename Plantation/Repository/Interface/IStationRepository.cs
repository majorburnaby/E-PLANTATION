using Plantation.Models.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Plantation.Repository.Interface
{
    public interface IStationRepository
    {
        List<Station> GetAll();
        Station Find(int? SID);
        Station Add(Station ST, string userid);
        Station Update(Station ST, string userid);
        void Remove(int SID);
    }
}