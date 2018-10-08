using Plantation.Models.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Plantation.Repository.Interface
{
    public interface ITransporterContractRepository
    {
        List<TRANSPORTERCONTRACT> GetAll();
        TRANSPORTERCONTRACT Find(int? SID);
        TRANSPORTERCONTRACT Add(TRANSPORTERCONTRACT TransporterContract);
        TRANSPORTERCONTRACT Update(TRANSPORTERCONTRACT TransporterContract);
        void Remove(int SID);
    }
}