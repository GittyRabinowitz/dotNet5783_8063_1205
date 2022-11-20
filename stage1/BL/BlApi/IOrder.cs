using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;

namespace BlApi
{
    public interface IOrder
    {
        public IEnumerable<Order> GetOrderList();
        public Order GetOrderDetails(int id);
        public Order updateSentOrder(int id);
        public Order updateOrderDelivery(int id);
        public Order update(int id);



    }
}
