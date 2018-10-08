using Plantation.Models.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Plantation.Repository.Interface
{
    public interface ISalesAgreementRepository
    {
        List<SALESAGREEMENT> GetAll();
        SALESAGREEMENT Find(int? SID);
        SALESAGREEMENT Add(SALESAGREEMENT SalesAgreement);
        SALESAGREEMENT Update(SALESAGREEMENT SalesAgreement);
        void Remove(int SID);
    }
}