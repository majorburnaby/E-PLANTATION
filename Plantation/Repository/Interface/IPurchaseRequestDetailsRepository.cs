using Plantation.Models;
using Plantation.Models.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Plantation.Repository.Interface
{
    public interface IPurchaseRequestDetailsRepository
    {
        List<ViewModelPurchaseDetail> GetPurchaseDetail(int IDPURCHASEREQUEST);
        List<PurchaseRequestDetails> GetAll();
        PurchaseRequestDetails Find(int? SID);
        PurchaseRequestDetails Add(PurchaseRequestDetails PRD);
        PurchaseRequestDetails Update(PurchaseRequestDetails PRD);
        void Remove(int SID);
    }
}