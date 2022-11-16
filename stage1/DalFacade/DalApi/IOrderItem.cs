using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dal.DO;
namespace DalApi
{
    public interface IOrderItem:ICrud<OrderItem>
    {
        public OrderItem GetOrderItemByOrderIdAndProductId(int orderId, int productId);
        public IEnumerable<OrderItem> GetOrderItemByOrderId(int orderId);
    }
}
