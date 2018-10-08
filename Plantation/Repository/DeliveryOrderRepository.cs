using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dapper;
using Plantation.Models.DB;
using System.Data;
using System.Data.SqlClient;
using Plantation.Utility;
using Plantation.Repository.Interface;

namespace Plantation.Repository
{
    public class DeliveryOrderRepository : IDeliveryOrderRepository
    {
        private IDbConnection _db = new SqlConnection(Constant.DatabaseConnection);

        public DELIVERYORDER Add(DELIVERYORDER DeliveryOrder)
        {
            throw new NotImplementedException();
        }

        public DELIVERYORDER Find(int? SID)
        {
            throw new NotImplementedException();
        }

        public List<DELIVERYORDER> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Remove(int SID)
        {
            throw new NotImplementedException();
        }

        public DELIVERYORDER Update(DELIVERYORDER DeliveryOrder)
        {
            throw new NotImplementedException();
        }
    }
}