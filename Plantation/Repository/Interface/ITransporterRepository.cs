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
        List<TRANSPORTER> GetAll();
        TRANSPORTER Find(int? SID);
        TRANSPORTER Add(TRANSPORTER Transporter);
        TRANSPORTER Update(TRANSPORTER Transporter);
        void Remove(int SID);
    }
}