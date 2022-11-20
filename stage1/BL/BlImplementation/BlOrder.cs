using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlApi;
using BO;
using DalApi;
using Dal;


namespace BlImplementation
{
    internal class BlOrder:BlApi.IOrder
    {
        private IDal Dal = new DalList();

        public Order GetOrderDetails(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<OrderForList> GetOrderList()
        {
            throw new NotImplementedException();
        }

        public Order update(int id)
        {
            throw new NotImplementedException();
        }

        public Order updateDeliveryOrder(int id)
        {
            throw new NotImplementedException();
        }

        public Order updateShippedOrder(int id)
        {
            throw new NotImplementedException();
        }
    }
}
