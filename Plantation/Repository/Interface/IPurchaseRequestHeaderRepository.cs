using Plantation.Models.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Plantation.Repository.Interface
{
    public interface IPurchaseRequestHeaderRepository
    {
        List<PurchaseRequestHeader> GetAll();
        List<PurchaseRequestHeader> GetAllByCompanySite(int CompanySite);
        PurchaseRequestHeader Find(int? SID);
        PurchaseRequestHeader Add(PurchaseRequestHeader PRH, string userid);
        PurchaseRequestHeader Update(PurchaseRequestHeader PRH);
        void Remove(int SID);
        bool HasPurchaseRequestDetails(int SID);
    }
}