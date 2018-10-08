using Plantation.Models.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Plantation.Repository.Interface
{
    public interface IDeliveryOrderRepository
    {
        List<DELIVERYORDER> GetAll();
        DELIVERYORDER Find(int? SID);
        DELIVERYORDER Add(DELIVERYORDER DeliveryOrder);
        DELIVERYORDER Update(DELIVERYORDER DeliveryOrder);
        void Remove(int SID);
    }
}