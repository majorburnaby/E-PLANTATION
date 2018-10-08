using Plantation.Models.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Plantation.Repository.Interface
{
    public interface ITransporterGroupRepository
    {
        List<TransporterGroup> GetAll();
        TransporterGroup Find(int? SID);
        TransporterGroup Add(TransporterGroup transportergroup, string userid);
        TransporterGroup Update(TransporterGroup transportergroup, string userid);
        void Remove(int SID);
    }
}