using Plantation.Models.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Plantation.Repository.Interface
{
    public interface ITransporterRepository
    {
        List<Transporter> GetAll();
        List<Transporter> GetAllByCompanySite(int? CompanySite);
        Transporter Find(int? SID);
        Transporter Add(Transporter TSP, string userid);
        Transporter Update(Transporter TSP, string userid);
        void Remove(int SID);
    }
}